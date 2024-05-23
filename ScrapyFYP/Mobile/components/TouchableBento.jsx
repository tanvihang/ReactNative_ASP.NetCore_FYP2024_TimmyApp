import { View, Text, TouchableOpacity, Image } from "react-native";
import React from "react";
import { router } from "expo-router";

const TouchableBento = ({
  title,
  bentoHref,
  handlePress,
  containerStyle,
  image,
  textStyle,
  imageStyle
}) => {
  return (
    <TouchableOpacity
      onPress={handlePress}
      className={` border-black-200 focus:bg-slate-300 flex-row px-2 rounded-2xl justify-center items-center w-2/5 ${containerStyle} my-2`}
    >
      <View className="flex flex-row justify-center items-center gap-1">
        <Text className={`${textStyle}`}>{title}</Text>
        {image ? <Image source={image} className="w-7 h-7" resizeMode="contain"/> : <Text></Text>}
      </View>
    </TouchableOpacity>
  );
};

export default TouchableBento;
