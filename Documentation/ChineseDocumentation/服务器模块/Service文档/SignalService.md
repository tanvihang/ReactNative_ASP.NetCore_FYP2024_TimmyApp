# Signal Service
| Method Name                            | Describe/描述                                      | Complete/完成情况 |
| -------------------------------------- | -------------------------------------------------- | ----------------- |
| ExecuteSearchBestUserSubscribedProduct | 对于每一个用户订阅商品进行检索获取最符合要求的产品 | 完成              |
| ExecuteScrapeSubscribedProduct         | 进行数据爬取                                       | *未完成*          |
| ExecuteScrapeCategoryBrandProduct      | 爬取种类品牌商品                                   | *未完成*          |
| ExecuteUpdateElasticProduct            | 分流更新产品                                       | *未完成*          |
| ExecuteGetWeeklyLowestPrice            | 每星期获取TimmyProduct最低价格商品                 | *未完成* |

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
