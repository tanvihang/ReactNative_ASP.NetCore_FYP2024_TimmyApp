import { View, Text, ActivityIndicator, FlatList, useWindowDimensions } from "react-native";
import { React, useState, useEffect } from "react";
import { useGlobalContext } from "../../context/GlobalProvider";
import useFetch from "../../hooks/useFetch";
import ProductBox from "../../components/ProductBox";
import PaginationButton from "../../components/Buttons/PaginationButton";
import { Stack, router } from "expo-router";
import { SafeAreaView } from "react-native-safe-area-context";
import NoSearchProduct from "../../components/Search/NoSearchProduct";
import PriceHistoryChart from "../../components/Chart/PriceHistoryChart";

const Search = () => {
  const { searchParams, setSearchParams, jwtToken } = useGlobalContext();

  // pagination state
  const [page, setPage] = useState(1);

  // useEffect will refresh the whole screen
  useEffect(() => {
    console.log("Page changed " + page);

    // 解构赋值
    nextPageDTO = { ...searchParams.pageDTO, currentPage: page };
    setSearchParams({ ...searchParams, pageDTO: nextPageDTO });
  }, [page]);

  useEffect(() => {
    refetch();
  }, [searchParams]);

  const { data, isLoading, error, refetch } = useFetch(
    "ElasticSearch/SearchProduct",
    "POST",
    searchParams,
    { jwtToken: jwtToken }
  );

  

  const headerComp = () => {
    return(
      <View className = "pt-5 flex justify-center items-center">
        <Text className = "font-pblack text-center">{searchParams.productSearchTerm.model} price history chart</Text>
        <PriceHistoryChart
              category={searchParams.productSearchTerm.category}
              brand={searchParams.productSearchTerm.brand}
              model={searchParams.productSearchTerm.model}
            />
      </View>
    )
  }

  return (
    <SafeAreaView>
      <Stack.Screen options={{ header: () => null }} />
      {/* Dev */}
      {isLoading ? (
        <ActivityIndicator size="large"></ActivityIndicator>
      ) : error ? (
        <Text>Something went wrong while fetching</Text>
      ) : data.data === null ? (
        <NoSearchProduct />
      ) : (
        <FlatList
          data={data?.data.rows}
          renderItem={({ item }) => <ProductBox data={item} />}
          keyExtractor={(item) => item.unique_id}
          ListHeaderComponent={
              headerComp()
          }
          ListFooterComponent={
            <PaginationButton page={page} setPage={setPage} />
          }
        />
      )}
    </SafeAreaView>
  );
};

export default Search;
