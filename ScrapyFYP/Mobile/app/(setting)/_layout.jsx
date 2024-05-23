import { View, Text } from "react-native";
import React from "react";
import { Slot, Stack } from "expo-router";
import { SafeAreaView } from "react-native-safe-area-context";

const SettingLayout = () => {
  return (
    <SafeAreaView>
      <Stack.Screen options={{ header: () => null }} />
      <Slot />
    </SafeAreaView>
  );
};

export default SettingLayout;
