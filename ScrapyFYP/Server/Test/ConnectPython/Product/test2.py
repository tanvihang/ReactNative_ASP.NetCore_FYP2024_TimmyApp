dictionary = ["iphone 15", "iphone 14", "iphone 13", "iphone 15 pro max"]
string = "IPHONE 15 pro max"

# 去除字符串中的空格
string = string.lower()
string_without_spaces = string.replace(" ", "")


# 去除字典中每个元素的空格
dictionary_without_spaces = [item.replace(" ", "") for item in dictionary]

# 遍历去除空格后的字典中的每个元素，检查它是否是字符串的一部分（考虑去除空格后）
for index, item in enumerate(dictionary_without_spaces):
    if item in string_without_spaces:
        print(f'查询字符串 {string}')
        print("字符串中包含字典中的某个元素（考虑去除空格后）:", dictionary[index] )
        break  # 如果找到了一个匹配的元素，就停止遍历

# 如果没有找到任何匹配的元素
else:
    print("字符串中不包含字典中的任何元素（考虑去除空格后）")
