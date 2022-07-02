<script setup lang="ts">
import HeaderNav from './components/HeaderNav.vue';
import FootNav from './components/FootNav.vue';
import ToolBar from './components/toolComp/ToolBar.vue';

</script>

<template>
  <div id="Form">
    <header-nav/>
    <div class="routerView">
      <router-view/>
      <tool-bar/>
    </div>
    <foot-nav/>
  </div>
</template>

<style lang="scss">
*{
  margin: 0;
  padding: 0;
  --el-color-primary: #000;
  --el-color-primary-light-9:#ddd;
  --el-color-primary-light-3: #ddd;
}
#app{
  background-color:#FAFAFA;
  min-width: max-content;
}
#Form{
  display: flex;
  min-height: 100vh;
  flex-direction: column;
  .routerView{
    flex: 1;
    margin: 0 200px;
    @media screen and (max-width: 500px){
      margin: 0
    }
  }
}
</style>
