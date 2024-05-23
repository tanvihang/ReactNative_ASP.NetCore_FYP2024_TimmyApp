import { View, Text, FlatList, Animated } from "react-native";
import { React, useRef, useState } from "react";
import onboardingSlides from "../../constants/onboardingSlides";
import { SafeAreaView } from "react-native-safe-area-context";
import OnboardingItem from "./OnboardingItem";
import Paginator from "./Paginator";

const Onboarding = () => {
  // scrolling reference
  const scrollXX = useRef(new Animated.Value(0)).current;
  const [currentIndex, setCurrentIndex] = useState(0);
  const slidesRef = useRef(null);

  const viewableItemsChanged = useRef(({ viewableItems }) => {
    setCurrentIndex(viewableItems[0].index);
  }).current;

  const viewConfig = useRef({ viewAreaCoveragePercentThreshold: 50 }).current;


  return (
    <View className="h-screen flex">
      <View className=" flex-grow-[4]">
        <FlatList
          data={onboardingSlides}
          renderItem={({ item }) => <OnboardingItem item={item} />}
          horizontal
          showsHorizontalScrollIndicator={false}
          pagingEnabled
          bounces={false}
          keyExtractor={(item) => item.id}
          //Map the scrolling event to ref
          onViewableItemsChanged={viewableItemsChanged}
          viewabilityConfig={viewConfig}
          scrollEventThrottle={32}
          ref={slidesRef}
        />
      </View>

      <Animated.View className="flex flex-grow items-center justify-center">
        <Paginator data={onboardingSlides} scrollX={scrollXX} />
      </Animated.View>
    </View>
  );
};

export default Onboarding;
