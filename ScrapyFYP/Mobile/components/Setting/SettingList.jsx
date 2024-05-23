import { View, Text, FlatList, Image, TouchableOpacity } from 'react-native'
import React from 'react'
import { icons } from '../../constants'
import { router } from 'expo-router'


const SettingList = ({setting}) => {

  const goToPage = (pathName) => {
    console.log(pathName)
    if(pathName == ""){

    }else{
      router.push(pathName)
    }

  }

  const itemComponent = (item, pathName) => {

    return (
      <TouchableOpacity
        onPress={()=>{goToPage(pathName)}}
      >
        <View className = "flex flex-row justify-between items-center py-4 px-2 bg-primary-200 rounded-xl  border-primary-100 border-2">
          <Text className = "text-primary-100 font-psemibold text-xl">{item}</Text>
          <Image
            source={`${pathName == "" ? '' : icons.rightArrow}`}
            className = "mr-2 w-5 h-5"
          />
        </View>
      </TouchableOpacity>
      
    )
  }
  
    return (
    <View className = "mx-3">
      <Text className ="font-pregular text-xl mx-3">{setting.name}</Text>

      <FlatList
        data = {setting.items}
        renderItem = {({item, index}) => itemComponent(item,setting.fileName[index])}
        keyExtractor= {(item) => item}
        className = " "
        ItemSeparatorComponent={<Text className ="w-full h-1"></Text>}
      />

    </View>
  )
}

export default SettingList