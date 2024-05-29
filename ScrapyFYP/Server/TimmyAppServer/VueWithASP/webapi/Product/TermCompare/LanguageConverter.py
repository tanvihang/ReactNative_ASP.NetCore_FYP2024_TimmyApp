import json

class LanguageConverter():
    def __init__(self,category,brand,spider):

        self.original = None
        self.english = None

        with open("./Utils/ProductNameConvert.json", "r") as f:
            odata = json.load(f)
            language = odata["platform"][spider]
            
            if(language == "english"):
                return None
            odata = odata[category][brand]
            self.original = odata[language]
            self.english = odata["english"]

    def convertToCorrespondingPlatformLanguage(self, enString):
        updated = ""
        for index, en in enumerate(self.english):
            if en in enString:
                updated = enString.replace(en, self.original[index])
                enString = updated

        # print(updated)
        return updated

    def convertToEnglish(self, oriStr):
        print(oriStr)
        updated = ""
        if(len(self.original) > 1):
            for index, ori in enumerate(self.original):
                if ori in oriStr:
                    # replace the value 
                    updated = oriStr.replace(ori, " "+ self.english[index] + " ")
                    oriStr = updated

            # print(f'Updated string: {updated}')
            return updated
        else:   
            # print("No update: " + oriStr)
            return oriStr
    
lc = LanguageConverter("camera","canon","aihuishou")

if lc.original is None:
    print("Is english")

ori = lc.convertToCorrespondingPlatformLanguage("canon m50 mark ii")
print(ori)
eng = lc.convertToEnglish("佳能m50二代")
print(eng)