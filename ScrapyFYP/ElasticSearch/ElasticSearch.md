# Elastic Search
Includes configurations and scripts for integrating and managing ElasticSearch for powerful search functionality within the application.

---

## Setup your own Elastic server
Tutorial: https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html

## Mapping of my ElasticSearch to store products

| Name               | Type    |
| ------------------ | ------- |
| brand              | keyword |
| category           | keyword |
| condition          | keyword |
| country            | keyword |
| created_date       | date    |
| currency           | keyword |
| description        | keyword |
| is_test            | keyword |
| model              | keyword |
| price              | float   |
| price_CNY          | keyword |
| product_detail_url | text    |
| product_image      | text    |
| product_url        | text    |
| root_url           | text    |
| scraped_date       | date    |
| server             | text    |
| spider             | text    |
| state              | keyword |
| title              | text    |
| unique_id          | keyword |


```json
{
  "product": {
    "mappings": {
      "properties": {
        "brand": {
          "type": "keyword"
        },
        "category": {
          "type": "keyword"
        },
        "condition": {
          "type": "keyword"
        },
        "country": {
          "type": "keyword"
        },
        "created_date": {
          "type": "date"
        },
        "currency": {
          "type": "keyword"
        },
        "description": {
          "type": "text",
          "analyzer": "ik_max_word"
        },
        "is_test": {
          "type": "keyword"
        },
        "model": {
          "type": "keyword"
        },
        "price": {
          "type": "float"
        },
        "price_CNY": {
          "type": "float"
        },
        "product_detail_url": {
          "type": "text"
        },
        "product_image": {
          "type": "text",
          "index": false
        },
        "product_url": {
          "type": "text"
        },
        "root_url": {
          "type": "text"
        },
        "scraped_date": {
          "type": "date"
        },
        "server": {
          "type": "text",
          "index": false
        },
        "spider": {
          "type": "text"
        },
        "state": {
          "type": "keyword"
        },
        "title": {
          "type": "text",
          "analyzer": "ik_max_word"
        },
        "unique_id": {
          "type": "keyword"
        }
      }
    }
  }
}
```

### Elastic Query To Create the index
```json
PUT product
{
  "mappings": {
    "properties": {
      "title": {
        "type": "text",
        "analyzer": "ik_max_word"
      },
      "price": {
        "type": "float"
      },
      "condition": {
        "type": "keyword"
      },
      "description": {
        "type": "text",
        "analyzer": "ik_max_word"
      },
      "product_url": {
        "type": "text"
      },
      "product_image": {
        "type": "text",
        "index": false
      },
      "currency": {
        "type": "keyword"
      },
      "unique_id": {
        "type": "keyword"
      },
      "category": {
        "type": "keyword"
      },
      "brand": {
        "type": "keyword"
      },
      "model": {
        "type": "keyword"
      },
      "root_url": {
        "type": "text"
      },
      "spider": {
        "type": "text"
      },
      "server": {
        "type": "text",
        "index": false
      },
      "created_date": {
        "type": "date"
      },
      "scraped_date": {
        "type": "date"
      },
      "is_test": {
        "type": "keyword"
      },
      "country": {
        "type": "keyword"
      },
      "state": {
        "type": "keyword"
      }
    }
  }
}

# 更新product的Properties
PUT /product/_mapping
{
  "properties":{
    "product_detail_url":{
      "type": "text"
    }
  }
}
```