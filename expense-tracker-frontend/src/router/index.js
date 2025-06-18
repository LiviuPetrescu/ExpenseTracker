import { createRouter, createWebHistory } from 'vue-router'
import AllExpensesPage from '@/components/pages/AllExpensesPage.vue'
import NewExpensePage from '@/components/pages/NewExpensePage.vue'
import ChartsPage from '@/components/pages/ChartsPage.vue'

const routes = [
  {
    path: '/',
    redirect: '/home'
  },
  {
    path: '/home',
    name: 'AllExpenses',
    component: AllExpensesPage,
    meta: { title: 'All Expenses - Expense Tracker' }
  },
  {
    path: '/add',
    name: 'new-expense',
    component: NewExpensePage,
    meta: { title: 'Add New Expense - Expense Tracker' }
  },
  {
    path: '/charts',
    name: 'Charts',
    component: ChartsPage,
    meta: { title: 'Charts - Expense Tracker' }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})


router.beforeEach((to, from, next) => {
  document.title = to.meta.title || 'Expense Tracker'
  next()
})

export default router
