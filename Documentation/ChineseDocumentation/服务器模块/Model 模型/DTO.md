# DTO
| DTO名                      | 描述                 |
| -------------------------- | -------------------- |
| UserLoginDTO               | 用户登录输入参数     |
| UserRegisterDTO            | 用户注册输入参数     |
| ResetPasswordDTO           | 用户重置密码输入参数 |
| ElasticProductDTO          | Elastic商品数据      |
| ProductSearchTermDTO       | 查询商品输入参数     |
| UpdateSubscribedProductDTO | 更新订阅商品参数 |

---

### UserLoginDTO
| 参数名       | 类型   | 描述              |
| ------------ | ------ | ----------------- |
| UserToken    | string | 用户邮箱/用户名称 |
| UserPassword | string | 用户密码 |

---

### UserRegisterDTO
| 参数名           | 类型   | 描述         |
| ---------------- | ------ | ------------ |
| UserName         | string | 用户名称     |
| UserPassword     | string | 用户密码     |
| UserEmail        | string | 用户邮箱     |
| UserPhone        | string | 用户手机号码 |
| VerificationCode | string | 注册验证码   |

---

### ResetPasswordDTO
| 参数名           | 类型   | 描述         |
| ---------------- | ------ | ------------ |
| UserEmail        | string | 用户邮箱     |
| NewPassword      | string | 新密码       |
| VerificationCode | string | 验证码       |

---

### ElasticProductDTO
| 参数名        | 类型     | 描述           |
| ------------- | -------- | -------------- |
| title         | string   | 商品标题       |
| price         | decimal  | 商品价格       |
| condition     | string   | 商品品质       |
| description   | string   | 商品详情       |
| product_url   | string   | 商品链接       |
| product_image | string   | 商品图片链接   |
| created_date  | DateTime | 商品创建时间   |
| country       | string   | 商品国家       |
| state         | string   | 商品省         |
| currency      | string   | 商品汇率       |
| unique_id     | string   | 商品唯一标识符 |
| category      | string   | 商品种类       |
| brand         | string   | 商品品牌       |
| model         | string   | 商品型号       |
| spider        | string   | 商品来源       |
| is_test       | bool     | 商品为测试     |

---

### ProductSearchTermDTO
| 参数名        | 类型     | 描述         |
| ------------- | -------- | ------------ |
| category      | string   | 商品种类     |
| brand         | string   | 商品品牌     |
| model         | string   | 商品型号     |
| description   | string   | 商品区域详情 |
| highest_price | decimal  | 商品最高价格 |
| lowest_price  | decimal  | 商品最低价格 |
| country       | string   | 商品国家     |
| state         | string   | 商品省       |
| condition     | string   | 商品品质     |
| spider        | string[] | 一组商品来源 |

---

### UpdateSubscribedProductDTO
| 参数名        | 类型   | 描述     |
| ------------- | ------ | -------- |
| category      | string | 商品种类 |
| brand         | string | 商品品牌 |
| model         | string | 商品型号 |
| user_level | int    | 用户等级 |
