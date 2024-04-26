import { View, Text } from 'react-native'
import React from 'react'
import { useGlobalContext } from '../context/GlobalProvider'
import CustomSelector from './CustomSelector'

const SearchFooter = (form, setForm) => {

    const {user, spiders} = useGlobalContext()

    const select = (selItem) => {
        if()
    }

  return (
    <View>
      <View>
        <Text>Choose Platform</Text>
        <View>
            <CustomSelector 
                title = {spiders[0]}
            />
        </View>
      </View>
    </View>
  )
}

export default SearchFooter