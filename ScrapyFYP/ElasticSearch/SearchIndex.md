# Search Index
*该文件包含了之后常用的检索指令，可以进行组合。*

Example data
```json
{"title": "iphone 15 green 128gb", 
"price": 3200, 
"condition": "Second-hand (Used)", 
"description": "Dapat smua fullset\n\nadapter ja brand ugreen\ncasing free\n\npostage tlong add ok insurance dengan ninja van ! rm20 kebwah ja .", 
"product_url": "https://www.mudah.my/iphone+15+green+128gb-105942839.htm", 
"product_image": "https://img.rnudah.com/images/28/2868712361719731594.jpg", 
"currency": "MYR", 
"unique_id": "mudah105942839", 
"root_url": "https://search.mudah.my/v1/search?from=0&include=extra_images%2Cbody&limit=40&q=iphone+15", "project": "Product", 
"spider": "mudah", 
"server": "Vi-Laptop", 
"date": "2024-03-08 17:20:25"},

```

**用户可用选项：**
- title
- price
- condition
  - new
  - mint
  - used
- currency
  - RMB
  - MYR
- spider
  - mudah
  - aihuishou
- date


```python
def search(title, pricelow = None, pricehigh = None, condition = None, currency = None, spider = None, date = None, size = 10 ,index = None):
     # Construct the base query
    query = {
        "query": {
            "bool": {
                "must": []
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

    response = es.search(index="product", body=query)

     # Return the search results
    return response["hits"]["hits"]
```

---

