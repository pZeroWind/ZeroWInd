import { createRouter, createWebHistory, RouteLocationNormalized, RouteRecordNormalized } from "vue-router"
import HomeView from "../page/HomeView.vue"
import BlogView from "../page/BlogView.vue"
import TrendView from "../page/TrendView.vue"
import BlogDetailsView from "../page/BlogDetailsView.vue"
import SysView from "../page/SysView.vue"
import SysLoginView from "../components/sysComp/SysLoginView.vue"
import BlogSysView from "../components/sysComp/BlogSysView.vue"
import UserSysView from "../components/sysComp/UserSysView.vue"
import NotFoundView from "../page/NotFoundView.vue"
import BlogWriteView from "../components/sysComp/BlogWriteView.vue"

//路由拦截器
const wall = async (to: RouteLocationNormalized) => {
  // if ((to.name === "User" || to.name === "BlogSys" || to.name === "BlogSysModif" || to.name !== "Login")) {
  //   let token = localStorage.getItem("token")
  //   if (token) {
  //     return true
  //   } else {
  //     return { name: "Login" }
  //   }
  // }
}

const routes = [
  {
    path: "/",//主页重定向
    name: "Index",
    redirect: "/home"
  },
  {
    path: "/home",//主页
    name: "Home",
    component: HomeView
  },
  {
    path: "/blog",//博客页
    name: "Blog",
    component: BlogView,
  },
  {
    path: "/blog/:id",//博客详情页
    name: "BlogDetails",
    component: BlogDetailsView
  },
  {
    path: "/trend",//动态页
    name: "Trend",
    component: TrendView
  },
  {
    path: "/sys",
    name: "Sys",
    component: SysView,
    children: [
      {
        path: "",//后台登录
        name: "Login",
        component: SysLoginView
      },
      {
        path: "user",//个人数据管理
        name: "User",
        component: UserSysView,
        beforeEnter: wall
      },
      {
        path: "blog",//博客管理
        name: "BlogSys",
        component: BlogSysView,
        beforeEnter: wall
      },
      {
        path: "blog/:id",//博客修改
        name: "BlogSysModif",
        component: BlogWriteView,
        beforeEnter: wall
      }
    ]
  },
  {
    path: "/:w+",//页面不存在
    name: "notFound",
    component: NotFoundView
  }
]


const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router