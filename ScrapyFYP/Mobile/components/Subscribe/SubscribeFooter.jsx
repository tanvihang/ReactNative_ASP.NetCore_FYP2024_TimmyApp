import { View, Text } from "react-native";
import { React, useEffect, useState, useLayoutEffect } from "react";
import { useGlobalContext } from "../../context/GlobalProvider";
import CustomSelector from "../CustomSelector";
import InputRange from "../InputRange";
import FormField from "../FormField";
import CustomButton from "../CustomButton";
import { router } from "expo-router";
import normalApiCall from "../../hooks/normalApiCall";
import CTAButton from "../Buttons/CTAButton";

const SubscribeFooter = ({ form, setForm, category, brand, model }) => {
  const {
    user,
    spiders,
    condition,
    subscribeParams,
    setSubscribeParams,
    jwtToken,
  } = useGlobalContext();

  const [platform, setPlatform] = useState([]);
  const [pcondition, setPCondition] = useState(null);
  const [notificationType, setNotificationType] = useState(null);
  const [notificationTime, setNotificationTime] = useState(0);
  const [lowPrice, setLowPrice] = useState("");
  const [highPrice, setHighPrice] = useState("");
  const [description, setDescription] = useState("");

  const curParams = {
    subscription_notification_method: "",
    subscription_notification_time: 0,
    category: "",
    brand: "",
    model: "",
    description: "",
    highest_price: 0,
    lowest_price: 0,
    country: "",
    state: "",
    condition: "",
    spider: [],
  };

  // useEffect(()=>{
  //     setSubscribeParams({...subscribeParams, spider: platform})
  // },[platform])

  const selectPlatform = (selItem) => {
    if (platform.indexOf(selItem) !== -1) {
      setPlatform(platform.filter((a) => a !== selItem));
    } else {
      setPlatform([...platform, selItem]);
    }
  };

  const selectPCondition = (selItem) => {
    setPCondition(selItem);
  };

  const selectNotificationType = (selItem) => {
    setNotificationType(selItem);
  };

  const selectNotificationTime = (selItem) => {
    setNotificationTime(selItem);
  };

  const sendUserSubscribeReqeust = async (body) => {
    try {
      const data = await normalApiCall(
        "UserSubscription/AddUserSubscription",
        "POST",
        body,
        { jwtToken: jwtToken }
      );
      router.push("/subscribed");
    } catch (error) {
      console.log(error);
    }
  };

  const searchHandler = () => {
    var low = parseInt(lowPrice);
    var high = parseInt(highPrice);

    setSubscribeParams({
      ...subscribeParams,
      lowest_price: low,
      highest_price: high,
      spider: platform,
      condition: pcondition,
      description: description,
    });

    curParams.brand = subscribeParams.brand;
    curParams.category = subscribeParams.category;
    curParams.model = subscribeParams.model;
    curParams.lowest_price = low;
    curParams.highest_price = high;
    curParams.spider = platform;
    curParams.condition = pcondition;
    curParams.description = description;
    curParams.subscription_notification_method = notificationType;
    curParams.subscription_notification_time = notificationTime;

    sendUserSubscribeReqeust(curParams);
  };

  const goToPage = () => {
    router.push({
      pathname: `/subscribed`,
    });
  };

  return (
    <View  className=" pb-3">
      <View>
        <Text className="font-psemibold text-primary text-2xl text-center mt-3 pt-2">
          Choose Platform
        </Text>
        <View className="flex flex-row justify-around">
          <CustomSelector
            title={spiders[0]}
            customStyle="w-32"
            handleClick={() => {
              selectPlatform(spiders[0]);
            }}
            state={platform}
          />
          <CustomSelector
            title={spiders[1]}
            customStyle="w-32"
            handleClick={() => {
              selectPlatform(spiders[1]);
            }}
            state={platform}
          />
        </View>
      </View>

      <View>
        <Text className="font-psemibold text-primary text-2xl text-center mt-3 pt-2">
          Choose Condition
        </Text>
        <View className="flex flex-row justify-around">
          <CustomSelector
            title={condition[0]}
            customStyle="w-32"
            handleClick={() => {
              selectPCondition(condition[0]);
            }}
            state={pcondition}
          />
          <CustomSelector
            title={condition[1]}
            customStyle="w-32"
            handleClick={() => {
              selectPCondition(condition[1]);
            }}
            state={pcondition}
          />
          <CustomSelector
            title={condition[2]}
            customStyle="w-32"
            handleClick={() => {
              selectPCondition(condition[2]);
            }}
            state={pcondition}
          />
        </View>
      </View>

      <View>
        <Text className="font-psemibold text-primary text-2xl text-center mt-3 pt-2">
          Choose Notification Type
        </Text>
        <View className="flex flex-row justify-around">
          <CustomSelector
            title="email"
            customStyle="w-32"
            handleClick={() => {
              selectNotificationType("email");
            }}
            state={notificationType}
          />
          <CustomSelector
            title="phone (Unavailable)"
            customStyle="w-32"
            handleClick={() => {
              selectNotificationType("phone");
            }}
            state={notificationType}
          />
        </View>
      </View>

      <View>
        <Text className="font-psemibold text-primary text-2xl text-center mt-3 pt-2">
          Choose Notification Time
        </Text>
        <FormField
          title="Notification Time (input 1,2, ...24)"
          value={notificationTime}
          handleChangeText={(e) => {
            // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
            // setForm({...form, productSearchTerm:nextProductSearchTerm})
            var time = parseInt(e);
            setNotificationTime(time);
          }}
          otherStyles="mt-7"
          placeholder="12"
          inputMode="decimal"
        />

        <Text className="font-psemibold text-primary text-2xl text-center mt-3 pt-2">
          Price Range
        </Text>
        <FormField
          title="Lowest Price"
          value={lowPrice}
          handleChangeText={(e) => {
            // var nextProductSearchTerm = {...form.productSearchTerm, lowest_price:e }
            // setForm({...form, productSearchTerm:nextProductSearchTerm})
            setLowPrice(e);
          }}
          otherStyles="mt-7"
          placeholder="0"
          inputMode="decimal"
        />
        <FormField
          title="Highest Price"
          value={highPrice}
          handleChangeText={(e) => {
            // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
            // setForm({...form, productSearchTerm:nextProductSearchTerm})
            setHighPrice(e);
          }}
          otherStyles="mt-7"
          placeholder="20000"
          inputMode="decimal"
        />

        <FormField
          title="Description"
          value={description}
          handleChangeText={(e) => {
            // var nextProductSearchTerm = {...form.productSearchTerm, highest_price:e }
            // setForm({...form, productSearchTerm:nextProductSearchTerm})
            setDescription(e);
          }}
          otherStyles="mt-7"
          placeholder="20000"
        />

        <CTAButton
          title="Subscribe"
          handlePress={() => {
            searchHandler();
          }}
          containerStyles="mt-4"
          textStyles="text-white text-2xl font-pextrabold text-center"
        />

        <CTAButton
          title="No Product? Request One!"
          handlePress={() => {
            router.push("/requestProduct");
          }}
          containerStyles="mt-4"
          textStyles="text-white text-2xl font-pextrabold text-center"
        />
      </View>
    </View>
  );
};

export default SubscribeFooter;
