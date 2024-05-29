<template>
    <div class=" flex flex-col min-w-52 h-full">

        <el-radio-group v-model="isCollapse" class="pl-4 pb-4">
            <el-radio-button :value="false">expand</el-radio-button>
            <el-radio-button :value="true">collapse</el-radio-button>
        </el-radio-group>

        <el-menu default-active="1" class="el-menu-vertical-demo flex-grow outline-cyan-500" :collapse="isCollapse"
            @open="handleOpen" @close="handleClose">

            <el-menu-item index="1" @click="goToHome">
                <el-icon>
                    <HomeFilled />
                </el-icon>
                <template #title>Home</template>
            </el-menu-item>

            <el-sub-menu index="2">
                <template #title>
                    <el-icon>
                        <Management />
                    </el-icon>
                    <span>Management</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="1-1" @click="goToUserManagement">User</el-menu-item>
                    <el-sub-menu index ="1-2">
                        <template #title>Timmy Product</template>
                        <el-menu-item index="1-2-1" @click="goToAdoptTimmyProductManagement">Adopt</el-menu-item>
                        <el-menu-item index="1-2-1" @click="goToExistingTimmyProductManagement">Existing</el-menu-item>

                    </el-sub-menu>
                    <el-menu-item index="1-3" @click="goToElasticProductManagement">Elastic Product</el-menu-item>
                </el-menu-item-group>
            </el-sub-menu>

            <el-menu-item index="3" @click="goToAbout">
                <el-icon>
                    <Avatar />
                </el-icon>
                <template #title>About</template>
            </el-menu-item>

            <el-menu-item index="4" disabled>
                <el-icon>
                    <WarnTriangleFilled />
                </el-icon>
                <template #title>Coming Soon</template>
            </el-menu-item>

        </el-menu>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import {
    WarnTriangleFilled,
    Avatar,
    HomeFilled,
    Management
} from '@element-plus/icons-vue'
import { useRouter } from 'vue-router';

const router = useRouter()
const isCollapse = ref(false)

const handleOpen = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}

const handleClose = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}

const goToHome = () => {
    router.push("/")
}

const goToUserManagement = () => {
    router.push("/manage/user")
}

const goToAdoptTimmyProductManagement = () => {
    router.push("/manage/timmyproduct/adopt")
}

const goToExistingTimmyProductManagement = () => {
    router.push("/manage/timmyproduct/existing")
} 

const goToElasticProductManagement = () => {
    router.push("/manage/elasticproduct")
}

const goToAbout = () => {
    router.push("/about")
}

</script>

<style>
.el-menu-vertical-demo:not(.el-menu--collapse) {
    width: 200px;
}

</style>