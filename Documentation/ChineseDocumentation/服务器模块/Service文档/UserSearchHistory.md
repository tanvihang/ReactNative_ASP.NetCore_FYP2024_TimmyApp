# User Search History Service
| Method Name           | Describe/描述        | Complete/完成情况 |
| --------------------- | -------------------- | ----------------- |
| SaveUserSearchHistory | 保存用户搜索历史     | 完成              |
| GetUserSearchHistory  | 获取用户搜索历史列表 | 完成              |

---

### SaveUserSearchHistory
**方法名称**： `SaveUserSearchHistory`

**方法描述**：
1. 保存用户的查询历史
2. 如果查询相同的商品将覆盖上次时间   

**请求参数**： 
| 参数名          | 类型    | 必填 | 描述           |
| --------------- | ------- | ---- | -------------- |
| productFullName | string  | 是   | 商品全名       |
| userId          | string  | 是   | 用户唯一标识符 |

**返回数据**
| 类型 | 描述           |
| ---- | -------------- |
| bool | 添加状态 |


---

### GetUserSearchHistory
**方法名称**： `GetUserSearchHistory`

**方法描述**：
1. 获取单个用户的搜索历史

**请求参数**： 
| 参数名          | 类型    | 必填 | 描述           |
| --------------- | ------- | ---- | -------------- |
| userId          | string  | 是   | 用户唯一标识符 |

**返回数据**
| 类型                      | 描述             |
| ------------------------- | ---------------- |
| List< UserSearchHistory > | 用户搜索历史列表 |
