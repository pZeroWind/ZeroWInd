<script lang="ts" setup>
import SelectBar from "../components/sysComp/SelectBar.vue";
import { useRoute } from "vue-router";
const path = useRoute().path
</script>

<template>
    <div class="sys">
        <div class="sys-bar" v-if="path!='/sys'">
            <select-bar></select-bar>
        </div>
        <div>
            <router-view></router-view>
        </div>
    </div>
</template>

<style lang="scss" scoped>

.sys{
    margin-top: 40px;
    display: flex;
    justify-content: center;
}
</style>