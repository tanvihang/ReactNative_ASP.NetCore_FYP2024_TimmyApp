import { View, Text } from 'react-native'
import React from 'react'
import { Slot, Stack } from 'expo-router'


const SubscribeLayout = () => {
  return (
    <View>
      <Stack.Screen options={{ header: () => null }} />
      <Slot/>
    </View>
  )
}

export default SubscribeLayout