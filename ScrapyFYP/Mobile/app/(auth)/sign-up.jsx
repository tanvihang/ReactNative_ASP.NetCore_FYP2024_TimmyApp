import { View, Text, ScrollView, Alert } from 'react-native'
import { React,  useState } from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import FormField from '../../components/FormField'
import CustomButton from '../../components/CustomButton'
import { Link } from 'expo-router'
import CTAButton from '../../components/Buttons/CTAButton'
import apiCall from '../../api/apiCall'
import normalApiCall from '../../hooks/normalApiCall'

const SignUp = () => {

  // user form state
  const [form, setForm] = useState({
    userEmail : '',
    userPassword : '',
    userName: '',
    userPhone: '',
    verificationCode:''
  })

  const [isSubmitting, setIsSubmitting] = useState(false)
  const [isSubmittingCode, setIsSubmittingCode] = useState(false)

  const sendCode = async (email) => {
    try{
      setIsSubmittingCode(true)
      const data = await normalApiCall("UserVerificationCode/SendVerificationCode", "GET", {},{email: email})
      setIsSubmittingCode(false)
    }catch(error){
      Alert.alert("Error", error.message)
      setIsSubmittingCode(false)
    }
  }

  const sendRegister = async(form) => {
    try{
      setIsSubmitting(true)
      const data = await normalApiCall("User/Register","POST",form,{})
      setIsSubmitting(false)
    }catch(error){
      Alert.alert("Error", error.message)
      setIsSubmitting(false)
    }
  }
  


  return (
    <SafeAreaView className = "h-full ">
      <ScrollView
        showsVerticalScrollIndicator = {false}
      >

      <View className ="justify-center w-full min-h-[85vh]">
        <Text className = "text-3xl mt-10 font-semibold text-primary-200">Sign up an account!</Text>
      
        <FormField
          title="Email"
          value = {form.userEmail}
          handleChangeText = {(e)=>setForm({...form, userEmail:e})} 
          otherStyles= "mt-7"
          placeholder="email"
          keyboardType = "email-address"
        />

        <FormField
          title="Username"
          value = {form.userName}
          handleChangeText = {(e)=>setForm({...form, userName:e})} 
          otherStyles= "mt-7"
          placeholder="username"
        />

        <FormField
          title="Phone"
          value = {form.userPhone}
          handleChangeText = {(e)=>setForm({...form, userPhone:e})} 
          otherStyles= "mt-7"
          placeholder="phone"
        />

        <View className="flex-row mt-7 justify-between">
          <FormField
            title="Verification Code"
            value = {form.verificationCode}
            handleChangeText = {(e)=>setForm({...form, verificationCode:e})} 
            otherStyles= "w-56"
            placeholder="verification code"
          />

          <CTAButton
              title = "GET CODE"
              handlePress={()=>{
                sendCode(form.userEmail)
              }}
              containerStyles= "mt-7 w-40"
              textStyles= "text-white-100"
              className = "text-white-100"
              isLoading={isSubmittingCode}
          />

        </View>
        

        <FormField
            title="Password"
            value = {form.userPassword}
            handleChangeText = {(e)=>setForm({...form, userPassword:e})} 
            otherStyles= "mt-7"
            placeholder="password"
        />

        <CTAButton 
          title = "SIGN UP"
          handlePress={()=>{sendRegister(form)}}
          containerStyles= "mt-7"
          textStyles= "text-white-100"
          isLoading={isSubmitting}
        />

        <View className = "items-center justify-center flex-row gap-1 pt-3">
          <Text className="text-lg font-regular">Return to</Text>
          <Link href="/sign-in" className='text-lg font-regular text-secondary'>Sign In</Link>
        </View>

      </View>

      </ScrollView>
    </SafeAreaView>
  )
}

export default SignUp