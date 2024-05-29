import { View, Text, ScrollView } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import SearchHero from "../../components/Search/SearchHero";
import CategoryBrand from "../../components/CategoryBrand/CategoryBrand";
import SearchFooter from "../../components/Search/SearchFooter";

const searchV1 = () => {
  return (
    <SafeAreaView>
      <ScrollView className="px-3">
        <SearchHero />
        <CategoryBrand />
        <SearchFooter />
      </ScrollView>
    </SafeAreaView>
  );
};

export default searchV1;
