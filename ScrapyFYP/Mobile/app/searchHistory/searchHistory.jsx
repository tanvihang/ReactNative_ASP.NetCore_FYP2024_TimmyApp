import { View, Text, FlatList, ActivityIndicator } from "react-native";
import React from "react";
import useFetch from "../../hooks/useFetch";
import { useGlobalContext } from "../../context/GlobalProvider";
import { SafeAreaView } from "react-native-safe-area-context";
import SearchHistoryHeader from "../../components/SearchHistory/SearchHistoryHeader";
import NoSearchHistory from "../../components/SearchHistory/NoSearchHistory"

const searchHistory = () => {
  const { jwtToken } = useGlobalContext();
  const { data, isLoading, error, refetch } = useFetch(
    "UserSearchHistory/GetUserSearchHistory",
    "POST",
    {},
    { jwtToken: jwtToken }
  );

  return (
    <SafeAreaView>
      {isLoading ? (
        <ActivityIndicator size="large"></ActivityIndicator>
      ) : error ? (
        <NoSearchHistory/>
      ) : (
        <View className="flex flex-col justify-center items-center pt-7 pb-5 w-full">
          <FlatList
            data={data?.data}
            renderItem={({ item }) => (
              <View>
                <Text className = "font-pregular">{item.userSearchHistoryProductFullName}</Text>
                <Text className = "font-plight">{item.userSearhHistoryDate}</Text>
              </View>
            )}
            keyExtractor={(item) => item.userSearhHistoryDate}
            showsVerticalScrollIndicator = {false}
            ListHeaderComponent={<SearchHistoryHeader />}
            className="mb-3"
          ></FlatList>
        </View>
      )}
    </SafeAreaView>
  );
};

export default searchHistory;
