import { View, Text, SafeAreaView, FlatList, ActivityIndicator, Alert} from 'react-native'
import {React, useState} from 'react'
import FormField from '../../components/FormField'
import CustomButton from '../../components/CustomButton'
import normalApiCall from '../../hooks/normalApiCall'
import useFetch from '../../hooks/useFetch'
import CTAButton from '../../components/Buttons/CTAButton'
import { router } from 'expo-router'

const requestProduct = () => {

    const [category, setCategory] = useState("")
    const [brand, setBrand] = useState("")
    const [model, setModel] = useState("")

    const {data, isLoading, error, refetch} = useFetch("TimmyProduct/GetUnAdoptedTimmyProductName", "GET", {},{})

    const sendAddProductRequest = async (body) => {
      try{
        const data = await normalApiCall("TimmyProduct/AddTimmyProduct", "POST",body,{})
        Alert.alert("Success", "Product request sent!")
        router.replace("/requestProduct")
      }
      catch(error){
        Alert.alert("Error",error.message)
      }
    }

    const requestButton = () => {

      // Check input
      if(category == "" || brand == "" || model == ""){
        Alert.alert("Error", "Must fill in all blank!")
        return
      }

        var req = {
            category: category.toLowerCase(),
            brand: brand.toLowerCase(),
            model: model.toLowerCase(),
            subModel: brand.toLowerCase(),
            adopt: 0
          }

        sendAddProductRequest(req)
    }

  return (
    <SafeAreaView className = "mx-3 mt-7 h-full">

            {
                isLoading ? (
                    <ActivityIndicator size='large'></ActivityIndicator>
                ) : error ? (
                    <Text>Something went wrong while fetching</Text>
                ) : data.data.length === 0 ? (
                  <View>  
                  <FormField
                          title="Category"
                          value = {category}
                          handleChangeText = {(e)=>{
                            // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
                            // setForm({...form, productSearchTerm:nextProductSearchTerm})
                            setCategory(e)
                          }} 
                          otherStyles= "mt-7"
                          placeholder="mobile"
                      />
                  <FormField
                          title="Brand"
                          value = {brand}
                          handleChangeText = {(e)=>{
                            // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
                            // setForm({...form, productSearchTerm:nextProductSearchTerm})
                            setBrand(e)
                          }} 
                          otherStyles= "mt-7"
                          placeholder="apple"
                      />
                  <FormField
                          title="Model"
                          value = {model}
                          handleChangeText = {(e)=>{
                            // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
                            // setForm({...form, productSearchTerm:nextProductSearchTerm})
                            setModel(e)
                          }} 
                          otherStyles= "mt-7"
                          placeholder="iphone 14 pro max"
                      />


                  <CTAButton
                        title = "Request Product"
                        handlePress={()=>{requestButton()}}
                        containerStyles= "mt-4"
                        textStyles = "text-white text-3xl font-pextrabold text-center"
                      />
                  </View>
                )
                :(
                  <FlatList
                    data = {data?.data}
                    renderItem={({item}) => <Text className="text-center font-pregular">{item}</Text>}
                    keyExtractor={item=>item}

                    ListHeaderComponent={
                      <View>
                        <Text className = "text-3xl font-pbold text-center pt-5 text-primary">Existing UnAdopted Product</Text>
                      </View>
                    }

                    ListFooterComponent={
                      <View>  
                      <Text className = "text-5xl font-pbold text-center pt-5 text-primary">Request A Product!</Text>
                      <FormField
                              title="Category"
                              value = {category}
                              handleChangeText = {(e)=>{
                                // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
                                // setForm({...form, productSearchTerm:nextProductSearchTerm})
                                setCategory(e)
                              }} 
                              otherStyles= "mt-7"
                              placeholder="mobile"
                          />
                      <FormField
                              title="Brand"
                              value = {brand}
                              handleChangeText = {(e)=>{
                                // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
                                // setForm({...form, productSearchTerm:nextProductSearchTerm})
                                setBrand(e)
                              }} 
                              otherStyles= "mt-7"
                              placeholder="apple"
                          />
                      <FormField
                              title="Model"
                              value = {model}
                              handleChangeText = {(e)=>{
                                // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
                                // setForm({...form, productSearchTerm:nextProductSearchTerm})
                                setModel(e)
                              }} 
                              otherStyles= "mt-7"
                              placeholder="iphone 14 pro max"
                          />
    
    
                      <CTAButton
                            title = "Request Product"
                            handlePress={()=>{requestButton()}}
                            containerStyles= "mt-4"
                            textStyles = "text-white text-3xl font-pextrabold text-center"
                          />
                      </View>
                    }
                  />
                )
            }


    
    </SafeAreaView>
  )
}

export default requestProduct