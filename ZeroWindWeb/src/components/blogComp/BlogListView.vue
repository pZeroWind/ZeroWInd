<script lang="ts" setup>
import { reactive, ref } from "@vue/reactivity";
import { Blog, BlogModel, SearchModel } from "../../apis/blogApi";
import { Type, OptionModel } from "../../apis/typeApi";
import { Tag } from "../../apis/tagsApi";
import { PageModel, ResultModel } from "../../plugins/axiosConfig";
import { GetDate, ClearHtml } from "../../plugins/Tool";
import BlogItem from "../toolComp/BlogItem.vue";
import SelectBox from "./SelectBox.vue";
import TagListBox from "./TagListBox.vue";
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
        label: "全部",
        selected: true
    }
])
typeApi.Get((res: ResultModel<OptionModel[]>) => {
    opType.push(...res.data)
})

//排序
const orderType: OptionModel[] = reactive([
    {
        value: 0,
        label: "最近更新",
        selected: true
    },
    {
        value: 2,
        label: "最新发布",
        selected: false
    },
    {
        value: 3,
        label: "最早发布",
        selected: false
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
//排序变更
const OrderChange = (v: number) => {
    srcModel.order = v
    GetList()

}
//分区变更
const AreaChange = (v: number) => {
    srcModel.type = v
    GetList()
}
//重置搜索条件
const Reset = () => {
    srcModel.page = 1
    srcModel.order = 0
    opType.forEach(async function(p, i) {
        if(i==0) {
          p.selected=true;
        } else {
          p.selected=false;
        }
      })
    orderType.forEach(async function(p, i) {
        if (i == 0) {
            p.selected = true
        } else {
            p.selected = false
        }
    })
    srcModel.type = 0
    srcModel.search = ""
    srcModel.tags.length = 0
    GetList()
}
//默认加载
GetList()
</script>

<template>
    <div v-loading="load" class="list">
        <div>
            <select-box title="分区" :list="opType" @on-change="AreaChange"></select-box>
            <select-box title="排序" :list="orderType" @on-change="OrderChange"></select-box>
            <tag-list-box title="标签"></tag-list-box>
        </div>
        <div style="width: 700px;">
            <div class="list-search">
                <el-input v-model="srcModel.search" placeholder="搜索你想知道的博客内容..." style="width: 400px;height: 35px;" />
                <button @click="GetList">搜索</button>
                <button @click="Reset">重置</button>
            </div>
            <div class="list-box">
                <blog-item v-for="it in list" :key="it.id" :title="it.title" :id="it.id"
                    :context="ClearHtml(it.context)" :create-time="GetDate(it.createTime)"
                    :update-time="GetDate(it.updateTime)" :tags="it.tags" :path="'/blog/'" />
            </div>
            <div class="list-pager">
                <el-pagination background layout="prev, pager, next" :total="count" :page-size="srcModel.size"
                    @current-change="PageChange" />
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>

    .list{
        display: flex;
        margin: 0px 5px;
        &-box{
            flex: 1;
            min-height: 500px;
        }
        &-pager{
            display: flex;
            justify-content: center;
            align-items: center;
        }
        &-search{
            margin: 5px;
            display: flex;
            align-items: center;
            button{
                cursor: pointer;
                padding: 7px 20px;
                margin: 5px;
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