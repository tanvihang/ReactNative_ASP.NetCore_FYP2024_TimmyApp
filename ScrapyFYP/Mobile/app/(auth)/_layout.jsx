import { View, Text } from 'react-native'
import React from 'react'
import { Slot } from 'expo-router'


const AuthLayout = () => {
  return (
    <View className = "mx-3">
      <Slot/>
    </View>
  )
}

export default AuthLayout