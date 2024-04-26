# Scraper Service
| Method Name                | Describe/描述            | Complete/完成情况 |
| -------------------------- | ------------------------ | ----------------- |
| ScrapeProduct              | 通过输入爬取参数爬取商品 | 完成              |
| ScrapeSubscribedProduct    | 通过获取订阅商品进行爬取 | 完成              |
| ScrapeCategoryBrandProduct | 通过种类品牌爬取商品     | *未完成* |

---
### ScrapeProduct
**方法名称**： `ScrapeProduct`

**方法描述**：
1. 最基础的爬取商品函数
2. 调用python进行爬取，返回爬取的数据数量
3. 并保存在数据库内

**请求参数**： 
| 参数名                 | 类型                   | 必填 | 描述             |
| ---------------------- | ---------------------- | ---- | ---------------- |
| productScrapeParamsDTO | ProductScrapeParamsDTO | 是   | 爬取商品必须参数 |

**返回数据**
 | 类型 | 描述     |
 | ---- | -------- |
 | bool | 爬取状态 |

---
### ScrapeSubscribedProduct
**方法名称**： `ScrapeProduct`

**方法描述**：
1. 提供爬取等级商品，对所有商品进行循环爬取，调用ScrapeProduct即可

**请求参数**： 
| 参数名                 | 类型                   | 必填 | 描述             |
| ---------------------- | ---------------------- | ---- | ---------------- |
| productScrapeParamsDTO | ProductScrapeParamsDTO | 是   | 爬取商品必须参数 |

**返回数据**
 | 类型 | 描述     |
 | ---- | -------- |
 | bool | 爬取状态 |