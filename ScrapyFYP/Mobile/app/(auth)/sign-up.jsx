import { View, Text, ScrollView } from 'react-native'
import { React,  useState } from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import FormField from '../../components/FormField'
import CustomButton from '../../components/CustomButton'
import { Link } from 'expo-router'

const SignUp = () => {

  // user form state
  const [form, setForm] = useState({
    userToken : '',
    password : '',
    userName: '',
    verificationCode:''
  })

  const [isSubmitting, setIsSubmitting] = useState(false)
  const [isSubmittintCode, setIsSubmittintCode] = useState(false)

  return (
    <SafeAreaView className = "h-full ">
      <ScrollView
      contentContainerStyle={{
        height: '100%'
      }}>

      <View className ="justify-center w-full min-h-[85vh]">
        <Text className = "text-2xl mt-10 font-semibold">Sign up an account!</Text>
      
        <FormField
          title="Email"
          value = {form.email}
          handleChangeText = {(e)=>setForm({...form, email:e})} 
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

        <View className="flex-row mt-7 justify-between">
          <FormField
            title="Verification Code"
            value = {form.verificationCode}
            handleChangeText = {(e)=>setForm({...form, verificationCode:e})} 
            otherStyles= "w-56"
            placeholder="verification code"
          />

          <CustomButton
              title = "GET CODE"
              handlePress={()=>{}}
              containerStyles= "mt-7 w-36"
              isLoading={isSubmitting}
          />

        </View>
        

        <FormField
            title="Password"
            value = {form.password}
            handleChangeText = {(e)=>setForm({...form, password:e})} 
            otherStyles= "mt-7"
            placeholder="password"
        />

        <CustomButton 
          title = "SIGN UP"
          handlePress={()=>{}}
          containerStyles= "mt-7"
          isLoading={isSubmitting}
        />

        <View className = "items-center justify-center flex-row gap-1 pt-3">
          <Text className="text-lg font-regular">Return to</Text>
          <Link href="/sign-in" className='text-lg font-regular text-secondary-100'>Sign In</Link>
        </View>

      </View>

      </ScrollView>
    </SafeAreaView>
  )
}

export default SignUp