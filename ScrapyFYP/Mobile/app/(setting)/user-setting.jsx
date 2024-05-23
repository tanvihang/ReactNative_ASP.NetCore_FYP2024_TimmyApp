import { View, Text, FlatList } from 'react-native'
import React from 'react'
import { Stack } from 'expo-router'
import {settingOptions} from '../../constants/settingOptions'
import SettingList from '../../components/Setting/SettingList'

const userSetting = () => {

  const userHeader = () => {
    return (
      <Text className ="w-full text-center pt-10 font-pbold text-5xl">Setting</Text>
    )
  }

  return (
    <View>
      <FlatList
        data={settingOptions}
        renderItem={({item}) => <SettingList setting = {item}/>}
        keyExtractor={item=>item.name}
        className = " rounded-xl"
        ListHeaderComponent={userHeader}
        ItemSeparatorComponent={<Text className ="h-3"></Text>}
      >

      </FlatList>
    </View>
  )
}

export default userSetting