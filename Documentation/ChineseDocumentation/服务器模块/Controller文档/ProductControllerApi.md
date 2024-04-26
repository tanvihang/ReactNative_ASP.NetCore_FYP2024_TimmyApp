# Product Controller Api
| API Name                                  | Describe/描述                      | Complete/完成情况 |
| ----------------------------------------- | ---------------------------------- | ----------------- |
| [SearchProduct](#searchproduct)           | Search product / 查找商品          | 完成              |
| [FavouriteProduct](#favouriteproduct)     | Favourite Product / 收藏商品       | *未完成*          |
| [UnfavouriteProduct](#unfavouriteproduct) | Unfavourite Product / 取消收藏商品 | *未完成*          |

---


### SearchProduct

**接口地址**：`/api/product/searchproduct`

**请求方法**：`POST`

**请求参数**：

| 参数名               | 类型                 | 必填 | 描述   |
| -------------------- | -------------------- | ---- | ------ |
| elasticProductSearch | ElasticProductSearch | 是   | 查询类 |
| jwtToken             | string               | 是   | 用户jwt令牌 |

**响应数据**：

发送成功时：

```json
200

{

}
```

发送失败时：

```json
400

{

}
```

**注意事项**：

---

### FavouriteProduct

**接口地址**：`/api/products/{productId}/favorite`

**请求方法**：`POST`（收藏） 或 `DELETE`（取消收藏）

**请求参数**（对于 `POST` 请求）：

无需额外参数，通过 URL 中的 `productId` 指定商品。

**响应数据**：

```json
{
  "status": "success",
  "message": "商品已收藏"
}
```

或取消收藏时：

```json
{
  "status": "success",
  "message": "商品已取消收藏"
}
```

**注意事项**：

- 确保商品 ID 正确无误。
---

### UnFavouriteProduct

**接口地址**：`/api/products/{productId}/unfavorite`

**请求方法**：`POST`（收藏） 或 `DELETE`（取消收藏）

**请求参数**（对于 `POST` 请求）：

无需额外参数，通过 URL 中的 `productId` 指定商品。

**响应数据**：

```json
{
  "status": "success",
  "message": "商品已收藏"
}
```

或取消收藏时：

```json
{
  "status": "success",
  "message": "商品已取消收藏"
}
```

**注意事项**：

- 确保商品 ID 正确无误。
---