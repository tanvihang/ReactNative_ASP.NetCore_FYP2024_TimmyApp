# User Subscription Service
| Method Name                                                                     | Describe/描述                        | Complete/完成情况 |
| ------------------------------------------------------------------------------- | ------------------------------------ | ----------------- |
| [AddUserSubscription](#addusersubscription)                                     | 添加用户订阅商品                     | 完成              |
| [RemoveUserSubscription](#removeusersubscription)                               | 删除用户订阅商品                     | 完成              |
| [GetUserSubscriptions](#getusersubscriptions)                                   | 获取用户订阅商品                     | 完成              |
| [GetUserSubscriptionBySubscriptionId](#getusersubscriptionbysubscriptionid)     | 通过用户订阅标识符获取用户订阅物品   | 完成·             |
| [GetUserSubscriptionByAboveUserLevel](#getusersubscriptionbyaboveuserlevel)     | 通过用户等级获取用户订阅列表         | 完成              |
| [GetUserSubscriptionByNotificationTime](#getusersubscriptionbynotificationtime) | 通过用户提示订阅时间获取用户订阅列表 | 完成              |

---
### AddUserSubscription
**方法名称**： `AddUserSubscription`

**方法描述**：
1. 获取jwt令牌以及订阅商品参数AddUserSubscriptionDTO
2. 检查是否有相同的订阅以及是否超出订阅数量
3. 返回添加的UserSubscription

**请求参数**： 
| 参数名                 | 类型                   | 必填 | 描述        |
| ---------------------- | ---------------------- | ---- | ----------- |
| addUserSubscriptionDTO | AddUserSubscriptionDTO | 是   | 添加商品DTO |
| userId                 | string                 | 是   | 用户Id      |

**返回数据**
| 类型             | 描述       |
| ---------------- | ---------- |
| UserSubscription | 返回UserSubscription对象 |

**成功测试输入**
```
jwtToken: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVX2NlOWRlOWNkLWMxODMtNDA0ZS05MDBjLWQ3MmRhNGUxN2FhNSIsInVuaXF1ZV9uYW1lIjoidHZoIiwibmJmIjoxNzEzNTg4NzUyLCJleHAiOjE3MTQxOTM1NTIsImlhdCI6MTcxMzU4ODc1Mn0._ciyLRsTvZGHIEvBl4DlgUJJi8Y6T5lEC8LMl-wg580

{
  "subscription_notification_method": "email",
  "subscription_notification_time": 17,
  "category": "mobile",
  "brand": "olympus",
  "model": "epl2",
  "description": "",
  "highest_price": 3000,
  "lowest_price": 0,
  "country": "",
  "state": "",
  "condition": "",
  "spider": [
    ""
  ]
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Successfully added",
  "data": {
    "userSubscriptionId": "US_d68517ea-097d-4f43-a809-e3998b3b08b5",
    "userSubscriptionProductFullName": "mobile olympus epl2",
    "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
    "userSubscriptionProductCategory": "mobile",
    "userSubscriptionProductBrand": "olympus",
    "userSubscriptionProductModel": "epl2",
    "userSubscriptionProductSubModel": "olympus",
    "userSubscriptionProductDescription": "",
    "userSubscriptionProductHighestPrice": 3000,
    "userSubscriptionProductLowestPrice": 0,
    "userSubscriptionProductCountry": "",
    "userSubscriptionProductState": "",
    "userSubscriptionProductCondition": "",
    "userSubscriptionNotificationMethod": "email",
    "userSubscriptionNotificationTime": 17,
    "userSubscriptionDate": "2024-04-20T12:53:41.0163111+08:00",
    "userSubscriptionPrice": 0,
    "userSubscriptionStatus": 0,
    "userSubscriptionSpiders": ""
  }
}
```

**失败测试输入（商品参数错误）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Product not exist in TimmyProduct, cant subscribe: epl2",
  "data": null
}
```

---

### RemoveUserSubscription
**方法名称**： `RemoveUserSubscription`

**方法描述**：
1. 获取jwt令牌以及取消订阅商品参数RemoveUserSubscriptionDTO
2. 返回删除的UserSubscription

**请求参数**： 
| 参数名                    | 类型                      | 必填 | 描述        |
| ------------------------- | ------------------------- | ---- | ----------- |
| removeUserSubscriptionDTO | RemoveUserSubscriptionDTO | 是   | DTO         |
| userId                    | string                    | 是   | 用户Id |

**返回数据**
| 类型             | 描述                     |
| ---------------- | ------------------------ |
| UserSubscription | 返回UserSubscription对象 |

**成功测试输入**
```json
jwtToken: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVX2NlOWRlOWNkLWMxODMtNDA0ZS05MDBjLWQ3MmRhNGUxN2FhNSIsInVuaXF1ZV9uYW1lIjoidHZoIiwibmJmIjoxNzEzNTg4NzUyLCJleHAiOjE3MTQxOTM1NTIsImlhdCI6MTcxMzU4ODc1Mn0._ciyLRsTvZGHIEvBl4DlgUJJi8Y6T5lEC8LMl-wg580

{
  "category": "mobile",
  "brand": "olympus",
  "model": "epl2"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": {
    "userSubscriptionId": "US_d68517ea-097d-4f43-a809-e3998b3b08b5",
    "userSubscriptionProductFullName": "mobile olympus epl2",
    "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
    "userSubscriptionProductCategory": "mobile",
    "userSubscriptionProductBrand": "olympus",
    "userSubscriptionProductModel": "epl2",
    "userSubscriptionProductSubModel": "olympus",
    "userSubscriptionProductDescription": "",
    "userSubscriptionProductHighestPrice": 3000,
    "userSubscriptionProductLowestPrice": 0,
    "userSubscriptionProductCountry": "",
    "userSubscriptionProductState": "",
    "userSubscriptionProductCondition": "",
    "userSubscriptionNotificationMethod": "email",
    "userSubscriptionNotificationTime": 17,
    "userSubscriptionDate": "2024-04-20T12:53:41.017",
    "userSubscriptionPrice": 0,
    "userSubscriptionStatus": 0,
    "userSubscriptionSpiders": ""
  }
}
```

**失败测试输入（用户没有该订阅）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Error occured in UserSubscriptionDAO : RemoveUserSubscription - Error occured in UserSubscriptionDAO : RemoveUserSubscription - User Subscription not found",
  "data": null
}
```

---
### GetUserSubscriptions
**方法名称**： `GetUserSubscriptions`

**方法描述**：
1. 通过用户jwtToken获取用户的订阅商品

**请求参数**： 
| 参数名   | 类型   | 必填 | 描述         |
| -------- | ------ | ---- | ------------ |
| jwtToken | string | 是   | 用户唯一jwt令牌 |

**返回数据**
| 类型                     | 描述     |
| ------------------------ | -------- |
| List< UserSubscription > | 返回列表 |

**成功测试输入（拥有订阅商品的用户jwtToken）**

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Got user subscription list",
  "data": [
    {
      "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
      "userSubscriptionProductFullName": "mobile apple iphone 13 pro max",
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userSubscriptionProductCategory": "mobile",
      "userSubscriptionProductBrand": "apple",
      "userSubscriptionProductModel": "iphone 13 pro max",
      "userSubscriptionProductSubModel": "apple",
      "userSubscriptionProductDescription": "128",
      "userSubscriptionProductHighestPrice": 8000,
      "userSubscriptionProductLowestPrice": 5000,
      "userSubscriptionProductCountry": "",
      "userSubscriptionProductState": "",
      "userSubscriptionProductCondition": "",
      "userSubscriptionNotificationMethod": "email",
      "userSubscriptionNotificationTime": 16,
      "userSubscriptionDate": "2024-04-16T10:17:04.91",
      "userSubscriptionPrice": 0,
      "userSubscriptionStatus": 0,
      "userSubscriptionSpiders": ""
    },
    {
      "userSubscriptionId": "US_7e35d2c3-7024-48ac-8065-36b6c756ef5e",
      "userSubscriptionProductFullName": "mobile apple iphone 14 pro",
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userSubscriptionProductCategory": "mobile",
      "userSubscriptionProductBrand": "apple",
      "userSubscriptionProductModel": "iphone 14 pro",
      "userSubscriptionProductSubModel": "apple",
      "userSubscriptionProductDescription": "256",
      "userSubscriptionProductHighestPrice": 10000,
      "userSubscriptionProductLowestPrice": 6000,
      "userSubscriptionProductCountry": "china",
      "userSubscriptionProductState": "china",
      "userSubscriptionProductCondition": "",
      "userSubscriptionNotificationMethod": "email",
      "userSubscriptionNotificationTime": 16,
      "userSubscriptionDate": "2024-04-16T16:39:52.59",
      "userSubscriptionPrice": 0,
      "userSubscriptionStatus": 0,
      "userSubscriptionSpiders": ""
    },
    {
      "userSubscriptionId": "US_d68517ea-097d-4f43-a809-e3998b3b08b5",
      "userSubscriptionProductFullName": "mobile olympus epl2",
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userSubscriptionProductCategory": "mobile",
      "userSubscriptionProductBrand": "olympus",
      "userSubscriptionProductModel": "epl2",
      "userSubscriptionProductSubModel": "olympus",
      "userSubscriptionProductDescription": "",
      "userSubscriptionProductHighestPrice": 3000,
      "userSubscriptionProductLowestPrice": 0,
      "userSubscriptionProductCountry": "",
      "userSubscriptionProductState": "",
      "userSubscriptionProductCondition": "",
      "userSubscriptionNotificationMethod": "email",
      "userSubscriptionNotificationTime": 17,
      "userSubscriptionDate": "2024-04-20T12:53:41.017",
      "userSubscriptionPrice": 0,
      "userSubscriptionStatus": 0,
      "userSubscriptionSpiders": ""
    }
  ]
}
```

**失败测试输入（没有订阅商品的用户）**

**失败测试输入**
```json
{
  "statusCode": 400,
  "message": "Error occured in UserSubscriptionDAO : GetUserSubscription - Error occured in UserSubscriptionDAO : GetUserSubscription - User dont have subscription",
  "data": null
}
```

---

### GetUserSubscriptionByAboveUserLevel
**方法名称**： `GetUserSubscriptionByAboveUserLevel`

**方法描述**：
1. 获取任何大于输入等级的用户订阅列表
2. 返回用户订阅列表

**请求参数**： 
| 参数名 | 类型   | 必填 | 描述   |
| ------ | ------ | ---- | ------ |
| level  | int    | 是   | 大于用户等级    |

**返回数据**
| 类型                     | 描述     |
| ------------------------ | -------- |
| List< UserSubscription > | 返回列表 |

**成功测试输入（有订阅商品的用户）**

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Got user subscription list above level 1",
  "data": [
    {
      "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
      "userSubscriptionProductFullName": "mobile apple iphone 13 pro max",
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userSubscriptionProductCategory": "mobile",
      "userSubscriptionProductBrand": "apple",
      "userSubscriptionProductModel": "iphone 13 pro max",
      "userSubscriptionProductSubModel": "apple",
      "userSubscriptionProductDescription": "128",
      "userSubscriptionProductHighestPrice": 8000,
      "userSubscriptionProductLowestPrice": 5000,
      "userSubscriptionProductCountry": "",
      "userSubscriptionProductState": "",
      "userSubscriptionProductCondition": "",
      "userSubscriptionNotificationMethod": "email",
      "userSubscriptionNotificationTime": 16,
      "userSubscriptionDate": "2024-04-16T10:17:04.91",
      "userSubscriptionPrice": 0,
      "userSubscriptionStatus": 0,
      "userSubscriptionSpiders": ""
    },
    {
      "userSubscriptionId": "US_7e35d2c3-7024-48ac-8065-36b6c756ef5e",
      "userSubscriptionProductFullName": "mobile apple iphone 14 pro",
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userSubscriptionProductCategory": "mobile",
      "userSubscriptionProductBrand": "apple",
      "userSubscriptionProductModel": "iphone 14 pro",
      "userSubscriptionProductSubModel": "apple",
      "userSubscriptionProductDescription": "256",
      "userSubscriptionProductHighestPrice": 10000,
      "userSubscriptionProductLowestPrice": 6000,
      "userSubscriptionProductCountry": "china",
      "userSubscriptionProductState": "china",
      "userSubscriptionProductCondition": "",
      "userSubscriptionNotificationMethod": "email",
      "userSubscriptionNotificationTime": 16,
      "userSubscriptionDate": "2024-04-16T16:39:52.59",
      "userSubscriptionPrice": 0,
      "userSubscriptionStatus": 0,
      "userSubscriptionSpiders": ""
    }
  ]
}
```

**失败测试输入（没有订阅商品的用户）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Error occured in UserSubscriptionDAO : GetUserSubscriptionByUserLevel - Error occured in UserSubscriptionDAO : GetUserSubscriptionByUserLevel - No user subscription for level above 2",
  "data": null
}
```

---

### GetUserSubscriptionByNotificationTime
**方法名称**： `GetUserSubscriptionByNotificationTime`

**方法描述**：
1. 获取特定订阅时间的订阅商品类

**请求参数**： 
| 参数名 | 类型 | 必填 | 描述                 |
| ------ | ---- | ---- | -------------------- |
| time   | int  | 是   | 哪个小时想要获取信息 |

**返回数据**
| 类型                     | 描述     |
| ------------------------ | -------- |
| List< UserSubscription > | 返回列表 |

**成功测试输入（有订阅商品的用户）**

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Got user subscription list at time 16",
  "data": [
    {
      "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
      "userSubscriptionProductFullName": "mobile apple iphone 13 pro max",
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userSubscriptionProductCategory": "mobile",
      "userSubscriptionProductBrand": "apple",
      "userSubscriptionProductModel": "iphone 13 pro max",
      "userSubscriptionProductSubModel": "apple",
      "userSubscriptionProductDescription": "128",
      "userSubscriptionProductHighestPrice": 8000,
      "userSubscriptionProductLowestPrice": 5000,
      "userSubscriptionProductCountry": "",
      "userSubscriptionProductState": "",
      "userSubscriptionProductCondition": "",
      "userSubscriptionNotificationMethod": "email",
      "userSubscriptionNotificationTime": 16,
      "userSubscriptionDate": "2024-04-16T10:17:04.91",
      "userSubscriptionPrice": 0,
      "userSubscriptionStatus": 0,
      "userSubscriptionSpiders": ""
    },
    {
      "userSubscriptionId": "US_7e35d2c3-7024-48ac-8065-36b6c756ef5e",
      "userSubscriptionProductFullName": "mobile apple iphone 14 pro",
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userSubscriptionProductCategory": "mobile",
      "userSubscriptionProductBrand": "apple",
      "userSubscriptionProductModel": "iphone 14 pro",
      "userSubscriptionProductSubModel": "apple",
      "userSubscriptionProductDescription": "256",
      "userSubscriptionProductHighestPrice": 10000,
      "userSubscriptionProductLowestPrice": 6000,
      "userSubscriptionProductCountry": "china",
      "userSubscriptionProductState": "china",
      "userSubscriptionProductCondition": "",
      "userSubscriptionNotificationMethod": "email",
      "userSubscriptionNotificationTime": 16,
      "userSubscriptionDate": "2024-04-16T16:39:52.59",
      "userSubscriptionPrice": 0,
      "userSubscriptionStatus": 0,
      "userSubscriptionSpiders": ""
    }
  ]
}
```

**失败测试输入（没有订阅商品的用户）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Error occured in UserSubscriptionDAO : GetUserSubscriptionByNotificationTime - Error occured in UserSubscriptionDAO : GetUserSubscriptionByNotificationTime - No user subscription for time 11",
  "data": null
}
```

---

### GetUserSubscriptionBySubscriptionId
**方法名称**： `GetUserSubscriptionBySubscriptionId`

**方法描述**：
1. 通过用户订阅唯一标识符获取用户订阅对象

**请求参数**： 
| 参数名             | 类型   | 必填 | 描述                 |
| ------------------ | ------ | ---- | -------------------- |
| userSubscriptionId | string | 是   | 用户订阅商品的唯一标识符 |

**返回数据**
| 类型                     | 描述             |
| ------------------------ | ---------------- |
| UserSubscription | 返回用户订阅对象 |

**成功测试输入（正确的用户订阅商品唯一标识符）**

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Got user subscription: US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
  "data": {
    "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
    "userSubscriptionProductFullName": "mobile apple iphone 13 pro max",
    "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
    "userSubscriptionProductCategory": "mobile",
    "userSubscriptionProductBrand": "apple",
    "userSubscriptionProductModel": "iphone 13 pro max",
    "userSubscriptionProductSubModel": "apple",
    "userSubscriptionProductDescription": "128",
    "userSubscriptionProductHighestPrice": 8000,
    "userSubscriptionProductLowestPrice": 5000,
    "userSubscriptionProductCountry": "",
    "userSubscriptionProductState": "",
    "userSubscriptionProductCondition": "",
    "userSubscriptionNotificationMethod": "email",
    "userSubscriptionNotificationTime": 16,
    "userSubscriptionDate": "2024-04-16T10:17:04.91",
    "userSubscriptionPrice": 0,
    "userSubscriptionStatus": 0,
    "userSubscriptionSpiders": ""
  }
}
```

**失败测试输入（错误的用户订阅商品唯一标识符）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "No user subscription id: US_23fe11b1-8748-4870-941c-ce2ccf3d8",
  "data": null
}
```