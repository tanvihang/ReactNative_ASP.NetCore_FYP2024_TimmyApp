# Elastic Search Service
| Method Name                                                               | Describe/描述              | Complete/完成情况 |
| ------------------------------------------------------------------------- | -------------------------- | ----------------- |
| [SearchProduct](#searchproduct)                                           | 获取商品                   | 完成              |
| [GetRandom10Product](#getrandom10product)                                 | 获取10个随机的商品         | 完成              |
| [UpdateProduct](#updateproduct)                                           | 更新商品                   | 完成              |
| [GetLowPriceProductForUserSubscribe](#getlowpriceproductforusersubscribe) | 获取数个订阅商品低价格产品 | 完成              |
| [GetOneLowestPriceProduct](#getonelowestpriceproduct)                                                  | 获取一个最小价格产品       | 完成              |
| [GetProductOver7Days](#getproductover7days)                               | 获取爬取时间大于7天的产品  | 完成              |
| [DeleteAllProduct](#deleteallproduct)                                     | 删除所有ElasticSearch产品  | 完成              |
| [RemoveOneProduct](#removeoneproduct)                                     | 删除一个ElasticSearch产品  | 完成              |
---

### SearchProduct
**方法名称**： `SearchProduct`

**方法描述**：
1. 索引产品并以分页形式查询   

**请求参数**： 
| 参数名               | 类型                 | 必填 | 描述           |
| -------------------- | -------------------- | ---- | -------------- |
| productSearchTermDTO | ProductSearchTermDTO | 是   | 查询商品DTO    |
| pageDTO              | PageDTO              | 是   | 分页查询DTO    |
| userId               | string               | 是   | 用户唯一标识符 |

**返回数据**
| 类型                          | 描述     |
| ----------------------------- | -------- |
| PageEntity<ElasticProductDTO > | 一组得查询商品 |

**成功测试输入**

```json
jwtToken :eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVX2NlOWRlOWNkLWMxODMtNDA0ZS05MDBjLWQ3MmRhNGUxN2FhNSIsInVuaXF1ZV9uYW1lIjoidHZoIiwibmJmIjoxNzEzNDk5MjI2LCJleHAiOjE3MTQxMDQwMjYsImlhdCI6MTcxMzQ5OTIyNn0.xH4A6DdP9AYna3Rz47nSfFjzVlCXGFOEAySYeNabyZs

{
  "productSearchTerm": {
    "category": "mobile",
    "brand": "apple",
    "model": "iphone 13 pro max",
    "description": "128",
    "highest_price": 10000,
    "lowest_price": 5000,
    "country": "china",
    "state": "china",
    "condition": "mint",
    "spider": [
      ""
    ],
    "sort": "priceasc",
    "isTest": 0
  },
  "pageDTO": {
    "pageSize": 10,
    "currentPage": 1
  }
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": {
    "count": 10,
    "rows": [
      {
        "title": "Apple 苹果 iPhone 13 Pro Max 128G 石墨色 国行 全网通",
        "price": 4589,
        "price_CNY": 4589,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240412143041178196",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240412143041178196",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240414/RW22G04038/202404141527131520-0-exposure(-5.0)-brightness(20.0)-20240414152716.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:05:15.1986Z",
        "scraped_date": "2024-04-18T14:05:15.1986Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240412143041178196",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 13 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
    }
}
```

**失败测试输入**
```json
{
  "productSearchTerm": {
    "category": "mobile",
    "brand": "apple",
    "model": "iphone 10 pro max",
    "description": "128",
    "highest_price": -10000,
    "lowest_price": 5000,
    "country": "china",
    "state": "china",
    "condition": "mint",
    "spider": [
      ""
    ],
    "sort": "priceasc",
    "isTest": 0
  },
  "pageDTO": {
    "pageSize": 10,
    "currentPage": 0
  }
}
```


**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "No product with the search term given",
  "data": null
}
```

---

### GetRandom10Product
**方法名称**： `GetRandom10Product`

**方法描述**：
1. 随机获取10个商品

**请求参数**： 
| 参数名               | 类型                 | 必填 | 描述        |
| -------------------- | -------------------- | ---- | ----------- |
| pageDTO              | PageDTO              | 是   | 分页查询DTO |

**返回数据**
| 类型                            | 描述           |
| ------------------------------- | -------------- |
| PageEntity< ElasticProductDTO > | 一组得查询商品 |

**成功测试输入**
```json
{
  "pageSize": 10,
  "currentPage": 1
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Got 10 random product",
  "data": {
    "count": 10,
    "rows": [
      {
        "title": "Apple 苹果 iPhone 13 Pro Max 256G 银色 国行 全网通",
        "price": 4849,
        "price_CNY": 4849,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240407155848404148",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240407155848404148",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240413/RW22G05190/202404131839372733-0-exposure(-6.0)-brightness(25.0)-20240413183951.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:04:43.715104Z",
        "scraped_date": "2024-04-18T14:04:43.715104Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240407155848404148",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 13 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      }]
  }
}
```

**失败测试输入**
```json
{
  "pageSize": -10,
  "currentPage": 1
}
```

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "No Product",
  "data": null
}
```

---

### GetLowestPriceProduct

**方法名称**： `GetLowestPriceProduct`

**方法描述**：
1. 通过商品中英文查找商品，并获取最小价格
2. 对商品进行再次分析（去除不符合规则的商品）
3. 返回最小价格

**请求参数**： 
| 参数名   | 类型        | 必填 | 描述     |
| -------- | ----------- | ---- | -------- |
| string[] | productName | 是   | 搜索商品中英文 |

**返回数据**
| 类型   | 描述     |
| ------ | -------- |
| double | 最小价格 |


**成功测试输入**

**成功测试输出**

**失败测试输入**

**失败测试输出**

---

### UpdateProduct

**方法名称**： `UpdateProduct`

**方法描述**：
1. 跟新商品

**请求参数**： 
| 参数名         | 类型              | 必填 | 描述              |
| -------------- | ----------------- | ---- | ----------------- |
| elasticProduct | ElasticProductDTO | 是   | ElasticSearch商品 |

**返回数据**
| 类型 | 描述     |
| ---- | -------- |
| bool | 更新状态 |

**注意事项**
更新前数据
```json
{
        "_index": "product",
        "_id": "ahs20240405204006377343",
        "_score": 5.1119876,
        "_source": {
          "title": "Apple 苹果 iPhone 13 Pro Max 256G 苍岭绿色 国行 全网通",
          "price": 5059,
          "price_CNY": 5059,
          "condition": "mint",
          "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
          "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240405204006377343",
          "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240405204006377343",
          "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240408/RW22G05200/202404081126376206-0-exposure(-5.0)-brightness(5.0)-20240408112641.jpg?x-oss-process=image/resize,s_750",
          "created_date": "2024-04-18T14:04:39.360234Z",
          "currency": "CNY",
          "unique_id": "ahs20240405204006377343",
          "category": "mobile",
          "brand": "apple",
          "model": "iphone 13 pro max",
          "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2",
          "spider": "aihuishou",
          "server": "Vi-Laptop",
          "scraped_date": "2024-04-18T14:04:39.360234Z",
          "is_test": 0,
          "country": "china",
          "state": "china"
        }
```

**成功测试输入**
```json
{
  "title": "aaa",
  "price": 10000,
  "price_CNY": 20000,
  "condition": "mint",
  "description": "",
  "product_url": "",
  "product_detail_url": "",
  "product_image": "",
  "created_date": "2024-04-19T04:34:32.430Z",
  "scraped_date": "2024-04-19T04:34:32.430Z",
  "country": "chinia",
  "state": "china",
  "currency": "CNY",
  "unique_id": "ahs20240405204006377343",
  "category": "aaa",
  "brand": "aaa",
  "model": "aaa",
  "spider": "aaa",
  "is_test": 1,
  "server": "aaa",
  "root_url": "aaa"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": true
}
```

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Update product ahs20240415133238219 failed",
  "data": false
}
```

**注意**
更新后数据
```json
{
        "_index": "product",
        "_id": "ahs20240405204006377343",
        "_score": 4.60517,
        "_source": {
          "title": "aaa",
          "price": 10000,
          "price_CNY": 20000,
          "condition": "mint",
          "description": "",
          "product_url": "",
          "product_detail_url": "",
          "product_image": "",
          "created_date": "2024-04-19T04:34:32.4300000Z",
          "currency": "CNY",
          "unique_id": "ahs20240405204006377343",
          "category": "aaa",
          "brand": "aaa",
          "model": "aaa",
          "root_url": "aaa",
          "spider": "aaa",
          "server": "aaa",
          "scraped_date": "2024-04-19T04:34:32.4300000Z",
          "is_test": 1,
          "country": "chinia",
          "state": "china"
        }
      }
```

---

### GetLowPriceProductForUserSubscribe

**方法名称**： `GetLowPriceProductForUserSubscribe`

**方法描述**：
1. 通过用户订阅条件进行数据的爬取
2. 同时以价格递增的方式返回给用户

**请求参数**： 
| 参数名           | 类型             | 必填 | 描述         |
| ---------------- | ---------------- | ---- | ------------ |
| userSubscription | UserSubscription | 是   | 用户订阅类型 |

**返回数据**
| 类型                      | 描述     |
| ------------------------- | -------- |
| List< ElasticProductDTO > | ElasticProduct的数据 |

**成功测试输入**
```
userSubscriptionId : US_23fe11b1-8748-4870-941c-ce2ccc07f3d8
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": [
    {
      "title": "Apple 苹果 iPhone 13 Pro Max 128G 远峰蓝色 国行 全网通",
      "price": 4169,
      "price_CNY": 4169,
      "condition": "mint",
      "description": "电池85%-90% 功能完好",
      "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240409114400336021",
      "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240409114400336021",
      "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240417/RW22G05177/202404171350007487-0-exposure(-5.0)-brightness(20.0)-20240417134958.jpg?x-oss-process=image/resize,s_750",
      "created_date": "2024-04-18T14:04:39.47384Z",
      "scraped_date": "2024-04-18T14:04:39.479795Z",
      "country": "china",
      "state": "china",
      "currency": "CNY",
      "unique_id": "ahs20240409114400336021",
      "category": "mobile",
      "brand": "apple",
      "model": "iphone 13 pro max",
      "spider": "aihuishou",
      "is_test": 0,
      "server": "Vi-Laptop",
      "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
    }
  ]
}
```

**失败测试输入**
不正确的`userSubscriptionId`

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Object reference not set to an instance of an object.",
  "data": null
}
```

---

### GetOneLowestPriceProduct

**方法名称**： `GetOneLowestPriceProduct`

**方法描述**：
1. 获取单个最低价格商品

**请求参数**： 
| 参数名               | 类型                 | 必填 | 描述                 |
| -------------------- | -------------------- | ---- | -------------------- |
| productSearchTermDTO | ProductSearchTermDTO | 是   | 搜索必备参数ElasticSearchDTO |

**返回数据**
| 类型              | 描述              |
| ----------------- | ----------------- |
| ElasticProductDTO | ElasticProductDTO |

**注意事项**

**成功测试输入**

**成功测试输出**

**失败测试输入**

**失败测试输出**

---

### GetProductOver7Days

**方法名称**： `GetProductOver7Days`

**方法描述**：
1. 获取超过7天未更新的数据

**请求参数**： 
| 参数名   | 类型        | 必填 | 描述     |
| -------- | ----------- | ---- | -------- |


**返回数据**
| 类型                      | 描述     |
| ------------------------- | -------- |
| List< ElasticProductDTO > | ElasticSearch产品列表 |

**注意事项**
- 若无数据返回空。

**有数据**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": [
    {
      "title": "Apple 苹果 iPhone 13 Pro Max 256G 远峰蓝色 国行 全网通",
      "price": 5029,
      "price_CNY": 5029,
      "condition": "mint",
      "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
      "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240415133238219510",
      "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240415133238219510",
      "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240417/RW22G04078/202404171849269985-0-exposure(-5.0)-brightness(20.0)-20240417184929.jpg?x-oss-process=image/resize,s_750",
      "created_date": "2024-04-18T14:04:39.47384Z",
      "scraped_date": "2024-04-18T14:04:39.47384Z",
      "country": "china",
      "state": "china",
      "currency": "CNY",
      "unique_id": "ahs20240415133238219510",
      "category": "mobile",
      "brand": "apple",
      "model": "iphone 13 pro max",
      "spider": "aihuishou",
      "is_test": 0,
      "server": "Vi-Laptop",
      "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
    }]
}
    
```

**无数据**
```json
{
  "statusCode": 400,
  "message": "No Product over 7 days",
  "data": null
}
```

---

### DeleteAllProduct

**方法名称**： `DeleteAllProduct`

**方法描述**：
1. 删除所有ElasticSearch内的产品

**请求参数**： 
| 参数名   | 类型        | 必填 | 描述     |
| -------- | ----------- | ---- | -------- |


**返回数据**
| 类型 | 描述                  |
| ---- | --------------------- |
| bool | 删除状态 |

**注意事项**
- 若成功删除返回true，否则为false

**成功测试输入**

**成功测试输出**

**失败测试输入**

**失败测试输出**

---

### RemoveOneProduct

**方法名称**： `RemoveOneProduct`

**方法描述**：
1. 删除一个特定ElasticSearch内的产品

**请求参数**： 
| 参数名    | 类型   | 必填 | 描述           |
| --------- | ------ | ---- | -------------- |
| unique_id | string | 是   | 商品唯一标识符 |


**返回数据**
| 类型 | 描述     |
| ---- | -------- |
| bool | 删除状态 |

**注意事项**
- 若成功删除返回true，否则为false

**成功测试输入**
```json
unique_id : ahs20240405204006377343
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": true
}
```

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "No item ahs12312412412",
  "data": false
}
```

---