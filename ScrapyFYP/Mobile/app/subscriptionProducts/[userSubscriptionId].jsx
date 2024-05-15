import { View, Text, FlatList, ActivityIndicator } from 'react-native'
import React from 'react'
import { useLocalSearchParams } from 'expo-router'
import { SafeAreaView } from 'react-native-safe-area-context';
import SubscribedItemProduct from '../../components/Subscribed/SubscribedItemProduct';
import useFetch from '../../hooks/useFetch';

const UserSubscriptionProduct = () => {

    const{ userSubscriptionId } = useLocalSearchParams();

    const {data, isLoading, error, refetch} = useFetch("UserSubscriptionProduct/GetUserSubscriptionProductsByUserSubscriptionId","POST",{},{userSubscriptionId:userSubscriptionId})

    // TODO change it to real api call, now using example data
    // const data = {
    //     "statusCode": 200,
    //     "message": "Success",
    //     "data": [
    //       {
    //         "userSubscriptionProductId": "USP_0cf0e3ab-2e9b-4aca-b6d3-b6ca9f62d12c",
    //         "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
    //         "userSubscriptionProductCurrency": "CNY",
    //         "userSubscriptionProductAddedDate": "2024-04-20T12:51:15.62",
    //         "userSubscriptionProductUserPreference": 1,
    //         "userSubscriptionProductUrl": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240412142240486235",
    //         "userSubscriptionProductImage": "https://ahs-creative.aihuishou.com/photo-cube/20240416/DMT0003/20240412142240486235-1-0-2024-04-16-15-26-09-030-DMT0003.jpg?x-oss-process=image/resize,s_750",
    //         "userSubscriptionProductUniqueId": "ahs20240412142240486235",
    //         "userSubscriptionProductTitle": "Apple 苹果 iPhone 13 Pro Max 128G 远峰蓝色 国行 全网通",
    //         "userSubscriptionProductDescription": "电池90%-95% 外观完好 屏幕完好 功能完好",
    //         "userSubscriptionProductCondition": "mint",
    //         "userSubscriptionProductSpider": "aihuishou",
    //         "userSubscriptionProductPrice": 4659,
    //         "userSubscriptionProductPriceCny": 4329,
    //         "userSubscription": null
    //       },
    //       {
    //         "userSubscriptionProductId": "USP_18a5e379-6b00-41b5-b747-e9a4c4ef71f2",
    //         "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
    //         "userSubscriptionProductCurrency": "CNY",
    //         "userSubscriptionProductAddedDate": "2024-04-20T12:51:15.593",
    //         "userSubscriptionProductUserPreference": 1,
    //         "userSubscriptionProductUrl": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240408220511234728",
    //         "userSubscriptionProductImage": "https://ahs-creative.aihuishou.com/photo-cube/20240413/RW22G04045/202404131947149039-0-exposure(-5.0)-brightness(20.0)-20240413194720.jpg?x-oss-process=image/resize,s_750",
    //         "userSubscriptionProductUniqueId": "ahs20240408220511234728",
    //         "userSubscriptionProductTitle": "Apple 苹果 iPhone 13 Pro Max 128G 远峰蓝色 国行 全网通",
    //         "userSubscriptionProductDescription": "电池85%-90% 外观完好 屏幕完好",
    //         "userSubscriptionProductCondition": "mint",
    //         "userSubscriptionProductSpider": "aihuishou",
    //         "userSubscriptionProductPrice": 4299,
    //         "userSubscriptionProductPriceCny": 4299,
    //         "userSubscription": null
    //       },
    //       {
    //         "userSubscriptionProductId": "USP_7c3ebc6e-f976-4845-a7ff-26b1441711f6",
    //         "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
    //         "userSubscriptionProductCurrency": "CNY",
    //         "userSubscriptionProductAddedDate": "2024-04-20T12:51:15.537",
    //         "userSubscriptionProductUserPreference": 1,
    //         "userSubscriptionProductUrl": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240414105505225194",
    //         "userSubscriptionProductImage": "https://ahs-creative.aihuishou.com/photo-cube/20240417/RW22G05200/202404171504556996-0-exposure(-5.0)-brightness(20.0)-20240417150501.jpg?x-oss-process=image/resize,s_750",
    //         "userSubscriptionProductUniqueId": "ahs20240414105505225194",
    //         "userSubscriptionProductTitle": "Apple 苹果 iPhone 13 Pro Max 128G 石墨色 国行 全网通",
    //         "userSubscriptionProductDescription": "电池85%-90% 外观完好 屏幕完好",
    //         "userSubscriptionProductCondition": "mint",
    //         "userSubscriptionProductSpider": "aihuishou",
    //         "userSubscriptionProductPrice": 4589,
    //         "userSubscriptionProductPriceCny": 4259,
    //         "userSubscription": null
    //       },
    //       {
    //         "userSubscriptionProductId": "USP_ac653df3-94d0-441e-a5a2-012fc9d84363",
    //         "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
    //         "userSubscriptionProductCurrency": "CNY",
    //         "userSubscriptionProductAddedDate": "2024-04-20T12:51:15.24",
    //         "userSubscriptionProductUserPreference": 1,
    //         "userSubscriptionProductUrl": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240409114400336021",
    //         "userSubscriptionProductImage": "https://ahs-creative.aihuishou.com/photo-cube/20240417/RW22G05177/202404171350007487-0-exposure(-5.0)-brightness(20.0)-20240417134958.jpg?x-oss-process=image/resize,s_750",
    //         "userSubscriptionProductUniqueId": "ahs20240409114400336021",
    //         "userSubscriptionProductTitle": "Apple 苹果 iPhone 13 Pro Max 128G 远峰蓝色 国行 全网通",
    //         "userSubscriptionProductDescription": "电池85%-90% 功能完好",
    //         "userSubscriptionProductCondition": "mint",
    //         "userSubscriptionProductSpider": "aihuishou",
    //         "userSubscriptionProductPrice": 4169,
    //         "userSubscriptionProductPriceCny": 4169,
    //         "userSubscription": null
    //       },
    //       {
    //         "userSubscriptionProductId": "USP_e5f19aff-cd95-4613-8401-de5a07ea4c70",
    //         "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
    //         "userSubscriptionProductCurrency": "CNY",
    //         "userSubscriptionProductAddedDate": "2024-04-20T12:51:15.567",
    //         "userSubscriptionProductUserPreference": 1,
    //         "userSubscriptionProductUrl": "https://m.aihuishou.com/n/ofn/strict-selected/product/detail?productNo=20240411175328153932",
    //         "userSubscriptionProductImage": "https://ahs-creative.aihuishou.com/photo-cube/20240417/RW22G04045/202404171318599918-0-exposure(-5.0)-brightness(20.0)-20240417131907.jpg?x-oss-process=image/resize,s_750",
    //         "userSubscriptionProductUniqueId": "ahs20240411175328153932",
    //         "userSubscriptionProductTitle": "Apple 苹果 iPhone 13 Pro Max 128G 远峰蓝色 国行 全网通",
    //         "userSubscriptionProductDescription": "电池85%-90% 外观完好 屏幕完好",
    //         "userSubscriptionProductCondition": "mint",
    //         "userSubscriptionProductSpider": "aihuishou",
    //         "userSubscriptionProductPrice": 4269,
    //         "userSubscriptionProductPriceCny": 4269,
    //         "userSubscription": null
    //       }
    //     ]
    //   }

  return (
    <SafeAreaView>
        {
          isLoading? (
            <ActivityIndicator size="large"></ActivityIndicator>
          ) : error? (
            <Text>Something went wrong while fetching</Text>
          ) : data.data.length === 0 ? (
            <Text>No favourited product</Text>
          ) : (
            <FlatList
            data = {data?.data}
            renderItem={({item}) => <SubscribedItemProduct data={item} customStyle="" customTextStyle = ""/>}
            keyExtractor={item => item.userSubscriptionProductId}
            className = "mx-3"
        >

        </FlatList>
          )
        }



    </SafeAreaView>

  )
}

export default UserSubscriptionProduct