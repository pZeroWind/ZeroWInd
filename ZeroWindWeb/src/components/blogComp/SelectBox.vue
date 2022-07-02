<script lang="ts" setup>
interface SelectModel{
    id: number,
    value: any,
    label: string,
    selected: boolean
}

//入参
const props = defineProps({
    List: Array<SelectModel>
})

//自定义返回函数
const emits = defineEmits(["OnChange"])

const OnSelected = (id: number) => {
    if (props.List != undefined) {
        for (let item of props.List) {
            item.selected = false
            if (item.id == id) {
                item.selected = true
                emits("OnChange",item.value)
            }
        }
    }
}
</script>

<template>
    <ul>
        <li v-for="it in List" :key="it.id" @click="OnSelected(it.id)">{{it.label}}</li>
    </ul>
</template>

<style>

</style>