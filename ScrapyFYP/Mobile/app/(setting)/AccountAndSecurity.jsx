import { View, Text, FlatList } from 'react-native'
import React from 'react'
import {AccountAndSecurityOptions} from '../../constants/settingOptions'
import SettingList from '../../components/Setting/SettingList'

const AccountAndSecurity = () => {

  return (
    <View className = "h-full">

      <FlatList
        data = {AccountAndSecurityOptions}
        renderItem={({item})=>
          <SettingList setting = {item}/>
        }
        keyExtractor={item=>item.name}
        ListHeaderComponent={()=>{
          return(
            <Text className="w-full text-center pt-10 font-pbold text-5xl">
              Account And Security
            </Text>
          )
        }}
      />
    </View>
  )
}

export default AccountAndSecurity