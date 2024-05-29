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
  };
  return (
    <View className=" h-full flex justify-center items-center">
      <Text className="font-pbold text-2xl text-center pt-5">
        No Subscribed Item
      </Text>

      <TouchableOpacity
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

    </View>
  );
};

export default NoSubscribedItem;
