<script setup lang="ts">
import { Ref, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { Blog, BlogModel } from '../apis/blogApi';
import { ResultModel } from '../plugins/axiosConfig';
import { GetDate } from '../plugins/Tool';
//注入Api
const blogApi = new Blog()
//获取路由
const route = useRoute();
//跳转器
const router = useRouter();
//获取id
const id = route.params.id.toString();
//个人数据资料
const data: Ref<BlogModel> = ref({
    id: "",
    title: "",
    context: "",
    createTime: 0,
    updateTime: 0,
    typeId: 1,
    type: "",
    tags: []
})

//返回上一页
const back = () => {
    history.back()
}

//据id获取blog数据
blogApi.getById(id, (res: ResultModel<BlogModel>) => {
    if (res.data.id === "") {
        router.push("/notFuond");
    }
    data.value = res.data;
})
</script>

<template>
    <div v-loading="data.id == ''" class="details">
        <div class="details-title">
            <div>
                {{ data.title }}
            </div>
            <button class="details-title-btn" @click="back">返回</button>
        </div>
        <div class="details-time">
            发布时间：{{ GetDate(data.createTime) }}&nbsp;&nbsp;&nbsp;&nbsp;最后更新：{{ GetDate(data.updateTime) }}
        </div>
        <div class="details-tags">
            <el-tag type="info" v-for="(it, i) in data.tags" :key="i">{{ it }}</el-tag>
        </div>
        <div class="details-html" v-html="data.context">
        </div>
    </div>
</template>

<style lang="scss" scoped>
.details {
    background-color: white;
    border-radius: 5px;
    min-height: 550px;
    min-width: 700px;
    padding: 10px 30px;
    margin-top: 25px;
    box-shadow: 0 0 5px #eee;

    div {
        margin: 5px;
    }

    &-title {
        display: flex;
        justify-content: space-between;
        align-items: center;
        font-size: 24px;
        font-weight: bold;

        &-btn {
            cursor: pointer;
            font-size: 14px;
            font-weight: bold;
            border: none;
            outline: none;
            padding: 5px 20px;
            border-radius: 5px;
            box-shadow: 0 0 5px #ddd;
            color: #000;
            background-color: #fff;
            transition: all .25s;

            &:hover {
                box-shadow: 0 0 5px #aaa;
            }
        }
    }

    &-time {
        color: #aaa;
        font-size: 12px;
    }

    &-tags {
        span {
            margin-right: 5px;
        }
    }

    &-html {
        padding-top: 10px;
    }

    @media screen and (max-width: 500px) {
        min-width: auto;
        min-height: auto;
        box-shadow: none;
        margin-top: 0;
        background-color: transparent;
    }
}
</style>