import { View, Text, Image, TouchableOpacity } from 'react-native'
import React, { useState } from 'react'
import { images } from '../../constants'
import { router } from 'expo-router'

const NoSubscriptionProduct = () => {
  const [pressFavourite, setPressFavourite] = useState(false)
  
  const userPressFavourite = () => {
    setPressFavourite(true)
  }

  const userReleasePressFavourite = () => {
    setPressFavourite(false)
    router.replace("/home")
  }

  return (
    <View className = "bg-secondary-100 h-full flex justify-center items-center">

        <Text className = "font-pbold text-7xl text-center pt-5">No Product founded yet.</Text>
        <Text className = " font-pthin">please wait until your notification time to check out the item.</Text>

        <TouchableOpacity
            onPressIn={userPressFavourite}
            onPressOut={userReleasePressFavourite}
            activeOpacity={1}
            className = "h-52 w-1/2 mx-auto"
        >
          <Image
              source={images.sleepingHeart}
              resizeMode="contain"
              className = {` w-full h-full ${pressFavourite ? 'hidden' : 'block'}`}
          />

          <Image
              source={images.workoutHeart}
              resizeMode="contain"
              className = {` w-full h-full ${pressFavourite ? 'block' : 'hidden'}`}
          />

        </TouchableOpacity>

        <Text className = " font-pthin text-3xl mt-10 text-center text-secondary">Browse Items</Text>

    </View>
  )
}

export default NoSubscriptionProduct