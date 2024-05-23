import {
  View,
  Text,
  SafeAreaView,
  FlatList,
  TouchableOpacity,
} from "react-native";
import { React, useEffect, useState } from "react";
import useFetch from "../hooks/useFetch";
import { useGlobalContext } from "../context/GlobalProvider";
import CustomSelector from "./CustomSelector";

const ProductModels = ({
  category,
  selectCategory,
  setSelectCategory,
  selectProduct,
  setSelectProduct,
  selectBrand,
  setSelectBrand,
}) => {
  const {
    modelDictionary,
    categoryBrand,
    searchParams,
    setSearchParams,
    subscribeParams,
    setSubscribeParams,
  } = useGlobalContext();

  const [showCat, setShowCat] = useState("");

  const userSelectModel = (model, brand, category) => {
    setSelectProduct(model);

    var nextProductSearchTerm = {
      ...searchParams.productSearchTerm,
      category: category,
      model: model,
      brand: brand,
    };
    setSearchParams({
      ...searchParams,
      productSearchTerm: nextProductSearchTerm,
    });

    setSubscribeParams({
      ...subscribeParams,
      category: category,
      model: model,
      brand: brand,
    });
  };

  const userSelectCategory = (selProduct) => {
    setSelectCategory(selProduct);
  };

  useEffect(() => {
    if (selectCategory == category) {
      setShowCat("flex");
    } else {
      setShowCat("");
    }
  }, [selectCategory]);

  const RenderBrand = (brand, category) => {
    //   console.log(modelDictionary.data.products[category][brand][brand])
    return (
      <View>
        <Text className="text-xl font-pbold text-black">{brand}</Text>
        <FlatList
          data={modelDictionary.products[category][brand][brand]}
          renderItem={({ item }) => RenderModel(item, brand, category)}
          keyExtractor={(item) => item}
          className="flex flex-row flex-wrap justify-between"
        />
      </View>
    );
  };

  const RenderModel = (model, brand, category) => {
    return (
      <CustomSelector
        title={model}
        customStyle="w-24"
        handleClick={() => {
          userSelectModel(model, brand, category);
        }}
        state={selectProduct}
      />
    );
  };

  return (
    <View className="border-2 rounded-2xl my-2 p-2 ">
      <TouchableOpacity
        className="flex flex-row justify-between"
        onPress={() => {
          userSelectCategory(category);
        }}
      >
        <Text className="text-2xl font-pregular text-primary">{category}</Text>
        <Text className=" font-pregular text-secondary">show</Text>
      </TouchableOpacity>
      <FlatList
        data={categoryBrand.categoryBrands[category]}
        renderItem={({ item }) => RenderBrand(item, category)}
        keyExtractor={(item) => item}
        className={`hidden ${showCat}`}
      />
    </View>
  );
  // return (
  //     <FlatList
  //       data = {categoryBrand.data.categories}
  //       renderItem={({item}) => RenderCategory(item)}
  //       keyExtractor={item => item}
  //       className = "mt-7 mx-3"
  //     ></FlatList>
  // )
};

export default ProductModels;
