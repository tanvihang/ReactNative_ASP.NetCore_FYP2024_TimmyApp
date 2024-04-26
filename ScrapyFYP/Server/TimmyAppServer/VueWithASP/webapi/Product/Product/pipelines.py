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
import treq
import json

class ProductPipeline:
    def process_item(self, item, spider):
        return item

class MyProductPipeline:
    
    settings = get_project_settings()
    def open_spider(self, spider):
        dynamic_parameter = spider.dynamic_parameter
        self.category = dynamic_parameter['category']
        self.brandName = dynamic_parameter['brand']

        self.productCount = 0
        self.modelOfProduct = {}

        # Create Bloom Filter        
        file_path = f"./BloomFilter/{spider.name}BloomFilter.bin"
        if os.path.exists(file_path):
            self.bloomFilter = BloomFilter.load_from_file(file_path)
        else:
            self.bloomFilter = BloomFilter(capacity= 1000000 ,error_rate=0.0001)
            self.bloomFilter.save_to_file(file_path)
            logging.info(f"Created file")

        # Open converter.json file
        with open('./Utils/convert.json', encoding='utf-8', mode='r') as file:
            self.converter = json.load(file)

        # Open connection to Database retrieve product list
        # self.db = TimmyDatabase(self.category, self.brandName)
        # self.db.openConnection()
        # self.productList = self.db.getProduct()
        # self.productListWithoutSpaces = [item.replace(" ", "") for item in self.productList]
        # self.db.closeConnection()

        # Use ScrapyTermCompare
        self.termCompare = ScrapyTermCompare(self.category, self.brandName)

        # Open connection to ElasticSearch
        self.es = MyElasticSearch()

    def close_spider(self, spider):
        # Send bulk request to elasticSearch
        isSuccessfullyIndexed = self.es.bulkIndex(spider)

        # Close and save Bloom Filter
        if(isSuccessfullyIndexed):
            file_path = f"./BloomFilter/{spider.name}BloomFilter.bin"
            if os.path.exists(file_path):
                self.bloomFilter.save_to_file(file_path)

            print(f'{spider.name} scraped: {self.productCount} item, models included: {self.modelOfProduct}')

        else:
            print("Failed to index product to ElasticSearch")

    def process_item(self, item, spider):
        adapter = ItemAdapter(item)
        self.productCount += 1

        # checking is same url
        unique_id = str(adapter['unique_id'][0])
        # 可以添加检查ElasticSearch查找uniqueid
        hasItem = unique_id in self.bloomFilter

        if hasItem:
            elasticProduct = self.es.search(uniqueId=unique_id, index=0)
            
            # 经过双从测试，才丢弃，否则继续添加
            elastic_price = int(elasticProduct[0]['_source']['price_CNY'])
            item_price = int(item['price_CNY'])

            if(elasticProduct):
                if(elastic_price != item_price):
                    # Remove the product inside es
                    self.es.delete_one_document(unique_id)
                else:
                    raise DropItem(f"Price not changed ori: {item['price_CNY']}, another price {elasticProduct[0]['_source']['price_CNY']}")
    
        item = self.generalize(item)

        # 判断商品型号（两次判断，确保数据正确）
        # 1. 利用TermCompare
        mostSimilarModel, mostSimilarCategory = self.termCompare.GetMostSimilarProduct(item['title'])
        
        # 2. 利用字符串匹配
        if(mostSimilarModel == None and mostSimilarCategory == None):
            raise DropItem(f"Model not in json: {item['title']}, dropping..., Consider to add the model into json")
        

        item['model'] = mostSimilarModel
        item['category'] = mostSimilarCategory

        # 统计商品
        self.SaveModelOfProduct(mostSimilarModel)

        # add item to elasticsearch bulk_data
        self.es.appendProduct(item)

        # add item to bloomfilter
        self.bloomFilter.add(unique_id)
        return item

    def SaveModelOfProduct(self, mostSimilarModel):
        if self.modelOfProduct.get(mostSimilarModel) is not None:
            self.modelOfProduct[mostSimilarModel] = self.modelOfProduct[mostSimilarModel] + 1
        else:
            self.modelOfProduct[mostSimilarModel] = 1

    def generalize(self, item):
        
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
    