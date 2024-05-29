import { View, Text } from 'react-native'
import React from 'react'

const PersonalInfoItem = ({title,item}) => {
  return (
    <View className = "flex flex-row gap-2 m-2 items-center flex-wrap">

      <Text className = "font-pbold text-lg">{title}</Text>
      <Text className = "font-pregular text-base ">{item}</Text>
    </View>
  )
}

export default PersonalInfoItem