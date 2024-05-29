import json

category = "mobile"
brand = "xiaomi"


# 将爬回来的数据进行翻译
def convertToEnglish(oriStr, data, language):
    original = data[language]
    english = data["english"]

    if(len(original) > 1):
        for index, ori in enumerate(original):
            if ori in oriStr:
                # replace the value 
                updated = oriStr.replace(ori, " "+ english[index] + " ")
                oriStr = updated

    print(updated)
    return updated    

# 将系统调用的爬取参数转换成该平台利用的语言例如：xiaomi redmi note 10 pro -> 小米 红米 note 10 pro
def convertToCorrespondingPlatformLanguage(enString, data, language):
    original = data[language]
    english = data["english"]
    
    for index, en in enumerate(english):
        if en in enString:
            updated = enString.replace(en, original[index])
            enString = updated

    print(updated)
    return updated

with open("./Utils/ProductNameConvert.json", "r") as f:
    data = json.load(f)
    data = data[category][brand]


# 模拟爬回来的数据进行变换
strWithChinese = "xiaomi 红米 note 10 pro"
print(strWithChinese)
convertToEnglish(strWithChinese, data, "chinese")

# 模拟传去爬对应的平台
strInEnglish = "xiaomi redmi note 10 pro"
print(strInEnglish)
convertToCorrespondingPlatformLanguage(strInEnglish,data,"chinese")