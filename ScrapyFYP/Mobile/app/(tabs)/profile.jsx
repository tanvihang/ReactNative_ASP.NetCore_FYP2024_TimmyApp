import { View, Text, Image } from 'react-native'
import React from 'react'
import { SafeAreaView } from 'react-native-safe-area-context'
import { useGlobalContext } from '../../context/GlobalProvider'
import TouchableBento from '../../components/TouchableBento'
import { icons, images } from '../../constants'

const Profile = () => {

  const {user} = useGlobalContext()

  // TODO api call to fetch user
  const data = {
    "statusCode": 200,
    "message": "Success",
    "data": {
      "userId": "U_ce9de9cd-c183-404e-900c-d72da4e17aa5",
      "userName": "tvh",
      "userEmail": "tvhang7@gmail.com",
      "userLevel": 1,
      "userRegisterDate": "2024-04-15T16:05:53.707",
      "userPhoneNo": "1111"
    }
  }

  return (
    <View>
      <SafeAreaView>
        <View className = "flex flex-col justify-center items-center mt-10 mb-5">
              <Text className = "font-semibold text-2xl">Hello,</Text>
              <Text className = "font-bold text-6xl">{user}</Text>
      
              {/* Profile Picture, can be added later on */}
              <Image
                source={images.profilepic}
                className = "mt-4 w-44 h-44 rounded-full"
              />

              {/* profile details */}
              <View className = "flex flex-col mt-3">
                <Text className="">Userid: {data.data.userId}</Text>
                <Text className="">UserEmail: {data.data.userEmail}</Text>
                <Text className="">UserLevel: {data.data.userLevel}</Text>
                <Text className="">JoinedDate: {data.data.userRegisterDate}</Text>
              </View>

        </View>



        {/* Bento box design */}
        <View  className = "flex flex-row flex-wrap w-full justify-center">
              <TouchableBento
                title = "Settings"
                bentoHref="/user-setting"
                customStyle= ""
                image = {icons.settings}
              />

            <TouchableBento
                title = "Favourites"
                bentoHref={`/favourite/${user}`}
                customStyle= ""
                image = {icons.favourite}
              />

            <TouchableBento
                title = "Req Product"
                bentoHref="/requestProduct"
                customStyle= ""
                image = {icons.upgrade}
              />

            <TouchableBento
                title = "Subscription"
                bentoHref="/subscribed"
                customStyle= ""
                image = {icons.bookmark}
              />

            <TouchableBento
                title = "Logout"
                bentoHref="/"
                customStyle= "w-10/12"
                image = {icons.about}
              />

        </View>

      </SafeAreaView>
    </View>
  )
}

export default Profile