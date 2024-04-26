import { View, Text, Image } from 'react-native'
import React from 'react'
import { useGlobalContext } from '../context/GlobalProvider'
import {images} from '../constants'
import FormField from '../components/FormField'

const SearchHero = () => {

    const {user} = useGlobalContext()

  return (
    <View className = "flex flex-row justify-between">
        <View>
            <Text className = "font-semibold text-2xl">Search any item {user}</Text>
            <Text className = "font-bold text-6xl">TimmyApp</Text>
            </View>

            <View className="flex items-center justify-center">
            <Image
            source={images.placeHolder}
            className = "w-15 h-15"
            tintColor= "#000000"
            />
        </View>

        
    </View>
  )
}

export default SearchHero