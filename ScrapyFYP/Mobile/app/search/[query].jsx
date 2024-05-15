import { View, Text, SafeAreaView, ActivityIndicator, FlatList } from 'react-native'
import {React, useState, useEffect} from 'react'
import { useGlobalContext } from '../../context/GlobalProvider'
import useFetch from '../../hooks/useFetch'
import ProductBox from '../../components/ProductBox'
import PaginationButton from '../../components/Buttons/PaginationButton'
import { router } from 'expo-router'

const Search = () => {

  const {searchParams, setSearchParams, jwtToken} = useGlobalContext()

  // pagination state
  const [page, setPage] = useState(1)

  // useEffect will refresh the whole screen
  useEffect(()=>{
    console.log("Page changed " + page)

    // 解构赋值
    nextPageDTO = {...searchParams.pageDTO, currentPage:page}
    setSearchParams({...searchParams, pageDTO:nextPageDTO})
    
  },[page])

  useEffect(()=>{
    refetch()
  },[searchParams])

  // TODO send axios request to get search products
  const {data, isLoading, error, refetch} = useFetch("ElasticSearch/SearchProduct","POST",searchParams,{jwtToken: jwtToken})

  // const data = {
  //   "statusCode": 200,
  //   "message": "Success",
  //   "data": {
  //     "count": 10,
  //     "rows": [
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
  //         "price": 5859,
  //         "price_CNY": 5859,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240325133812063533",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240325133812063533",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240326/RW22G05112/202403261619234648-0-exposure(-5.0)-brightness(20.0)-20240326161926.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:29.079214Z",
  //         "scraped_date": "2024-04-26T13:13:29.080213Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240325133812063533",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
  //         "price": 5869,
  //         "price_CNY": 5869,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240401005317115674",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240401005317115674",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240405/RW22G04066/202404052155398602-0-exposure(-5.0)-brightness(20.0)-20240405215545.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:28.518048Z",
  //         "scraped_date": "2024-04-26T13:13:28.519172Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240401005317115674",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
  //         "price": 5939,
  //         "price_CNY": 5939,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240414135940460717",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240414135940460717",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240418/RW22G04044/202404181710136707-0-exposure(-5.0)-brightness(20.0)-20240418170917.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:28.574582Z",
  //         "scraped_date": "2024-04-26T13:13:28.575499Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240414135940460717",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
  //         "price": 5939,
  //         "price_CNY": 5939,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240417191737293757",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240417191737293757",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240420/RW22G04038/202404201558271526-0-exposure(-5.0)-brightness(20.0)-20240420155831.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:52.904193Z",
  //         "scraped_date": "2024-04-26T13:13:52.90526Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240417191737293757",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 深空黑色 国行 全网通",
  //         "price": 5939,
  //         "price_CNY": 5939,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240404090756400832",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240404090756400832",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240407/RW22G05143/202404071406449613-0-exposure(-6.0)-brightness(25.0)-20240407140650.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-18T14:07:56.865527Z",
  //         "scraped_date": "2024-04-18T14:07:56.873065Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240404090756400832",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 0,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 深空黑色 国行 全网通",
  //         "price": 5949,
  //         "price_CNY": 5949,
  //         "condition": "mint",
  //         "description": "电池90%-95% 外观完好 屏幕完好 功能完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240224170737280481",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240224170737280481",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240228/RW22G04067/202402281340003492-0-exposure(-6.0)-brightness(25.0)-20240228133959.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:28.757201Z",
  //         "scraped_date": "2024-04-26T13:13:28.758201Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240224170737280481",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 深空黑色 国行 全网通",
  //         "price": 5959,
  //         "price_CNY": 5959,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240404022608336256",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240404022608336256",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240407/DMT0002/20240404022608336256-1-0-2024-04-07-11-53-20-410-DMT0002.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:28.559783Z",
  //         "scraped_date": "2024-04-26T13:13:28.568583Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240404022608336256",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 银色 国行 全网通",
  //         "price": 5969,
  //         "price_CNY": 5969,
  //         "condition": "mint",
  //         "description": "电池90%-95% 外观完好 屏幕完好 功能完好 在保",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240411153734371298",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240411153734371298",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240420/RW22G05198/2024042021171910189-0-exposure(-6.0)-brightness(25.0)-20240420211723.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:28.923803Z",
  //         "scraped_date": "2024-04-26T13:13:28.925367Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240411153734371298",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
  //         "price": 5979,
  //         "price_CNY": 5979,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好 功能完好",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240418130137412210",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240418130137412210",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240419/DMT0002/20240418130137412210-1-0-2024-04-19-11-09-33-990-DMT0002.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:57.388552Z",
  //         "scraped_date": "2024-04-26T13:13:57.388552Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240418130137412210",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       },
  //       {
  //         "title": "Apple 苹果 iPhone 14 Pro Max 128G 暗紫色 国行 全网通",
  //         "price": 5979,
  //         "price_CNY": 5979,
  //         "condition": "mint",
  //         "description": "电池85%-90% 外观完好 屏幕完好 功能完好 在保",
  //         "product_url": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240417080906318091",
  //         "product_detail_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/goods-detail?productNo=20240417080906318091",
  //         "product_image": "https://ahs-creative.aihuishou.com/photo-cube/20240419/RW22G04044/202404191421479340-0-exposure(-5.0)-brightness(20.0)-20240419142049.jpg?x-oss-process=image/resize,s_750",
  //         "created_date": "2024-04-26T13:13:57.398476Z",
  //         "scraped_date": "2024-04-26T13:13:57.400063Z",
  //         "country": "china",
  //         "state": "china",
  //         "currency": "CNY",
  //         "unique_id": "ahs20240417080906318091",
  //         "category": "mobile",
  //         "brand": "apple",
  //         "model": "iphone 14 pro max",
  //         "spider": "aihuishou",
  //         "is_test": 1,
  //         "server": "Vi-Laptop",
  //         "root_url": "https://dubai.aihuishou.com/dubai-gateway/yanxuan-products/search-goods-v2"
  //       }
  //     ]
  //   }
  // }

  return (
    <SafeAreaView>
      <Text>Search</Text>

      {/* Dev */}
      {
        isLoading ? (
          <ActivityIndicator size='large'></ActivityIndicator>
        ) : error ? (
          <Text>Something went wrong while fetching</Text>
        ) : data.data.length === 0 ? (
          <Text>No product founded</Text>
        ) : (
          <FlatList
            data = {data?.data.rows}
            renderItem = {({item}) => <ProductBox data = {item}/>}
            keyExtractor = {item => item.unique_id}

            ListFooterComponent={<PaginationButton page = {page} setPage = {setPage}/>}
          />
        )
      }
    
      {/* Test */}
      {/* <FlatList
          data = {data?.data.rows}
          renderItem = {({item}) => <ProductBox data = {item}/>}
          keyExtractor = {item => item.unique_id}

          ListFooterComponent={<PaginationButton page = {page} setPage = {setPage}/>}
      /> */}



    </SafeAreaView>
  )
}

export default Search