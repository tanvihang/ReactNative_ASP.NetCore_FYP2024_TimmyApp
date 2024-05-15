import { View, Text, TouchableOpacity, Image, SafeAreaView, Alert } from 'react-native'
import React from 'react'
import { icons } from '../../constants'

const PaginationButton = ({page, setPage}) => {
  return (
    <SafeAreaView className = "flex flex-row items-center justify-evenly h-16 w-full ">
        <TouchableOpacity 
            className = ""
            onPress={()=>{
                if(page == 1){
                    Alert.alert("Not valid input")
                }
                else{
                    setPage(page - 1)
                }
            }}>
            <Image
                source={icons.leftArrow}
                className = "h-8 w-8"
                resizeMode='contain'
                tintColor= "#000000"
            />
        </TouchableOpacity>
        
        <Text >{page}</Text>

        <TouchableOpacity 
            className = ""
            onPress = {()=>{
                setPage(page + 1)
            }}>
            <Image
                source={icons.rightArrow}
                className = "h-8 w-8"
                resizeMode='contain'
                tintColor= "#000000"
            />
        </TouchableOpacity>
    </SafeAreaView>
  )
}

export default PaginationButton