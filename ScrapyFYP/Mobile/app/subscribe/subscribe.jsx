import { View, Text, SafeAreaView, FlatList } from 'react-native'
import {React,  useEffect,  useState } from 'react'
import useFetch from '../../hooks/useFetch'
import { useGlobalContext } from '../../context/GlobalProvider'
import ProductModels from '../../components/ProductModels'
import { router } from 'expo-router'
import SubscribeHero from '../../components/Subscribe/SubscribeHero'
import SubscribeFooter from '../../components/Subscribe/SubscribeFooter'

const Subscribe = () => {

  const {categoryBrand} = useGlobalContext();

  const [selectCategory, setSelectCategory] = useState("")
  const [selectProduct, setSelectProduct] = useState("")
  const [selectBrand, setSelectBrand] = useState("")


  return (
    <SafeAreaView>

        <FlatList
          data = {categoryBrand.data.categories}
          renderItem={({item}) => <ProductModels category = {item} selectCategory = {selectCategory} setSelectCategory = {setSelectCategory} selectProduct = {selectProduct} setSelectProduct = {setSelectProduct} selectBrand = {selectBrand} setSelectBrand = {setSelectBrand} />}
          keyExtractor={item => item}
          className = "mt-7 mx-3"
          showsVerticalScrollIndicator = {false}
          ListHeaderComponent={() => <SubscribeHero/>}
          ListFooterComponent={() => <SubscribeFooter/>}
        ></FlatList>

    </SafeAreaView>
  )
}

export default Subscribe