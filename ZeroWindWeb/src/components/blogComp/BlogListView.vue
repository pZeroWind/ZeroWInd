<script lang="ts" setup>
import { reactive, ref } from "@vue/reactivity";
import { Blog, BlogModel, SearchModel } from "../../apis/blogApi";
import { Type, OptionModel } from "../../apis/typeApi";
import { Tag } from "../../apis/tagsApi";
import { PageModel, ResultModel } from "../../plugins/axiosConfig";
import { GetDate, ClearHtml } from "../../plugins/Tool";
import BlogItem from "../toolComp/BlogItem.vue";
import SelectBox from "./SelectBox.vue";
//注入Api
const blogApi = new Blog()
const typeApi = new Type()
const tagsApi = new Tag()
//博客列表
const list: Array<BlogModel> = reactive([])
//加载状态
const load = ref(true)
//当前搜索状态
const srcModel: SearchModel = reactive({
    page: 1,
    size: 3,
    type: 0,
    order: 0,
    search: "",
    tags: []
})


//总数
const count = ref(1)

//标签
const tags: OptionModel[] = reactive([])
const loading = ref(false)
const GetTags = (val: string) => {
    tags.length = 0
    loading.value = true
    tagsApi.Get(val, (res: ResultModel<OptionModel[]>) => {
        loading.value = false
        tags.push(...res.data)
    })
} 
GetTags("")

//分区类型
const opType: OptionModel[] = reactive([
    {
        value: 0,
        label:"全部"
    }
])
typeApi.Get((res: ResultModel<OptionModel[]>) => {
    opType.push(...res.data)
})

//排序
const orderType: OptionModel[] = reactive([
    {
        value: 0,
        label: "更新-正序"
    },
    {
        value: 1,
        label: "更新-倒序"
    },
    {
        value: 2,
        label: "发布-正序"
    },
    {
        value: 3,
        label: "发布-倒序"
    }
])

//获取博客列表
const GetList = () => {
    //开始加载
    load.value = true
    //清空列表
    list.length = 0
    //异步获取列表
    blogApi.get(srcModel, (res: ResultModel<PageModel<BlogModel>>) => {
        load.value = false
        list.push(...res.data.data)
        srcModel.page = res.data.page
        count.value = res.data.count
    })
}
//翻页
const PageChange = (page: number) => {
    srcModel.page = page
    GetList()
}

const SelList = [
    {
        id: 1,
        value: "1",
        label: "1",
        selected:false
    },
    {
        id: 2,
        value: "12",
        label: "12",
        selected: false
    }
]

//默认加载
GetList()
</script>

<template>
    <div v-loading="load" class="list">
        <div class="list-search">
            <select-box :list="SelList"></select-box>
            <div class="list-search-model">
                <el-select v-model="srcModel.type" placeholder="分区" style="width: 75px;height: 35px;">
                    <el-option v-for="it in opType" :key="it.value" :label="it.label" :value="it.value" />
                </el-select>
                <el-select v-model="srcModel.order" placeholder="排序" style="width: 110px;height: 35px;margin: 0 5px;">
                    <el-option v-for="it in orderType" :key="it.value" :label="it.label" :value="it.value" />
                </el-select>
                <el-input v-model="srcModel.search" placeholder="标题查询"
                    style="width: 400px;height: 35px;margin: 0 5px;" />
                <button @click="GetList">查询</button>
            </div>
        </div>
        <el-select v-model="srcModel.tags" multiple placeholder="标签查询" style="height: 35px;margin: 0 5px;" allow-create
            filterable default-first-option :multiple-limit="5" :loading="loading" :remote-method="GetTags" remote>
            <el-option v-for="it in tags" :key="it.value" :label="it.label" :value="it.value" />
        </el-select>
        <div class="list-box">
            <blog-item v-for="it in list" :key="it.id" :title="it.title" :id="it.id" :context="ClearHtml(it.context)"
                :create-time="GetDate(it.createTime)" :update-time="GetDate(it.updateTime)" :tags="it.tags"
                :path="'/blog/'" />
        </div>
        <div class="list-pager">
            <el-pagination background layout="prev, pager, next" :total="count" :page-size="srcModel.size"
                @current-change="PageChange" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
    .list{
        display: flex;
        flex-direction: column;
        margin: 0px 5px;
        min-height: 600px;
        &-box{
            flex: 1
        }
        &-pager{
            display: flex;
            justify-content: center;
            align-items: center;
        }
        &-search{
            margin: 5px;
            display: flex;
            button{
                cursor: pointer;
                padding: 7px 20px;
                margin: 0 5px;
                border: none;
                font-weight: bold;
                background-color: #fff;
                box-shadow: 0 0 5px #ddd;
                border-radius: 5px;
                transition: all .25s;
                &:hover{
                    box-shadow: 0 0 5px #aaa;
                }
            }
            &-model{
                display: flex;
                .el-input{
                    width: 200px;
                }
            }
        }
    }
</style>