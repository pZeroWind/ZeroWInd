import { createRouter, createWebHistory } from "vue-router"
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
        component: UserSysView
      },
      {
        path: "blog",//博客管理
        name: "BlogSys",
        component: BlogSysView
      },
      {
        path: "blog/:id",//博客修改
        name: "BlogSysModif",
        component: BlogWriteView
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