import { View, Text, FlatList, ActivityIndicator } from "react-native";
import React from "react";
import { Stack, useLocalSearchParams } from "expo-router";
import { SafeAreaView } from "react-native-safe-area-context";
import SubscribedItemProduct from "../../components/Subscribed/SubscribedItemProduct";
import useFetch from "../../hooks/useFetch";
import NoSubscriptionProduct from "../../components/SubscriptionProduct/NoSubscriptionProduct";

const UserSubscriptionProduct = () => {
  const { userSubscriptionId } = useLocalSearchParams();

  const { data, isLoading, error, refetch } = useFetch(
    "UserSubscriptionProduct/GetUserSubscriptionProductsByUserSubscriptionId",
    "POST",
    {},
    { userSubscriptionId: userSubscriptionId }
  );

  const header = () => {
    return (
      <View>
        <Text className = "font-pbold text-5xl pt-5 text-center">Selected items</Text>
        <Text className = "font-pthin text-primary-200">INFO - maximum 5 item will be selected, to renew item, {<Text className = "text-red-500 font-semibold">delete</Text>} not interested item in the list</Text>
      </View>
      
    )
  }

  return (
    <SafeAreaView className = "">
      <Stack.Screen options={{ header: () => null }} />
      {isLoading ? (
        <ActivityIndicator size="large"></ActivityIndicator>
      ) : error ? (
        <Text>Something went wrong while fetching</Text>
      ) : data.data.length === 0 ? (
        <NoSubscriptionProduct/>
      ) : (
        <FlatList
          data={data?.data}
          renderItem={({ item }) => (
            <SubscribedItemProduct
              data={item}
              customStyle="w-full bg-white-100 py-5 my-2"
              customTextStyle=""
            />
          )}
          keyExtractor={(item) => item.userSubscriptionProductId}
          className="mx-3 mt-7"
          showsVerticalScrollIndicator = {false}
          ListHeaderComponent={header()}
        ></FlatList>
      )}
    </SafeAreaView>
  );
};

export default UserSubscriptionProduct;
