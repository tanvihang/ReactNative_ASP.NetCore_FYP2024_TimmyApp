# SubscribedProductService
| Method Name                                         | Describe/描述    | Complete/完成情况 |
| --------------------------------------------------- | ---------------- | ----------------- |
| [AddSubscribedProduct](#addsubscribedproduct)       | 订阅商品count +1 | 完成              |
| [RemoveSubscribedProduct](#removesubscribedproduct) | 订阅商品count -1 | 完成              |
| [GetAllSubscribedProduct](#getallsubscribedproduct) | 获取所有订阅商品 | 完成              |

 ---

### AddSubscribedProduct
**方法名称**： `AddSubscribedProduct`

**方法描述**：
1. 添加SubscribedProduct到数据库
2. 如果已有重复商品对其的Count + 1

**请求参数**： 
| 参数名                  | 类型                    | 必填 | 描述        |
| ----------------------- | ----------------------- | ---- | ----------- |
| addSubscribedProductDTO | AddSubscribedProductDTO | 是   | 增加订阅商品DTO |

**返回数据**
| 类型              | 描述               |
| ----------------- | ------------------ |
| SubscribedProduct | 返回添加的订阅商品 |

**注意**
此方法由UserSubscriptionService调用，不直接调用该犯法。

---

### RemoveSubscribedProduct
**方法名称**： `RemoveSubscribedProduct`

**方法描述**：
1. 减少特定SubscribedProduct的Count
2. 若商品的Count为0，将其从数据库删除

**请求参数**： 
| 参数名                     | 类型                       | 必填 | 描述        |
| -------------------------- | -------------------------- | ---- | ----------- |
| removeSubscribedProductDTO | RemoveSubscribedProductDTO | 是   | 删除订阅商品DTO |

**返回数据**
| 类型              | 描述           |
| ----------------- | -------------- |
| SubscribedProduct | 返回删除的订阅商品 |

**注意**
此方法由UserSubscriptionService调用，不直接调用该方法。

---

### GetAllSubscribedProduct
**方法名称**： `GetAllSubscribedProduct`

**方法描述**：
1. 获取所有订阅商品

**请求参数**： 
| 参数名                     | 类型                       | 必填 | 描述        |
| -------------------------- | -------------------------- | ---- | ----------- |

**返回数据**
| 类型                    | 描述             |
| ----------------------- | ---------------- |
| List< SubscribedProduct > | 返回所有订阅商品 |

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": [
    {
      "subscribedProductFullName": "mobile apple iphone 13 pro max",
      "subscribedProductCategory": "mobile",
      "subscribedProductBrand": "apple",
      "subscribedProductModel": "iphone 13 pro max",
      "subscribedProductHighestLevel": 1,
      "subscribedProductCount": 1
    },
    {
      "subscribedProductFullName": "mobile apple iphone 14 pro",
      "subscribedProductCategory": "mobile",
      "subscribedProductBrand": "apple",
      "subscribedProductModel": "iphone 14 pro",
      "subscribedProductHighestLevel": 1,
      "subscribedProductCount": 1
    }
  ]
}
```

**失败测试输出**

---