# Basic knowledge for elastic search

---

## CURL command
### set document
`es.index(index=index_name, body=first_document_data, id="2")`
- set a document into index called index_name with data called first_docuemnt_data, and id

### get document
`curl -i -XGET http://localhost:9200/website/blog/124?pretty`

`response = es.get(index="website",id="123",source='title')`
可以只是返回特定的data


**mget 批量获取不同的document（不同的索引参数）**

### check document
`curl -i -XHEAD http://localhost:9200/website/blog/123`

`response = es.exists(index="website",id="123")`

### update document
##### 更新整个文档
有两种更新方式，
1. 如果存在则不更新 `create`
2. 如果存在照样更新 `index`

`es.index(index=index_name, body=first_document_data, id="2", op_type ="create / index")`

##### 更新部分文档
update

### delete document

### bulk

---

## 各种查询语句
包括了选择特定的field，特定的范围等等
https://www.elastic.co/guide/cn/elasticsearch/guide/current/_most_important_queries.html
- match
- match_all
- range
- multi_match（可用在title和description）
- terms （可用在我的spider上面，特定值）
- exists和missing

组合以上查询
https://www.elastic.co/guide/cn/elasticsearch/guide/current/combining-queries-together.html

```
GET /testindex/_search
{
  "query":{
    "bool": {
        "must":     { "match": { "spider": "mudah" }},
        "should": [
            { "range": { "price": { "gte": 3100, "lt":3500 }}}
        ],
    }
  }
}
```

---

## 避免冲突Update
乐观并发控制
1. version来确保修改的文档不会冲突

```
PUT /website/blog/1?version=1 
{
  "title": "My first blog entry",
  "text":  "Starting to get the hang of this..."
}
```

所有文档的**更新**或**删除** API，都可以接受 version 参数，这允许你在代码中使用乐观的并发控制，这是一种明智的做法。

### 还是有另一个方法来解决冲突（如果你的主数据库是外部的）
当外部数据库进行更改时，需要利用timestamp或者version来进行更改
```
PUT /website/blog/2?version=5&version_type=external
{
  "title": "My first external blog entry",
  "text":  "Starting to get the hang of this..."
}
```