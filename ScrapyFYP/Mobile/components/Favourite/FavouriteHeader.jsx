import { View, Text, Image } from 'react-native'
import React from 'react'
import {images} from '../../constants/index'

const FavouriteHeader = () => {
  return (
    <View className = "flex flex-row items-center mx-3">
        <Text className = " w-2/3 text-4xl text-center font-pextrabold">
            Favourited Product
        </Text>
      <Image
        source={images.favouriteHeart}
        resizeMode = "contain"
        className = " w-1/3 h-40"
      />
    </View>
  )
}

export default FavouriteHeader