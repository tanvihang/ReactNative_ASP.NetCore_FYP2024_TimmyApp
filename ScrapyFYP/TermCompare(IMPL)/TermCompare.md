# TermCompare
主要流程


### 分类可能出错的问题
1. Title: Apple IPhone 13 128GB Ori Part All --- MostSimilar: iphone 12 
2. Title: Apple iphone 11 128GB mint green --- MostSimilar: iphone 12
3. Title: iphone 11 128gb as new free apple earphone --- MostSimilar: iphone 12 
4. Title: Apple Iphone 15 New |Promo CNY  8-13 February 2024 --- MostSimilar: iphone 13 pro 
5. Title: Apple iPhone 15 Pro 512GB --- MostSimilar: iphone 12 pro 
6. Title: iphone 11 128GB Apple warranty till march --- MostSimilar: iphone 12 
7. Title: Apple IPHONE 12 (256GB) --- MostSimilar: iphone 6 --- Price: 1688
8. Title: NEW UNBOXED APPLE iPHONE 14 256GB STARLIGHT --- MostSimilar: iphone 6 
9. Title: Apple IPhone 13 128GB Ori Part All --- MostSimilar: iphone 8 
10. Title: [USED] Apple iPhone 12 128GB - Macam Baru --- MostSimilar: iphone 8

经过第一次测试只是通过匹配字，可能出错的问题存在于类似上面的
如：型号会与容量相撞（iphone 13 **12**8gb 会误认为为 iphone **12**），（iphone 12 (25**6**GB)误认为为iphone **6**）
以上会发生是因为匹配从上到下，必须在此之前进行判断
```json
"apple": [
      "iphone 15 pro max",
      "iphone 15 pro",
      "iphone 15",
      "iphone 14 pro max",
      "iphone 14 pro",
      "iphone 14",
      "iphone 13 pro max",
      "iphone 13 pro",
      "iphone 13",
      "iphone 12 pro max",
      "iphone 12 pro",
      "iphone 12",
      "iphone 11 pro max",
      "iphone 11 pro",
      "iphone 11",
      "iphone xs pro",
      "iphone xs",
      "iphone x pro",
      "iphone x",
      "iphone xr",
      "iphone 8 plus",
      "iphone 8",
      "iphone 7 plus",
      "iphone 7",
      "iphone 6 plus",
      "iphone 6",
      "iphone se"
    ]
```

解决方法可以是进行合并后也相同，可是这样做不如一早就这么匹配，因为用户可能会在。



---
### 如何不浪费商品（二次分类）
数据库Model设计
| 参数名      | 类型   | 说明                                                    |
| ----------- | ------ | ------------------------------------------------------- |
| category    | string | 商品类型（mobile, tablet, laptop等等）                  |
| brand       | string | 商品品牌（apple, huawei, acer等等）                     |
| model       | string | 商品型号（iphone 12 pro等等）                           |
| sub_category | string | 商品二次类型（ipad, iphone, mate, p），减少不必要的索引 |
| full_name   | string | 商品全名（apple iphone 12 pro） |

如后台种类品牌爬取时，在mobile-apple种类内爬到tablet-apple时（ipad pro）会有再次二次判断，获取apple底下的subCategory可能可以获取到想要的数据。
- 读取可能的数据并保存，方式为key以及array of值
  - 例如："ipad" = [一系列值], "iwatch" = [一系列值]
  - 并保存在 "apple" = { "ipad" : [], "iwatch": []}

这么做就可以尽量保证数据的保存，不去浪费

---

### 商品二阶形式
如：
- acer
  - predator
  - aspire
这么做可以将数据分得更加细致

必要吗，是可以加入进去的，让整个产品界面更加好？
- **重要的还是如何准确的辨认商品并准确的分类**

其他重要的功能，在4月20日之前完成，其余时间需要开发前端了（手机端不一定需要先）
1. 商品定期刷新
2. 商品种类定期爬取
3. 订阅商品定期爬取（订阅的商品爬取制度稍微不同，搜索可以直接加上，但不涉及到这里的判别机制，因为也有可能判别错误）
4. 删除商品
5. 商品比价（目前直接实现价格比对就可以了，返回5个选择给用户。）

---

# 测试商品

测试商品

- 可以先尝试使用cosine similarity，然后设置一个阈值，才使用ld来计算（测试测试看看吧~！~~~）

**可以分开的（已解决）**
      "title": "Apple iPad (9th Gen) Wifi + Cellular",
      "trimmedTitle": "ipad",
1. 应该将 括号去掉， 并将 number和字符分开
（解决，不计算（）进去）

**可通过Cosine Similarity解决**
次序的倒反
      "title": "Apple 10.9 10th Gen WiFi iPad Pink 256GB",
      "trimmedTitle": "10 ipad",
      "predictedModel": "Not in database",
      "realModel": "ipad 10",

pencil -> pen
      "title": "Apple Pencil (2nd Generation)",
      "trimmedTitle": "",
      "predictedModel": "Not in database",
      "realModel": "pen 2",

辨认错误
      "title": "Apple iPhone 13 128GB 1 Month Warranty",
      "trimmedTitle": "iphone 13 1",
      "predictedModel": "iphone 11",
      "realModel": "iphone 13",

辨认错误
      "title": "IPad Pro 11inch (3rd Gen) + Apple Pencil",
      "trimmedTitle": "ipad pro 11 3",
      "predictedModel": "ipad pro 2",
      "realModel": "ipad pro 3",

标题奇怪的
      "title": "Iphone 14 Pro Max + Apple Watch SE",
      "trimmedTitle": "iphone 14 pro max watch se",
      "predictedModel": "Not in database",
      "realModel": "iphone 14 pro max",

**可以尝试二次比较，利用LD进行辨认**
他的结果
Most Similar: ipad air 2
Top 3 most similar: ['ipad air 3', 'ipad air 5', 'ipad air 2']
1. ipad air 3 - 相似度: 0.5773502691896258
2. ipad air 5 - 相似度: 0.8660254037844388
3. ipad air 2 - 相似度: 0.8660254037844388

可能出错的
      "title": "iPad Air 5 256gb w Apple Pencil 2",
      "trimmedTitle": "ipad air 5 2",
      "predictedModel": "ipad air 2",
      "realModel": "ipad air 5",

      "title": "iPad Air 5 256gb Wifi with Apple Pen 2",
      "trimmedTitle": "ipad air 5 pen 2",
      "predictedModel": "Not in database",
      "realModel": "ipad air 5",****

**没办法的，输入标题误导性，不标准**
辨认错误
      "title": "New Apple Iphone 15 Promax | 15 Pro Cash & Ansuran",
      "trimmedTitle": "iphone 15 15 pro",
      "predictedModel": "iphone 11 pro",
      "realModel": "iphone 15 pro max",

可以预先处理，若存在可以将字符拆开 -> promax -> pro max
      "title": "256gb Apple iPhone 12 Promax",
      "trimmedTitle": "iphone 12",
      "predictedModel": "iphone 12",


辨认错误
      "title": "Ipad 5 gen 256 gb wifi + apple pencil 2",
      "trimmedTitle": "ipad 5 2",
      "predictedModel": "ipad 10",
      "realModel": "ipad 5",


---

# 二次比较
对于cosinesim值有相同的



