import '@/assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)

router.beforeEach((to, from, next) => {
  const defaultTitle = 'Expense Tracker'
  document.title = to.meta.title || defaultTitle
  next()
})


app.mount('#app')
