<script lang="ts" setup>
import BlogListView from '../components/blogComp/BlogListView.vue';
</script>

<template>
    <div class="content">
        <blog-list-view />
    </div>
</template>

<style lang="scss" scoped>
.content{
    display: flex;
    padding: 25px;
    height: 100%;
    justify-content: center;
    align-items: center;
    font-size: 18px;
    font-weight: bold;
}
</style>