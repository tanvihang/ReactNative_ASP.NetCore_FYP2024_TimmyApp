from ElasticSearch.MyElasticSearch import MyElasticSearch

es = MyElasticSearch()

product = es.search(uniqueId="ahs20240412202153060766", index = 0)

if(product):
    print(product[0]['_source']['price_CNY'])
    es.delete_one_document("ahs20240412202153060766")

product = es.search(uniqueId="ahs20240412202153060766", index = 0)

if(product):
    print(product[0])
