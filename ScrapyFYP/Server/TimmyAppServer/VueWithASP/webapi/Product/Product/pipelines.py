# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://docs.scrapy.org/en/latest/topics/item-pipeline.html

# useful for handling different item types with a single interface
from scrapy.utils.project import get_project_settings
from scrapy.exceptions import DropItem
from itemadapter import ItemAdapter
from BloomFilter.MyBloomFilter import BloomFilter
from ElasticSearch.MyElasticSearch import MyElasticSearch
from TermCompare.ScrapyTermCompare import ScrapyTermCompare
from Database.TimmyDatabase import TimmyDatabase
import os
import logging
import json
from datetime import datetime



logging.basicConfig(level = logging.INFO, format='[%(asctime)s] {%(name)s} %(levelname)s:  %(message)s', 
                    datefmt='%y-%m-%d %H:%M:%S',
                    filename=f'./Log/pipeline_{datetime.now()}_logs.log')
logger = logging.getLogger('Pipeline_logger')


class ProductPipeline:
    def process_item(self, item, spider):
        return item

class MyProductPipeline:
    
    settings = get_project_settings()
    def open_spider(self, spider):
        dynamic_parameter = spider.dynamic_parameter
        self.category = dynamic_parameter['category']
        self.brandName = dynamic_parameter['brand']
        self.modelOfProduct = {}
        self.averagePrice = {}

        # Create Bloom Filter        
        file_path = f"./BloomFilter/{spider.name}BloomFilter.bin"
        if os.path.exists(file_path):
            self.bloomFilter = BloomFilter.load_from_file(file_path)
        else:
            self.bloomFilter = BloomFilter(capacity= 1000000 ,error_rate=0.0001)
            self.bloomFilter.save_to_file(file_path)
            logger.info(f"Created file")

        # Open converter.json file
        with open('./Utils/convert.json', encoding='utf-8', mode='r') as file:
            self.converter = json.load(file)

        # Use ScrapyTermCompare
        self.termCompare = ScrapyTermCompare(self.category, self.brandName, spider.name)

        # Open connection to ElasticSearch
        self.es = MyElasticSearch()

    def close_spider(self, spider):
        # Send bulk request to elasticSearch
        isSuccessfullyIndexed, count = self.es.bulkIndex(spider)

        # Close and save Bloom Filter
        if(isSuccessfullyIndexed):
            file_path = f"./BloomFilter/{spider.name}BloomFilter.bin"
            if os.path.exists(file_path):
                self.bloomFilter.save_to_file(file_path)

            logger.info(f'Scrape item {self.category} {self.brandName}')
            print(f'{spider.name} scraped: {count} item, models included: {self.modelOfProduct}')
            logger.info(f'{spider.name} scraped: {count} item, models included: {self.modelOfProduct}')
        else:
            logger.error("Failed to index product to ElasticSearch")
            print("Failed to index product to ElasticSearch")

    def process_item(self, item, spider):
        try:
            alreadyInDB = False
            # Adapter is used to ensure data scraped by spiders is in correct format
            adapter = ItemAdapter(item)

            # checking is same url
            unique_id = str(adapter['unique_id'][0])
            # 可以添加检查ElasticSearch查找uniqueid
            hasItem = unique_id in self.bloomFilter

            if hasItem:
                elasticProduct = self.es.search(uniqueId=unique_id, index=0)
                
                if(elasticProduct):
                    # 经过双从测试，才丢弃，否则继续添加
                    alreadyInDB = True
                    try:
                        elastic_price = int(elasticProduct[0]['_source']['price_CNY'])
                        item_price = int(item['price_CNY'])
                        if(elastic_price != item_price):
                            # Remove the product inside es
                            self.es.delete_one_document(unique_id)
                        else:
                            raise DropItem(f"Price not changed ori: {item['price_CNY']}, another price {elasticProduct[0]['_source']['price_CNY']}")          
                    except Exception as e:
                        logger.error(f'Error occured in getting Elastic Price: {e}')
                        raise Exception(f'Error occured in Elastic Price: {e}')
                    
            item = self.generalize(item)

            # 判断商品型号（两次判断，确保数据正确）
            # 1. 利用TermCompare
            mostSimilarModel, mostSimilarCategory, cleanedTitle, distance = self.termCompare.GetMostSimilarProduct(item['title'])
            
            # 2. 利用字符串匹配
            if(mostSimilarModel == None and mostSimilarCategory == None):
                raise DropItem(f"Model not in json: {item['title']}, dropping..., Consider to add the model into json")
            
            # 3. 字符串匹配后，通过比较平均价格去除商品价格不合理的商品
            # 基于百分位数法进行过滤
            if mostSimilarModel not in self.averagePrice:
                price = self.es.get_average_price(mostSimilarModel)
                self.averagePrice[mostSimilarModel] = price
            
            isValidPrice = self.filterByPrice(item, mostSimilarModel)    

            if not isValidPrice:
                raise DropItem(f"Price not in average range : {item['title']}, item price: {item['price_CNY']}, average price: {self.averagePrice[mostSimilarModel]}, dropping...")                

            item['model'] = mostSimilarModel
            item['category'] = mostSimilarCategory

            # 统计商品
            self.SaveModelOfProduct(mostSimilarModel)

            # add item to elasticsearch bulk_data
            self.es.appendProduct(item)

            # add item to bloomfilter
            if(not alreadyInDB):
                self.bloomFilter.add(unique_id)
            return item
        except Exception as e:
            logger.error(f'Error occured in generalizing item: {e}')

    def SaveModelOfProduct(self, mostSimilarModel):
        if self.modelOfProduct.get(mostSimilarModel) is not None:
            self.modelOfProduct[mostSimilarModel] = self.modelOfProduct[mostSimilarModel] + 1
        else:
            self.modelOfProduct[mostSimilarModel] = 1

    def filterByPrice(self, item, mostSimilarModel):
        # 未能判断，直接返回
        averagePrice = self.averagePrice[mostSimilarModel]
        
        if averagePrice == 0:
            return True
        else:
            itemPrice = item['price_CNY']
            if itemPrice < averagePrice * 0.2 or itemPrice > averagePrice * 2.5:
                return False
            else:
                return True
                

    def generalize(self, item):
        
        try:
            # 1. set to single item for every 'item'
            for key,value in item.items():
                item[key] = value[0]

            # 2. generalize the condition
            condition_generalize_dict = self.converter['product']['condition']
            flag = 1

            for condition in condition_generalize_dict:

                for condition_atom in condition_generalize_dict[condition]:
                    if condition_atom in item['condition']:
                        item['condition'] = condition
                        flag = 0
                        break

            if flag:
                item['condition'] = 'used'


            return item
        except Exception as e:
            logger.error(f'Error occured in generalizing item: {e}')
            raise Exception(f'Error occured in generalizing item: {e}')