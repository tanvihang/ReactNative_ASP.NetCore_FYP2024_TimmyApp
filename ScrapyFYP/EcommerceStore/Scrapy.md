# 各个阶段处理事项

### Generalize 对数据进行归一化 2024/3/12
```json
{
    "product":{
        "condition":{
            "new": ["New", "99"],
            "mint": ["New", "95", "9"],
            "used": ["Used","8", "7"]
        }
    }
}
```

```py
def generalize(item):
    
    with open("./Data/converter.json", mode='r') as file1:
        json_data = json.load(file1)

    # generalize the condition
    condition_generalize_dict = json_data['product']['condition']
    flag = 1

    for condition in condition_generalize_dict:

        for condition_atom in condition_generalize_dict[condition]:
            if condition_atom in item['condition']:
                item['condition'] = condition
                flag = 0
                break

    if flag:
        item['condition'] = condition_generalize_dict[len(condition_generalize_dict)-1]

    return item
```

### 批量上传到ElasticSearch 2024/3/12
再closespider的时候关闭并上传

### 调用Scrapy Spider的入口 2024/3/13
应该多样化：包含爬取的数据，使用哪个spider(因为spider可能存在故障)
```
python main.py -a search="itemname" -s mudah aihuishou
```

