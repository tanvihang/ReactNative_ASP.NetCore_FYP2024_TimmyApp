import { View, Text } from 'react-native'
import {React, useEffect, useState, useLayoutEffect} from 'react'
import { useGlobalContext } from '../../context/GlobalProvider'
import CustomSelector from '../CustomSelector'
import InputRange from '../InputRange'
import FormField from '../FormField'
import CustomButton from '../CustomButton'
import { router } from 'expo-router'

const SearchFooter = ({form, setForm, category, brand, model}) => {

    const {user, spiders, condition, searchParams, setSearchParams} = useGlobalContext()

    const [platform, setPlatform] = useState([])
    const [pcondition, setPCondition] = useState(null)
    const [lowPrice, setLowPrice] = useState("")
    const [highPrice, setHighPrice] = useState("")
    const [description, setDescription] = useState("")
    
    const curParams = {
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
        sort: "priceasc",
        isTest: 0
      },
      pageDTO: {
        pageSize: 10,
        currentPage: 1
      }
    }


    const selectPlatform = (selItem) => {

      if(platform.indexOf(selItem) !== -1){
        setPlatform(
          platform.filter(a => a !== selItem)
        )
      }else{
        setPlatform([
          ...platform,
          selItem
        ])
      }
    }

    const selectPCondition = (selItem) => {
      setPCondition(selItem)
    }

    const searchHandler = ()=>{

      var low = parseInt(lowPrice)
      var high = parseInt(highPrice)

      var nextProductSearchTerm = {...searchParams.productSearchTerm, lowest_price:low, highest_price: high, spider: platform, condition:pcondition, description:description}
      setSearchParams({...searchParams, productSearchTerm:nextProductSearchTerm})

      curParams.productSearchTerm.brand = searchParams.productSearchTerm.brand
      curParams.productSearchTerm.category = searchParams.productSearchTerm.category
      curParams.productSearchTerm.model = searchParams.productSearchTerm.model
      curParams.productSearchTerm.highest_price = high
      curParams.productSearchTerm.lowest_price = low
      curParams.productSearchTerm.description = description
      curParams.productSearchTerm.condition = pcondition
      curParams.productSearchTerm.spider = platform
      
      // console.log(form)
      goToPage()
    }
    
    const goToPage = () => {
      router.push({
        pathname: `/search/item`,
      })
    }

  return (
    <View>
      <View>
        <Text className = "text-3xl font-bold italic text-center mt-3">Choose Platform</Text>
        <View className ="flex flex-row justify-around">
            <CustomSelector 
                title = {spiders[0]}
                customStyle = "w-32"
                handleClick = {() => {
                  selectPlatform(spiders[0])
                }}
                state = {platform}
            />
            <CustomSelector 
                title = {spiders[1]}
                customStyle = "w-32"
                handleClick = {() => {
                  selectPlatform(spiders[1])
                }}
                state = {platform}
            />
        </View>
      </View>

      <View>
        <Text className = "text-3xl font-bold italic text-center mt-3">Choose Condition</Text>
        <View className ="flex flex-row justify-around">
            <CustomSelector 
                title = {condition[0]}
                customStyle = "w-32"
                handleClick = {() => {
                  selectPCondition(condition[0])
                }}
                state = {pcondition}
            />
            <CustomSelector 
                title = {condition[1]}
                customStyle = "w-32"
                handleClick = {() => {
                  selectPCondition(condition[1])
                }}
                state = {pcondition}
            />
            <CustomSelector 
                title = {condition[2]}
                customStyle = "w-32"
                handleClick = {() => {
                  selectPCondition(condition[2])
                }}
                state = {pcondition}
            />
        </View>
      </View>

      <View>
        <Text className = "text-3xl font-bold italic text-center mt-3">Price Range</Text>
        <FormField
            title="Lowest Price"
            value = {lowPrice}
            handleChangeText = {(e)=>{

              // var nextProductSearchTerm = {...form.productSearchTerm, lowest_price:e }
              // setForm({...form, productSearchTerm:nextProductSearchTerm})
              setLowPrice(e)
            }} 
            otherStyles= "mt-7"
            placeholder="0"
            inputMode="decimal"
        />
        <FormField
            title="Highest Price"
            value = {highPrice}
            handleChangeText = {(e)=>{
              // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
              // setForm({...form, productSearchTerm:nextProductSearchTerm})
              setHighPrice(e)
            }} 
            otherStyles= "mt-7"
            placeholder="20000"
            inputMode="decimal"
        />

        <FormField
            title="Description"
            value = {description}
            handleChangeText = {(e)=>{
              // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
              // setForm({...form, productSearchTerm:nextProductSearchTerm})
              setDescription(e)
            }} 
            otherStyles= "mt-7"
            placeholder="20000"
        />

        <CustomButton
          title = "Search"
          handlePress={()=>{searchHandler()}}
          containerStyles= "mt-4"
        />

        <CustomButton
          title = "No Product? Request One!"
          handlePress={()=>{
            router.push("/requestProduct")
          }}
          containerStyles= "mt-4"
        />

      </View>



    </View>
  )
}

export default SearchFooter