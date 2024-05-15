import { View, Text, TouchableOpacity, Image, Alert } from 'react-native'
import React from 'react'
import {images, icons} from "../../constants"
import { router } from 'expo-router'
import normalApiCall from '../../hooks/normalApiCall'
import { useGlobalContext } from '../../context/GlobalProvider'

const SubscribedItem = ({data,customStyle}) => {

    const {jwtToken} = useGlobalContext()

  return (
    <View className={`border-2 border-black-200 h-44 focus:bg-slate-300 flex-row rounded-2xl justify-center items-center ${customStyle} my-2 `}>

            <TouchableOpacity
                onPress={() =>
                    {
                        router.push(`/subscriptionProducts/${data.userSubscriptionId}`)         
                    }}
            >
                <View>
                    <Text className="font-bold text-base">Product: {data.userSubscriptionProductFullName}</Text>
                    <Text className="font-bold text-base">Price range: {data.userSubscriptionProductLowestPrice} ~ {data.userSubscriptionProductHighestPrice}</Text>   
                    <Text className="font-bold text-base">Description: {data.userSubscriptionProductDescription}</Text>
                    <Text className="font-bold text-base">Platform: {data.userSubscriptionSpiders == "" ? "No selected platform" : data.userSubscriptionSpiders}</Text>
                    <Text className="font-bold text-base">Inform Time: {data.userSubscriptionNotificationTime} UTC</Text>
                    <Text className="font-bold text-base">Condition: {data.userSubscriptionProductCondition == "" ? "No selected condition" : data.userSubscriptionProductCondition}</Text>
                    <Text className="font-bold text-base">Subscription Date: {data.userSubscriptionDate}</Text>
                </View>
            </TouchableOpacity>

            <TouchableOpacity 
                onPress={() =>
                    {
                        Alert.alert(
                            //title
                            'Remove Subscribtion',
                            //message
                            'Do you really want to remove this subscription',
                            [{
                              text: 'Confirm',
                              onPress: ()=>{
                                try{
                                    normalApiCall("UserSubscription/RemoveUserSubscription","POST",
                                    {
                                        category: data.userSubscriptionProductCategory,
                                        brand: data.userSubscriptionProductBrand,
                                        model: data.userSubscriptionProductModel
                                    },
                                    {
                                        jwtToken: jwtToken
                                    }
                                    )
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
    </View>


  )
}

export default SubscribedItem