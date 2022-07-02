<script setup lang="ts">
import { Ref, ref, onMounted, reactive } from 'vue';
import { useRoute,useRouter } from 'vue-router';
import { Blog, BlogModel } from '../../apis/blogApi';
import { ResultModel } from '../../plugins/axiosConfig';
import { GetDate } from '../../plugins/Tool';
import { ElNotification } from 'element-plus';
import E from "wangeditor"
import { OptionModel, Type } from '../../apis/typeApi';
import { Tag } from '../../apis/tagsApi';
//注入Api
const blogApi = new Blog()
const tagsApi = new Tag()
const typeApi = new Type()
//初始化富文本编辑框
onMounted(() => {
    const editor = new E("#eidtor")
    editor.config.height = 400
    editor.config.placeholder = "请输入正文内容..."
    editor.config.zIndex = 10
    editor.config.onchange = (html:string) => {
        data.value.context = html
    }
    editor.create()
    //如果不等于新建博客
    if (id !== "new") {
        GetData(() => {
            editor.txt.html(data.value.context)
        })
    }
})
//获取路由
const route = useRoute();
//跳转器
const router = useRouter();
//获取id
const id = route.params.id.toString();

//个人数据资料
const data:Ref<BlogModel> = ref({
    id: "",
    title: "",
    context: "",
    createTime: 0,
    updateTime: 0,
    typeId: 1,
    type:"",
    tags: []
})
data.value.id = id
//返回上一页
const back = () => {
    history.back()
}

const GetData = (out?:Function) => {
    //据id获取blog数据
    blogApi.getById(id, (res: ResultModel<BlogModel>) => {
        if (res.data.id === "") {
            router.push("/notFuond");
        }
        data.value = res.data;
        if (out) {
            out()
        }
    })
}


//保存博客
const SaveBlog = () => {
    if (id !== "new") {
        blogApi.update(data.value, (res:ResultModel<any>) => {
            if (res.code === 200) {
                ElNotification({
                    message: "更新成功",
                    type: "success",
                    position: "bottom-right",
                    duration: 2500
                })
                GetData()
            }
        })
    } else {
        blogApi.add(data.value, (res: ResultModel<any>) => {
            if (res.code === 200) {
                ElNotification({
                    message: "发布成功",
                    type: "success",
                    position: "bottom-right",
                    duration: 2500
                })
                if (res.data.id) {
                    router.push("/sys/blog/" + res.data.id)
                }
            }
        })
    }
}

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
const opType: OptionModel[] = reactive([])
typeApi.Get((res: ResultModel<OptionModel[]>) => {
    opType.push(...res.data)
})

</script>

<template>
    <div v-loading="data.id===''" class="details">
        <div class="details-title">
            <div>
                <el-input type="text" style="width: 300px;margin: 0 5px;" v-model="data.title" placeholder="请输入博客标题" />
                <el-select v-model="data.typeId" placeholder="分区" style="width: 75px;margin: 0 5px;">
                    <el-option v-for="it in opType" :key="it.value" :label="it.label" :value="it.value" />
                </el-select>

            </div>
            <div>
                <button class="details-title-btn" v-if="id!=='new'" @click="SaveBlog">保存</button>
                <button class="details-title-btn" v-else @click="SaveBlog">发布</button>
                <button class="details-title-btn" @click="back">返回</button>
            </div>
        </div>
        <div v-if="id!=='new'" class="details-time">
            发布时间：{{GetDate(data.createTime)}}&nbsp;&nbsp;&nbsp;&nbsp;最后更新：{{GetDate(data.updateTime)}}
        </div>
        <div class="details-tags">
            <label style="font-weight: bold;">标签：</label>
            <el-select v-model="data.tags" multiple placeholder="添加标签更容易找到博客" style="width: 300px;margin: 0 5px;" allow-create
                filterable default-first-option :multiple-limit="5" :loading="loading" :remote-method="GetTags" remote>
                <el-option v-for="it in tags" :key="it.value" :label="it.label" :value="it.value" />
            </el-select>
        </div>
        <div class="details-html" id="eidtor">
        </div>
    </div>
</template>

<style lang="scss" scoped>
.details{
    background-color: white;
    border-radius: 5px;
    min-height: 500px;
    min-width: 700px;
    padding: 10px;
    box-shadow: 0 0 5px #eee;
    div{
        margin: 5px;
    }
    &-title{
        display: flex;
        justify-content: space-between;
        align-items: center;
        font-size: 24px;
        font-weight: bold;
        &-btn{
            cursor: pointer;
            font-size: 14px;
            font-weight: bold;
            border: none;
            outline: none;
            padding: 5px 20px;
            margin: 2.5px;
            border-radius: 5px;
            box-shadow: 0 0 5px #ddd;
            color: #000;
            background-color: #fff;
            transition: all .25s;
            &:hover{
                box-shadow: 0 0 5px #aaa;
            }
        }
    }
    &-time{
        color: #aaa;
        font-size: 12px;
    }
    &-tags{
        span{
            margin-right: 5px;
        }
    }
    &-html{
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