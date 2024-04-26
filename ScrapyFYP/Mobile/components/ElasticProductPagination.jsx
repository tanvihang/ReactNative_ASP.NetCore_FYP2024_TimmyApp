import { View, Text, ScrollView, FlatList, ActivityIndicator, SafeAreaView, Alert } from 'react-native'
import { React, useEffect, useState } from 'react'
import useFetch from '../hooks/useFetch'
import normalApiCall from '../hooks/normalApiCall'
import ProductBox from './ProductBox'

const ElasticProductPagination = ({page,header,setPage}) => {

    console.log("Getting random product at page " + page)
    const {data, isLoading, error, refetch} = useFetch("ElasticSearch/GetRandom10Product","POST",
    {
        pageSize: 10,
        currentPage: page
    },{})

    const ExampleData = {
        "count": 10,
    "rows": [
      {
        "title": "Apple 苹果 iPhone 13 Pro Max 256G 远峰蓝色 国行 全网通",
        "price": 4999,
        "price_CNY": 4999,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240412194700216479",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240412194700216479",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240415/RW22G04066/202404151532471582-0-exposure(-5.0)-brightness(20.0)-20240415153253.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:05:15.299717Z",
        "scraped_date": "2024-04-18T14:05:15.299717Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240412194700216479",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 13 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
        "price": 5709,
        "price_CNY": 5709,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240414134659027333",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240414134659027333",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240416/RW22G04078/202404162002217809-0-exposure(-5.0)-brightness(20.0)-20240416200224.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:07:56.694183Z",
        "scraped_date": "2024-04-18T14:07:56.695876Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240414134659027333",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 14 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 14 Pro Max 128G 银色 国行 全网通",
        "price": 5709,
        "price_CNY": 5709,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240324180248150705",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240324180248150705",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240416/DMT0002/20240324180248150705-1-0-2024-04-16-13-25-46-252-DMT0002.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:07:58.816867Z",
        "scraped_date": "2024-04-18T14:07:58.824888Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240324180248150705",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 14 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 14 Pro Max 128G 银色 国行 全网通",
        "price": 6259,
        "price_CNY": 6259,
        "condition": "new",
        "description": "电池90%-95% 外观完好 屏幕完好 功能完好 在保",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240407160326224023",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240407160326224023",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240418/RW22G04066/202404181254047606-0-exposure(-6.0)-brightness(25.0)-20240418125412.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:07:54.91672Z",
        "scraped_date": "2024-04-18T14:07:54.91672Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240407160326224023",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 14 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 13 Pro Max 128G 石墨色 国行 全网通",
        "price": 4859,
        "price_CNY": 4529,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240413201955463977",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240413201955463977",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240415/RW22G05184/202404151725109859-0-exposure(-5.0)-brightness(20.0)-20240415172514.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:04:39.134687Z",
        "scraped_date": "2024-04-18T14:40:57.1152778+08:00",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240413201955463977",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 13 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 14 Pro Max 256G 深空黑色 国行 全网通",
        "price": 6259,
        "price_CNY": 6259,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240408172804392371",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240408172804392371",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240410/RW22G05190/202404101347217606-0-exposure(-6.0)-brightness(25.0)-20240410134732.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:07:55.444933Z",
        "scraped_date": "2024-04-18T14:07:55.448364Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240408172804392371",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 14 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
        "price": 5999,
        "price_CNY": 5999,
        "condition": "new",
        "description": "电池90%-95% 外观完好 屏幕完好 功能完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240414225846426063",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240414225846426063",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240417/RW22G04095/20240417122834746-0-exposure(-5.0)-brightness(20.0)-20240417122835.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:07:56.365644Z",
        "scraped_date": "2024-04-18T14:07:56.370657Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240414225846426063",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 14 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 13 Pro Max 256G 苍岭绿色 国行 全网通",
        "price": 5499,
        "price_CNY": 5169,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240415210018189820",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240415210018189820",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240418/RW22G04044/202404181115098301-0-exposure(-5.0)-brightness(5.0)-20240418111412.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T14:04:39.346663Z",
        "scraped_date": "2024-04-18T14:44:12.0687643+08:00",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240415210018189820",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 13 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple 苹果 iPhone 14 Pro Max 256G 深空黑色 国行 全网通",
        "price": 6649,
        "price_CNY": 6649,
        "condition": "mint",
        "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
        "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240414164501331658",
        "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240414164501331658",
        "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240416/RW22G04074/202404161427562321-0-exposure(-6.0)-brightness(25.0)-20240416142758.jpg?x-oss-process=image/resize,s_750",
        "created_date": "2024-04-18T15:23:19.903795Z",
        "scraped_date": "2024-04-18T15:23:19.905795Z",
        "country": "china",
        "state": "china",
        "currency": "CNY",
        "unique_id": "ahs20240414164501331658",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 14 pro max",
        "spider": "aihuishou",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
      },
      {
        "title": "Apple iphone 14 pro space black 256gb",
        "price": 3699,
        "price_CNY": 5548.5,
        "condition": "used",
        "description": "Apple iphone 14 pro 256gb space black\n\nphone✅\noriginal charging cable✅\noriginal box✅\napple pay✅\ntrue tone✅\nface id✅\nnon telco set✅\nbattery health 88%✅\nworking 100%✅\ntempered glass privacy✅\ntriple camera cover lenses✅\nnego✅\n\ncan check face to face\n\nsama macam beli di kedai🙂\n\ncod cyberjaya/putrajaya",
        "product_url": "https://www.mudah.my/Apple+iphone+14+pro+space+black+256gb-106422169.htm",
        "product_detail_url": "https://www.mudah.my/Apple+iphone+14+pro+space+black+256gb-106422169.htm",
        "product_image": "https://img.rnudah.com/images/28/2874547478112389372.jpg",
        "created_date": "2024-04-18T11:38:28Z",
        "scraped_date": "2024-04-18T14:07:57.08839Z",
        "country": "malaysia",
        "state": "Selangor",
        "currency": "MYR",
        "unique_id": "mudah106422169",
        "category": "mobile",
        "brand": "apple",
        "model": "iphone 14 pro",
        "spider": "mudah",
        "is_test": 0,
        "server": "Vi-Laptop",
        "root_url": "https://search.mudah.my/v1/search?from=0&include=extra_images%2Cbody&limit=40&q=apple+iphone+14+pro&category=3020"
      }
    ]
    }

    const renderLoader = () => {
        return(
            <View className = "my-3">
                <ActivityIndicator size="large" color="#000000"/>
            </View>
        )
    }

    const loadMoreItem = (() => {
        if(isLoading){
            Alert.alert("Still fetching page " + page)
        }
        else{
            setPage(page + 1)
        }
    })


  return (
    <SafeAreaView>
        {
            isLoading? <FlatList
            data = {ExampleData.rows}
            renderItem={({item}) => <ProductBox data={item}/>}
            keyExtractor={item => item.unique_id}
            ListHeaderComponent={header}
            ListFooterComponent={renderLoader}
            onEndReached={loadMoreItem}
            onEndReachedThreshold={0}
            >
            </FlatList>:
            (<FlatList
                data = {ExampleData.rows}
                renderItem={({item}) => <ProductBox data={item}/>}
                keyExtractor={item => item.unique_id}
                ListHeaderComponent={header}
                ListFooterComponent={renderLoader}
                onEndReached={loadMoreItem}
                onEndReachedThreshold={0}
                >
                </FlatList>)
        }

    </SafeAreaView>
  )
}

export default ElasticProductPagination