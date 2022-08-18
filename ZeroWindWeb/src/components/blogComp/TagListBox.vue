<script lang="ts" setup>
import { OptionModel } from "../../apis/typeApi"

//入参
const props = defineProps({
    title:String,
    list: Array<OptionModel>
})

//自定义返回函数
const emits = defineEmits(["OnChange"])

const OnSelected = (id: any) => {
    if (props.list) {
        for (let item of props.list) {
            item.selected = false
            if (item.value == id) {
                item.selected = true
                emits("OnChange",item.value)
            }
        }
    }
}
</script>

<template>
    <div class="SelectBox">
        <div class="SelectBox-Title">
            {{title}}
        </div>
        <div class="SelectBox-Input">
            <el-input></el-input>
        </div>
        <ul class="SelectBox-List">
            <li v-for="it in list" :key="it.value" @click="OnSelected(it.value)" :class="{Selected:it.selected}">{{it.label}}</li>
        </ul>
    </div>
</template>

<style lang="scss" scoped>

.SelectBox{
    border-radius: 5px;
    box-shadow: 0 0 5px #ddd;
    width: 240px;
    padding-bottom: 5px;
    background-color: #fff;
    margin: 5px;
    &-Title{
        font-size: 16px;
        font-weight: bold;
        padding: 10px;
        padding-bottom: 0;
    }
    &-Input{
        padding: 10px;
    }
    &-List{
        display: flex;
        flex-wrap: wrap;
        li{
            padding: 10px;
            cursor: pointer;
            color: #aaa;
            list-style: none;
            font-size: 14px;
            
        }
        .Selected{
            color: #333;
        }
    }
}
</style>