import { View, Text, Image, TouchableOpacity, Alert } from "react-native";
import React, { useState } from "react";
import { images } from "../../constants";
import { router } from "expo-router";
import normalApiCall from "../../hooks/normalApiCall";
import * as SecureStore from "expo-secure-store";
import { useGlobalContext } from "../../context/GlobalProvider";

const DeleteAccount = () => {
  const{user, jwtToken, setJwtToken, setIsLoggedIn} = useGlobalContext()
  const [pressDelete, setPressDelete] = useState(false);

  const userpressDelete = () => {
    setPressDelete(true);

    Alert.alert(
        "Warning",
        "Delete account permanantly?",
        [
          {
            text: "Confirm",
            onPress: () => {
              try {
                setJwtToken(null);
                setIsLoggedIn(false);
                SecureStore.deleteItemAsync("jwtToken");
                normalApiCall("User/DeleteUser","POST",{},{
                    validation: user.userName,
                    jwtToken: jwtToken
                })
                router.push("/");

              } catch (error) {
                console.log(error);
              }
            },
          },
          {
            text: "Cancel",
            onPress: () => {setPressDelete(false);}
          },
        ],
        { cancelable: true }
      );
  };

  const userReleasepressDelete = () => {
    
  };

  return (
    <View className="bg-secondary-100 h-full flex justify-center items-center">
      <Text className="font-pbold text-7xl text-center">Are you sure?</Text>

      <TouchableOpacity
        onPressIn={userpressDelete}
        onPressOut={userReleasepressDelete}
        activeOpacity={1}
        className="h-52 w-1/2 mx-auto"
      >
        <Image
          source={images.shockedHeart}
          resizeMode="contain"
          className={` w-full h-full ${pressDelete ? "hidden" : "block"}`}
        />

        <Image
          source={images.deadHeart}
          resizeMode="contain"
          className={` w-full h-full ${pressDelete ? "block" : "hidden"}`}
        />
      </TouchableOpacity>

      <Text className=" font-pthin mt-10 text-center text-secondary">
        deleted account cant be recovered.
      </Text>
    </View>
  );
};

export default DeleteAccount;
