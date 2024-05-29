<template>
  <div class="flex">
    <div class="flex flex-col items-center">
      <div class="echart-box" id="categoryChart"></div>
      <p class=" font-bold text-2xl">Total items {{categoryCount}}</p>
    </div>
  
    <div class="flex flex-col items-center">
      <div class="echart-box" id="brandChart"></div>
      <p class=" font-bold text-2xl">Category: {{choosenCategory}}</p>
      <p class=" font-bold text-2xl">Brand: {{choosenBrand}}</p>
    </div>
  </div>
  <div class="echart-box-1 flex items-center justify-center" id="modelChart"></div>
</template>

<script setup>
import { defineComponent, onMounted, ref } from 'vue';
import * as echarts from 'echarts'
import apiCall from '@/api/apiCall'
import {
  TitleComponent,
  TooltipComponent,
  LegendComponent
} from 'echarts/components';
import { PieChart } from 'echarts/charts';
import { LabelLayout } from 'echarts/features';
import { CanvasRenderer } from 'echarts/renderers';

echarts.use([
  TitleComponent,
  TooltipComponent,
  LegendComponent,
  PieChart,
  CanvasRenderer,
  LabelLayout
]);

const categoryData = ref(null)
const brandData = ref(null)
const modelData = ref(null)
const categoryCount = ref(0)
const choosenCategory = ref("mobile")
const choosenBrand = ref("apple")



const getCategoriesData = async () => {
  var count = 0
  const response = await apiCall("ElasticSearch/GetElasticProductCategoriesCount", "GET", {}, {})
  categoryData.value = response.data
  categoryData.value.forEach(element => {
    count += element.value
  });
  categoryCount.value = count
  showEcharts()
}

const getBrandData = async (category) => {
  console.log(category)
  const response = await apiCall("ElasticSearch/GetElasticProductBrandCount", "POST", {}, {category: category})
  brandData.value = response.data
  showEchartsBrand()
}

const getModelData = async(category, brand) => {
  console.log("Getting model data")
  console.log(category)
  console.log(brand)
  const response = await apiCall("ElasticSearch/GetElasticProductModelCount", "POST", {}, {category: category, brand: brand})
  modelData.value = response.data
  console.log(modelData.value)
  showEchartsModel()
}

const showEcharts = async () => {
  const chartDom = document.querySelector("#categoryChart")
  const myChart = echarts.init(chartDom)

  const option = {
    title: {
      text: 'ElasticProduct',
      subtext: 'category counts',
      left: 'center'
    },
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: 'Access From',
        type: 'pie',
        radius: '50%',
        data: categoryData.value,
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  };

  myChart.setOption(option)

  myChart.on('click', function(params){
    choosenCategory.value = params.name
    getBrandData(params.name)
  })

  getBrandData(choosenCategory.value)
  getModelData(choosenCategory.value, choosenBrand.value)
  showEchartsBrand()
}

const showEchartsBrand = async () => {
  const chartDom = document.querySelector("#brandChart")
  const myChart = echarts.init(chartDom)

  const option = {
    title: {
      text: 'Brand ',
      subtext: 'item counts',
      left: 'center'
    },
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: 'Access From',
        type: 'pie',
        radius: '50%',
        data: brandData.value,
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  };

  myChart.setOption(option)

  myChart.on('click', function(params){
    choosenBrand.value = params.name
    getModelData(choosenCategory.value,choosenBrand.value)
  })
  
}

const showEchartsModel = async () => {
  const chartDom = document.querySelector("#modelChart")
  const myChart = echarts.init(chartDom)

  const option = {
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'shadow'
    }
  },
  grid: {
    left: '3%',
    right: '4%',
    bottom: '3%',
    containLabel: true
  },
  xAxis: [
    {
      type: 'category',
      data: modelData.value.data,
      axisTick: {
        alignWithLabel: true
      }
    }
  ],
  yAxis: [
    {
      type: 'value'
    }
  ],
  series: [
    {
      name: 'Model name',
      type: 'bar',
      barWidth: '30%',
      data: modelData.value.value
    }
  ]
};

  myChart.setOption(option)
}
onMounted(() => {
  getCategoriesData()
})

</script>

<style scoped>
.echart-box{
  width: 500px;
  height: 300px;
  margin: 10px;
}
.echart-box-1{
  height: 300px;
  margin: 10px;
}
</style>