import { View, Text, ScrollView, Animated} from 'react-native'
import React, {useEffect, useState} from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import { Link , Redirect, router} from 'expo-router'
import CustomButton from '../components/CustomButton'
import { useGlobalContext } from '../context/GlobalProvider'
import Onboarding from '../components/OnBoarding/Onboarding'
import TabsLayout from './(tabs)/_layout'
import Home from './(tabs)/home'
import * as SecureStore from 'expo-secure-store'


const index = () => {

  const {isLoggedIn,setJwtToken, setIsLoggedIn} = useGlobalContext()   

  // Check login status
  const checkLogin = async() => {
    let result = await SecureStore.getItemAsync("jwtToken")

    if(result !== null){
        setJwtToken(result)
        setIsLoggedIn(true)
    }
  }

  checkLogin()

  return (
    <View className = "bg-white">
       {
          isLoggedIn ? (
            <Redirect href='/searchV1'/>
          ):(
            <Onboarding/>
          )
        }
    </View>
  )
}


export default index