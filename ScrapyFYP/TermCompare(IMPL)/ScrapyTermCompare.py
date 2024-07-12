import Levenshtein
from TimmyDatabase import TimmyDatabase
import re
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from LanguageConverter import LanguageConverter

class ScrapyTermCompare:
    def __init__(self, category, brand, spider):
        self.category = category
        self.brand = brand

        self.timmyDB = TimmyDatabase()
        self.timmyDB.openConnection()

        # 重点最终比对的数据
        self.modelDictionary = {}

        categoryBrandModelsList = self.GetCategoryBrandModelsList(category, brand)
        self.wordDictionary = self.GetListWordDict(categoryBrandModelsList)
        self.UpdateListToDictWithCategory(categoryBrandModelsList, category)
        # print("Word Dictionary")
        print(self.wordDictionary)
        tempdict = self.wordDictionary.copy()
        # for every word in dictionary take apart the number and character
        for word in tempdict:
            split_words = re.findall(r'[a-zA-Z]+|[\d.]+|[^,.()\-\s]+', word)
            for key in split_words:
                self.wordDictionary[key] = 1

        print(self.wordDictionary)
        # print("Word Dictionary")

        self.GetOtherCategory(category, brand)

        self.corpus = list(self.modelDictionary.keys())

        # 初始化CosineSimilarity
        self.tfidf_vectorizer = TfidfVectorizer(use_idf=False, token_pattern=r"(?u)\b\w+\b")

        # 计算TF-IDF矩阵
        self.tfidf_matrix = self.tfidf_vectorizer.fit_transform(self.corpus)

        print(self.modelDictionary)

        self.timmyDB.closeConnection()
        
        self.languageConverter = LanguageConverter(self.category, self.brand, spider)

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
    def CleanTitle1(self, title):
        title = title.lower()

        # 去除中文字
        title = re.sub(r'[\u4e00-\u9fff]', '', title)

        # 分割字符串，但是数字字符在一起
        # 例如 6s等等
        split_words = re.findall(r'[a-zA-Z0-9]+|[\d.]+|[^,.()\-\s]+', title)

        print(split_words)

        filtered_word = [word for word in split_words if word in self.wordDictionary]

        print(filtered_word)

        filtered_string = ' '.join(filtered_word)

        # print("Filtered: " + filtered_string)
        return filtered_string

    def CleanTitle2(self, title):
        title = title.lower()

        # 去除中文字
        title = re.sub(r'[\u4e00-\u9fff]', '', title)

        # 分割字符串，但是数字字符在一起
        # 例如 6s等等
        split_words = re.findall(r'[a-zA-Z0-9]+|[\d.]+|[^,.()\-\s]+', title)

        # print(split_words)

        # filtered_word = [word for word in split_words if word in self.wordDictionary]

        filtered_string = ' '.join(split_words)

        # print("Filtered: " + filtered_string)
        return  filtered_string
    
    def CleanTitle(self, title):
        title = title.lower()

        # 去除中文字
        title = re.sub(r'[\u4e00-\u9fff]', '', title)

        # 分割字符串
        split_words = re.findall(r'[a-zA-Z]+|[\d.]+|[^,.()\-\s]+', title)

        print(split_words)

        filtered_word = [word for word in split_words if word in self.wordDictionary]

        print(filtered_word)

        filtered_string = ' '.join(filtered_word)

        # print("Filtered: " + filtered_string)
        return filtered_string



    # 获取最相近的产品型号
    def GetMostSimilarProduct(self, title):
        title = title.lower()

        if(self.languageConverter.original is not None):
            title = self.languageConverter.convertToEnglish(title)
            
        print("Converted title")
        print(title)    
            
        cleaned_title1 = self.CleanTitle1(title)
        most_similar_product1, distance1 = self.CosineSim(cleaned_title1)


        cleaned_title = self.CleanTitle(title)
        most_similar_product, distance = self.CosineSim(cleaned_title)

        # if(most_similar_product == None or most_similar_product1 == None):
        if(most_similar_product == "" and most_similar_product1 == ""):
            return None, None,cleaned_title,distance

        # return most_similar_product,self.modelDictionary[most_similar_product],cleaned_title,distance        


        print("Ori")
        print(title)

        print("6s")
        print(f'Cleaned: {cleaned_title1}')
        print(distance1)
        print(f'Most sim: {most_similar_product1}')

        
        print("6 s")
        print(f'Cleaned: {cleaned_title}')
        print(distance)
        print(f'Most sim: {most_similar_product}')

        if(distance < 0.2 and distance1 < 0.2):
            return None, None, "",0
        # 如果两个长度不一样,但是相似度一样，用LD进行判断，选更小的
        elif(len(most_similar_product) != len(most_similar_product1) and (distance == distance1) ):
            arr = []
            arr.append(most_similar_product1)
            arr.append(most_similar_product)
            
            most_similar_product2, distance2 = self.LevenstheinFinal(title, arr)

            if(distance2 > 20):
                return None, None,cleaned_title1,distance2

            return most_similar_product2,self.modelDictionary[most_similar_product2], cleaned_title1, distance2

        elif(distance1 > distance):

            return most_similar_product1,self.modelDictionary[most_similar_product1],cleaned_title1,distance1
        else:

            return most_similar_product,self.modelDictionary[most_similar_product],cleaned_title,distance        

    # 对标题与型号列表进行比较
    def LevenstheinFinal(self, title, decision):
        title = title.replace(" ","")

        distances = {model: Levenshtein.distance(title, model) for model in decision}
        most_similar_product = min(distances, key=distances.get)
        
        print(distances)
        print(f'By LD: {most_similar_product}')

        return most_similar_product, distances[most_similar_product]
    # 对标题与型号列表进行比较
    def Levensthein(self, cleaned_title):

        distances = {model: Levenshtein.distance(cleaned_title, model) for model in self.modelDictionary.keys()}
        most_similar_product = min(distances, key=distances.get)
        
        print(most_similar_product)
        print(distances[most_similar_product])
        if(distances[most_similar_product] > 3):
            return None, distances[most_similar_product]

        return most_similar_product, distances[most_similar_product]
    
    def Nearest(self, cleaned_title, corpus):

        mostSim = 0
        minIndex = 999

        corpus_words = [text.split() for text in corpus]

        # Initialize the index of the first difference to None
        first_different_index = None

        # Iterate through each word index in the first string
        for i in range(len(corpus_words[0])):
            # Get the word at index i in the first string
            word = corpus_words[0][i]
            # Check if the word at index i is different in any other string
            if any(i >= len(text) or word != text[i] for text in corpus_words[1:]):
                first_different_index = i
                break

        # If no difference is found, set the first different index to the length of the shortest word
        if first_different_index is None:
            first_different_index = min(len(word) for word in (corpus_words[0]))


        # After get first different index, see whose is lowest
        for i in range(len(corpus)):
            index = cleaned_title.find(corpus_words[i][first_different_index])

            minIndex = index if index < minIndex else minIndex
            mostSim = i if index<=minIndex else mostSim
            
        return corpus[mostSim]


    def CosineSim(self, cleaned_title):

        # print(f'Cleaned: {cleaned_title}')
        # 将输入文本转换为TF-IDF向量
        input_vector = self.tfidf_vectorizer.transform([cleaned_title])

        # 计算余弦相似度
        similarities = cosine_similarity(input_vector,self.tfidf_matrix)

        # 查找最相似的文本
        most_similar_index = similarities.argmax()
        same_similarity_indices = [i for i, sim in enumerate(similarities[0]) if sim == similarities[0][most_similar_index]]


        # 查找最相似的前几个
        top_indices = similarities.argsort()[0][-3:][::1]
        top_texts = [self.corpus[i] for i in top_indices]
        top_similarities = similarities[0][top_indices]

        most_similar_text = self.corpus[most_similar_index]

        # print(cleaned_title)
        # print(f'Top 3 most similar: {top_texts}' )
        # for i, (text, similarity) in enumerate(zip(top_texts, top_similarities),1):
        #     print(f"{i}. {text} - 相似度: {similarity}")


        # 如果有相同比率的就进行下一步的执行
        if(len(same_similarity_indices) > 1):
            same_similarity_texts = [self.corpus[i] for i in same_similarity_indices]
            item = self.Nearest(cleaned_title, same_similarity_texts)

            # print("Passed 2nd filter")
            # print(f'Most Similar: {item}')

            if(similarities[0][same_similarity_indices[0]] < 0.1):
                return "", 0

            return item, similarities[0][same_similarity_indices[0]]

        else:
            # print("Without second filter")
            # print(f'Most Similar: {most_similar_text}')
            
            if(similarities[0][same_similarity_indices[0]] < 0.1):
                return "", 0
            
            return most_similar_text, similarities[0][most_similar_index]



# 示例用法
if __name__ == "__main__":
    # 假设有一个型号列表
    category = "mobile"
    brand = "asus"
    # 创建一个 ScrapyTermCompare 实例
    comparer = ScrapyTermCompare(category, brand,"aihuishou")

    title = "zenfone 10 pro"

    model,_,_,_ = comparer.GetMostSimilarProduct(title)
    print(f'Result: {model}')
    # comparer.Levensthein(title)
