import { View, Text, RefreshControl, ScrollView, FlatList, ActivityIndicator } from 'react-native'
import {React, useState, useEffect, useCallback} from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import { Stack, useLocalSearchParams } from 'expo-router'
import { useGlobalContext } from '../../context/GlobalProvider'
import useFetch from '../../hooks/useFetch'
import ProductBox from '../../components/ProductBox'
import FavouriteHeader from '../../components/Favourite/FavouriteHeader'
import NoFavourite from '../../components/Favourite/NoFavourite'

const userFavourite = () => {

  const {userFavourite, setuserFavourite, jwtToken} = useGlobalContext()

  const [refreshing, setRefreshing] = useState(false);

  const onRefresh = useCallback(()=>{
      setRefreshing(true);
      refetch();
      setRefreshing(false);
  },[userFavourite]);

  // Call the get Favourite again 
  const {data, isLoading, error, refetch} = useFetch("UserFavourite/GetUserFavourite", "POST", {},{jwtToken:jwtToken})

  return (
    <SafeAreaView className = " bg-primary-100 h-full">
      <Stack.Screen options={{ header: () => null }} />
            {
                isLoading ? (
                    <ActivityIndicator size='large'></ActivityIndicator>
                ) : error ? (
                    <Text>Something went wrong while fetching</Text>
                ) : data.data === null ? (
                    <NoFavourite/>
                )
                :(
                  <FlatList
                    data = {data?.data}
                    renderItem={({item}) => <ProductBox data={item}/>}
                    keyExtractor={item => item.unique_id}
                    ListHeaderComponent={<FavouriteHeader/>}
                    className = "mb-3"
                  />
                )
            }
    </SafeAreaView>
  )
}

export default userFavourite