import {
  View,
  Text,
  ScrollView,
  FlatList,
  ActivityIndicator,
  Alert,
} from "react-native";
import { React, useEffect, useState, useCallback, useRef } from "react";
import useFetch from "../hooks/useFetch";
import normalApiCall from "../hooks/normalApiCall";
import ProductBox from "./ProductBox";
import { useGlobalContext } from "../context/GlobalProvider";
import { SafeAreaView } from "react-native-safe-area-context";

const ElasticProductPagination = ({ page, header }) => {
  const [datas, setDatas] = useState(null);
  const [page1, setPage1] = useState(1)
  var page = useRef(1)
  const {category} = useGlobalContext();
  const [isLoading, setIsLoading] = useState(true)
  
  // Check for the category changing
  useEffect(()=>{
    page.current = 1
    console.log(page)
    async function getProduct(category){
      try{
        setIsLoading(true)
        const tmpData = []
        const data = await normalApiCall("ElasticSearch/GetRandom10Product","POST",
        {
            pageSize: 10,
            currentPage: page.current
        },{
          category: category
        })
    
        for (let index = 0; index < data.data.rows.length; index++) {
          tmpData.push(data.data.rows[index])  
      }

      page.current = page.current+1

      setDatas(tmpData)
      setIsLoading(false)
    }
      catch(error){
        console.log(error)
        setIsLoading(false)
      }
    }
  
    getProduct(category)
  }, [category])
  

  const renderLoader = () => {
    return (
      <View className="my-3">
        <ActivityIndicator size="large" color="#000000" />
      </View>
    );
  };

  const loadMoreItem = async() => {
    try{
      console.log(page)
      const data = await normalApiCall("ElasticSearch/GetRandom10Product","POST",
      {
          pageSize: 10,
          currentPage: page.current
      },{
        category: category
      })

      page.current = page.current+1
      tmpData = datas

      for (let index = 0; index < data.data.rows.length; index++) {
        tmpData.push(data.data.rows[index])  
    }

      setDatas(tmpData)

    }
    catch(error){

    }

  }

  return (
    <SafeAreaView>
      {isLoading ? (
        <ActivityIndicator size="large"></ActivityIndicator>
      ) : datas === null ? (
        <FlatList
        ListHeaderComponent={header}
        ListFooterComponent={renderLoader}

      ></FlatList>
      ) : (
        <FlatList
          data={datas}
          renderItem={({ item }) => <ProductBox data={item} />}
          keyExtractor={(item) => item.unique_id}
          ListHeaderComponent={header}
          ListFooterComponent={renderLoader}
          onEndReached={loadMoreItem}
          onEndReachedThreshold={0.7}
        ></FlatList>
      )}
    </SafeAreaView>
  );
};

export default ElasticProductPagination;
