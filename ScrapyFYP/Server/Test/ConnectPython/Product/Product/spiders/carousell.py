import scrapy


class CarousellSpider(scrapy.Spider):
    name = "carousell"
    allowed_domains = ["carousell.com.my"]
    start_urls = ["https://carousell.com.my"]

    def parse(self, response):
        pass
