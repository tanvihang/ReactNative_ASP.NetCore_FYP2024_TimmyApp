import { View, Text, TouchableOpacity, Image } from 'react-native'
import React from 'react'
import {images} from '../constants'

const ProductBox = ({data}) => {
  return (
    <View>

      <TouchableOpacity 
            onPress={() =>
            {}}
            className = "h-40 mt-3 mx-3"
        >

            <View className = "bg-black-100 h-40 rounded-2xl flex flex-row items-center justify-between px-7">
               
                {/* Product information */}
                <View className = "text-white basis-3/6 flex flex-col gap-1">
                    <Text className = "text-white">{data.title}</Text>
                    
                    <View className = "flex flex-row">
                        <Text className = "text-white">RMB {data.price_CNY} - </Text>
                        <Text className = "text-white">{data.condition}</Text>
                    </View>

                    <Image
                        source={data.spider === "aihuishou" ? images.aihuishou : images.mudah}
                        resizeMode='contain'
                        className = 'w-8 h-8'
                    />
                </View>

                {/* Product Image */}
                <Image
                    src={data.product_image}
                    resizeMode='contain'
                    className = 'w-32 h-32 rounded-2xl'
                />

            </View>

            

        </TouchableOpacity>
    </View>
  )
}

export default ProductBox