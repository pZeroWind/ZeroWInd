<script lang="ts" setup>
import UserData from '../components/homeComp/UserData.vue';
import ListView from '../components/homeComp/ListView.vue';

</script>

<template>
    <div class="content">
        <user-data/>
        <list-view/>
    </div>
</template>

<style lang="scss" scoped>

.content{
    padding: 25px;
    display: flex;
    @media screen and (max-width: 768px) {
      display: block;
    }
}
</style>