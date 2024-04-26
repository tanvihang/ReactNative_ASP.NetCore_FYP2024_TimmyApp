from typing import Iterable
import scrapy
from scrapy import Request
from scrapy.loader import ItemLoader
from scrapy.loader.processors import MapCompose, Join
import json
import os
import time
from items import ProductItem
from datetime import datetime
import socket
import logging
from scrapy.utils.log import configure_logging 
from scrapy.exceptions import CloseSpider

class AihuishouSpider(scrapy.Spider):

    # logger = logging.getLogger()
    # logger.setLevel(logging.WARNING)

    name = "aihuishou"
    allowed_domains = ["aihuishou.com"]
    page_exceed = False
    page_index = 0

    def __init__(self, *args, **kwargs):
        super(AihuishouSpider, self).__init__(*args, **kwargs)
        self.logger.setLevel(logging.WARNING)
        self.dynamic_parameter = {
            "category": getattr(self, 'search', 'none')['category'],
            "brand": getattr(self, 'search', 'none')['brand'],
        }

    def start_requests(self):
        self.searchAttr = getattr(self,'search','none')

        if(self.searchAttr == 'none'):
            raise ValueError("Search should not be empty")

        self.category = self.searchAttr['category']
        self.brandName = self.searchAttr['brand']
        self.modelName = self.searchAttr['model']
        self.isTest = self.searchAttr['isTest']
        self.iteration = self.searchAttr['iteration']

        if(self.category == 'none' or self.brandName == 'none'):
            raise ValueError("search parameter(category/brandName) cannot be 'none'")
        
        # translate brandname to chinese
        with open('./Utils/ProductChinese.json', 'r', encoding='utf-8') as file:
            jsonData = json.load(file)
            self.chineseBrandName = jsonData[self.brandName]
        
        # get category code
        with open('./Utils/PlatformCategory.json', 'r', encoding='utf-8') as file:
            jsonData2 = json.load(file)
            self.categoryId = jsonData2['aihuishou'][self.category]

        self.searchTerm = self.chineseBrandName + " " + self.modelName
        print(self.searchTerm)

        base_url = "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"

        headers = {
            'Content-Type': 'application/json;charset=UTF-8',
            'Accept' : 'application/json, text/plain, */*',
            'Ahs-App-id': 10002,
        }

        cookies = {
            'tfstk': 'eesJW1VQRSVunvGwouUcYuHRohe0ogByl_WsxBAoRsCAiI2PEMvuvB1Pg4YQa3jdMsdVVQOuUJndOO_lx3OovHBdT6YW',
            'ssxmod_itna2': 'Yqfxu7qiwE3mq0dPYIYPCqDIvIf2Q0D0h40IdPG9W=5DBwSh47p4P8yH=S837zln2qidVjORnt8e5iu0bwn0e69DGQL1Yfurid+w4h5asfEj+RvAA407vxjKD2nYD===',
            'ssxmod_itna': 'Yqfxu7qiwE3mq0dPYIYPCqDIvIf2Q0D0h40IdpdD/AY+D3q0=GFDf47fY8pr6Ax83AOeIaen7BRG5wF=RmEFQcj4qPPLAroTdqwKQo4GLDmIjeHixYYDtxBYDQxAYDGDDpcD84DrD7EUZBLtDm4G0DGQ7DB=OUqDfnqGWfM=DmuiDGqDn3qG2D7tlnDDlnBWNQ2DTAmXRjeKNxGtqBwLSD0tixBd49awSRmHscDi/fpdoHg6PHeGuDG=7fqGmm2TDCxNxpeAjFY+sK7mYY0d3KgxPbRea8Dw=KrhPYvL2V7eqYS+0gHqDG+43g5WiDD=',
            'acw_tc': '781bad3717090094341123079e7a5895e27c9e3f73964db8a0be1be96ef109',
            'sensorsdata2015jssdkcross': 'sensorsdata2015jssdkcross',
        }

        while not self.page_exceed and self.page_index != self.iteration:
            payload = {
                "keyword": self.searchTerm,
                "sortValue": "sort_composite",
                "pageIndex": self.page_index,
                "pageSize": 30,
                "gaeaCategoryId": self.categoryId
            }
            self.page_index = self.page_index + 1
            yield Request(base_url, method="POST", headers=headers, body=json.dumps(payload), callback=self.parse)


    def parse(self, response):
        
        try:
            string_data = response.body.decode('utf-8')
            json_data = json.loads(string_data)

            json_item_list = json_data['data']['list']

            if(len(json_item_list) < 1):
                self.page_exceed = True
                return

            for item in json_item_list:
                loader = ItemLoader(item=ProductItem())

                loader.add_value('title', item['name'])
                loader.add_value('price', item['price'])
                loader.add_value('condition',item['gaeaFinenessName'])
                product_tags = ' '.join(item['productTag'])
                loader.add_value('description', product_tags)
                loader.add_value('product_url','https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo='+item['productNo'])
                loader.add_value('product_image', item['images'][0])
                loader.add_value('currency', 'RMB')


                #"https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240215163424267014"]
                loader.add_value('unique_id','ahs'+item['productNo'])

                loader.add_value('category', self.category)
                loader.add_value('brand', self.brandName)
                loader.add_value('model', self.modelName)
                loader.add_value('is_test', self.isTest)

                loader.add_value('root_url', response.url)
                loader.add_value('project', self.settings.get('BOT_NAME'))
                loader.add_value('spider', self.name)
                loader.add_value('server', socket.gethostname())
                
                loader.add_value('created_date', datetime.now())
                loader.add_value('scraped_date', datetime.now())

                loader.add_value('country','china')
                loader.add_value('state', 'china')

                yield loader.load_item()

        except Exception as e: 
            logging.error(f"Error occured in parse at spider {self.name}: {e}")
            raise CloseSpider(f"Error occured in parse at spider {self.name}: {e}")
        # with open('output.json','w', encoding='utf-8') as json_file:
        #     json.dump(json_data, json_file, indent=4, ensure_ascii=False)
