import time
from datetime import datetime

def languageConverter():
    return

def CleanTitle():
    return

def CleanTitle1():
    return

def CosineSim():
    return

def LevenstheinFinal():
    return

modelDictionary= []


def GetMostSimilarProduct(title):
    # 将标题小写化
    title = title.lower()
    # 把标题的关键字转换成英文
    title = languageConverter.convertToEnglish(title)
    # 数字与字符分开，去除不存在于字典集合的字
    cleaned_title = CleanTitle(title)
    most_similar_product, distance = CosineSim(cleaned_title)
    # 数字与字符一起，去除不存在于字典集合的字
    cleaned_title1 = CleanTitle1(title)
    most_similar_product1, distance1 = CosineSim(cleaned_title1)

    if(most_similar_product == None and most_similar_product1 == None):
        return None, None,cleaned_title,distance

    # 如果两个长度不一样，用LD进行判断，选更小的
    if(len(most_similar_product) != len(most_similar_product1)):
        arr = []
        arr.append(most_similar_product1)
        arr.append(most_similar_product)
        most_similar_product_ld = LevenstheinFinal(title, arr)
        return most_similar_product_ld, modelDictionary[most_similar_product_ld]
    elif(distance1 > distance):
        return most_similar_product1, modelDictionary[most_similar_product1]
    else:
        return most_similar_product, modelDictionary[most_similar_product]        

def DropItem():
    return 


# 增量式爬取
def process_item(self, item):
    unique_id = str(item['unique_id'][0])
    # 检查布隆过滤器的数组
    hasItem = unique_id in self.bloomFilter
    if hasItem:
        # 再次检查，因为布隆过滤器可能会有假阳性（False Positive）
        elasticProduct = self.es.search(uniqueId=unique_id, index=0)
        if(elasticProduct):
            elastic_price = int(elasticProduct[0]['_source']['price_CNY'])
            item_price = int(item['price_CNY'])
            if(elastic_price != item_price):
                # 检查价格是否有变化
                self.es.delete_one_document(unique_id)
            else:
                # 丢弃该商品
                DropItem()      
    
    # 一系列对商品清理的操作
    self.es.appendProduct(item)
    # 将该数据加入布隆过滤器
    self.bloomFilter.add(unique_id)
    return item


def GetProductOver7Days():
    return

def SendRequest(data):
    return

def RetrievePrice():
    return 

def RemoveOneProduct():
    return

def UpdateProduct():
    return

# 增量式更新
def ExecuteUpdateElasticProduct():
    # 获取超过7天未更新的商品
    productOver7Days = GetProductOver7Days()
    for product in productOver7Days:
        time.sleep(2)
        # 对页面发送请求
        response = SendRequest(product.product_detail_url)
        if response.responseCode == 200:
            newPrice = RetrievePrice(response.responseContentString, product.spider)
            # 如果价格不存在，代表了页面不存在
            if newPrice == 0:
                RemoveOneProduct(product.unique_id)
            # 存在，更新其价格
            else:
                product.price = newPrice
                product.scraped_date = datetime.now()
                # 更新商品
                UpdateProduct(product)
        else:
            # 丢弃商品
            DropItem()
            
            
            