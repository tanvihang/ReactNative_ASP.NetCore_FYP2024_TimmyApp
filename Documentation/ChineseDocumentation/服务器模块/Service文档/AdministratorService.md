# Administrator Service
| Method Name | Describe/描述 | Complete/完成情况 |
| ----------- | ------------- | ----------------- |
|             |               | 未完成            |


1. 管理员可以管理用户（包括删除用户，查看用户的订阅商品）
2. 管理员可以查看数据的情况（所有产品的数据量，删除某种类的商品，删除某个商品）
3. 管理员可以手动爬取商品、更改基本爬取参数

---
### ExecuteSearchBestUserSubscribedProduct
**方法名称**： `ExecuteSearchBestUserSubscribedProduct`

**方法描述**：
1. 获取UserSubscription列表，对里面的所有数据调用ElasticSearchService的GetLowPriceProductForUserSubscribe获取商品
2. 并通过UserSubscriptionProductService的AddUserSubscriptionProduct将数据写入UserSubscriptionProduct

**请求参数**： 
| 参数名                 | 类型                   | 必填 | 描述             |
| ---------------------- | ---------------------- | ---- | ---------------- |

**返回数据**
 | 类型 | 描述     |
 | ---- | -------- |
 | bool | 更新状态 |
