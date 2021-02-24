import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/profile',
    name: 'Profile',
    redirect: 'profile/personal',
    component: () => import(/* webpackChunkName: "about" */ '../views/Profile.vue'),
    children: [
      {
        path: 'personal',
        default: true,
        component: () => import('../views/PersonalInfo.vue'),
      },
      {
        path: 'reset',
        component: () => import('../views/Reset.vue'),
      }
    ]
  },
  {
    name: 'Contact',
    path: '/contact',
    component: () => import('../views/Contact')
  },
  {
    name: 'Category',
    path: '/category/:id',
    component: () => import('../views/Category')
  },
  {
    name: 'Login',
    path: '/login',
    component: () => import('../views/Login')
  },
  {
    name: 'Register',
    path: '/register',
    component: () => import('../views/Register')
  },
  {
    name: 'Publish',
    path: '/publish',
    component: () => import('../views/Publish')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
