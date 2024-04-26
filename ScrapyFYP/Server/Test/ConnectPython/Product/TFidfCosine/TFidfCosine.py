from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
import json
import re

def custom_tokenizer(text):
    # 使用正则表达式来分词，保留小数点
    tokens = re.findall(r'\b\w+\.\w+\b|\b\w+\b', text)
    return tokens 

class TFidfCosine:
    def __init__(self,modelList):
        self.modelList = modelList 

        self.vectorizer = TfidfVectorizer(tokenizer=custom_tokenizer)
        self.X = self.vectorizer.fit_transform(self.modelList)

        print("----TFidf----")
        print(self.modelList)
        print("-------------")

    def getMostSimilarModel(self, productTitle):
        cosine_similarities = cosine_similarity(self.X, self.vectorizer.transform([productTitle]))

        most_similar_index = cosine_similarities.argmax()

        if cosine_similarities[most_similar_index] > 0:
            most_similar_model = self.modelList[most_similar_index]
            print("商品名称：", productTitle)
            print("最相似的已知型号：", most_similar_model)
            return most_similar_model
        else:
            print(f'未找到匹配的已知型号: {productTitle}。')
            return None