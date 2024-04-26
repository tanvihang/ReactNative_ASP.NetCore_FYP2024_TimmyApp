import json
import sys
import chardet

def getEncoding():
    print(sys.getdefaultencoding())

def readfile(fileurl):
    # open file with 'utf-8' encoding
    with open(file=fileurl, mode='r', encoding='utf-8') as file:
        for line in file:
            print(line.strip())

def writefile(fileurl):
    # user_input = input("Enter anything including chinsese and emojis: ")
    user_input = "🐻🐨🐻‍❄️啊啊啊"

    with open(fileurl, mode='w', encoding='utf-8') as file:
        file.write(user_input)
        file.close()

def readJson(fileurl):
    with open(fileurl, mode='r', encoding='utf-8') as file:
        data = json.load(file)
        print(data[0])

# readfile("./LearnEncodeDecode/demotext.txt")
# writefile("./LearnEncodeDecode/demotext2.txt")

readJson("./LearnEncodeDecode/aihuishou.json")