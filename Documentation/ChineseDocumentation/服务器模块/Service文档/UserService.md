# UserService
| Method Name           | Describe/描述 | Complete/完成情况 |
| --------------------- | ------------- | ----------------- |
| [Login](#login)       | 登录          | 完成              |
| [Register](#register) | 注册          | 完成              |
| [ResetPassword](#resetpassword)         | 重置密码      | 完成              |


---

### Login
**方法名称**： `Login`

**方法描述**：
1. 检查数据库匹配正确
2. 返回生成得JWT token

**请求参数**： 
| 参数名       | 类型         | 必填 | 描述        |
| ------------ | ------------ | ---- | ----------- |
| userLoginDTO | UserLoginDTO | 是   | 用户登录DTO |

**返回数据**
 | 类型   | 描述     |
 | ------ | -------- |
 | string | 用户JWT令牌 |

**成功测试输入**
```json
{
  "userToken": "tvh",
  "userPassword": "1111"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Login Success",
  "data": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJVX2NlOWRlOWNkLWMxODMtNDA0ZS05MDBjLWQ3MmRhNGUxN2FhNSIsInVuaXF1ZV9uYW1lIjoidHZoIiwibmJmIjoxNzEzNTI3NjExLCJleHAiOjE3MTQxMzI0MTEsImlhdCI6MTcxMzUyNzYxMX0.dRUlW68iAYZyeHQ1IBFVQJmYEVeDo31_vbUoi-ImyBA"
}
```

**失败测试输入（用户错误/密码错误）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Error occured in UserTDAO : User Login - No User",
  "data": null
}

{
  "statusCode": 400,
  "message": "Error occured in UserTDAO : UserLogin - Password Incorrect",
  "data": null
}
```

---

### Register
**方法名称**： `Register`

**方法描述**：
1. 接收用户注册信息
2. 检测是否可以加入数据库
3. 保存进入数据库

**请求参数**： 
| 参数名          | 类型            | 必填 | 描述        |
| --------------- | --------------- | ---- | ----------- |
| userRegisterDTO | UserRegisterDTO | 是   | 用户注册DTO |

**返回数据**
| 类型    | 描述     |
| ------- | -------- |
| boolean | 注册状态 |

**成功测试输入**
```json
{
  "userName": "tvh1",
  "userPassword": "1111",
  "userEmail": "tvhang6@gmail.com",
  "userPhone": "1234",
  "verificationCode": "193b"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Register Success",
  "data": null
}
```

**失败测试输入（用户/邮箱存在，验证码错误）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Email/Username exist",
  "data": null
}

{
  "statusCode": 400,
  "message": "Verification code incorrect",
  "data": null
}
```

---

### ResetPassword
**方法名称**： `ResetPassword`

**方法描述**：
1. 检查对应验证码是否正确
2. 更新密码

**请求参数**： 
| 参数名           | 类型             | 必填 | 描述   |
| ---------------- | ---------------- | ---- | ------ |
| resetPasswordDTO | ResetPasswordDTO | 是   | 重置密码DTO |

**返回数据**
| 类型    | 描述     |
| ------- | -------- |
| boolean | 更新状态 |

**成功测试输入**
```json
{
  "userEmail": "tvhang6@gmail.com",
  "newPassword": "12345",
  "verificationCode": "193b"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Password reseted",
  "data": null
}
```

**失败测试输入(验证码错误)**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Invalid code",
  "data": null
}
```

---


