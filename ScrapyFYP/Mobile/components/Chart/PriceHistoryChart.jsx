import { View, Text, useWindowDimensions } from "react-native";
import React, { useState, useRef, useEffect } from "react";
import * as echarts from "echarts/core";
import { LineChart } from "echarts/charts";
import {
  GridComponent,
  TitleComponent,
  LegendComponent,
} from "echarts/components";
import { SVGRenderer, SkiaChart } from "@wuba/react-native-echarts";
import normalApiCall from "../../hooks/normalApiCall";
import useFetch from "../../hooks/useFetch";
import { color } from "echarts";

echarts.use([
  SVGRenderer,
  LineChart,
  GridComponent,
  TitleComponent,
  LegendComponent,
]);
const PriceHistoryChart = ({ category, brand, model }) => {
  const { height, width } = useWindowDimensions();

  const skiaRef = useRef(null);
  var runOnce = 0;

  const { data, isLoading, error, refetch } = useFetch(
    "PriceHistory/GetProductPriceHistory",
    "GET",
    {},
    {
      category: category,
      brand: brand,
      productName: model,
    }
  );

  useEffect(() => {
    try{
      if (!error && isLoading == false && runOnce == 0) {
        runOnce == 1;
  
        const option = {
          legend: {
            data: data.data.platform,
          },
          xAxis: {
            type: "category",
            data: data.data.priceDate,
          },
          yAxis: {
            type: "value",
            min: data.data.minPrice,
          },
          series: [
            {
              data: data.data.platformPrice[0],
              type: "line",
              name: data.data.platform[0],
              color: "#cbc448",
            },
            {
              data: data.data.platformPrice[1],
              type: "line",
              name: data.data.platform[1],
              color: "#cc0000",
            },
          ],
        };
        let chart;
        if (skiaRef.current) {
          chart = echarts.init(skiaRef.current, "light", {
            renderer: "svg",
            width: width ,
            height: height / 2.5,
          });
          chart.setOption(option);
        }
        return () => chart?.dispose();
    }
  }
    catch(e){
        console.log(e)
    }
  
  }, [isLoading]);

  return (
  <View className = "">
    <SkiaChart ref={skiaRef} />
  </View>

);
};

export default PriceHistoryChart;
