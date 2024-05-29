import { View, Text, FlatList } from "react-native";
import React from "react";
import { useGlobalContext } from "../../context/GlobalProvider";
import PersonalInfoItem from "../../components/Setting/PersonalInfoItem";

const PersonalInformation = () => {
  const { user } = useGlobalContext();

  return (
    <View className = "h-full w-full flex items-start">
      <Text className="w-full text-center pt-10 font-pbold text-5xl">
        Personal Information
      </Text>

      <PersonalInfoItem title="UserId" item={user.userId} />
      <PersonalInfoItem title="UserName" item={user.userName} />
      <PersonalInfoItem title="UserEmail" item={user.userEmail} />
      <PersonalInfoItem title="UserPhoneNo" item={user.userPhoneNo} />
      <PersonalInfoItem title="Joined Date" item={user.userRegisterDate} />
      <PersonalInfoItem title="User Level" item={user.userLevel} />
    </View>
  );
};

export default PersonalInformation;
