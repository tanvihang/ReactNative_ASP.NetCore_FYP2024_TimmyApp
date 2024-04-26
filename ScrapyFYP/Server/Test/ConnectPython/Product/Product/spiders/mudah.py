import scrapy
import time
from scrapy import Request
from scrapy.loader import ItemLoader
import json
from items import ProductItem
import socket
from datetime import datetime
from urllib.parse import urlencode
import logging
from scrapy.utils.log import configure_logging 
from scrapy.exceptions import CloseSpider

# example execution with search parameter of iphone 12
# scrapy crawl mudah -o mudahOutput.json -a search="iphone 12" 
# scrapy crawl mudah -o "%(name)s_%(time)s.json" -a search="huawei mate 60"
class MudahSpider(scrapy.Spider):

    # logger = logging.getLogger()
    # logger.setLevel(logging.WARNING)

    name = "mudah"
    allowed_domains = ["mudah.com"]
    page_exceed = False
    count = 0

    def __init__(self, *args, **kwargs):
        super(MudahSpider, self).__init__(*args, **kwargs)
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

        if(self.category == 'none' or self.brandName == 'none' or self.modelName == 'none'):
            raise ValueError("search parameter(category/brandName) cannot be 'none'")

        with open("./Utils/PlatformCategory.json", 'r', encoding='utf-8') as file:
            jsonData = json.load(file)
            self.categoryId = jsonData['mudah'][self.category]

        searchTerm = self.brandName + " " + self.modelName
        print(searchTerm)

        base_url = "https://search.mudah.my/v1/search"

        headers = {
            'authority': 'search.mudah.my',
            'accept': '*/*',
            'accept-language': 'en-US,en;q=0.9',
            'cache-control': 'no-cache',
            'origin': 'https://www.mudah.my',
            'pragma': 'no-cache',
            'referer': 'https://www.mudah.my/',
            'sec-ch-ua': '"Chromium";v="122", "Not(A:Brand";v="24", "Google Chrome";v="122"',
            'sec-ch-ua-mobile': '?0',
            'sec-ch-ua-platform': '"Windows"',
            'sec-fetch-dest': 'empty',
            'sec-fetch-mode': 'cors',
            'sec-fetch-site': 'same-site',
            'user-agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36',
        }

        while not self.page_exceed and self.count != self.iteration:
            fromVal = 40 * self.count
            self.count = self.count + 1

            params = {
                'from': fromVal,
                'include': 'extra_images,body',
                'limit': '40',
                'q': searchTerm,
                'category': self.categoryId
            }

            # Encode the parameters
            encoded_params = urlencode(params)

            # Create the full URL
            full_url = '{}?{}'.format(base_url, encoded_params)
            logging.info(f"{self.count} page")
            yield Request(full_url, method='GET',headers=headers, callback=self.parse)


    def parse(self, response):

        try: 
            string_data = response.text
            json_data = json.loads(string_data)

            #json_item_list = json_data['data']
            json_item_list = json_data['data']

            if(len(json_item_list) < 1):
                self.page_exceed = True
                return


            for item in json_item_list:
                loader = ItemLoader(item=ProductItem())

                title = item['attributes']['subject']
                loader.add_value('title', title)

                loader.add_value('price', item['attributes']['price'])

                loader.add_value('condition',item['attributes']['condition_name'])

                description = item['attributes']['body']
                description = description.strip()
                loader.add_value('description',description)

                product_id = item['id']
                title_plus = title.replace(' ', '+')
                product_url = 'https://www.mudah.my/'+ title_plus +'-'+ str(product_id) +'.htm'
                loader.add_value('product_url',product_url)

                image_base_url = item['links']['image_baseurl']
                image_id = item['attributes']['image']
                image_url = image_base_url + image_id
                loader.add_value('product_image', image_url)

                loader.add_value('currency', 'MYR')
                loader.add_value('unique_id','mudah'+ str(product_id))

                created_date = item['attributes']['date']
                date_time_obj = datetime.strptime(created_date,  "%Y-%m-%d %H:%M:%S")
                loader.add_value('created_date', date_time_obj)

                loader.add_value('category', self.category)
                loader.add_value('brand', self.brandName)
                loader.add_value('model', self.modelName)
                loader.add_value('is_test', self.isTest)

                loader.add_value('root_url', response.url)
                loader.add_value('project', self.settings.get('BOT_NAME'))
                loader.add_value('spider', self.name)
                loader.add_value('server', socket.gethostname())
                
                loader.add_value('scraped_date', datetime.now())

                loader.add_value('country','malaysia')
                loader.add_value('state', item['attributes']['region_name'])

                yield loader.load_item()
        
        except Exception as e: 
            logging.error(f"Error occured in parse at spider {self.name}: {e} ")
            raise CloseSpider(f"Error occured in parseat spider {self.name}: {e}")
        