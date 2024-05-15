import { View, Text, TouchableOpacity, Image } from 'react-native'
import React from 'react'
import { router } from 'expo-router'



const TouchableBento = ({title, bentoHref, customStyle, image, textStyle}) => {
  return (
    <TouchableOpacity 
        onPress={() =>
        router.push(bentoHref)
    }
    className={`border-2 border-black-200 h-16 focus:bg-slate-300 flex-row px-2 rounded-2xl justify-center items-center w-2/5 ${customStyle} mx-2 my-2`}
    >
        <View className = "flex flex-row justify-center items-center gap-1">
            <Text className="font-bold text-2xl">{title}</Text>
            <Image
                source={image}
                className = "w-8 h-8"
            />
        </View>
    </TouchableOpacity>
  )
}

export default TouchableBento