import { createRouter, createWebHistory } from 'vue-router'
import productCategory from '../views/productsCategory.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'productCategory',
      component: productCategory
    },
    {
      path: '/productsPage/:categoryId',
      name: 'productsPage',
      component: () => import('../views/productsPage.vue')
    }
  ]
})

export default router
