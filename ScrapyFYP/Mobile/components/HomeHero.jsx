import { View, Text, Image, TouchableOpacity, SafeAreaView } from 'react-native'
import React from 'react'
import { images, icons } from "../constants"
import { router } from 'expo-router'

const HomeHero = ({user}) => {

    return (
        <SafeAreaView className = "mx-3 mt-7">
            
            {/* Hero welcome */}
            <View className = "flex flex-row justify-between">
              <View>
                <Text className = "font-semibold text-2xl">Welcome Back,</Text>
                <Text className = "font-bold text-6xl">{user}</Text>
              </View>
    
              <View className="flex items-center justify-center">
                <Image
                 source={images.placeHolder}
                 className = "w-15 h-15"
                 tintColor= "#000000"
                />
              </View>
            </View>
    
        {/* Fake search button to guide user to the search section */}
        <TouchableOpacity 
                onPress={() =>
                router.push("/search")
              }
            >
          <View className="border-2 border-black-200 w-full h-16 items-center focus:bg-slate-300 flex-row justify-between px-3 rounded-2xl">
            <Text className="font-bold">Search Product</Text>
            <Image
                 source={icons.search}
                 className = "w-8 h-8 "
                 tintColor= "#000000"
              />
          </View>
        </TouchableOpacity>
    
        {/* Subscribe Item and My Fav hahahha */}
        <View className = " h-40 mt-5 flex flex-row w-full items-center justify-around rounded-2xl">
            <TouchableOpacity 
                onPress={() =>
                router.push("/search")}
                className = "h-40 basis-2/6"
            >
                <View className = "bg-black-100 h-40 flex items-center justify-center rounded-2xl">
                  <Text className = "font-bold text-primary text-5xl italic text-center tracking-wide">Fav Item</Text>
                </View>
            </TouchableOpacity>
    
            <TouchableOpacity 
                onPress={() =>
                router.push("/search")}
                className = "h-40 basis-3/6"
            >
              <View className = "bg-black-100 h-40 flex items-center justify-center rounded-2xl">
                <Text className = "font-bold text-primary text-5xl italic text-center tracking-wider">Go Subscribe Product!</Text>
              </View>
            </TouchableOpacity>
    
        </View>
    
        {/* ElasticProductPagination */}
        {/* <ElasticProductPagination
          page = {page}
        /> */}
    
        {/* <TouchableOpacity 
                onPress={() =>
                setPage(page + 1)}
                className = "h-40 basis-3/6"
            >
              <View className = "bg-black-100 h-40 flex items-center justify-center rounded-2xl">
                <Text className = "font-bold text-primary text-5xl italic text-center tracking-wider">Next Page!</Text>
              </View>
        </TouchableOpacity> */}
    
        </SafeAreaView>
      )
}

export default HomeHero