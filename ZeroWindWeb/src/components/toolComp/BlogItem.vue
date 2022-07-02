<script setup lang="ts">
const props = defineProps({
    id:String,
    title:String,
    context:String,
    createTime: String,
    updateTime: String,
    tags: Array,
    path:String
})
const urlPath: string = String(props.path)
</script>

<template>
    <router-link :to="urlPath + id">
        <div class="blog">
            <div class="blog-title">{{title}}</div>
            <div class="blog-tags">
                <el-tag type="info" v-for="(it,i) in tags" :key="i">{{it}}</el-tag>
            </div>
            <div class="blog-context">{{context}}</div>
            <div class="blog-time">
                发布时间：{{createTime}}
                &nbsp;&nbsp;&nbsp;
                最后更新：{{updateTime}}
            </div>
        </div>
    </router-link>
</template>

<style lang="scss" scoped>


a{
    text-decoration: none;
    color: #000;
}
.blog{
    cursor: pointer;
    min-width: 600px;
    margin: 5px;
    background-color: #fff;
    border-radius: 5px;
    box-shadow: 0 0 5px #ddd;
    transition: all .5s;
    &:hover &-title{
        color: #555;
    }
    &:hover{
        box-shadow: 0 0 10px #aaa;
    }
    div{
        padding: 7.5px;
    }
    &-title{
        font-size: 18px;
        font-weight: bold;
    }
    &-tags{
        span{
            margin-right: 5px;
        }
    }
    &-context{
        font-size: 13.5px;
        width: 500px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
        @media screen and (max-width: 500px) {
            width: 200px;
        }
    }
    &-time{
        font-size: 12px;
        color: #aaa;
        text-align: right;
    }
    @media screen and (max-width: 500px) {
        min-width: auto;
    }
}
</style>