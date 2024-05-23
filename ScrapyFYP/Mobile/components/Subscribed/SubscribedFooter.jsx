import { View, Text } from 'react-native'
import React from 'react'
import CustomButton from "../CustomButton"
import { router } from 'expo-router'
import CTAButton from '../Buttons/CTAButton'

const SubscribedFooter = () => {
  return (
    <View>
      <CTAButton
        title = "Subscribe A Product +"
        textStyles="text-white text-3xl font-pextrabold text-center"
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