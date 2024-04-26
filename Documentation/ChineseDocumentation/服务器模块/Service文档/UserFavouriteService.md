# User Favourite Service
| Method Name        | Describe/描述        | Complete/完成情况 |
| ------------------ | -------------------- | ----------------- |
| FavouriteProduct   | 收藏商品             | 完成              |
| UnFavouriteProduct | 取消收藏商品         | 完成              |
| GetUserFavourite   | 获取用户收藏商品列表 | 完成              |

---

### FavouriteProduct
**方法名称**： `FavouriteProduct`

**方法描述**：
1. 保存用户收藏商品

**请求参数**： 
| 参数名          | 类型   | 必填 | 描述           |
| --------------- | ------ | ---- | -------------- |
| userId          | string | 是   | 用户唯一标识符 |
| productUniqueId | string | 是   | 商品唯一标识符    |

**返回数据**
| 类型    | 描述           |
| ------- | -------------- |
| Boolean | 收藏商品状态 |

**成功测试输入**
```json
jwtToken : eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVXzU0ZGNkNTg0LTVlMTMtNGVkNC1hMzk2LTQ1Mjc5ZjNmMmRhYiIsInVuaXF1ZV9uYW1lIjoidHZoMSIsIm5iZiI6MTcxMzUzMDI2NiwiZXhwIjoxNzE0MTM1MDY2LCJpYXQiOjE3MTM1MzAyNjZ9.GGR_y7WsjNFQgwPNECmIek3p8RfvTtlWW5lHpPoxKqM

{
  "productUniqueId": "ahs20240415133238219510"
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

**失败测试输入（错误jwt）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "IDX12729: Unable to decode the header '[PII of type 'System.String' is hidden. For more details, see https://aka.ms/IdentityModel/PII.]' as Base64Url encoded string.",
  "data": false
}
```

---

### UnFavouriteProduct
**方法名称**： `UnFavouriteProduct`

**方法描述**：
1. 保存用户收藏商品

**请求参数**： 
| 参数名          | 类型   | 必填 | 描述           |
| --------------- | ------ | ---- | -------------- |
| userId          | string | 是   | 用户唯一标识符 |
| productUniqueId | string | 是   | 商品唯一标识符    |

**返回数据**
| 类型    | 描述           |
| ------- | -------------- |
| Boolean | 取消收藏商品状态 |

**成功测试输入**
```json
jwtToken : eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVXzU0ZGNkNTg0LTVlMTMtNGVkNC1hMzk2LTQ1Mjc5ZjNmMmRhYiIsInVuaXF1ZV9uYW1lIjoidHZoMSIsIm5iZiI6MTcxMzUzMDI2NiwiZXhwIjoxNzE0MTM1MDY2LCJpYXQiOjE3MTM1MzAyNjZ9.GGR_y7WsjNFQgwPNECmIek3p8RfvTtlWW5lHpPoxKqM

{
  "productUniqueId": "ahs20240415133238219510"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": {
    "productUniqueId": "ahs20240415133238219510",
    "userId": "U_54dcd584-5e13-4ed4-a396-45279f3f2dab",
    "userFavouriteDate": "2024-04-19T20:38:02.833"
  }
}
```

**失败测试输入（商品不存在/jwt令牌错误）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Error occured in UserFavouriteDAO : UnFavouriteProduct - Error occured in UserFavouriteDAO : UnFavouriteProduct - Prduct ahs202404151338219510 not founded for user U_54dcd584-5e13-4ed4-a396-45279f3f2dab",
  "data": null
}

{
  "statusCode": 400,
  "message": "IDX12729: Unable to decode the header '[PII of type 'System.String' is hidden. For more details, see https://aka.ms/IdentityModel/PII.]' as Base64Url encoded string.",
  "data": null
}
```

---

### GetUserFavourite
**方法名称**： `GetUserFavourite`

**方法描述**：
1. 获取用户所有收藏商品

**请求参数**： 
| 参数名          | 类型   | 必填 | 描述           |
| --------------- | ------ | ---- | -------------- |
| userId          | string | 是   | 用户唯一标识符 |

**返回数据**
| 类型                      | 描述     |
| ------------------------- | -------- |
| List< ElasticProductDTO > | 商品列表 |


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

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Error occured in UserSubscriptionDAO : GetUserSubscription - Error occured in UserSubscriptionDAO : GetUserSubscription - User dont have subscription",
  "data": null
}
```

---