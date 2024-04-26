# UserControllerApi

| API Name                                          | Describe/描述                         | Complete/完成情况 |
| ------------------------------------------------- | ------------------------------------- | ----------------- |
| [GetCode](#GetCode)                               | Get Email Code / 获取验证码           | 完成              |
| [UserRegister](#userregister)                     | Register / 用户注册                   | 完成              |
| [UserLogin](#userlogin)                           | Login / 用户登录                      | 完成              |
| [UserLogout](#userlogout)                         | Logout / 用户登出                     | *未完成*          |
| [UserForgetPassword](#userforgetpassword)         | Forget Password / 用户忘记密码        | *未完成*          |
| [UserSubscribeItem](#usersubscribeitem)           | Subscribe Item / 用户订阅商品         | 完成              |
| [UserRemoveSubscription](#userremovesubscription) | Remove Subscribed Item / 用户删除订阅 | 完成              |
| [UserBrowseHistory](#userbrowsehistory)           | Get Browse History / 用户游览记录     | *未完成*          |
| [UserSearchHistory](#usersearchhistory)           | Get Search History / 用户搜索记录     | *未完成*          |
| [UserUpgrade](#userupgrade)                       | Upgrade User Level / 用户升级         | *未完成*          |
| [UserEditPublic](#usereditpublic)                 | Edit Public Info / 更改公共信息       | *未完成*          |
| [UserEditPrivate](#usereditprivate)               | Edit Private Info / 更改私人信息      | *未完成*            |




---
### GetCode

**接口地址**：`/api/user/getCode`

**请求方法**：`POST`

**请求参数**：

| 参数名           | 类型   | 必填 | 描述       |
| ---------------- | ------ | ---- | ---------- |
| email            | string | 是   | 邮箱地址     |

**响应数据**：

发送成功时：

```json
200

{
  "userEmail": "sabi@sharklasers.com",
  "verificationCode": "eb69",
  "expirationDate": "2024-03-22T10:19:56.9176863+08:00"
}
```

发送失败时：

```json
400

{
  "Verification not sent properly"
}
```

**注意事项**：

- 密码需符合系统要求的复杂度。
- 邮箱地址和手机号需唯一。

---

### UserRegister
（完成2024/03/21）

**接口地址**：`/api/user/register`

**请求方法**：`POST`

**请求参数**：

| 参数名           | 类型   | 必填 | 描述     |
| ---------------- | ------ | ---- | -------- |
| username         | string | 是   | 用户名   |
| password         | string | 是   | 密码     |
| email            | string | 是   | 邮箱地址 |
| verificationCode | string | 否   | 邮箱验证码   |

**响应数据**：

注册成功时：

```json
{
  "status": "success",
  "message": "注册成功",
  "user_id": "12345" // 用户唯一标识
}
```

注册失败时：

```json
400

{
  Email {email} already exitsts
}
```

**注意事项**：

- 密码需符合系统要求的复杂度。
- 邮箱地址和手机号需唯一。

---

### UserLogin 
(完成2024/03/22)

**接口地址**：`/api/user/login`

**请求方法**：`POST`

**请求参数**：

| 参数名    | 类型   | 必填 | 描述        |
| --------- | ------ | ---- | ----------- |
| userToken | string | 是   | 用户名/邮箱 |
| password  | string | 是   | 密码        |

**响应数据**：

登录成功时（返回令牌）：

```json
200

{
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVXzc1MTcwZWMxLTE3MjUtNDdlMy04ZjQwLTdlYTM5MmUyMDEzNiIsInVuaXF1ZV9uYW1lIjoidHZoMTAwOCIsIm5iZiI6MTcxMTA5MTMzMywiZXhwIjoxNzExMTc3NzMzLCJpYXQiOjE3MTEwOTEzMzN9.1OfZXIX5CwvUtOxviLBOZU1mR-45-jj7p1yPY-yO-RQ
}
```

登录失败时：

```json
400

{
  password incorrect / UserToken do not exist in database
}
```

**注意事项**：

- 登录成功后应返回 JWT 令牌，用于后续请求的认证。

---

### UserLogout

**接口地址**：`/api/user/logout`

**请求方法**：`POST`

**请求参数**：

| 参数名 | 类型 | 必填 | 描述 |
| --- | --- | --- | --- |
| token | string | 是 | JWT 令牌 |

**响应数据**：

登出成功时：

```json
{
  "status": "success",
  "message": "登出成功"
}
```

令牌无效时：

```json
{
  "status": "error",
  "message": "无效的令牌"
}
```

**注意事项**：

- 用户登出时应验证 JWT 令牌的有效性。
- 登出成功后应清除或失效用户令牌。

---

### UserForgetPassword

**接口地址**：`/api/user/forgot_password`

**请求方法**：`POST`

**请求参数**：

| 参数名 | 类型 | 必填 | 描述 |
| --- | --- | --- | --- |
| email | string | 是 | 邮箱地址 |

**响应数据**：

密码重置链接发送成功时：

```json
{
  "status": "success",
  "message": "密码重置链接已发送至您的邮箱，请查收"
}
```

邮箱不存在时：

```json
{
  "status": "error",
  "message": "邮箱地址不存在"
}
```

**注意事项**：

- 系统应发送包含密码重置链接的邮件到用户邮箱。

---

### UserSubscribeItem

**接口地址**：`/api/user/subscription`

**请求方法**：`POST`

**请求参数**：

| 参数名   | 类型   | 必填 | 描述            |
| -------- | ------ | ---- | --------------- |
| jwtToken | string | 是   | 用户唯一JWT令牌 |
| keyword  | string | 是   | 用户订阅产品    |


**响应数据**：

```json
{

}
```

或当订阅失败时：

```json
{

}
```

**注意事项**：

- 订阅时间间隔通过用户的等级设置

---

### UserRemoveSubscription

**接口地址**：`/api/user/removeSubscription`

**请求方法**：`POST`

**请求参数**：

| 参数名           | 类型             | 必填 | 描述            |
| ---------------- | ---------------- | ---- | --------------- |
| jwtToken         | string           | 是   | 用户唯一JWT令牌 |
| userSubscription | UserSubscription | 是   | 订阅商品信息    |


**响应数据**：

```json

```

或当订阅失败时：

```json

```

**注意事项**：

- 订阅时间间隔通过用户的等级设置

---

### UserBrowseHistory

**接口地址**：

- 浏览历史：`/api/user/browsehistory`

**请求方法**：`GET`

**请求参数**：无

**响应数据**：

```json
{

}
```

**注意事项**：

---

### UserSearchHistory

**接口地址**：

- 浏览历史：`/api/user/searchhistory`

**请求方法**：`GET`

**请求参数**：无

**响应数据**：

```json
{

}
```

**注意事项**：

---

### UserUpgrade

**接口地址**：`/api/user/upgrade`

**请求方法**：`POST`

**请求参数**：

| 参数名 | 类型 | 必填 | 描述 |
| --- | --- | --- | --- |
| plan_id | string | 是 | 升级计划 ID |
| payment_method | string | 是 | 支付方式（如：credit_card, paypal） |


**响应数据**：

升级成功时：

```json

```

升级失败时：

```json

```

**注意事项**：

- 升级计划 ID 应为系统提供的有效选项。
- 支付方式需为系统支持的支付手段。
- 升级成功后，应更新用户状态并返回升级详情。

---

### UserEditPublic

**接口地址**：`/api/user/editpublic`

**请求方法**：`POST`

**请求参数**：

| 参数名 | 类型 | 必填 | 描述 |
| --- | --- | --- | --- |



**响应数据**：

升级成功时：

```json

```

升级失败时：

```json

```

**注意事项**：


---
### UserEditPrivate

**接口地址**：`/api/user/editprivate`

**请求方法**：`POST`

**请求参数**：

| 参数名 | 类型 | 必填 | 描述 |
| --- | --- | --- | --- |



**响应数据**：

升级成功时：

```json

```

升级失败时：

```json

```

**注意事项**：

