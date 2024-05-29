# Search Index
*该文件包含了之后常用的检索指令，可以进行组合。*

Example data
```json
{
    "title": "Apple 13 Macbook Air M1 (New)",
    "price": 3800,
    "price_CNY": 5700,
    "condition": "new",
    "description": "Chip 256GB space Gray",
    "product_url": "https://www.mudah.my/Apple+13+Macbook+Air+M1+(New)-106868767.htm",
    "product_detail_url": "https://www.mudah.my/Apple+13+Macbook+Air+M1+(New)-106868767.htm",
    "product_image": "https://img.rnudah.com/images/28/2879721862911196477.jpg",
    "created_date": "2024-05-27T15:02:03.000000Z",
    "currency": "MYR",
    "unique_id": "mudah106868767",
    "category": "computer",
    "brand": "apple",
    "model": "macbook air 13",
    "root_url": "https://search.mudah.my/v1/search?from=0&include=extra_images%2Cbody&limit=40&q=apple+&category=3060",
    "spider": "mudah",
    "server": "Vi-Laptop",
    "scraped_date": "2024-05-27T15:24:42.552421Z",
    "is_test": 0,
    "country": "malaysia",
    "state": "Kuala Lumpur"
}
```

**用户可用选项：**
- title
- description
- price_CNY
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
- scraped_date
- country
- category
- brand
- model

---

### 重点搜索功能
#### 获取商品的平均价格
```bash
# Get model average price 
GET /product/_search
{
  "size":0,
  "aggs": {
    "specific_product": {
      "filter": {
        "term": {
          "model":"iphone 13"
        }
      },
      "aggs": {
        "average_price": {
          "avg": {
            "field": "price_CNY"
          }
        }
      }
    }
  }
}
```

#### 获取商品的百分数位
```bash
POST /product/_search
{
  "size": 0,
  "query": {
    "term": {
      "model": "iphone 12"  // 指定特定型号的商品
    }
  },
  "aggs": {
    "price_percentiles": {
      "percentiles": {
        "field": "price_CNY",
        "percents": [1,99]  // 指定要计算的百分位数
      }
    },
    "avg_price":{
      "avg": {
        "field": "price_CNY"
      }
    }
  }
}
```

#### 获取es内的商品种类数量分布
