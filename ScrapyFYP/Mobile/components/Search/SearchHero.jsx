import { View, Text, Image } from "react-native";
import React from "react";
import { useGlobalContext } from "../../context/GlobalProvider";
import { images } from "../../constants";
import FormField from "../FormField";

const SearchHero = () => {
  const { user } = useGlobalContext();

  return (
    <View className="flex flex-row justify-between">
      <View>
        <Text className="font-pbold text-6xl text-primary pt-3">TimmyApp</Text>
        <Text className="font-psemibold text-primary text-2xl">
          Search any item
        </Text>
      </View>
    </View>
  );
};

export default SearchHero;
