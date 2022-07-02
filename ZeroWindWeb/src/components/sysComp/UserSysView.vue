<script lang="ts" setup>
import { Ref, ref } from "@vue/reactivity";
import { ElNotification } from "element-plus";
import { User, UserModel } from "../../apis/userApi";
import axiosConfig, { ResultModel } from "../../plugins/axiosConfig";
import { GetDate } from "../../plugins/Tool";
import SelectBar from "./SelectBar.vue";
//注入用户Api
const userApi = new User()
const defImg = new URL("../../assets/icon/user-fill.svg", import.meta.url).href
//数据储存
const userData: Ref<UserModel> = ref({
    id: "1",
    nickName: "",
    img: defImg,
    intro: "",
    birthday: 0
})
//获取返回数据
const GetData = () => {
    userApi.get((res: ResultModel<UserModel>) => {
        userData.value = res.data
    })
}
GetData()

const SaveData = () => {
    userApi.put(userData.value, (res:ResultModel<boolean>) => {
        if (res.code == 200) {
            ElNotification({
                message: "修改成功",
                type: "success",
                position: "bottom-right",
                duration: 2500
            })
            GetData()
        }
    })
}

</script>

<template>
    <div class="sys">
        <div class="sys-bar">
            <select-bar :id="1"></select-bar>
        </div>
        <div v-loading="userData.id=='1'" class="sys-data">
            <div class="sys-data-line">
                <img :src="userData.img === defImg ? defImg : axiosConfig.baseURL + '/Server' + userData.img"
                    alt="头像" />
                <div>
                    <label>站主ID：</label>
                    <div>{{userData.nickName}}</div>
                </div>
            </div>
            <div class="sys-data-line">
                <label>出生日期：</label>
                <div>{{GetDate(userData.birthday)}}</div>
            </div>
            <div class="sys-data-line" style="display: block;flex: 1;">
                <label>个人简介：</label>
                <el-input class="sys-data-line-text" v-model="userData.intro" :rows="10" resize="none" type="textarea"
                    placeholder="啥也不是" />
            </div>
            <div class="sys-data-btn">
                <button @click="SaveData">确认保存</button>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>


.sys{
    display: flex;
    justify-content: center;
    &-data{
        width: 700px;
        min-height: 500px;
        margin: 5px;
        box-shadow: 0 0 5px #ddd;
        border-radius: 5px;
        background-color: #fff;
        display: flex;
        flex-direction: column;
        &-line{
            margin: 10px;
            display: flex;
            align-items: center;
            img{
                width: 50px;
                height: 50px;
                object-fit: cover;
                border-radius: 50%;
                margin: 10px;
            }
            label{
                font-weight: bold;
            }
            &-text{
                margin-top: 10px;
            }
        }
        &-btn{
            margin: 10px;
            display: flex;
            justify-content: center;
            align-items: center;
            button{
                background-color: #fff;
                box-shadow: 0 0 5px #ddd;
                border-radius: 5px;
                padding: 10px 20px;
                font-size: 16px;
                font-weight: bold;
                border: none;
                outline: none;
                &:hover {
                    box-shadow: 0 0 5px #aaa;
                }
            }
        }
    }
}
</style>