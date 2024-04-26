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
from TFidfCosine.TFidfCosine import TFidfCosine
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
        self.db = TimmyDatabase(self.category, self.brandName)
        self.db.openConnection()
        self.productList = self.db.getProduct()
        self.productListWithoutSpaces = [item.replace(" ", "") for item in self.productList]
        self.db.closeConnection()

        # Use TFIDF
        self.tfIdf = TFidfCosine(self.productList)

        # Open connection to ElasticSearch
        self.es = MyElasticSearch()

    def close_spider(self, spider):
        
        # Close and save Bloom Filter
        file_path = f"./BloomFilter/{spider.name}BloomFilter.bin"
        if os.path.exists(file_path):
            self.bloomFilter.save_to_file(file_path)

        # Send bulk request to elasticSearch
        self.es.bulkIndex()

        print(f'{spider.name} scraped: {self.productCount} item')

    def process_item(self, item, spider):
        adapter = ItemAdapter(item)
        self.productCount += 1

        # checking is same url
        unique_id = str(adapter['unique_id'][0])
        # 可以添加检查ElasticSearch查找uniqueid
        hasItem = unique_id in self.bloomFilter

        if hasItem:
            elasticHave = len(self.es.search(uniqueId=unique_id, index=0))
            
            # 经过双从测试，才丢弃，否则继续添加
            if(elasticHave):
                raise DropItem(f"Already have item {unique_id} in data, dropping...")
    
        item = self.generalize(item)

        # 判断商品型号（两次判断，确保数据正确）
        # 1. 利用TF-IDF
        mostSimilarModel = self.tfIdf.getMostSimilarModel(item['title'])
        
        # 2. 利用字符串匹配
        if(mostSimilarModel == None):
            raise DropItem(f"Model not in json: {item['title']}, dropping..., Consider to add the model into json")
        
        correctModel = self.isValidProductStringCompare(mostSimilarModel, item['title'])

        if(correctModel == False):
            print("通过TFidf但不是正确的型号")
            raise DropItem(f"Model not in json: {item['title']}, dropping..., Consider to add the model into json")

        item['model'] = mostSimilarModel

        # add item to elasticsearch bulk_data
        self.es.appendProduct(item)

        # add item to bloomfilter
        self.bloomFilter.add(unique_id)
        return item

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
    
    def isInProductList(self, string):
        # 去除字符串中的空格
        string = string.lower()
        string_without_spaces = string.replace(" ", "")

        # 遍历去除空格后的字典中的每个元素，检查它是否是字符串的一部分（考虑去除空格后）
        for index, item in enumerate(self.productListWithoutSpaces):
            if item in string_without_spaces:
                print(f'Search string {string}')
                print("Most likeable model:", self.productList[index] )
                return self.productList[index]
                break  # 如果找到了一个匹配的元素，就停止遍历

        # 如果没有找到任何匹配的元素
        else:
            return None
        
    def isValidProductStringCompare(self, similar, title):

        # eg Iphone 15 红色 -> iphone15
        # eg Ipad 15.6 啊啊啊 -> ipad15.6啊啊啊
        title = title.lower()
        title_without_spaces = title.replace(" ", "")

        # eg iphone 15 -> iphone15
        similar_without_spaces = similar.replace(" ", "")

        if similar_without_spaces in title_without_spaces:
            return True
        else:
            raise False