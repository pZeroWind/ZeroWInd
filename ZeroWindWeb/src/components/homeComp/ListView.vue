<script lang="ts" setup>
import { reactive, ref } from 'vue';
import BlogItem from '../toolComp/BlogItem.vue';
import  { Blog,BlogModel } from "../../apis/blogApi"
import { GetDate, ClearHtml } from '../../plugins/Tool'; 
//注入api
const blogApi = new Blog()
//当前菜单选择
const types = ref(0)
//主页菜单
const list = [
    "最新博客",
    "最新动态"
]
//修改选择
const change = (i:number) => {
    types.value = i
    blog.length = 0
    switch (i) {
        case 0:
            getBlog()
            break;
        case 1:
            break;
        default:
            break;
    }
}

//首页博客列表
const blog: Array<BlogModel> = reactive([])


//获取数据
const getBlog = () => {
    blogApi.get({
        page: 1,
        size: 3,
        type: 0,
        order: 0,
        search: "",
        tags: []
    }, (res: { data: { data: Array<BlogModel>; }; }) => {
        blog.push(...res.data.data)
    })
}
//调用一次change函数获取数据
change(0)

</script>

<template>
    <div class="home">
        <ul class="home-mune">
            <li v-for="(it,i) in list" :key="i" :class='{sel:types==i}' @click="change(i)">
                {{it}}
            </li>
        </ul>
        <div v-loading="blog.length==0" v-if="types==0" class="home-list">
            <blog-item
            v-for="it in blog" 
            :key="it.id" 
            :title="it.title" :id="it.id" 
            :context="ClearHtml(it.context)"
            :create-time="GetDate(it.createTime)" 
            :update-time="GetDate(it.updateTime)" 
            :tags="it.tags" 
            :path="'/blog/'" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
.home{
    flex: 1;
    &-mune{
        width: max-content;
        display: flex;
        list-style: none;
        border-radius: 5px;
        margin: 5px;
        box-shadow: 0 0 5px #ddd;
        background-color: #fff;
        overflow: hidden;
        li{
            cursor: pointer;
            user-select: none;
            font-weight: bold;
            padding: 10px 20px;
        }
        .sel{
            background-color: #555;
            color: #fff;
        }
    }
    &-list{
        min-height: 400px;
        min-width: 600px;
        @media screen and (max-width: 768px) {
            min-width: auto;
        }
    }
}
</style>