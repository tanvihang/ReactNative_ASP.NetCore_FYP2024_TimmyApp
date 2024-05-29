import { View, Text, Alert } from "react-native";
import React, { useState } from "react";
import FormField from "../../components/FormField";
import CTAButton from "../../components/Buttons/CTAButton";
import { useGlobalContext } from "../../context/GlobalProvider";
import normalApiCall from "../../hooks/normalApiCall";

const ChangePersonalInfo = () => {
  const { user, jwtToken, setUser } = useGlobalContext();

  const [form, setForm] = useState({
    newUserName: user.userName,
    verificationCode: "",
    Email: user.userEmail,
    newUserEmail: user.userEmail,
    newPhoneNo: user.userPhoneNo,
  });

  const [isSubmitting, setIsSubmitting] = useState(false);

  const [isSubmittingCode, setIsSubmittingCode] = useState(false);

  const sendCode = async (email) => {
    try {
      setIsSubmittingCode(true);
      const data = await normalApiCall(
        "UserVerificationCode/SendVerificationCode",
        "GET",
        {},
        { email: email }
      );
      Alert.alert("Success", data.message);
      setIsSubmittingCode(false);
    } catch (error) {
      Alert.alert("Error", error.message);
      setIsSubmittingCode(false);
    }
  };

  const sendResetPassword = async (form) => {
    try {
      console.log(user);
      console.log(form);
      console.log(jwtToken);
      setIsSubmitting(true);
      const data = await normalApiCall("User/ChangeUserInfo", "POST", form, {
        jwtToken: jwtToken,
      });
      var newUser = user;
      newUser.userEmail = form.newUserEmail
      newUser.userName = form.newUserName
      newUser.userPhoneNo = form.newPhoneNo
      setUser(newUser)
      Alert.alert("Success", "Changed user info!");
      setIsSubmitting(false);
    } catch (error) {
      Alert.alert("Error", error.message);
      setIsSubmitting(false);
    }
  };
  return (
    <View className>
      <Text className="w-full text-center pt-10 font-pbold text-5xl">
        Change Personal Info
      </Text>

      <View className="mx-3">
        <View className="flex flex-row">
          <FormField
            title="Verification Code"
            value={form.verificationCode}
            handleChangeText={(e) => setForm({ ...form, verificationCode: e })}
            otherStyles="w-2/3"
            placeholder="verification code"
          />

          <View className="w-1/3">
            <CTAButton
              title="GET CODE"
              handlePress={() => {
                sendCode(user.userEmail);
              }}
              containerStyles="mt-7 ml-2 w-full"
              textStyles="text-white-100"
              className="text-white-100"
              isLoading={isSubmittingCode}
            />
          </View>
        </View>

        <View>
          <FormField
            title="UserName"
            value={form.newUserName}
            handleChangeText={(e) => setForm({ ...form, newUserName: e })}
            otherStyles="mt-7"
            placeholder="userName"
          />

          <FormField
            title="Email"
            value={form.newUserEmail}
            handleChangeText={(e) => setForm({ ...form, newUserEmail: e })}
            otherStyles="mt-7"
            placeholder="userEmail"
          />

          <FormField
            title="Phone No"
            value={form.newPhoneNo}
            handleChangeText={(e) => setForm({ ...form, newPhoneNo: e })}
            otherStyles="mt-7"
            placeholder="phoneNo"
          />

          <CTAButton
            title="SAVE CHANGE"
            handlePress={() => {
              sendResetPassword(form);
            }}
            containerStyles="mt-7"
            textStyles="text-white-100"
            isLoading={isSubmitting}
          />
        </View>
      </View>
    </View>
  );
};

export default ChangePersonalInfo;
