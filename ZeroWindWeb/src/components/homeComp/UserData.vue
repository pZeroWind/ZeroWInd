<script lang="ts" setup>
import {Ref, ref} from "vue"
import { User, UserModel } from "../../apis/userApi";
import axiosConfig from "../../plugins/axiosConfig";
import { GetDate } from "../../plugins/Tool";
//注入Api
const userApi = new User()

const defImg = new URL("../../assets/icon/user-fill.svg", import.meta.url).href
//个人资料数据
const data: Ref<UserModel> = ref({
    id:"1",
    nickName:"",
    img: defImg,
    intro:"",
    birthday:0
})

//载入中
const isLoading = ref(true)

//获取数据
userApi.get((res: 
{ 
    data: UserModel
})=>{
    isLoading.value = false
    data.value = res.data
})
</script>

<template>
    <div class="UserBox">
        <div class="UserBox-Img">
            <img :src="data.img===defImg?defImg:axiosConfig.baseURL+'/File'+data.img" alt="头像"/>
        </div>
        <div v-loading="isLoading" v-if="isLoading" class="UserBox-Data">
        </div>
        <div v-else class="UserBox-Data">
            <div class="UserBox-Data-UserName Line">
                <label>站主：</label>
                <div>{{data.nickName}}</div>
            </div>
            <div class="UserBox-Data-UserName Line">
                <label>出生日期：</label>
                <div>{{GetDate(data.birthday)}}</div>
            </div>
            <div class="UserBox-Data-Intro Line">
                <label>个人简介：</label>
                <div>{{data.intro}}</div>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.UserBox{
    width: 240px;
    min-height: 300px;
    padding: 10px;
    margin: 5px;
    user-select: none;
    border-radius: 5px;
    background-color: #fff;
    box-shadow: 0 0 5px #ddd;
    transition: all 0.5s;
    &-Img{
        display: flex;
        height: max-content;
        justify-content: center;
        align-content: center;
        img{
            width: 90px;
            height: 90px;
            margin: 5px;
            border-radius: 50%;
            object-fit: cover;
        }
    }
    &-Data{
        font-size: 14px;
        margin-top: 10px;
        label{
            font-weight: bold;
            display: block;
            width: 75px;
        }
        .Line{
            margin: 5px;
        }
        &-UserName{
            display: flex;
        }
        &-Intro{
            div{
                margin-top: 5px;
            }
        }
    }
    @media screen and (max-width: 500px) {
        width: auto;
        height: 75px;
        margin-bottom: 20px;
        display: flex;
        align-items: center;
        &-Img{
            img{
                width: 50px;
                height: 50px;
                margin: 5px;
                border-radius: 50%;
                object-fit: cover;
            }
        }
        &-Data{
            margin-top: 0;
            &-UserName{
                display: flex;
            }
            &-Intro{
                display: none;
            }
        }
    }
}
</style>