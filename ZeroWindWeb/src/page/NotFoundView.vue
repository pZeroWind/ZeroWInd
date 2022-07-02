<script lang="ts" setup>

</script>

<template>
    <div class="notFound">
        <h1>404 Not Found</h1>
        <div>
            当前页面不存在
        </div>
    </div>
</template>

<style lang="scss" scoped>
.notFound{
    user-select: none;
    margin-top: 50px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    div{
        margin: 10px;
        color: #999;
    }
    h1{
        color:#555;
    }
}
</style>