import axios from "../plugins/axios";

const url = "/Blog"

//博客模型
export interface BlogModel {
    id: string,
    title: string,
    context: string,
    createTime: number,
    updateTime: number,
    typeId: number,
    tags: Array<string>
}

//筛选模型
export interface SearchModel {
    page: number,
    size: number,
    type: number,
    order: number,
    search: string,
    tags: Array<string>
}

export class Blog {
    //获取博客
    get(data: SearchModel, out: Function) {
        axios.post(url + "/Get", data, {}).then(res => {
            out(res)
        })
    }
    //按id获取博客详情
    getById(id: string, out: Function) {
        axios.get(url + "/Get/" + id, {}).then(res => {
            out(res)
        })
    }
    //更新博客
    update(data: BlogModel, out: Function) {
        axios.put(url, data, {}).then(res => {
            out(res)
        })
    }
    //添加博客
    add(data: BlogModel, out: Function) {
        axios.post(url, data, {}).then(res => {
            out(res)
        })
    }
}