import {
  View,
  Text,
  Image,
  TouchableOpacity,
  SafeAreaView,
  FlatList,
} from "react-native";
import React, { useState, useRef, useEffect } from "react";
import { images, icons } from "../constants";
import { router } from "expo-router";
import { useGlobalContext } from "../context/GlobalProvider";
import TouchableBento from "./TouchableBento";

const HomeHero = ({ user }) => {
  const { jwtToken, category, setCategory, categoryBrand } = useGlobalContext();

  const pressFavItem = () => {
    router.push(`/favourite`);
  };

  const pressSubscribeItem = () => {
    router.push("/subscribe");
  };

  const pressCategory = (item) => {
    if (item === category) {
      setCategory(null);
    } else {
      setCategory(item);
    }
  };

  const categoryList = categoryBrand.categories

  return (
    <SafeAreaView className="mx-3 mt-7">
      {/* Hero welcome */}
      <View className="flex flex-row justify-between py-4">
        <View>
          <Text className="font-psemibold text-2xl text-primary-200">
            Welcome,
          </Text>
          <Text className="font-pbold text-6xl text-primary-200">
            {user.userName}
          </Text>
        </View>
      </View>

      <TouchableOpacity onPress={() => router.push("/search")}>
        <View className=" border-2 w-full h-16 items-center focus:bg-slate-300 flex-row justify-between px-3 rounded-2xl">
          <Text className="font-pbold">Search Product</Text>
          <Image
            source={icons.search}
            className="w-8 h-8 "
            tintColor="#000000"
          />
        </View>
      </TouchableOpacity>

      <View className=" py-3 flex flex-row w-full items-center justify-around">
        <TouchableBento
          title="Fav"
          handlePress={pressFavItem}
          containerStyle="bg-primary-100 mr-2 py-5"
          textStyle="text-white text-3xl font-pextrabold text-center"
          className=" bg-primary-100 font-pextrabold"
        />
        <TouchableBento
          title="Subscribe"
          handlePress={pressSubscribeItem}
          containerStyle="flex-grow bg-secondary py-5"
          textStyle="text-white text-3xl font-pextrabold text-center"
          className=" "
        />
      </View>

      <View className="flex flex-row w-full">
        {
          <FlatList
            data={categoryList}
            renderItem={({ item }) => (
              <TouchableOpacity
                className={`w-fit px-2 py-1 bg-black rounded-full flex items-center ${
                  item === category ? "bg-primary-200" : ""
                } `}
                onPress={() => {
                  pressCategory(item);
                }}
              >
                <Text className="text-white-100 font-psemibold">{item}</Text>
              </TouchableOpacity>
            )}
            keyExtractor={(item) => item}
            horizontal
            showsHorizontalScrollIndicator={false}
            className=""
            ItemSeparatorComponent={<Text> </Text>}
            initialNumToRender={10}
          />
        }
      </View>
    </SafeAreaView>
  );
};

export default HomeHero;
