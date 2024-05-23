import { View, Text, Image, ScrollView, TouchableOpacity, Linking } from "react-native";
import React from "react";
import { images } from "../../constants";

const AboutApp = () => {
  return (
    <ScrollView className="bg-white-100 h-full">
      <Text className="w-full text-center pt-10 font-pextrabold text-5xl pb-5 text-secondary">
        About App
      </Text>

      <View className="flex items-center w-full">
        <Image
          source={images.chattingPerson}
          resizeMode="contain"
          className="w-1/2 h-44"
        />
      </View>

      <View className = "py-3 px-3">
        <Text className = "text-center font-pblack">Welcome to Timmy App, the {<Text className="line-through text-red-500">ultimate</Text>} (ordinary) designed to {<Text className=" text-teal-700">revolutionize</Text>} (?) your online shopping experience. Whether you're a {<Text className = "text-orange-400">savvy shopper</Text>} hunting for the best deals or a meticulous researcher tracking product trends, Timmy App has you covered. Our app offers a comprehensive suite of features that make shopping easier, smarter, and more {<Text className = "text-yellow-600">efficient</Text>} (trying hard) .</Text>
      
        <Text className = "text-center pt-5 font-pextrabold text-3xl pb-2 text-secondary">Key Features</Text>

        <Text className = "text-center font-pregular">{<Text className="font-pblack">Multi-Platform Scraping:</Text>} Product Scraper seamlessly extracts product information from a variety of popular online platforms. No more jumping between websites – get all the data you need in one place.</Text>
        <Text className = "text-center font-pregular">{<Text className="font-pblack">Advanced Categorization:</Text>} Our {<Text className = "font-pbold text-red-500 line-through">sophisticated</Text>} algorithm automatically categorizes items by their respective categories, brands, and models. This ensures you can quickly find exactly what you're looking for without the hassle.</Text>
        <Text className = "text-center font-pregular">{<Text className="font-pblack">Subscription and Favorites:</Text>} Stay updated with the products you care about most. Subscribe to your favorite categories, brands, or specific items to receive timely notifications on price changes and availability. Mark products as favorites for easy access and comparison later.</Text>
        <Text className = "text-center font-pregular">{<Text className="font-pblack">Price History Charts:</Text>} Make informed purchasing decisions with our {<Text  className = "font-pbold text-red-500 line-through">comprehensive</Text>} price history charts. Track the price changes of products across multiple platforms over time, helping you to identify the best time to buy and save money.</Text>
        <Text className = "text-center font-pregular">{<Text className="font-pblack">User-Friendly Interface:</Text>} Our intuitive interface is designed for ease of use, allowing you to navigate effortlessly and find the information you need with minimal effort.</Text>
      </View>

      <Text className = "text-center font-pextrabold text-3xl pb-2 text-secondary">Links</Text>

      <TouchableOpacity
        onPress={()=>{
          Linking.openURL("https://github.com/tanvihang/TimmyApp")
        }}
      >
        <Text className = "text-center font-pregular">{<Text className="font-pblack">Github repo:</Text>} https://github.com/tanvihang/TimmyApp </Text>
      </TouchableOpacity>
      
      <Text className="text-sm font-pthin flex text-right px-3 mb-2">2024/5/22 by AngusTan</Text>
    </ScrollView>
  );
};

export default AboutApp;
