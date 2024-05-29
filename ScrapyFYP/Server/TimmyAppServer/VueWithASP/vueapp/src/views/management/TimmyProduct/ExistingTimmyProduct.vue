<template>
    <div>

        <el-button @click="clearFilter">reset all filters</el-button>
        <el-table ref="tableRef" row-key="timmyProductFullName" :data="data" style="width: 100%">
            <el-table-column prop="timmyProductFullName" label="Timmy Product" width="250"
                column-key="timmyProductFullName" />

            <el-table-column prop="timmyProductCategory" label="Category" width="180" column-key="timmyProductCategory"
                :filter-multiple=false :filters=categoryFilter :filter-method="categoryFilterHandler" />

            <el-table-column prop="timmyProductBrand" label="Brand" width="180" column-key="timmyProductBrand"
                :filter-multiple=false :filters=brandFilter :filter-method="brandFilterHandler" />

            <el-table-column prop="timmyProductModel" label="Model" width="200" column-key="timmyProductModel" />

            <el-table-column fixed="right" label="Operations" width="100">
                <template #default="scope">
                    <el-popconfirm title="Are you sure to delete this product?"
                        @confirm="deleteTimmyProductHandler(scope.$index, scope.row)">
                        <template #reference>
                            <el-button link type="danger" size="small">
                                Remove
                            </el-button>
                        </template>
                    </el-popconfirm>
                </template>
            </el-table-column>

        </el-table>
        <el-pagination v-model:current-page="currentPage" v-model:page-size="pageSize" :page-sizes="[10, 20, 30]"
            :small="small" :disabled="disabled" :background="background" layout="sizes, prev, pager, next" :total="150"
            @size-change="handleSizeChange" @current-change="handleCurrentChange" />
    </div>

</template>

<script setup lang="ts">
import { ref, onMounted, reactive, watch } from 'vue'
import { type TableInstance } from 'element-plus'
import apiCall from '@/api/apiCall'
import { ElMessage } from 'element-plus'

interface User {
    date: string
    name: string
    address: string
    tag: string
}

var categoryBrandData
const tableRef = ref<TableInstance>()
const currentPage = ref(1)
const pageSize = ref(10)
const small = ref(false)
const background = ref(false)
const disabled = ref(false)
const categoryFilter = ref([])
const brandFilter = ref([])
const data = ref(null)
const dialogFormVisible = ref(false)
const selectedBrand = ref("")
const selectedCategory = ref("")

const form = reactive({
    category: "string",
    brand: "string",
    model: "string"
})

const clearFilter = () => {
    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    // @ts-expect-error
    tableRef.value!.clearFilter()
    selectedBrand.value = ""
    selectedCategory.value = ""
    getAdoptedTimmyProductPagination()

}

watch(selectedCategory, (newValue) => {
    getAdoptedTimmyProductPagination()
})

watch(selectedBrand, (newValue) => {
    console.log("Brand Updated " + newValue)
    getAdoptedTimmyProductPagination()
})

const handleSizeChange = () => {

    getAdoptedTimmyProductPagination()

}
const handleCurrentChange = () => {

    getAdoptedTimmyProductPagination()
}

const brandFilterHandler = (value, row, column) => {
    const property = column['property']
    selectedBrand.value = value
    return row[property] === value
}

const categoryFilterHandler = async (value, row, column) => {
    const property = column['property']
    selectedCategory.value = value

    brandFilter.value = []
    categoryBrandData.data.categoryBrands[value].forEach(brand => {
        var categoryObj = {
            text: brand,
            value: brand
        }

        brandFilter.value.push(categoryObj)
    });
    return row[property] === value
}

const getAdoptedTimmyProductPagination = async () => {

    // console.log(selectedCategory.value)
    // console.log(selectedBrand.value)
    // console.log(currentPage.value)
    // console.log(pageSize.value)

    const response = await apiCall("TimmyProduct/GetAdoptedTimmyProductPagination", "POST", { pageSize: pageSize.value, currentPage: currentPage.value }, {
        category: selectedCategory.value,
        brand: selectedBrand.value
    });

    // console.log(response.data)

    if (response.statusCode == 200 && response.data.count != 0) {
        data.value = response.data.rows
        // console.log("setted data")
    } else {
        data.value = []
    }
}

const deleteTimmyProductHandler = async (index, row) => {
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

onMounted(async () => {
    try {
        categoryBrandData = await apiCall("TimmyProduct/GetCategoryBrandList", "GET", {}, {})
        // Set category filter lists
        categoryBrandData.data.categories.forEach(category => {
            var categoryObj = {
                text: category,
                value: category
            }

            categoryFilter.value.push(categoryObj)
        });

        getAdoptedTimmyProductPagination()
    } catch (e) {
        console.log(e)
    }

})

</script>

<style lang="scss" scoped></style>