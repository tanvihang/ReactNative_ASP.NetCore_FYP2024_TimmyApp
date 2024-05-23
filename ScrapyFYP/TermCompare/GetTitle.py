from ScrapyTermCompare import ScrapyTermCompare 
import json
import time

class GetTitle:
    def __init__(self, fileName, outputFileName, output2, category, brand):
        self.title = {}
        self.filePath = f'./Dataset/{fileName}'
        self.outputPath = f'./Result/{outputFileName}'
        self.outputPath2 = f'./Result/{output2}'



        # Mock the realtime scraping
        # 1. scrape by category brand
        # 2. scrape by category brand model
        self.termCompare = ScrapyTermCompare(category, brand)


    def InitializeEmpty(self):
        self.ExtractTitle()
        self.WriteFile(self.outputPath)

    def Compare(self):

        # read the title
        with open(self.outputPath, "r", encoding="utf-8") as f:
            self.title = json.load(f)


        correct = 0
        # Start the timer here
        start = time.time()

        print(self.title['count'])

        # Calculate for every input
        for i in range(self.title['count']):
            most_similar_product, most_similar_category, cleaned_title, distance = self.termCompare.GetMostSimilarProduct(self.title['data'][i]['title'])
 
            if(most_similar_product != None):
                self.title['data'][i]['trimmedTitle'] = cleaned_title
                self.title['data'][i]['predictedModel'] = most_similar_product
                self.title['data'][i]['category'] = most_similar_category
                self.title['data'][i]['distance'] = distance

                if(most_similar_product == self.title['data'][i]['realModel']):
                    self.title['data'][i]['correct'] = True
                    correct += 1
                else:
                    self.title['data'][i]['correct'] = False

            else:
                self.title['data'][i]['trimmedTitle'] = cleaned_title
                self.title['data'][i]['predictedModel'] = "Not in database"
                self.title['data'][i]['category'] = "Not in database"
                self.title['data'][i]['distance'] = -1
                self.title['data'][i]['correct'] = False

        end = time.time()
        execution_time = end - start

        self.title['correct'] = correct
        self.title['executedTime'] = execution_time

        self.WriteFile(self.outputPath2)

    def ExtractTitle(self):
        with open (self.filePath, "r", encoding="utf-8") as f:
            # get json data
            data = json.load(f)
            print(len(data))

            self.title['count'] = len(data)
            self.title['correct'] = 0
            self.title['executedTime'] = 0

            individualData = []

            for item in data:
                obj = {}
                obj['title'] = item['attributes']['subject']
                obj['trimmedTitle'] = ""
                obj['predictedModel'] = ""
                obj['realModel'] = ""
                obj['category'] = ""
                obj['distance'] = 0

                individualData.append(obj)

            self.title['data'] = individualData


    def WriteFile(self,file):
        with open(file, "w", encoding="utf-8") as f:
            json.dump(self.title,f)
            print("Finish writing")




# gt = GetTitle("mobile_apple.json", "test_set_init.json","test_set_out_cosine_6.json")
gt = GetTitle("mobile_xiaomi_1.json", "test_set_init_xiaomi.json","out_xiaomi_6.json","mobile","xiaomi")

# gt.InitializeEmpty()
gt.Compare()