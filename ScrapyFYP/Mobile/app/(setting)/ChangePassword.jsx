import { View, Text, Alert } from "react-native";
import React, { useState } from "react";
import FormField from "../../components/FormField";
import CTAButton from "../../components/Buttons/CTAButton";
import { useGlobalContext } from "../../context/GlobalProvider";
import normalApiCall from "../../hooks/normalApiCall";

const ChangePassword = () => {
  const { user } = useGlobalContext();

  const [form, setForm] = useState({
    newPassword: "",
    verificationCode: "",
    userEmail: user.userEmail,
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
      Alert.alert("Success", data.message)
      setIsSubmittingCode(false);
    } catch (error) {
      Alert.alert("Error", error.message);
      setIsSubmittingCode(false);
    }
  };

  const sendResetPassword = async (form) => {
    try {
      console.log(form)
      setIsSubmitting(true);
      const data = await normalApiCall("User/ResetPassword", "POST", form, {});
      Alert.alert("Success", data.message)
      setIsSubmitting(false);
    } catch (error) {
      Alert.alert("Error", error.message);
      setIsSubmitting(false);
    }
  };

  return (
    <View className>
      <Text className="w-full text-center pt-10 font-pbold text-5xl">
        Change Password
      </Text>

      <View className = "mx-3">
        <View className = "flex flex-row">
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
            title="Password"
            value={form.newPassword}
            handleChangeText={(e) => setForm({ ...form, newPassword: e })}
            otherStyles="mt-7"
            placeholder="password"
            />

            <CTAButton
            title="RESET PASSWORD"
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

export default ChangePassword;
