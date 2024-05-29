import { View, Text, FlatList, ActivityIndicator } from "react-native";
import React, { useEffect, useRef, useState } from "react";
import { useGlobalContext } from "../../context/GlobalProvider";
import useFetch from "../../hooks/useFetch";
import CustomSelector from "../CustomSelector";
import { all } from "axios";

const CategoryBrand = () => {
  const {
    categoryBrand,
    searchCategory,
    setSearchCategory,
    searchBrand,
    setSearchBrand,
    searchModel,
    setSearchModel,
    allModel,
  } = useGlobalContext();

  const categoryRef = useRef("camera");
  const [brandList, setBrandList] = useState([]);
  const [modelList, setModelList] = useState([]);

  const { data, isLoading, error, refetch } = useFetch(
    "TimmyProduct/GetCategoryBrandList",
    "GET",
    {},
    {}
  );

  const setUserSelectCategory = (category) => {
    categoryRef.current = category;
    setBrandList(categoryBrand["categoryBrands"][category]);
    setSearchBrand("")
    setSearchModel("")
    setModelList([])
    setSearchCategory(category);
  };

  const setUserSelectBrand = (brand) => {
    setModelList(allModel["products"][categoryRef.current][brand][brand]);
    setSearchBrand(brand);
  };

  const setUserSelectModel = (model) => {
    setSearchModel(model)
    console.log(model)
  };

  return (
    <View>
      {isLoading ? (
        <ActivityIndicator size="large"></ActivityIndicator>
      ) : error ? (
        <Text>Error while fetching</Text>
      ) : data == null ? (
        <Text>No data</Text>
      ) : (
        <View>
          <FlatList
            data={data.data.categories}
            renderItem={({ item }) => (
              <CustomSelector
                title={item}
                handleClick={() => {
                  setUserSelectCategory(item);
                }}
                state={searchCategory}
                customStyle="mx-1"
              />
            )}
            horizontal={true}
            showsHorizontalScrollIndicator={false}
          />
          <FlatList
            data={brandList}
            renderItem={({ item }) => (
              <CustomSelector
                title={item}
                handleClick={() => {
                  setUserSelectBrand(item);
                }}
                state={searchBrand}
                customStyle="mx-1"
              />
            )}
            horizontal={true}
            showsHorizontalScrollIndicator={false}
          />
          <FlatList
            data={modelList}
            renderItem={({ item }) => (
              <CustomSelector
                title={item}
                handleClick={() => {
                  setUserSelectModel(item);
                }}
                state={searchModel}
                customStyle="mx-1"
              />
            )}
            horizontal={true}
            showsHorizontalScrollIndicator={false}
          />
        </View>
      )}
    </View>
  );
};

export default CategoryBrand;
