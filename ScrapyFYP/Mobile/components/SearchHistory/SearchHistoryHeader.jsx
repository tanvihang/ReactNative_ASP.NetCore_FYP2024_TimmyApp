import { View, Text, Alert } from "react-native";
import React, { useState } from "react";
import CTAButton from "../Buttons/CTAButton";
import normalApiCall from "../../hooks/normalApiCall";
import { useGlobalContext } from "../../context/GlobalProvider";
import { router } from "expo-router";

const SearchHistoryHeader = () => {
  const { jwtToken } = useGlobalContext();
  const [ isLoading, setIsLoading ] = useState(false);

  const pressClearSearchHistory = async () => {
    try {
      setIsLoading(true);
      const data = await normalApiCall(
        "UserSearchHistory/ClearUserSearchHistory",
        "POST",
        {},
        { jwtToken: jwtToken }
      );
      Alert.alert("Success", "Cleared user search history!");
      setIsLoading(false);
      router.replace("/searchHistory")
    } catch (error) {
      Alert.alert("Error", error.message);
      setIsLoading(false);
    }
  };

  return (
    <View>
      <Text className="font-pbold text-5xl pt-5">Search History</Text>
      <CTAButton
        title="Clear Search History"
        handlePress={() => {
          Alert.alert(
            "Warning",
            "Are you sure you want to clear search history?",
            [
              {
                text: "Confirm",
                onPress: () => {
                  try {
                    pressClearSearchHistory();
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
        }}
        isLoading={isLoading}
        textStyles="text-white text-xl font-pextrabold text-center"
        containerStyles="my-2"
      />
    </View>
  );
};

export default SearchHistoryHeader;
