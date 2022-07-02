import axios from "../plugins/axios";

const url = "/User"
//个人数据模型
export interface UserModel {
    id: string,
    nickName: string,
    img: string,
    birthday: number,
    intro: string
}

export class User {
    //获取个人数据
    get(out: Function) {
        axios.get(url + "/Get", {
            id: "Zw174303422"
        }).then(res => {
            out(res)
        })
    }
    //登录后台
    login(pass: string, out: Function) {
        axios.post(url + "/Login", {}, {
            pass
        }).then(res => {
            out(res)
        })
    }
    //获取后台验证码
    getCode(out: Function) {
        axios.get(url + "/Code", {}).then(res => {
            out(res)
        })
    }
    //更新个人数据
    put(data: UserModel, out: Function) {
        axios.put(url, data, {}).then(res => {
            out(res)
        })
    }
}