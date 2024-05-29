<template>
    <div class="">
        User Management
        <div class=" ">
            <el-table :data="data">
                <el-table-column fixed prop="userId" label="UserId" width="200" />
                <el-table-column prop="userName" label="UserName" width="120" />
                <el-table-column prop="userEmail" label="UserEmail" width="200" />
                <el-table-column prop="userLevel" label="UserLevel" width="120" />
                <el-table-column prop="userRegisterDate" label="UserRegisterDate" width="150" />
                <el-table-column prop="userPhoneNo" label="UserPhoneNo" width="120" />
                <el-table-column fixed="right" label="Operations" width="120">
                    <template #default="scope">
                        <el-popconfirm title="Are you sure to delete this user?"
                            @confirm="deleteUserHandler(scope.$index, scope.row)">
                            <template #reference>
                                <el-button link type="danger" size="small">
                                    Remove
                                </el-button>
                            </template>
                        </el-popconfirm>
                        <el-button link type="primary" size="small"
                            @click="editUserHandler(scope.$index, scope.row)">Edit</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination v-model:current-page="currentPage" v-model:page-size="pageSize" :page-sizes="[10, 20, 30]"
                :small="small" :disabled="disabled" :background="background" layout="sizes, prev, pager, next"
                :total="100" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
        </div>

        <el-dialog v-model="dialogFormVisible" title="Change user info" width="500">
            <el-form :model="form">
                <el-form-item label="User Name" :label-width="120">
                    <el-input v-model="form.newUserName" autocomplete="off" />
                </el-form-item>
                <el-form-item label="User Email" :label-width="120">
                    <el-input v-model="form.newUserEmail" autocomplete="off" />
                </el-form-item>
                <el-form-item label="User Phone No" :label-width="120">
                    <el-input v-model="form.newPhoneNo" autocomplete="off" />
                </el-form-item>
            </el-form>
            <template #footer>
                <div class="dialog-footer">
                    <el-button @click="dialogFormVisible = false">Cancel</el-button>
                    <el-button type="primary" @click="handleConfirmEditChange">
                        Confirm
                    </el-button>
                </div>
            </template>
        </el-dialog>
    </div>
</template>

<script setup>
import { ref, onMounted, reactive } from 'vue'
import apiCall from '@/api/apiCall'
import { ElMessage } from 'element-plus'

const currentPage = ref(1)
const pageSize = ref(10)
const small = ref(false)
const background = ref(false)
const disabled = ref(false)
const data = ref(null)
const dialogFormVisible = ref(false)

const handleSizeChange = () => {
    getUserPagination()
}
const handleCurrentChange = () => {
    getUserPagination()
}

const form = reactive({
    newUserName: '',
    newUserEmail: '',
    email: '',
    newPhoneNo: '',
    userId: ''
})

const getUserPagination = async () => {
    const response = await apiCall("User/GetUserPagination", "POST", { pageSize: pageSize.value, currentPage: currentPage.value }, {})

    console.log(response)

    if (response.statusCode == 200 && response.data.count != 0) {
        data.value = response.data.rows
    } else {
        data.value = []
    }
}

const deleteUserHandler = async(index, row) => {
    try{
        const response = await apiCall("User/DeleteUserAdmin","POST",{},{userId:row.userId})        
        if (response.statusCode == 200) {
            ElMessage({
                message: 'Delete successfully.',
                type: 'success',
            })
        }else{
            ElMessage.error(response.message)
        }
    }
    catch(e){
        ElMessage.error(e.message)
    }

}

const editUserHandler = (index, row) => {
    console.log(row)
    form.newUserName = row.userName
    form.newUserEmail = row.userEmail
    form.email = row.userEmail
    form.newPhoneNo = row.userPhoneNo
    form.userId = row.userId
    dialogFormVisible.value = true
}

const handleConfirmEditChange = async () => {
    try{
        dialogFormVisible.value = false
        const response = await apiCall("User/ChangeUserInfoAdmin", "POST", form, {})
        
        console.log(response)
        if (response.statusCode == 200) {
            ElMessage({
                message: 'Edit successfully.',
                type: 'success',
            })
        }else{
            ElMessage.error(response.message)
        }
    }catch(e){
        ElMessage.error(e.message)
    }

}

onMounted(async () => {
    getUserPagination()

})
</script>

<style lang="scss" scoped></style>