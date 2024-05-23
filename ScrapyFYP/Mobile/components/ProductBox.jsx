import { View, Text, TouchableOpacity, Image, Alert } from 'react-native'
import React from 'react'
import {images, icons} from '../constants'
import * as Linking from 'expo-linking'
import FavouriteButton from './Favourite/FavouriteButton'

const ProductBox = ({data}) => {


  return (
    <View>

      <TouchableOpacity 
            onPress={() =>
            {Linking.openURL(data.product_url)}}
            className = "h-40 mt-3 mx-3 bg-white-100 rounded-2xl"
        >

            <View className = "border-black-200 focus:bg-slate-300 h-40 rounded-2xl flex flex-row items-center justify-between px-7 ">
               
                {/* Product information */}
                <View className = " basis-3/6 flex flex-col gap-1">
                    <Text className = " font-pblack text-base">{data.title}</Text>
                    
                    <View className = "flex flex-row">
                        <Text className = " font-pmedium">RMB {data.price_CNY} - </Text>
                        <Text className = " font-pmedium text-secondary">{data.condition}</Text>
                    </View>

                    <View className = "flex flex-row">
                        <Image
                            source={data.spider === "aihuishou" ? images.aihuishou : images.mudah}
                            resizeMode='contain'
                            className = 'w-8 h-8'
                        />

                        {/* User Favourite */}
                        <FavouriteButton
                            productId={data.unique_id}
                        />
                    </View>
                

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