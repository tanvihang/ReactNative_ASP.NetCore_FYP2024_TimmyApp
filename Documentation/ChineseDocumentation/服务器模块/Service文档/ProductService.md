# Product Service
| Method Name                            | Describe/描述                                                  | Complete/完成情况 |
| -------------------------------------- | -------------------------------------------------------------- | ----------------- |
| [ProductTranslate](#product-translate) | translate product name to two languages / 翻译商品名称去中英文 | *未完成*          |
| [ProductCompare](#product-compare)     | compare new product prices / 比较新数据的价钱                  | *未完成* |

---
### Product Translate
**方法名称**： `ProductTranslate`

**方法描述**：
1. 用户登录的时候为用户生成时效性的JWT令牌

**请求参数**： 
| 参数名        | 类型   | 必填 | 描述           |
| ------------- | ------ | ---- | -------------- |
| productName   | string | 是   | 商品名称（中英文） |

**返回数据**
 | 类型           | 描述                                 |
 | -------------- | ------------------------------------ |
 | List< string > | 两个语言（第一个为英文，第二个中文） |

---
### Product Compare
**方法名称**： `ProductCompare`

**方法描述**：
1. 用户登录的时候为用户生成时效性的JWT令牌

**请求参数**： 
| 参数名         | 类型           | 必填 | 描述     |
| -------------- | -------------- | ---- | -------- |
| subscribedItem | SubscribedItem | 是   | 订阅商品 |

**返回数据**
 | 类型    | 描述                                 |
 | ------- | ------------------------------------ |
 | Boolean | 比价状态（True: 更好， False：保持） |
 