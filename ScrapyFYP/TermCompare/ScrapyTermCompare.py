import Levenshtein
from TimmyDatabase import TimmyDatabase
import re

class ScrapyTermCompare:
    def __init__(self, category, brand):
        self.category = category
        self.brand = brand

        self.timmyDB = TimmyDatabase()
        self.timmyDB.openConnection()

        # 重点最终比对的数据
        self.modelDictionary = {}

        categoryBrandModelsList = self.GetCategoryBrandModelsList(category, brand)
        self.wordDictionary = self.GetListWordDict(categoryBrandModelsList)
        self.UpdateListToDictWithCategory(categoryBrandModelsList, category)

        self.GetOtherCategory(category, brand)

        print(self.modelDictionary)

        self.timmyDB.closeConnection()

    # 获取当前爬取种类品牌底下的型号
    def GetCategoryBrandModelsList(self, category, brand):

        categoryBrandModelsList = self.timmyDB.getProductModel(category, brand)
        return categoryBrandModelsList
    
    def UpdateListToDictWithCategory(self, list, category):
        temp_dict = {}

        for item in list:
            temp_dict[item] = category

        self.modelDictionary.update(temp_dict)

    # 获取同样Models底下的SubModel，除了当前爬取的
    def GetOtherCategory(self, category, brand):

        categoryList = self.timmyDB.getCategories()
        categoryList.remove(category)

        for otherCategory in categoryList:

            # 获取其他种类底下的型号
            categoryBrandModelsList = self.timmyDB.getProductModel(otherCategory, brand)

            # 更新所有商品的字典
            self.UpdateListToDictWithCategory(categoryBrandModelsList, otherCategory)

            # 更新商品word字典
            otherModelsWordDict = self.GetListWordDict(categoryBrandModelsList)
            self.wordDictionary.update(otherModelsWordDict)
        

    # 获取型号的独特字符串
    def GetListWordDict(self, list):

        result_dict = {}

        for word in list:
            words = word.split()

            for w in words:
                if w in result_dict:
                    continue
                else:
                    result_dict[w] = 1

        return result_dict


    # 对标题进行处理
    def CleanTitle(self, title):
        title = re.sub(r'[\u4e00-\u9fff]', '', title)
        split_words = re.findall(r'[a-zA-Z]+|[\d.]+|[^,.\-\s]+', title)

        print(split_words)

        filtered_word = [word for word in split_words if word in self.wordDictionary]

        filtered_string = ' '.join(filtered_word)

        return filtered_string



    # 获取最相近的产品型号
    def GetMostSimilarProduct(self, title):
        title = title.lower()
        cleaned_title = self.CleanTitle(title)
        print(f"Cleaned title: {cleaned_title}")

        most_similar_product = self.Levensthein(cleaned_title)

        if(most_similar_product == None):
            return None, None

        return most_similar_product,self.modelDictionary[most_similar_product]

    # 对标题与型号列表进行比较
    def Levensthein(self, cleaned_title):
        distances = {model: Levenshtein.distance(cleaned_title, model) for model in self.modelDictionary.keys()}
        most_similar_product = min(distances, key=distances.get)
        
        if(distances[most_similar_product] > 3):
            return None

        return most_similar_product

# 示例用法
if __name__ == "__main__":
    # 假设有一个型号列表
    category = "mobile"
    brand = "apple"
    # 创建一个 ScrapyTermCompare 实例
    comparer = ScrapyTermCompare(category, brand)
    # 假设有一个标题
    title = "苹果ipad air4.2"
    # 获取最相近的产品型号
    most_similar_product, most_similar_category = comparer.GetMostSimilarProduct(title)
 
    if(most_similar_product != None):
        print("Most similar product:", most_similar_product)
        print("category:", most_similar_category)
