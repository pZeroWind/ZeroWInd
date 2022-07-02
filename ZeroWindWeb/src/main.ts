import { createApp } from 'vue'
import App from './App.vue'
import element from 'element-plus'
import router from './router/index'

import "element-plus/dist/index.css"

createApp(App).use(element).use(router).mount('#app')
