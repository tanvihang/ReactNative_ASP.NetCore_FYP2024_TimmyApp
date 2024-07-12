import json

class LanguageConverter():
    def __init__(self,category,brand,spider):

        self.original = None
        self.english = None

        with open("./ProductNameConvert.json", "r") as f:
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
        updated = oriStr
        if(len(self.original) > 1):
            for index, ori in enumerate(self.original):
                if ori in oriStr:
                    # replace the value 
                    updated = oriStr.replace(ori, " "+ self.english[index] + " ")
                    oriStr = updated

        # print(updated)
        return updated   
    
# lc = LanguageConverter("mobile","xiaomi","mudah")

# if lc.original is None:
#     print("Is english")

# lc.convertToCorrespondingPlatformLanguage("xiaomi redmi note 10 pro")
# lc.convertToEnglish("小米 红米 note 10 pro")