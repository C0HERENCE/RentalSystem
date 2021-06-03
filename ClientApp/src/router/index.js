import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '../store/index'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/Home')
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('../views/Register')
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../views/Login')
  },
  {
    path: '/guide',
    name: 'Guide',
    component: () => import('../views/Guide')
  },
  {
    path: '/profile',
    name: 'Profile',
    component: () => import('../views/Profile'),
    meta: {
      requiredAuth: true
    },
  },
  {
    path: '/publish',
    name: 'Publish',
    component: () => import('../views/Publish'),
    redirect: '/publish/choose',
    meta: {
      requiredAuth: true
    },
    children: [
      {
        path: 'choose',
        name: 'Choose',
        component: () => import('../components/publish/Choose'),
        meta: {
          requiredAuth: true
        },
      },
      {
        path: 'write',
        name: 'Write',
        component: () => import('../components/publish/Write'),
        meta: {
          requiredAuth: true
        },
      },
      {
        path: 'check/:id',
        name: 'Check',
        component: () => import('../components/publish/Check'),
        meta: {
          requiredAuth: true
        },
      },
      {
        path: 'demand',
        name: 'Demand',
        component: () => import('../components/publish/Demand'),
        meta: {
          requiredAuth: true
        },
      },
      {
        path: 'demandCheck/:id',
        name: 'Check',
        component: () => import('../components/publish/DemandCheck'),
        meta: {
          requiredAuth: true
        },
      }
    ]
  },
  {
    path: '/detail/:id',
    name: 'Detail',
    component: () => import('../views/Detail'),
  },
  {
    path: '/search',
    name: 'Search',
    component: () => import('../views/Search')
  },
  {
    path: '/pay',
    name: 'PayOrder',
    component: () => import('../views/PayOrder')
  },
  {
    path: '/admin',
    name: 'AdminLayout',
    component: () => import('../views/admin/AdminLayout'),
    children: [
      {
        path: '',
        name: 'UserManagement',
        component: () => import("../views/admin/UserManagement")
      },
      {
        path: 'product',
        name: 'ProductManagement',
        component: () => import("../views/admin/ProductManagement")
      },
      {
        path: 'order',
        name: 'OrderManagement',
        component: () => import("../views/admin/OrderManagement")
      },
      {
        path: 'demand',
        name: 'DemandManagement',
        component: () => import("../views/admin/DemandManagement")
      },
      {
        path: 'category',
        name: 'CategoryManagement',
        component: () => import("../views/admin/CategoryManagement")
      },
      {
        path: 'student',
        name: 'StudentManagement',
        component: () => import("../views/admin/StudentManagement")
      },
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  if (to.meta.requiredAuth === true) {
    if (store.state.isLogin)
      next()
    else
      next({
        path:'/login',
      })
  }
  else next()
})

export default router
