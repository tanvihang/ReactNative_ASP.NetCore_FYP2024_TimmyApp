import { View, Text, SafeAreaView, FlatList } from 'react-native'
import {React,  useEffect,  useState } from 'react'
import useFetch from '../../hooks/useFetch'
import { useGlobalContext } from '../../context/GlobalProvider'
import ProductModels from '../../components/ProductModels'
import SearchHero from '../../components/SearchHero'
import SearchFooter from '../../components/SearchFooter'

const Search = () => {

  const {modelDictionary, categoryBrand} = useGlobalContext();
  const {form, setForm} = useState({
    productSearchTerm: {
      category: "",
      brand: "",
      model: "",
      description: "",
      highest_price: 0,
      lowest_price: 0,
      country: "",
      state: "",
      condition: "",
      spider: [
        
      ],
      sort: "",
      isTest: 0
    },
    pageDTO: {
      pageSize: 10,
      currentPage: 1
    }
  })

  return (
    <SafeAreaView>

        <FlatList
          data = {categoryBrand.data.categories}
          renderItem={({item}) => <ProductModels category = {item}/>}
          keyExtractor={item => item}
          className = "mt-7 mx-3"
        
          ListHeaderComponent={() => <SearchHero/>}
          ListFooterComponent={() => <SearchFooter form = {form} setForm = {setForm}/>}
        ></FlatList>

    </SafeAreaView>
  )
}

export default Search