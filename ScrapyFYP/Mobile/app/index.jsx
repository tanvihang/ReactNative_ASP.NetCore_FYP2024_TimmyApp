import { View, Text, ScrollView} from 'react-native'
import React from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import { Link , Redirect, router} from 'expo-router'
import CustomButton from '../components/CustomButton'
import { useGlobalContext } from '../context/GlobalProvider'

const index = () => {

  const {isLoggedIn, jwtToken} = useGlobalContext() 

  if(isLoggedIn){
    return <Redirect href='/home'/>
  }

  return (
    <SafeAreaView className = "h-full bg-white ">
      <ScrollView contentContainerStyle={{
        height: '100%'
      }}>

        <View className = "w-full h-60 items-center justify-center">
          <Text>
              Timmy App
          </Text>
        </View>

        <View className = "w-full justify-center items-center">
          <CustomButton
            title="Continue with Email"
            handlePress = { ()=>{router.push("/sign-in")} }
            containerStyles = "w-9/12 mt-7"
          />
          <CustomButton
            title="Go to home page"
            handlePress = { ()=>{router.push("/home")} }
            containerStyles = "w-9/12 mt-7"
          />

        </View>
      </ScrollView>
    </SafeAreaView>
  )
}


export default index