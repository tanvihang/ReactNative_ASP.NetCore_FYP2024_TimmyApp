import { View, Text, Image, TouchableOpacity } from "react-native";
import React, { useState } from "react";
import { images } from "../../constants";
import { router } from "expo-router";

const NoSubscribedItem = () => {
  const [pressSubscribe, setPressSubscribe] = useState(false);

  const userPressSubscribe = () => {
    setPressSubscribe(true);
  };

  const userReleasePressSubscribe = () => {
    setPressSubscribe(false);
    router.push("/subscribe");
  };
  return (
    <View className="bg-secondary-100 h-full flex justify-center items-center">
      <Text className="font-pbold text-7xl text-center pt-5">
        No Subscribed Item
      </Text>

      <TouchableOpacity
        onPressIn={userPressSubscribe}
        onPressOut={userReleasePressSubscribe}
        activeOpacity={1}
        className="h-52 w-1/2 mx-auto"
      >
        <Image
          source={images.noFavourite}
          resizeMode="contain"
          className={` w-full h-full ${pressSubscribe ? "hidden" : "block"}`}
        />

        <Image
          source={images.PressSubscribe}
          resizeMode="contain"
          className={` w-full h-full ${pressSubscribe ? "block" : "hidden"}`}
        />
      </TouchableOpacity>

      <Text className=" font-pthin text-3xl mt-10 text-center text-secondary">
        Go Subscribe Item
      </Text>
    </View>
  );
};

export default NoSubscribedItem;
