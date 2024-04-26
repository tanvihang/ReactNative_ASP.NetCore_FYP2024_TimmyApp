# Define here the models for your scraped items
#
# See documentation in:
# https://docs.scrapy.org/en/latest/topics/items.html

import scrapy


class ProductItem(scrapy.Item):
    # define the fields for your item here like:
    # name = scrapy.Field()

    # Ecommerce essential fields
    unique_id = scrapy.Field()
    title = scrapy.Field()
    price = scrapy.Field()
    currency = scrapy.Field()
    condition = scrapy.Field()
    description = scrapy.Field()
    product_url = scrapy.Field()
    product_image = scrapy.Field()
    created_date = scrapy.Field()

    # Housekeeping fields
    category = scrapy.Field()
    brand = scrapy.Field()
    model = scrapy.Field()
    is_test = scrapy.Field()

    root_url = scrapy.Field()
    project = scrapy.Field()
    spider = scrapy.Field()
    server = scrapy.Field()

    # Date field
    scraped_date = scrapy.Field()
    
    country = scrapy.Field()
    state = scrapy.Field()

    pass
