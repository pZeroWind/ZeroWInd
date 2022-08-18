<scrpit lang="ts" setup>

</scrpit>

<template>
    <ul id="menu">
      <li class="logo">
        ZeroWindのホーム
      </li>
      <router-link to="/">
        <li class="link">主页</li>
      </router-link>
      <router-link :to="{path:'/blog'}">
        <li class="link">博客</li>
      </router-link>
      <router-link :to="{path:'/trend'}">
        <li class="link">动态</li>
      </router-link>
    </ul>
</template>

<style lang="scss" scoped>
//导航栏
#menu{
    position: sticky;
    top:0;
    display: flex;
    box-shadow: 0 0 5px #ddd;
    background-color: #fff;
    height: 50px;
    align-items: center;
    li{
      list-style: none;
    }
    .link{
      user-select: none;
      padding: 10px 20px;
      color: #555;
      font-weight: bold;
      font-size: 17.5px;
      transition: all .25s;
      &:hover{
        transform: translateY(-2px) scale(1.025);
      }
    }
    .logo{
      user-select: none;
      padding: 10px 20px;
      margin-left: 5px;
      font-size: 20px;
      font-weight: bold;
      font-style: oblique;
      font-family: "Logo";
      color: gray;
    }
    a{
      text-decoration: none;
    }
    @media screen and (max-width: 768px) {
      .logo{
        display: none;
      }
    }
}
</style>