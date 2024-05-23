import { View, Text } from 'react-native'
import React from 'react'

const SubscribeHero = () => {
  return (
    <View className="flex flex-row justify-between">
      <View>
        <Text className="font-pbold text-6xl text-primary pt-3">TimmyApp</Text>
        <Text className="font-psemibold text-primary text-2xl">
          Subscribe your dream item
        </Text>
      </View>
    </View>
  )
}

export default SubscribeHero