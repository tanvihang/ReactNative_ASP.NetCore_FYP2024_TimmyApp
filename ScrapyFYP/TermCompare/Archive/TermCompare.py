import json
import Levenshtein

class TermCompare:
    def __init__(self, category, brand):
        self.category = category
        self.brand = brand

        self.succeed_list = []
        self.failed_list = []
        self.other_list = []

        self.category_brand_list = []
        
        # 所有可能商品集合
        self.other_category_brand_dict = []

        # 其他可能的商品种类型号（macbook, ipad, watch）
        self.sub_category_item_list = []

        # 其他可能的商品种类（computer, tablet, watches）
        self.sub_category = []

        self.generate_category_brand_list()
        self.generate_other_category_brand_dict()
        self.category_brand_word_dict = self.generate_category_brand_word_dict()
        
        print(self.category_brand_word_dict)

        print(self.category_brand_list)
        print("---------------------------------")
        print(self.other_category_brand_dict)
        print(self.sub_category_item_list)
        print(self.sub_category)

        print("---------------------------------")

        pass

    def generate_category_brand_list(self):
        filename = f'./Models/{self.category}.json'
        try:
        # change to read database
            with open(filename, 'r', encoding='utf-8') as f:
                jsonData = json.load(f)
                # read the first value
                category_brand_list_key = list(jsonData[self.brand].keys())[0]
                
                self.category_brand_list = jsonData[self.brand][category_brand_list_key]
        except:
            print(f'Error reading {filename}')
            
    def generate_other_category_brand_dict(self):
        other_files = ["camera","computer","mobile","tablet","watches"]
        other_files.remove(self.category)
        
        self.other_category_brand_dict = {}
        self.sub_category_item_list = []
        self.sub_category = []


        for files in other_files:
            with open(f'./Models/{files}.json', 'r', encoding='utf-8') as f:
                jsonData = json.load(f)
            
                try:
                    hasSub = jsonData[self.brand]
                    keyValue = list(hasSub.keys())[0]
                    self.other_category_brand_dict[files] = hasSub
                    self.sub_category_item_list.append(keyValue)
                    self.sub_category.append(files)
                
                except:
                    print(f'No sub catagory for {files}')

    def generate_category_brand_word_dict(self):
        result_dict = {}

        for word in self.category_brand_list:
            words = word.split()

            for w in words:
                if w in result_dict:
                    continue
                else:
                    result_dict[w] = 1

        return result_dict

    def gen_product_length(self,products):
        products_length = []

        for product in products:
            product_length = len(product.split(" "))
            products_length.append(product_length)

        return products_length, max(products_length)

    def get_most_similar_product(self, title, productList):
        # title = title.lower()
        productsLength, maxProductLength = self.gen_product_length(productList)
        current_max = 0
        most_similar = ""

        # 遍历关键字列表，逐个检查是否在标题中出现
        for index, product in enumerate(productList):
            # 如果关键字的所有部分都在标题中出现，则认为匹配成功
            if all(part.lower() in title.lower() for part in product.split(" ")):
                if(productsLength[index] == maxProductLength):
                    return product
                
                if(productsLength[index] >= current_max):
                    most_similar = product
                    current_max = productsLength[index]

        if(most_similar != ""):
            return most_similar

        return None

    def get_most_similar_product2(self, title, productList):
        #1. clean title
        cleaned_title = self.clean_title(title)

        print(cleaned_title)
        #2. Use Levensthein distance
        most_similar_string = self.levenshtein(cleaned_title, productList)

        return most_similar_string

    def clean_title(self, title):
        split_words = title.split()

        filtered_word = [word for word in split_words if word in self.category_brand_word_dict]

        filtered_string = ' '.join(filtered_word)

        return filtered_string

    def levenshtein(self,title,productList):
        # Calculate Levenshtein distance for each string in the list
        distances = {s: Levenshtein.distance(title, s) for s in productList}

        # Find the string with the lowest distance (highest similarity)
        most_similar_string = min(distances, key=distances.get)

        if(distances[most_similar_string] > 1):
            return None

        return most_similar_string

    def compare_list_product(self, fileToTest):
        
        with open(fileToTest, 'r', encoding='utf-8') as f:
            jsonTestProductList = json.load(f)
        
        for item in jsonTestProductList:
            title = item['attributes']['subject']
            title = title.lower()
            most_similar = self.get_most_similar_product2(title, self.category_brand_list)

            if(most_similar != None):
                self.succeed_list.append(f'Title: {title} --- MostSimilar: {most_similar} --- Category: {self.category}')
            else:
                flag = 0
                for index, subproduct in enumerate(self.sub_category_item_list):
                    if subproduct in title:
                        othercategory = self.sub_category[index]
                        most_similar = self.get_most_similar_product2(title, self.other_category_brand_dict[othercategory][subproduct])

                        if(most_similar != None):
                            self.other_list.append(f'Title: {title} --- MostSimilar: {most_similar} --- Category: {othercategory}')
                            flag = 1
                            break

                if(flag == 0):
                    self.failed_list.append(f'Title: {title}')

        return

    def write_compare_result(self, resultFileName):

        with open(resultFileName, 'w', encoding='utf-8') as file:
            file.writelines("-------------Succeed Items-------------\n")
            for item in self.succeed_list:
                file.write(item + '\n')

            file.writelines("-------------Other Category Items-------------\n")
            for item in self.other_list:
                file.write(item + '\n')

            file.writelines("-------------Failed Items-------------\n")
            for item in self.failed_list:
                file.write(item + '\n')

            file.close()

tc = TermCompare("mobile","apple")
tc.compare_list_product("./Dataset/mobile_apple.json")
tc.write_compare_result("./Result/mobile_apple.txt")