import { View, Text, Image, Alert } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useGlobalContext } from "../../context/GlobalProvider";
import TouchableBento from "../../components/TouchableBento";
import { icons, images } from "../../constants";
import { router } from "expo-router";
import * as SecureStore from "expo-secure-store";

const Profile = () => {
  const { user, setJwtToken, setIsLoggedIn } = useGlobalContext();

  const pressSetting = () => {
    router.push("/user-setting");
  };

  const pressFavourite = () => {
    router.push(`/favourite`);
  };

  const pressRequestProduct = () => {
    router.push("/requestProduct");
  };

  const pressSubscription = () => {
    router.push("/subscribed");
  };

  const pressHistory = () => {
    router.push("/searchHistory");
  };

  const pressLogout = () => {

    Alert.alert(
      "Warning",
      "Are you sure you want Logout?",
      [
        {
          text: "Confirm",
          onPress: () => {
            try {
              setJwtToken(null);
              setIsLoggedIn(false);
              SecureStore.deleteItemAsync("jwtToken");
              router.push("/");
            } catch (error) {
              console.log(error);
            }
          },
        },
        {
          text: "Cancel",
          onPress: () => console.log("cancel"),
        },
      ],
      { cancelable: true }
    );
  };

  return (
    <View>
      <SafeAreaView className="flex justify-around h-full">
        <View className="flex flex-col justify-center items-center mt-10 mb-5">
          <Text className="font-psemibold text-2xl">Hello,</Text>
          <Text className="font-pbold text-6xl">{user.userName}</Text>

          {/* Profile Picture, can be added later on */}
          <Image
            source={images.guitarPerson}
            className="mt-4 w-44 h-44 "
            resizeMode="contain"
          />

        </View>

        <View>
        <Text className = " font-psemibold text-xl text-center">Browse product {<Text className = " text-teal-700">"seamlessly"</Text>} </Text>
        <Text className = " font-psemibold text-xl text-center">with {<Text className = " text-secondary">Timmy App</Text>} </Text>

        </View>

        {/* Bento box design */}
        <View className="flex flex-row flex-wrap w-full justify-center">
          <TouchableBento
            title="Settings"
            handlePress={pressSetting}
            containerStyle="mr-3 bg-primary-100 py-3"
            textStyle="font-pregular text-xl"
            className="bg-primary-100  "
            imageStyle="text-white-100"
            image={icons.settings}
          />

          <TouchableBento
            title="Favourites"
            handlePress={pressFavourite}
            containerStyle="ml-3 bg-primary-100 py-3"
            textStyle="font-pregular text-xl"
            image={icons.favourite}
          />

          <TouchableBento
            title="Req Product"
            handlePress={pressRequestProduct}
            containerStyle="mr-3 bg-primary-100 py-3"
            textStyle="font-pregular text-xl"
            image={icons.upgrade}
          />

          <TouchableBento
            title="Subscription"
            handlePress={pressSubscription}
            containerStyle="ml-3 bg-primary-100 py-3"
            textStyle="font-pregular text-xl"
            image={icons.bookmark}
          />

          <TouchableBento
            title="History"
            handlePress={pressHistory}
            containerStyle="mr-3 bg-primary-100 py-3"
            textStyle="font-pregular text-xl"
            image={icons.history}
          />

          <TouchableBento
            title="Logout"
            handlePress={pressLogout}
            containerStyle="ml-3 bg-primary-200 py-3"
            textStyle="font-pbold text-xl text-red-300"
            // image={icons.about}
          />
        </View>
      </SafeAreaView>
    </View>
  );
};

export default Profile;
