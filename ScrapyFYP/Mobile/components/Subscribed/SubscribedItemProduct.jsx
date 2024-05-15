import { Alert, View, Text, TouchableOpacity, Image } from 'react-native'
import React from 'react'
import {images, icons} from "../../constants"
import * as Linking from 'expo-linking'
import FavouriteButton from '../Favourite/FavouriteButton'
import normalApiCall from '../../hooks/normalApiCall'

const SubscribedItemProduct = ({data, customStyle, customTextStyle}) => {

    const removeProductRequest = async (id) => {
        try{
            const data = await normalApiCall("UserSubscriptionProduct/RemoveUserSubscriptionProduct","POST",{},{userSubscriptionProductId: id})
        }catch(error){
            console.log(error)
        }
    }

  return (
    <View className={`border-2 border-black-200 h-32 focus:bg-slate-300 flex-row rounded-2xl justify-center items-center ${customStyle} my-2 `}>

            <TouchableOpacity
                onPress={()=>{
                    Linking.openURL(data.userSubscriptionProductUrl)
                }}
            >
                <View className = " rounded-2xl flex flex-row items-center justify-between gap-3">

                    <Image
                        src={data.userSubscriptionProductImage}
                        resizeMode='contain'
                        className = 'w-20 h-20 rounded-2xl'
                    />
                    
                    {/* Product information */}
                    <View className = "basis-3/6 flex flex-col gap-1">
                        <Text className = "">{data.userSubscriptionProductTitle}</Text>
                        
                        <View className = "flex flex-row">
                            <Text>RMB {data.userSubscriptionProductPriceCny} - </Text>
                            <Text >{data.userSubscriptionProductCondition}</Text>
                        </View>

                        <Image
                            source={data.userSubscriptionProductSpider === "aihuishou" ? images.aihuishou : images.mudah}
                            resizeMode='contain'
                            className = 'w-8 h-8'
                        />
                    </View>

                </View>
            </TouchableOpacity>

            <View className = "flex flex-col justify-center items-center">
                <TouchableOpacity 
                    onPress={() =>
                        {
                            Alert.alert("Warning", "Are you sure you want to remove this product from subscription product?",
                            [{
                                text: 'Confirm',
                                onPress: ()=>{
                                  try{
                                        removeProductRequest(data.userSubscriptionProductId)
                                  }catch(error){
                                      console.log(error)
                                  }
                                  
                                },
                              },
                              {
                                text: 'Cancel',
                                onPress: () => console.log('cancel')
                              },
                              ], { cancelable: true }
                            )
                            

                        }}
                >
                    <Image
                        source={icons.remove}
                        className = "w-8 h-8 ml-4"
                        tintColor="#BB0000"
                    />
                </TouchableOpacity>

                <FavouriteButton
                    productId={data.userSubscriptionProductUniqueId}
                />
            </View>

    </View>
  )
}

export default SubscribedItemProduct