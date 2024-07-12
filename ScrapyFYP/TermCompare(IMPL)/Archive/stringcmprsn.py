import json
from datetime import datetime 

def read_models(category, brand):
    filename = f'./Models/{category}.json'
    with open(filename, 'r', encoding='utf-8') as file:
        jsonData = json.load(file)
        brandDataKey = list(jsonData[brand].keys())[0]

    return jsonData[brand][brandDataKey]

def getSubCategory(category, brand):
    other_files = ["camera","computer","mobile","tablet","watches"]
    other_files.remove(category)
    
    brand_sub = {}

    for files in other_files:
        with open(f'./Models/{files}.json', 'r', encoding='utf-8') as f:
            jsonData = json.load(f)
           
            try:
                hasSub = jsonData[brand]
                # key = list(hasSub.keys())[0]
                # value = hasSub[key]
                brand_sub.update(hasSub)
            
            except:
                print(f'No sub cat for {files}')

    return brand_sub

def gen_keyword_length(keywords):
    keywords_length = []

    for keyword in keywords:
        keyword_length = len(keyword.split(" "))
        keywords_length.append(keyword_length)

    return keywords_length, max(keywords_length)

def check_title_contains_keywords(title, keywords, keywordsLength, maxKeywordLength):
    # 拆分标题为单词列表
    # title_words = title.split()
    # print(f'原标题：{title}')
    most_similar = ""
    current_max = 0
    title = title.lower()

    # 遍历关键字列表，逐个检查是否在标题中出现
    for index, keyword in enumerate(keywords):
        # 如果关键字的所有部分都在标题中出现，则认为匹配成功
        if all(part.lower() in title.lower() for part in keyword.split(" ")):
            if(keywordsLength[index] == maxKeywordLength):
                # print(keywords)
                # print(keywordsLength)
                # print(maxKeywordLength)
                # print(keyword)
                # print(f'最相似商品（完全匹配最大字符串长度）：{keyword}')
                return keyword
            
            if(keywordsLength[index] >= current_max):
                most_similar = keyword
                current_max = keywordsLength[index]
                # print(f'Updated to {most_similar}')

    if(most_similar != ""):
        # print(f'最相似商品：{most_similar}')
        # print(index)
        return most_similar

    # print("没有找到匹配的商品")
    return None
    
def test_data(category, brand):
    datasetFile = f'./Dataset/{category}_{brand}.json'
    date = datetime.now()
    resultFileName = f'./Result/{category}_{brand}.txt'
    subCategory = getSubCategory(category, brand)

    with open (datasetFile, 'r', encoding='utf-8') as file:
        jsonData = json.load(file)
    
    models = read_models(category,brand)
    keywordsLength, maxKeywordLength = gen_keyword_length(models)
    succeed_item = []
    not_succeed_item = []
    other_succeed = []

    for item in jsonData:
        title = item['attributes']['subject']
        title = title.lower()
        price = item['attributes']['price']
        most_similar = check_title_contains_keywords(title, models, keywordsLength, maxKeywordLength)
        flag = 0

        if(most_similar != None):
            succeed_item.append(f'Title: {title} --- MostSimilar: {most_similar} --- Category: {category}')
        else:
            # try other sub category
            for sub in subCategory:
                if(sub in title):
                    keywordsLengtht, maxKeywordLengtht = gen_keyword_length(subCategory[sub])
                    most_similar = check_title_contains_keywords(title,subCategory[sub],keywordsLengtht, maxKeywordLengtht)


                    if(most_similar != None):
                        other_succeed.append(f'Title: {title} --- MostSimilar: {most_similar} --- Category: {sub}')
                        flag = 1
                
                if flag == 1:
                    break

            if(flag == 0):
                not_succeed_item.append(f'Title: {title} --- Price: {price}')



    with open(resultFileName, 'w', encoding='utf-8') as file:
        file.writelines("-------------Succeed Items-------------\n")
        for item in succeed_item:
            file.write(item + '\n')

        file.writelines("-------------Other Category Items-------------\n")
        for item in other_succeed:
            file.write(item + '\n')

        file.writelines("-------------Failed Items-------------\n")
        for item in not_succeed_item:
            file.write(item + '\n')

        file.close()

def test_sub_category():
    return

test_data("mobile","apple")

# print(brandsub['macbook'])
# data = read_models("mobile", "apple")
# print(data)


# brandsub = getSubCategory("mobile","apple")
# keywordsLengtht, maxKeywordLengtht = gen_keyword_length(brandsub['watch'])

# mostsim = check_title_contains_keywords(" Apple Watch Series 9 ", brandsub['watch'],keywordsLengtht,maxKeywordLengtht)
# print(mostsim)