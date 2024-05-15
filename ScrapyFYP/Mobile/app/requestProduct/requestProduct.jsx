import { View, Text, SafeAreaView, FlatList, ActivityIndicator} from 'react-native'
import {React, useState} from 'react'
import FormField from '../../components/FormField'
import CustomButton from '../../components/CustomButton'
import normalApiCall from '../../hooks/normalApiCall'
import useFetch from '../../hooks/useFetch'

const requestProduct = () => {

    const [category, setCategory] = useState(null)
    const [brand, setBrand] = useState(null)
    const [model, setModel] = useState(null)

    const {data, isLoading, error, refetch} = useFetch("TimmyProduct/GetUnAdoptedTimmyProductName", "GET", {},{})

    const sendAddProductRequest = async (body) => {
      try{
        const data = await normalApiCall("TimmyProduct/AddTimmyProduct", "POST",body,{})
      }
      catch(error){
        console.log(error)
      }
    }

    const requestButton = () => {
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
    <SafeAreaView className = "mx-3">

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


                  <CustomButton
                        title = "Request Product"
                        handlePress={()=>{requestButton()}}
                        containerStyles= "mt-4"
                      />
                  </View>
                )
                :(
                  <FlatList
                    data = {data?.data}
                    renderItem={({item}) => <Text className="text-center">{item}</Text>}
                    keyExtractor={item=>item}

                    ListHeaderComponent={
                      <View>
                        <Text className = "text-3xl font-bold italic text-center mt-3">Existing UnAdopted Product</Text>
                      </View>
                    }

                    ListFooterComponent={
                      <View>  
                      <Text className = "text-3xl font-bold italic text-center mt-3">Request A Product!</Text>
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
    
    
                      <CustomButton
                            title = "Request Product"
                            handlePress={()=>{requestButton()}}
                            containerStyles= "mt-4"
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