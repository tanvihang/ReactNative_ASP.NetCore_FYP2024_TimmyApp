import { View, Text, Image } from 'react-native'
import React from 'react'
import {images} from '../../constants/index'

const FavouriteHeader = () => {
  return (
    <View className = "flex flex-row items-center mx-3">
        <Text className = "flex-grow text-6xl font-bold text-center font-pextrabold">
            Favourited Product
        </Text>
      <Image
        source={images.favouriteHeart}
        resizeMode = "contain"
        className = " w-32 h-32"
      />
    </View>
  )
}

export default FavouriteHeader