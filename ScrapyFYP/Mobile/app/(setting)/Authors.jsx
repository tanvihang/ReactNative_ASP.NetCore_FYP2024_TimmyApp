import { View, Text, Image, ScrollView, TouchableOpacity } from "react-native";
import React from "react";
import { images } from "../../constants";


const Authors = () => {
  return (
    <View className="bg-white-100 h-full flex justify-between" >
      <View>
      <Text className="w-full text-center pt-10 font-pextrabold text-5xl pb-5 text-secondary">
        Author
      </Text>

      <View className="flex items-center w-full">
        <Image
          source={images.dumbbellPerson}
          resizeMode="contain"
          className="w-1/2 h-44"
        />
      </View>

      <View className = "py-3 px-3">
        <Text className = "text-center font-pblack">Angus Tan is a wannabe software developer with over {<Text className="line-through text-red-500">a year</Text>} experience (oh yeah) {<Text className=" text-teal-700"> with a strong background </Text>} (?) in Computer Science. He found out that prices of product differs from country to country {<Text className = "text-orange-400">for this reason led to the creation of Timmy App</Text>} , an app designed to streamline online shopping and make it more efficient and enjoyable for users. {<Text className = "text-yellow-600"></Text>}</Text>
      </View>
      <TouchableOpacity
        onPress={()=>{
          Linking.openURL("https://github.com/tanvihang/TimmyApp")
        }}
      >
        <Text className = "text-center font-pregular">{<Text className="font-pblack">Github:</Text>} https://github.com/tanvihang </Text>
      </TouchableOpacity>

      </View>

      <Text className="text-sm font-pthin flex text-right py-2 px-3">2024/5/22 by AngusTan</Text>
    </View>
  )
}

export default Authors