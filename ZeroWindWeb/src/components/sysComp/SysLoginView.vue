<script lang="ts" setup>
import { ref } from 'vue';
import { User } from '../../apis/userApi';
import { ResultModel } from "../../plugins/axiosConfig"
import { useRouter } from 'vue-router';
//注入Api
const userApi = new User()
//路由控制
const router = useRouter();
//判断token是否存在
let token = sessionStorage.getItem("token")
if (token) {
    router.push("/sys/user")
}

//验证码
const password = ref("");
//倒计时
const code = ref(0);
//获取验证码
const GetCode = () => {
    code.value = 60;
    userApi.getCode((res: ResultModel<any>) => {
        if (res.code === 200) {
            var timer = setInterval(() => {
                code.value--;
                if (code.value === 0) {
                    clearInterval(timer);
                }
            }, 1000);
        }
    })
}

//登入后台
const Login = () => {
    userApi.login(password.value, (res: ResultModel<string>) => {
        if (res.code === 200) {
            sessionStorage.setItem("token", res.data);
            router.push("/sys/user");
        }
    });
}

</script>

<template>
    <div class="login">
        <div class="login-line">
            <el-input show-password v-model="password" placeholder="请输入令牌" />
            <button class="login-line-hover" v-if="code === 0" @click="GetCode">获取令牌</button>
            <button v-else-if="code === 60" disabled="false">获取中...</button>
            <button v-else disabled="false">{{code}}秒</button>
        </div>
        <div class="login-line login-loginBtn">
            <button @click="Login">进入后台</button>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.login{
    box-shadow: 0 0 5px #ddd;
    border-radius: 5px;
    margin: 10px auto;
    margin-top: 100px;
    padding: 50px;
    background-color: #fff;
    max-width: 300px;
    &-line{
        display: flex;
        align-items: center;
        div{
            margin: 10px;
        }
        button{
            cursor: pointer;
            width: 100px;
            height: 30px;
            box-shadow: 0 0 5px #ddd;
            border-radius: 5px;
            border: none;
            background-color: #fff;
            font-weight: bold;
            font-size: 12px;
            transition: all .25s;
        }
        &-hover:hover{
            box-shadow: 0 0 5px #aaa;
        }
    }
    &-loginBtn{
        justify-content: center;
        button{
            cursor: pointer;
            width: 200px;
            font-size: 16px;
            &:hover {
                box-shadow: 0 0 5px #aaa;
            }
        }
    }
}
</style>