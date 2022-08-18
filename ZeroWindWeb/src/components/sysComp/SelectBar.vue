<script lang="ts" setup>
import { ref } from "@vue/reactivity"
import {useRoute} from "vue-router"
const router = useRoute().path
//后台列表
const list = [
    {
        id: 1,
        name: "资料管理",
        path: "/sys/user",
    },
    {
        id: 2,
        name: "博客管理",
        path: "/sys/blog",
    },
    {
        id: 3,
        name: "动态管理",
        path: "/sys/trend",
    },
    {
        id: 4,
        name: "网页收藏",
        path: "/sys/webSave",
    },
]
const id = ref(list.filter(p => p.path == router)[0].id)
const RouteChange = (v:number) => {
    id.value = v
}
</script>

<template>
    <div class="selectBar">
        <router-link v-for="it in list" :key="it.id" :to="it.path" @click="RouteChange(it.id)">
            <div class="selectBar-list" :class="{sel:id==it.id}">{{it.name}}
            </div>
        </router-link>
    </div>
</template>



<style lang="scss" scoped>
.selectBar{
    a{
        text-decoration: none;
    }
    padding: 5px;
    margin: 5px;
    border-radius: 5px;
    box-shadow: 0 0 5px #ddd;
    background-color: #fff;
    &-list{
        cursor: pointer;
        color: #000;
        user-select: none;
        padding: 10px;
        margin: 5px;
        font-size: 16px;
        border-radius: 5px;
        font-weight: bold;
        transition: all .25s;
        &:hover{
            background-color: #eee;
        }
    }
    .sel{
        background-color: #555;
        color: #fff;
    }
}
</style>