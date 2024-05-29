import { createRouter, createWebHashHistory, type RouteRecordRaw } from 'vue-router'
import HomeView from '../views/home/HomeView.vue'
import AboutView from '../views/about/AboutView.vue'
import UserManagement from '../views/management/UserManagement.vue'
import AdoptTimmyProduct from '../views/management/TimmyProduct/AdoptTimmyProduct.vue'
import ExistingTimmyProduct from '../views/management/TimmyProduct/ExistingTimmyProduct.vue'
import ElasticProductManagement from '../views/management/ElasticProductManagement.vue'


const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    component: AboutView
  },
  {
    path: '/manage/user',
    name: 'usermanagement',
    component: UserManagement
  },
  {
    path: '/manage/timmyproduct/adopt',
    name: 'adopttimmyproduct',
    component: AdoptTimmyProduct
  },
  {
    path: '/manage/timmyproduct/existing',
    name: 'existingtimmyproduct',
    component: ExistingTimmyProduct
  },
  {
    path: '/manage/elasticproduct',
    name: 'elasticproductmanagement',
    component: ElasticProductManagement
  }
]



const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
