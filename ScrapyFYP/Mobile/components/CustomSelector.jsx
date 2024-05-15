import { View, Text, TouchableOpacity } from 'react-native'
import {React, useEffect, useState} from 'react'

const CustomSelector = ({title, customStyle, handleClick,state}) => {

  const [color, setcolor] = useState("bg-black-100")

  useEffect(()=>{

    if(state == null){
      return;
    }

    switch(typeof(state)){
      case "object":
        if(state.indexOf(title) !== -1){
          setcolor("bg-purple-900")
        }
        else{
          setcolor("bg-black-100")
        }
        break;

      case "string":
        if(state == title){
          setcolor("bg-purple-900")
        }else{
          setcolor("bg-black-100")
        }
        break;

      default:
        break;
    }



  },[state])

  return (
    <View>
      <TouchableOpacity 
          onPress={() =>{handleClick()}}
          className = {`h-20 mt-3 ${customStyle}`}
      >
          <View className = {`h-20 flex items-center justify-center rounded-2xl ${color}`}>
            <Text className = "font-bold text-primary text-2xl italic text-center tracking-wide">{title}</Text>
          </View>
      </TouchableOpacity>
    </View>
    
  )
}

export default CustomSelector