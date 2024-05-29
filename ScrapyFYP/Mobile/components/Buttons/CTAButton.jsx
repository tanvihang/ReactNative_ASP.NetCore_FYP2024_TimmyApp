import { View, Text, TouchableOpacity } from "react-native";
import React from "react";

const CTAButton = ({ title, handlePress, containerStyles, textStyles, isLoading }) => {
  return (
    <View>
      <TouchableOpacity
        onPress={handlePress}
        activeOpacity={0.7}
        className={` rounded-2xl min-h-[62px] flex flex-row justify-center items-center bg-primary ${containerStyles} ${isLoading ? 'opacity-50' : ''} `}
        disabled={isLoading}
      >
        <Text className={` font-pmedium text-md ${textStyles}`}>
            {title}
        </Text>

      </TouchableOpacity>
    </View>
  );
};

export default CTAButton;
