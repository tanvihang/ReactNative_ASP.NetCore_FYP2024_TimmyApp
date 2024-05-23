import { View, Text, TouchableOpacity, Image, Alert } from 'react-native'
import React from 'react'
import {images, icons} from "../../constants"
import { router } from 'expo-router'
import normalApiCall from '../../hooks/normalApiCall'
import { useGlobalContext } from '../../context/GlobalProvider'

const SubscribedItem = ({data,customStyle}) => {

    const {jwtToken} = useGlobalContext()

  return (
    <View className={`focus:bg-slate-300 flex-row rounded-2xl justify-center items-center ${customStyle} `}>

            <TouchableOpacity
                onPress={() =>
                    {
                        router.push(`/subscriptionProducts/${data.userSubscriptionId}`)         
                    }}
            >
                <View>
                    <Text className="font-pbold text-sm">Product: {<Text className = "font-pmedium">{data.userSubscriptionProductFullName}</Text>} </Text>
                    <Text className="font-pbold text-sm">Price range: {<Text className = "font-pmedium">{data.userSubscriptionProductLowestPrice} ~ {data.userSubscriptionProductHighestPrice}</Text>} </Text>   
                    <Text className="font-pbold text-sm">Description: {<Text className = "font-pmedium">{data.userSubscriptionProductDescription}</Text>} </Text>
                    <Text className="font-pbold text-sm">Platform: {<Text className = "font-pmedium">{data.userSubscriptionSpiders == "" ? "No selected platform" : data.userSubscriptionSpiders}</Text>} </Text>
                    <Text className="font-pbold text-sm">Inform Time: {<Text className = "font-pmedium">{data.userSubscriptionNotificationTime} UTC</Text>} </Text>
                    <Text className="font-pbold text-sm">Condition: {<Text className = "font-pmedium">{data.userSubscriptionProductCondition == "" ? "No selected condition" : data.userSubscriptionProductCondition}</Text>} </Text>
                    <Text className="font-pbold text-sm">Subscription Date: {<Text className = "font-pmedium">{data.userSubscriptionDate}</Text>} </Text>
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