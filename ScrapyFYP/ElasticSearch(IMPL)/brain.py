from elasticsearch import Elasticsearch
from elasticsearch.helpers import bulk
from elasticsearch import ApiError
from datetime import datetime
import json

scheme = "https://"
host = 'localhost'
port = 9200
username = 'elastic'
password = '6gCqElpEIxnENzt5tT3U'


def generalize(item):
    
    with open("./Data/converter.json", mode='r') as file1:
        json_data = json.load(file1)

    # generalize the condition
    condition_generalize_dict = json_data['product']['condition']
    flag = 1

    for condition in condition_generalize_dict:

        for condition_atom in condition_generalize_dict[condition]:
            if condition_atom in item['condition']:
                item['condition'] = condition
                flag = 0
                break

    if flag:
        item['condition'] = condition_generalize_dict[len(condition_generalize_dict)-1]

    return item

# Create Elasticsearch client instance
es = Elasticsearch(
    hosts=f"https://localhost:9200/",
    basic_auth=(username, password),
    verify_certs=True,  # Disable SSL certificate verification
    ca_certs="http_ca.crt"
)

index_name = 'website'
first_document_data = {
    'title': "Wohoo",
    'text': 'well its a text',
    'date': '2014/01/01'
}

bulk_data = []

def bulk_index():
    with open("./Data/mudah.json",mode='r', errors='ignore') as file:
        json_data = json.load(file)
        for item in json_data:

            # Convert the original date string to a datetime object
            original_date = datetime.strptime(str(item['date']), "%Y-%m-%d %H:%M:%S")

            # Format the datetime object as a string in ISO 8601 format
            iso_date_string = original_date.strftime("%Y-%m-%dT%H:%M:%S.%fZ")

            item = generalize(item)

            bulk_data.append(
                {
                    "_index": "product",
                    "_id": item['unique_id'],
                    "_source":{
                        "title": item['title'],
                        "condition": item['condition'],
                        "description": item['description'],
                        "product_url": item['product_url'],
                        "product_image": item['product_image'],
                        "currency": item['currency'],
                        "unique_id": item['unique_id'],
                        "root_url": item['root_url'],
                        "spider": item['spider'],
                        "server": item['server'],
                        "date": iso_date_string
                    }
                }
            )

    try:
        # Use es.bulk to index the documents
        success, failed = bulk(es, bulk_data)

        print(f"Successfully indexed: {success}")
        print(f"Failed to index: {failed}")
    except ApiError as e:
        print(f"Failed to index documents: {e}")
        for idx, error in e.errors.items():
            print(f"Error for document {idx}: {error}")

def search(title, pricelow = None, pricehigh = None, condition = None, currency = None, spider = None, date = None, size = 10 ,index = None, sortby = []):
     # Construct the base query
    query = {
        "query": {
            "bool": {
                "must": []
            }
        },
        "size": size,
        "from": (int(index) * int(size)),
        "sort":[
        ]
    }

    for sortCondition in sortby:
        if sortCondition == 'priceasc':
            query['sort'].append({"price":{"order":"asc"}})
        if sortCondition == 'pricedesc':
            query['sort'].append({"price":{"order":"desc"}})
        if sortCondition == 'score':
            query['sort'].append({"_score":{"order":"desc"}})
        

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

    response = es.search(index="product", body=query)

     # Return the search results
    return response["hits"]["hits"]

response = search(title="nikon", size="20", index="1",sortby=["score"])
print(response)