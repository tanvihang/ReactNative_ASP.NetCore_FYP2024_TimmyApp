import { View, Text, ActivityIndicator, ScrollView, RefreshControl, FlatList } from 'react-native'
import {React, useState, useCallback} from 'react'
import { useGlobalContext } from '../../context/GlobalProvider'
import useFetch from '../../hooks/useFetch'
import SubscribedItem from './SubscribedItem'
import NoSubscribedItem from './NoSubscribedItem'

const SubscribedHeader = () => {

    const {user, jwtToken} = useGlobalContext()

    const {data, isLoading, error, refetch} = useFetch("UserSubscription/GetUserSubscriptions","GET",{},{
        jwtToken:jwtToken
    })


    const [refreshing, setRefreshing] = useState(false);
    const onRefresh = useCallback(()=>{
        setRefreshing(true);
        refetch();
        setRefreshing(false);
    },[]);

  return (

    // Header
    <View className = "flex flex-col justify-center items-center mt-7 mb-5 w-full">
        <Text className = "font-pbold text-5xl pt-5">Subscriptions</Text>

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
                    <NoSubscribedItem/>
                )
                :(
                    <FlatList
                        data = {data?.data}
                        renderItem={({item}) => <SubscribedItem data={item} customStyle="w-full bg-white-100 py-3 my-2"/>}
                        keyExtractor={item => item.userSubscriptionId}
                        className = "mx-3"
                    />
                )
            }
        </ScrollView>
    
    </View>



  )
}

export default SubscribedHeader