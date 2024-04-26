# User Subscription Product Service
| Method Name                   | Describe/描述        | Complete/完成情况 |
| ----------------------------- | -------------------- | ----------------- |
| AddUserSubscriptionProduct    | 添加用户订阅商品产品 | 完成              |
| RemoveUserSubscriptionProduct | 删除用户订阅商品产品 | 完成              |

---

### AddUserSubscriptionProduct
**方法名称**： `AddUserSubscriptionProduct`

**方法描述**：
1. 添加用户订阅商品的物品
2. 若物品大于5个提醒用户去除不要的商品
3. 添加商品进入  

**请求参数**： 
| 参数名                        | 类型                          | 必填 | 描述                    |
| ----------------------------- | ----------------------------- | ---- | ----------------------- |
| addUserSubscriptionProductDTO | AddUserSubscriptionProductDTO | 是   | 添加用户订阅商品产品DTO |

**返回数据**
| 类型                    | 描述           |
| ----------------------- | -------------- |
| UserSubscriptionProduct | 添加的商品产品 |

---

### RemoveUserSubscriptionProduct
**方法名称**： `RemoveUserSubscriptionProducts`

**方法描述**：
1. 删除用户不想要的订阅返回的产品 

**请求参数**： 
| 参数名                    | 类型                             | 必填 | 描述                    |
| ------------------------- | -------------------------------- | ---- | ----------------------- |
| userSubscriptionProductId | RemoveUserSubscriptionProductDTO | 是   | 订阅商品产品唯一标识符 |

**返回数据**
| 类型                    | 描述           |
| ----------------------- | -------------- |
| UserSubscriptionProduct | 删除的商品产品 |