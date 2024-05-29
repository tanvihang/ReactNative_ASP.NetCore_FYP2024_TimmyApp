import { View, Text, ScrollView, Alert } from 'react-native'
import { React,  useState } from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import FormField from '../../components/FormField'
import CustomButton from '../../components/CustomButton'
import { Link, router, useLocalSearchParams } from 'expo-router'
import normalApiCall from '../../hooks/normalApiCall'
import { useGlobalContext } from '../../context/GlobalProvider'
import CTAButton from '../../components/Buttons/CTAButton'
import * as SecureStore from 'expo-secure-store'

const SignIn = () => {
  const {setIsLoggedIn, setJwtToken, jwtToken} = useGlobalContext();

  const [key, setKey] = useState(null)
  const [value, setValue] = useState(null)

  const saveSecureValue = async(value) => {
    await SecureStore.setItemAsync("jwtToken",value)
  }

  // user form state
  const [form, setForm] = useState({
    userToken : '',
    password : ''
  });

  const [isSubmitting, setIsSubmitting] = useState(false)

  const submit = async() => {

    if(form.userToken === "" || form.password === ""){
      Alert.alert("Error", "Please fill in all fields.")
    }

    console.log("Requesting");
    setIsSubmitting(true);

    try{
      const data = await normalApiCall("User/Login","POST",{
        userPassword:form.password,
        userToken: form.userToken
      },{});
  
      if(data.statusCode == 200){
        setKey("jwtToken")
        setValue(data.data)
        await saveSecureValue(data.data)
        setJwtToken(data.data);
        setIsLoggedIn(true)
        router.replace("/home") 
      }
    }
    catch(ex){
      Alert.alert("Error", ex.message)
    }
    finally{
      setIsSubmitting(false)
    }
  }

  return (
    <SafeAreaView className = "h-full ">
      <ScrollView
      contentContainerStyle={{
        height: '100%'
      }}>

      <View className ="justify-center w-full min-h-[85vh]">
        <Text className = "text-3xl mt-10 font-semibold text-primary-200">Log in to TimmyApp</Text>
      
        <FormField
          title="UserToken"
          value = {form.userToken}
          handleChangeText = {(e)=>setForm({...form, userToken:e})} 
          otherStyles= "mt-7"
          placeholder="email/username"
          keyboardType = "email-address"
        />
      
        <FormField
            title="Password"
            value = {form.password}
            handleChangeText = {(e)=>setForm({...form, password:e})} 
            otherStyles= "mt-7"
            placeholder="password"
        />

        <CTAButton 
          title = "SIGN IN"
          handlePress={()=>{
            submit()
          }}
          textStyles= "text-white-100"
          containerStyles= "mt-7"
          isLoading={isSubmitting}
        />

        <View className = "items-center justify-center flex-row gap-3 pt-3">
          <Text className="text-lg font-regular">Dont't have an account?</Text>
          <Link href="/sign-up" className='text-lg font-regular text-secondary'>Sign Up</Link>
        </View>

      </View>

      </ScrollView>
    </SafeAreaView>
  )
}



export default SignIn