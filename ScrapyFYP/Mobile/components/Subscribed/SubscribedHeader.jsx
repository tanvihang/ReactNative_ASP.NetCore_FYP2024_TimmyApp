import { View, Text, ActivityIndicator, ScrollView, RefreshControl, FlatList } from 'react-native'
import {React, useState, useCallback} from 'react'
import { useGlobalContext } from '../../context/GlobalProvider'
import useFetch from '../../hooks/useFetch'
import SubscribedItem from './SubscribedItem'

const SubscribedHeader = () => {

    const {user, jwtToken} = useGlobalContext()
    // TODO api call to get user subscribed product
    const {data, isLoading, error, refetch} = useFetch("UserSubscription/GetUserSubscriptions","GET",{},{
        jwtToken:jwtToken
    })

    // example data
    // const data = {
    //     "statusCode": 200,
    //     "message": "Got user subscription list",
    //     "data": [
    //       {
    //         "userSubscriptionId": "US_23fe11b1-8748-4870-941c-ce2ccc07f3d8",
    //         "userSubscriptionProductFullName": "mobile apple iphone 13 pro max",
    //         "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
    //         "userSubscriptionProductCategory": "mobile",
    //         "userSubscriptionProductBrand": "apple",
    //         "userSubscriptionProductModel": "iphone 13 pro max",
    //         "userSubscriptionProductSubModel": "apple",
    //         "userSubscriptionProductDescription": "128",
    //         "userSubscriptionProductHighestPrice": 8000,
    //         "userSubscriptionProductLowestPrice": 5000,
    //         "userSubscriptionProductCountry": "",
    //         "userSubscriptionProductState": "",
    //         "userSubscriptionProductCondition": "",
    //         "userSubscriptionNotificationMethod": "email",
    //         "userSubscriptionNotificationTime": 16,
    //         "userSubscriptionDate": "2024-04-16T10:17:04.91",
    //         "userSubscriptionPrice": 0,
    //         "userSubscriptionStatus": 0,
    //         "userSubscriptionSpiders": "",
    //         "user": null,
    //         "userSubscriptionProducts": []
    //       },
    //       {
    //         "userSubscriptionId": "US_7e35d2c3-7024-48ac-8065-36b6c756ef5e",
    //         "userSubscriptionProductFullName": "mobile apple iphone 14 pro",
    //         "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
    //         "userSubscriptionProductCategory": "mobile",
    //         "userSubscriptionProductBrand": "apple",
    //         "userSubscriptionProductModel": "iphone 14 pro",
    //         "userSubscriptionProductSubModel": "apple",
    //         "userSubscriptionProductDescription": "256",
    //         "userSubscriptionProductHighestPrice": 10000,
    //         "userSubscriptionProductLowestPrice": 6000,
    //         "userSubscriptionProductCountry": "china",
    //         "userSubscriptionProductState": "china",
    //         "userSubscriptionProductCondition": "",
    //         "userSubscriptionNotificationMethod": "email",
    //         "userSubscriptionNotificationTime": 16,
    //         "userSubscriptionDate": "2024-04-16T16:39:52.59",
    //         "userSubscriptionPrice": 0,
    //         "userSubscriptionStatus": 0,
    //         "userSubscriptionSpiders": "",
    //         "user": null,
    //         "userSubscriptionProducts": []
    //       }
    //     ]
    //   }

    const [refreshing, setRefreshing] = useState(false);
    const onRefresh = useCallback(()=>{
        setRefreshing(true);
        refetch();
        setRefreshing(false);
    },[]);

  return (

    // Header
    <View className = "flex flex-col justify-center items-center mt-10 mb-5 w-full">
        <Text className = "font-bold text-6xl">{user}</Text>
        <Text className = "font-semibold text-2xl">Subscriptions</Text>

        <ScrollView
            refreshControl={
                <RefreshControl refreshing={refreshing} onRefresh={onRefresh}/>
            }
            className = "w-full"
        >
            {
                isLoading ? (
                    <ActivityIndicator size='large'></ActivityIndicator>
                ) : error ? (
                    <Text>Something went wrong while fetching</Text>
                ) : data.data.length === 0 ? (
                    <Text>No subscribed product</Text>
                )
                :(
                    <FlatList
                        data = {data?.data}
                        renderItem={({item}) => <SubscribedItem data={item} customStyle="w-full"/>}
                        keyExtractor={item => item.userSubscriptionId}
                        className = "mx-3"
                    />
                )
            }
        </ScrollView>
    
        {/* <View>
            <FlatList
                data = {data?.data}
                renderItem={({item}) => <SubscribedItem data={item} customStyle="w-full"/>}
                keyExtractor={item => item.userSubscriptionId}
            />
        </View> */}
    </View>



  )
}

export default SubscribedHeader