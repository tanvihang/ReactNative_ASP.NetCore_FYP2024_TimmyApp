import { View, Text, SafeAreaView, FlatList } from 'react-native'
import {React,  useEffect,  useState } from 'react'
import useFetch from '../hooks/useFetch'
import { useGlobalContext } from '../context/GlobalProvider'

const ProductModels = ({category}) => {
    const {modelDictionary, categoryBrand} = useGlobalContext();
  
    // for(const [key, value] of Object.entries(exampleBrandDict.data.products)){
    //   for(const[key1,value1] of Object.entries(exampleBrandDict.data.products[key])){
    //     for(const[key2,value2] of Object.entries(exampleBrandDict.data.products[key][key1])){
    //       console.log("Category: " + key)
    //       console.log("Brand: " + key1)
    //       console.log("Models: " + value2)  
    //     }
    //   }
    // }
  

    
  
    const RenderBrand = (brand, category) => {
    //   console.log(modelDictionary.data.products[category][brand][brand])
      return(
        <View>
          <Text className="text-xl font-bold italic">{brand}</Text>
          <FlatList
            data = {modelDictionary.data.products[category][brand][brand]}
            renderItem={({item}) => RenderModel(item)}
            keyExtractor={item => item}
          />
        </View>
      )
    }
  
    const RenderModel = (model) =>{
      return(
        <Text>{model}</Text>
      )
    }
  
    return(
        <View className="border-2 my-2 p-2">
            <Text className="text-2xl font-bold italic">{category}</Text>
            <FlatList
            data = {categoryBrand.data.categoryBrands[category]}
            renderItem={({item}) => RenderBrand(item, category)}
            keyExtractor={item => item}
            />
        </View>
        )
    // return (
    //     <FlatList
    //       data = {categoryBrand.data.categories}
    //       renderItem={({item}) => RenderCategory(item)}
    //       keyExtractor={item => item}
    //       className = "mt-7 mx-3"
    //     ></FlatList>
    // )
}

export default ProductModels