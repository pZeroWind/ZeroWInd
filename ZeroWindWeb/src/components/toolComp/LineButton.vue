<script lang="ts" setup>
defineProps({
    title:String
})
const emits = defineEmits(["Press"])

const PressMethod = ()=> {
    emits("Press")
}
</script>

<template>
    <div class="T-btn" @click="PressMethod">
        {{title}}
    </div>
</template>

<style lang="scss" scoped>
.T-btn{
    padding: 10px 20px;
    margin: 5px;
    border-radius: 5px;
    box-shadow: 0 0 5px #ddd;
    background-color: #fff;
}
</style>