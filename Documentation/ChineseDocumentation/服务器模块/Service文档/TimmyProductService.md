# TimmyProductService
| Method Name                                                     | Describe/描述                      | Complete/完成情况 |
| --------------------------------------------------------------- | ---------------------------------- | ----------------- |
| [AddTimmyProduct](#addtimmyproduct)                             | 添加商品进入系统                   | 完成              |
| [RemoveTimmyProduct](#removetimmyproduct)                       | 从系统删除商品                     | 完成              |
| [GetAllAdoptedTimmyProductDict](#getalladoptedtimmyproductdict) | 获取所有商品并返回字典形式         | 完成              |
| [GetAllUnAdoptedTimmyProduct](#getallunadoptedtimmyproductdict) | 获取未被录取的商品并以字典形式返回 | 完成              |
| [AdoptTimmyProduct](#adopttimmyproduct)                         | 录入商品进入系统                   | 完成              |
| [GetCategoryBrandList](#getcategorybrandlists)                  | 获取CategoryBrand列表              | 完成              |
| [GetTimmyProductByName](#gettimmyproductbyname)                                           | 通过商品全名获取Timmy商品          | 完成              |

---

### AddTimmyProduct
**方法名称**： `AddTimmyProduct`

**方法描述**：
1. 添加商品进入系统，将在前端展示商品

**请求参数**： 
| 参数名             | 类型               | 必填 | 描述                 |
| ------------------ | ------------------ | ---- | -------------------- |
| addTimmyProductDTO | AddTimmyProductDTO | 是   | 注册新的Timmy商品DTO |

**返回数据**
 | 类型         | 描述            |
 | ------------ | --------------- |
 | TimmyProduct | 加入的Timmy商品 |

**成功测试输入**
```json
{
  "category": "mobile",
  "brand": "apple",
  "model": "iphone x",
  "subModel": "apple",
  "adopt": 1
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Add TimmyProduct succeed: item model iphone x",
  "data": {
    "timmyProductFullName": "mobile apple iphone x",
    "timmyProductCategory": "mobile",
    "timmyProductBrand": "apple",
    "timmyProductModel": "iphone x",
    "timmyProductSubModel": "apple",
    "timmyProductAdopted": 1,
    "priceHistories": []
  }
}
```

**失败测试输入（数据已存在于数据库 / 输入错误）**

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Error occured in TimmyProductDAO : AddTimmyProduct - Product already exist mobile apple iphone x",
  "data": null
}
```


---

### RemoveTimmyProduct
**方法名称**： `RemoveTimmyProduct`

**方法描述**：
1. 将原有的Timmy商品删除

**请求参数**： 
| 参数名               | 类型   | 必填 | 描述                 |
| -------------------- | ------ | ---- | -------------------- |
| timmyProductFullName | string | 是   | Timmy商品的全名 |

**返回数据**
 | 类型         | 描述               |
 | ------------ | ------------------ |
 | TimmyProduct | 删除的TimmyProduct |

**成功测试输入**
```json
{
  "category": "computer",
  "brand": "apple",
  "model": "imac"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Delete TimmyProduct succeed：computer apple imac",
  "data": {
    "timmyProductFullName": "computer apple imac",
    "timmyProductCategory": "computer",
    "timmyProductBrand": "apple",
    "timmyProductModel": "imac",
    "timmyProductSubModel": "apple",
    "timmyProductAdopted": 1,
    "priceHistories": []
  }
}
```


**失败测试输入（输入数据却缺少或不存在）**


**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "TimmyProduct not exist or wrong input：string",
  "data": null
}
```

---

### GetAllAdoptedTimmyProductDict
**方法名称**： `GetAllAdoptedTimmyProductDict`

**方法描述**：
1. 获取所有已被录取的商品

**请求参数**： 
| 参数名 | 类型 | 必填 | 描述 |
| ------ | ---- | ---- | ---- |
| 无     |      |      |      |

**返回数据**
 | 类型             | 描述 |
 | ---------------- | ---- |
 | TimmyProductData | 一个字典类型，让前端进行解析 |

**成功测试输入**

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": {
    "products": {
      "camera": {
        "canon": {
          "canon": [
            "eos 5d mark iv",
            "eos 6d mark ii",
            "eos r5"
          ]
        },
        "nikon": {
          "nikon": [
            "d3500",
            "d780",
            "z7 ii"
          ]
        }
      },
      "computer": {
        "apple": {
          "apple": [
            "mac mini",
            "macbook air",
            "macbook pro"
          ]
        }
      },
      "mobile": {
        "apple": {
          "apple": [
            "iphone 11",
            "iphone 12",
            "iphone 12 pro",
            "iphone 12 pro max",
            "iphone 13",
            "iphone 13 pro",
            "iphone 13 pro max",
            "iphone 14",
            "iphone 14 pro",
            "iphone 14 pro max",
            "iphone 15",
            "iphone 15 pro",
            "iphone 15 pro max",
            "iphone 6s",
            "iphone 7",
            "iphone 8",
            "iphone se",
            "iphone x",
            "iphone xr"
          ]
        },
        "asus": {
          "asus": [
            "rog",
            "rog phone 5",
            "zenfone 8"
          ]
        },
        "samsung": {
          "samsung": [
            "galaxy a52",
            "galaxy note 20",
            "galaxy s21"
          ]
        }
      },
      "tablet": {
        "apple": {
          "apple": [
            "ipad 7",
            "ipad 8",
            "ipad 9",
            "ipad air 4",
            "ipad air 5",
            "ipad air 6",
            "ipad mini 4",
            "ipad mini 5",
            "ipad mini 6",
            "ipad pro 3",
            "ipad pro 4",
            "ipad pro 5"
          ]
        }
      },
      "watches": {
        "apple": {
          "apple": [
            "watch 6",
            "watch se",
            "watch series 1",
            "watch series 2",
            "watch series 3",
            "watch series 4",
            "watch series 5",
            "watch series 6",
            "watch series 7",
            "watch series 8",
            "watch series 9",
            "watch series se"
          ]
        }
      }
    }
  }
}
```

---

### GetAllUnAdoptedTimmyProductDict
**方法名称**： `GetAllUnAdoptedTimmyProductDict`

**方法描述**：
1. 获取所有已被未录取的商品

**请求参数**： 
| 参数名 | 类型 | 必填 | 描述 |
| ------ | ---- | ---- | ---- |
| 无     |      |      |      |

**返回数据**
 | 类型             | 描述 |
 | ---------------- | ---- |
 | TimmyProductData |      |


**成功测试返回**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": {
    "products": {
      "camera": {
        "olympus": {
          "olympus": [
            "epl2"
          ]
        }
      }
    }
  }
}
```

---

### AdoptTimmyProduct
**方法名称**： `AdoptTimmyProduct`

**方法描述**：
1. 将商品录入，IsAdopted变为1

**请求参数**： 
| 参数名                   | 类型                     | 必填 | 描述 |
| ------------------------ | ------------------------ | ---- | ---- |
| timmyProductEssentialDTO | TimmyProductEssentialDTO | 是   | Timmy商品的主要参数（种类、品牌、型号） |

**返回数据**
 | 类型    | 描述         |
 | ------- | ------------ |
 | boolean | 是否成功录入 |

**成功测试输入**
```json
{
  "category": "camera",
  "brand": "olympus",
  "model": "epl2"
}
```

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Adopt Successfully",
  "data": true
}
```

**失败测试输入**
```json
{
  "category": "camera",
  "brand": "olympus",
  "model": "epl3"
}
```

**失败测试输出**
```json
{
  "statusCode": 400,
  "message": "Adopt Unsuccessfully",
  "data": false
}
```

---

### GetCategoryBrandList
**方法名称**： `GetCategoryBrandList`

**方法描述**：
1. 获取所有的TimmyProduct的Category brand列表，供爬取数据模块使用

**请求参数**： 
| 参数名                   | 类型                     | 必填 | 描述                                    |
| ------------------------ | ------------------------ | ---- | --------------------------------------- |

**返回数据**
 | 类型                     | 描述         |
 | ------------------------ | ------------ |
 | List< CategoryBrandDTO > | CategoryBrandDTO李彪 |

**成功测试输入**

**成功测试输出**
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": [
    {
      "category": "computer",
      "brand": "apple"
    },
    {
      "category": "mobile",
      "brand": "apple"
    },
    {
      "category": "tablet",
      "brand": "apple"
    },
    {
      "category": "watches",
      "brand": "apple"
    },
    {
      "category": "mobile",
      "brand": "asus"
    },
    {
      "category": "camera",
      "brand": "canon"
    },
    {
      "category": "camera",
      "brand": "nikon"
    },
    {
      "category": "camera",
      "brand": "olympus"
    },
    {
      "category": "mobile",
      "brand": "samsung"
    }
  ]
}
```

---

### GetTimmyProductByName
**方法名称**： `GetTimmyProductByName`

**方法描述**：
1. 通过商品全名如： mobile apple iphone 15 pro max，获取该商品在TimmyProduct表的内容

**请求参数**： 
| 参数名   | 类型   | 必填 | 描述                 |
| -------- | ------ | ---- | -------------------- |
| fullName | string | 是   | 商品全名 |

**返回数据**
 | 类型         | 描述            |
 | ------------ | --------------- |
 | TimmyProduct | 查找到的TimmyProduct |

**成功测试输入**
```json
{
  "category": "mobile",
  "brand": "apple",
  "model": "iphone x",
  "subModel": "apple",
  "adopt": 1
}
```