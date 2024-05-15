import { View, Text, FlatList } from 'react-native'
import React from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import SubscribedHeader from '../../components/Subscribed/SubscribedHeader'
import SubscribedFooter from '../../components/Subscribed/SubscribedFooter'

const Subscribed = () => {
  return (
    <SafeAreaView>
      <FlatList
        ListHeaderComponent= <SubscribedHeader/>
        ListFooterComponent = <SubscribedFooter/>
      />
    </SafeAreaView>
  )
}

export default Subscribed