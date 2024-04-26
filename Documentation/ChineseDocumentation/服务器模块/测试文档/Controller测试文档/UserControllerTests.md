# UserControllerApiTests

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

| 测试用例     | 输入数据                  | 预期结果                   | 实际结果                 | 通过/失败 |
|--------------|--------------------------|----------------------------|--------------------------|-----------|
| 正确邮箱和有效验证码 | CorrectEmail, ValidVerifCode | 返回OkObjectResult，包含UserVerificationCode对象 | OkObjectResult，包含UserVerificationCode对象 | 通过      |
| 无效邮箱和空验证码   | InvalidEmail, ""          | 返回BadRequestObjectResult，包含错误消息     | BadRequestObjectResult，包含错误消息 | 通过      |
