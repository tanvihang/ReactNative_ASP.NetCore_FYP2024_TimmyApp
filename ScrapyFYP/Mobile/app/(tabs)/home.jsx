import { View, Text, Image, TouchableOpacity } from 'react-native'
import {React, useEffect, useState} from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import { useGlobalContext } from '../../context/GlobalProvider'
import { images, icons } from "../../constants"
import { router } from 'expo-router'
import ElasticProductPagination from '../../components/ElasticProductPagination'
import HomeHero from '../../components/HomeHero'


const Home = () => {
  
  const {user} = useGlobalContext()
  // Pagination for random product
  var page = 1


  return (
    <ElasticProductPagination
      header = <HomeHero user = {user}/>
      page = {page}
    />
  )
}

export default Home