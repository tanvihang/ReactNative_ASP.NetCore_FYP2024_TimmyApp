<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router';
import apiCall from '@/api/apiCall'
import HeaderComp from './components/header/HeaderComp.vue';
import AsideComp from './components/aside/AsideComp.vue';

</script>

<template>
  <div class="flex flex-col w-screen">
    <HeaderComp />

    <div class="flex flex-row h-full pt-4 ">
      <AsideComp class="  w-1/6"/>

      <div class=" w-5/6">
        <RouterView />
      </div>
    </div>

  </div>
</template>


<style scoped></style>
