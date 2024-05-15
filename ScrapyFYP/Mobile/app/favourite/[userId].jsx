import { View, Text, RefreshControl, ScrollView, FlatList, ActivityIndicator } from 'react-native'
import {React, useState, useEffect, useCallback} from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import { useLocalSearchParams } from 'expo-router'
import { useGlobalContext } from '../../context/GlobalProvider'
import useFetch from '../../hooks/useFetch'
import ProductBox from '../../components/ProductBox'

const userFavourite = () => {

  const {userid} = useLocalSearchParams()
  const {userFavourite, setuserFavourite, jwtToken} = useGlobalContext()

  const [refreshing, setRefreshing] = useState(false);
  const onRefresh = useCallback(()=>{
      setRefreshing(true);
      refetch();
      setRefreshing(false);
  },[]);

  // Call the get Favourite again 
  console.log("Getting user favourite list and update")
  const {data, isLoading, error, refetch} = useFetch("UserFavourite/GetUserFavourite", "POST", {},{jwtToken:jwtToken})

  return (
    <SafeAreaView>
            {
                isLoading ? (
                    <ActivityIndicator size='large'></ActivityIndicator>
                ) : error ? (
                    <Text>Something went wrong while fetching</Text>
                ) : data.data.length === 0 ? (
                    <Text>No favourited product</Text>
                )
                :(
                  <FlatList
                    data = {data?.data}
                    renderItem={({item}) => <ProductBox data={item}/>}
                    keyExtractor={item => item.unique_id}
                  />
                )
            }
    </SafeAreaView>
  )
}

export default userFavourite