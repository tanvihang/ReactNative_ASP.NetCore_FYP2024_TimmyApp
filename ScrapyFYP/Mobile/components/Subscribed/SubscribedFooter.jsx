import { View, Text } from 'react-native'
import React from 'react'
import CustomButton from "../CustomButton"
import { router } from 'expo-router'

const SubscribedFooter = () => {
  return (
    <View>
      <CustomButton
        title = "Subscribe A Product +"
        textStyles="text-2xl font-bold italic tracking-widest"
        containerStyles= "mx-3"
        className = "text-2xl font-bold italic tracking-widest m-3"
        handlePress={()=>{
          router.push("/subscribe")
        }}
      />
    </View>
  )
}

export default SubscribedFooter