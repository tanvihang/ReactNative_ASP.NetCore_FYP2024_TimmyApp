import { View, Text, useWindowDimensions, Image } from "react-native";
import React from "react";
import CTAButton from "../Buttons/CTAButton";
import { router } from "expo-router";

const OnboardingItem = ({ item }) => {

  const handlePressRegister = () => {
    router.push("/sign-up")
  }

  return (
    <View className="flex-1 justify-center items-center w-screen">
      <Image source={item.image} className="w-64 h-64 " resizeMode="contain" />

      <Text className="text-6xl font-bold text-primary-200 mt-3">{item.title}</Text>
      <Text className="text-lg font-semibold">{item.description}</Text>

      {
        (item.id == 1)?
          (
            <View className = "mt-14 ">
              <CTAButton title = "Register" handlePress = {handlePressRegister} containerStyles = "min-w-[200px]" textStyles = "text-lg text-white-100" className = " "/>
            </View>
        ):
          <Text></Text>
        
      }
    </View>
  );
};

export default OnboardingItem;
