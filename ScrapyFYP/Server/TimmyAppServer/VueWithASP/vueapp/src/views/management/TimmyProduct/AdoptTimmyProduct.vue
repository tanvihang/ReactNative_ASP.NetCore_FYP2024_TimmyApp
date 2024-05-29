<template>
    <div>
        <div class=" ">
            <el-button type="success" @click="handleAddTimmy">Add Timmy Product</el-button>

            <h1 class=" text-lg font-bold pt-5">UnAdopted TimmyProduct</h1>
            <el-table :data="data">
                <el-table-column fixed prop="timmyProductFullName" label="Full name" width="200" />
                <el-table-column prop="timmyProductCategory" label="Category" width="120" />
                <el-table-column prop="timmyProductBrand" label="Brand" width="200" />
                <el-table-column prop="timmyProductModel" label="Model" width="120" />
                <el-table-column prop="timmyProductSubModel" label="Sub Model" width="150" />
                <el-table-column prop="timmyProductAdopted" label="Adopted" width="120" />

                <el-table-column fixed="right" label="Operations" width="120">
                    <template #default="scope">
                        <el-popconfirm title="Are you sure to delete this UnAdopted product?"
                            @confirm="deleteUnAdoptedHandler(scope.$index, scope.row)">
                            <template #reference>
                                <el-button link type="danger" size="small">
                                    Remove
                                </el-button>
                            </template>
                        </el-popconfirm>
                        <el-button link type="success" size="small"
                            @click="editUnAdoptedHandler(scope.$index, scope.row)">Edit</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination v-model:current-page="currentPage" v-model:page-size="pageSize" :page-sizes="[10, 20, 30]"
                :small="small" :disabled="disabled" :background="background" layout="sizes, prev, pager, next"
                :total="100" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
        </div>

        <el-dialog v-model="dialogFormVisible" title="Change TimmyProduct info" width="500">
            <el-form :model="form">
                <el-form-item label="Category" :label-width="120">
                    <el-input v-model="form.timmyProductCategory" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Brand" :label-width="120">
                    <el-input v-model="form.timmyProductBrand" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Model" :label-width="120">
                    <el-input v-model="form.timmyProductModel" autocomplete="off" />
                </el-form-item>
            </el-form>
            <template #footer>
                <div class="dialog-footer">
                    <el-button @click="dialogFormVisible = false">Cancel</el-button>
                    <el-button type="primary" @click="handleConfirmEditChange">
                        Save And Adopt Product
                    </el-button>
                </div>
            </template>
        </el-dialog>
    

        <el-dialog v-model="dialogFormAddVisible" title="Add TimmyProduct" width="500">
            <el-form :model="formAdd">
                <el-form-item label="Category" :label-width="120">
                    <el-input v-model="formAdd.category" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Brand" :label-width="120">
                    <el-input v-model="formAdd.brand" autocomplete="off" />
                </el-form-item>
                <el-form-item label="Model" :label-width="120">
                    <el-input v-model="formAdd.model" autocomplete="off" />
                </el-form-item>
            </el-form>
            <template #footer>
                <div class="dialog-footer">
                    <el-button @click="dialogFormAddVisible = false">Cancel</el-button>
                    <el-button type="primary" @click="handleConfirmAddProduct">
                        Add Product
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
const dialogFormAddVisible = ref(false)

const handleSizeChange = () => {
    getUnAdoptedTimmyProductPagination()
}
const handleCurrentChange = () => {
    getUnAdoptedTimmyProductPagination()
}
const handleAddTimmy = () => {
    dialogFormAddVisible.value = true
}


const form = reactive({
    timmyProductFullName: "mobile huawei mate 50",
    timmyProductCategory: "mobile",
    timmyProductBrand: "huawei",
    timmyProductModel: "mate 50",
    timmyProductSubModel: "huawei",
    timmyProductAdopted: 0
})

const formAdd = reactive({
    category: "",
    brand: "",
    model: "",
    subModel: "",
    adopt: 1
})

const handleConfirmAddProduct = async() => {
    dialogFormAddVisible.value = false
    formAdd.subModel = formAdd.model
    
    const response = await apiCall("TimmyProduct/AddTimmyProduct", "POST", formAdd, {})
    if (response.statusCode == 200) {
            ElMessage({
                message: 'Add successfully.',
                type: 'success',
            })
        } else {
            ElMessage.error(response.message)
        }
}

const getUnAdoptedTimmyProductPagination = async () => {
    const response = await apiCall("TimmyProduct/GetUnAdoptedTimmyProductPagination", "POST", { pageSize: pageSize.value, currentPage: currentPage.value }, {})

    console.log(response)
    if (response.statusCode == 200 && response.data.count != 0) {
        data.value = response.data.rows
    } else {
        data.value = []
    }
}

const deleteUnAdoptedHandler = async (index, row) => {
    try {
        const response = await apiCall("TimmyProduct/DeleteTimmyProduct", "POST", {
            category: row.timmyProductCategory,
            brand: row.timmyProductBrand,
            model: row.timmyProductModel
        }, {})
        if (response.statusCode == 200) {
            ElMessage({
                message: 'Delete successfully.',
                type: 'success',
            })
        } else {
            ElMessage.error(response.message)
        }
    }
    catch (e) {
        ElMessage.error(e.message)
    }

}

const editUnAdoptedHandler = (index, row) => {
    dialogFormVisible.value = true

    form.timmyProductFullName = row.timmyProductFullName
    form.timmyProductCategory = row.timmyProductCategory
    form.timmyProductBrand = row.timmyProductBrand
    form.timmyProductModel = row.timmyProductModel
    form.timmyProductSubModel = row.timmyProductSubModel
    form.timmyProductAdopted = row.timmyProductAdopted
}

const handleConfirmEditChange = async () => {
    try {
        dialogFormVisible.value = false
        const response = await apiCall("TimmyProduct/AdoptTimmyProduct", "POST", {
            category: form.timmyProductCategory,
            brand: form.timmyProductBrand,
            model: form.timmyProductModel
        }, {})

        console.log(response)
        if (response.statusCode == 200) {
            ElMessage({
                message: 'Save successfully.',
                type: 'success',
            })
        } else {
            ElMessage.error(response.message)
        }
    } catch (e) {
        ElMessage.error(e.message)
    }

}

onMounted(async () => {
    getUnAdoptedTimmyProductPagination()

})
</script>

<style lang="scss" scoped></style>