from elasticsearch import Elasticsearch
from elasticsearch.helpers import bulk
from elasticsearch import ApiError
from datetime import datetime
import json

class MyElasticSearch:
    def __init__(self):
        self.scheme = "https://"
        self.host = 'localhost'
        self.port = 9200
        self.username = 'elastic'
        self.password = '6gCqElpEIxnENzt5tT3U'
        self.bulk_data= []
        self.index = "product"

        # Create Elasticsearch client instance
        self.es = Elasticsearch(
            hosts=f"https://localhost:9200/",
            basic_auth=(self.username, self.password),
            verify_certs=True,  # Disable SSL certificate verification
            ca_certs="./ElasticSearch/http_ca.crt"
        )

    def appendProduct(self,item):
        # Convert the original date string to a datetime object
        # original_date = datetime.strptime(item['date'], "%Y-%m-%d %H:%M:%S")
        current_date = datetime.now()

        # Format the datetime object as a string in ISO 8601 format
        # iso_date_string = original_date.strftime("%Y-%m-%dT%H:%M:%S.%fZ")
        formatted_date = current_date.strftime("%Y-%m-%dT%H:%M:%S.%fZ")

        formatted_date2 = item['created_date'].strftime("%Y-%m-%dT%H:%M:%S.%fZ")

        self.bulk_data.append(
            {
                "_index": self.index,
                "_id": item['unique_id'],
                "_source":{
                    "title": item['title'],
                    "price": item['price'],
                    "price_CNY": item['price_CNY'],
                    "condition": item['condition'],
                    "description": item['description'],
                    "product_url": item['product_url'],
                    "product_detail_url": item['product_detail_url'],
                    "product_image": item['product_image'],
                    "created_date": formatted_date2,
                    "currency": item['currency'],
                    "unique_id": item['unique_id'],
                    "category": item['category'],
                    "brand": item['brand'],
                    "model": item['model'],
                    "root_url": item['root_url'],
                    "spider": item['spider'],
                    "server": item['server'],
                    "scraped_date": formatted_date,
                    "is_test": item['is_test'],
                    "country": item['country'],
                    "state": item['state']
                }
            }
        )

    
    def bulkIndex(self, spider):
        try:
            # Use es.bulk to index the documents
            success, failed = bulk(self.es, self.bulk_data)

            print(f"{spider.name} Successfully indexed: {success}")
            print(f'scraped: {success}')

            return True
        
        except ApiError as e:
            print(f"Failed to index documents: {e}")
            for idx, error in e.errors.items():
                print(f"Error for document {idx}: {error}")

            return False

    def indexOne(self):
        # Define the document data
        document_data = {
            "title": "iPhone 8 Pro",
            "price": 1099.99,
            "condition": "New",
            "description": "Brand new iPhone 12 Pro with 256GB storage. Includes original box and accessories.",
            "product_url": "https://example.com/iphone-12-pro",
            "product_image": "https://example.com/images/iphone-12-pro.jpg",
            "currency": "USD",
            "unique_id": "123456789",
            "category": "Electronics",
            "brand": "Apple",
            "model": "iPhone 12 Pro",
            "root_url": "https://example.com",
            "spider": "WebCrawler",
            "server": "WebServer1",
            "created_date": datetime.now(),
            "scraped_date": datetime.now(),
            "is_test": "1",
            "country": "United States",
            "state": "California"
        }

        # Index the document
        response = self.es.index(index="product", body=document_data)

        # Check the response
        print(response)

    def demoBulkIndex(self):

        # Define the documents to index
        documents = [
            {
                "_index": "product",
                "_id": "1",
                "_source": {
                    "title": "iPhone 12 Pro",
                    "price": 1099.99,
                    "condition": "New",
                    "description": "Brand new iPhone 12 Pro with 256GB storage. Includes original box and accessories.",
                    "product_url": "https://example.com/iphone-12-pro",
                    "product_image": "https://example.com/images/iphone-12-pro.jpg",
                    "currency": "USD",
                    "unique_id": "123456789",
                    "category": "Electronics",
                    "brand": "Apple",
                    "model": "iPhone 12 Pro",
                    "root_url": "https://example.com",
                    "spider": "WebCrawler",
                    "server": "WebServer1",
                    "created_date": "2023-12-01",
                    "scraped_date": "2024-03-15",
                    "is_test": "1",
                    "country": "United States",
                    "state": "California"
                }
            },
            {
                "_index": "product",
                "_id": "2",
                "_source": {
                    "title": "iPhone 11 Pro",
                    "price": 1099.99,
                    "condition": "New",
                    "description": "Brand new iPhone 11 Pro with 256GB storage. Includes original box and accessories.",
                    "product_url": "https://example.com/iphone-12-pro",
                    "product_image": "https://example.com/images/iphone-12-pro.jpg",
                    "currency": "USD",
                    "unique_id": "12345789",
                    "category": "Electronics",
                    "brand": "Apple",
                    "model": "iPhone 11 Pro",
                    "root_url": "https://example.com",
                    "spider": "WebCrawler",
                    "server": "WebServer1",
                    "created_date": "2023-12-01",
                    "scraped_date": "2024-03-15",
                    "is_test": "1",
                    "country": "United States",
                    "state": "California"
                }
            },
            # Add more documents here if needed
        ]

        # Perform bulk indexing
        success, failed = bulk(self.es, documents)

        # Check for failed documents
        if failed:
            print(f"Failed to index {len(failed)} documents:", failed)
        else:
            print("Bulk indexing successful.")


    def search(self, title = None, pricelow = None, pricehigh = None, condition = None, currency = None, spider = None, date = None, size = 10 ,index = None, uniqueId = None):
         # Construct the base query
        query = {
            "query": {
                "bool": {
                    "must": [],
                    "filter":[]
                }
            },
            "size": size,
            "from": (int(index) * int(size))
        }

        # Add individual query conditions based on provided parameters
        if title:
            query["query"]["bool"]["must"].append({"match": {"title": title}})
        if pricelow:
            query["query"]["bool"]["must"].append({"range": {"price": {"gte": pricelow}}})
        if pricehigh:
            query["query"]["bool"]["must"].append({"range": {"price": {"lte": pricehigh}}})
        if condition:
            query["query"]["bool"]["must"].append({"match": {"condition": condition}})
        if currency:
            query["query"]["bool"]["must"].append({"match": {"currency": currency}})
        if spider:
            query["query"]["bool"]["must"].append({"match": {"spider": spider}})
        if date:
            query["query"]["bool"]["must"].append({"match": {"date": date}})
        if uniqueId:
            query["query"]["bool"]["filter"].append({"term": {"unique_id": uniqueId}})  # Add the unique ID search condition


        response = self.es.search(index="product", body=query)
        # Return the search results
        return response["hits"]["hits"]


    def delete_one_document(self, product_unique_id):
        try:
            response = self.es.delete(index = self.index, id=product_unique_id)
           # 检查响应
            if response['result'] == 'deleted':
                print(f"Document with ID {product_unique_id} deleted successfully.")
                return True
            else:
                print(f"Failed to delete document with ID {product_unique_id}.")
                return False
        except Exception as e:
            print(f"An error occurred while deleting document with ID {product_unique_id}: {e}")
            return False
