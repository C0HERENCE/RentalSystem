import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import store from "../store"

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
        meta: {
            requireAuth: true
        },
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
            },
            {
                name: 'PersonalGoods',
                path: '/profile/goods',
                component: () => import('../views/PersonalGoods')
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
        component: () => import('../views/Publish'),
        meta: {
            requireAuth: true
        }
    },
    {
        name: 'GoodsDetail',
        path: '/goods/:id',
        component: () => import('../views/GoodsDetail')
    },
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

router.beforeEach(((to, from, next) => {
    if (to.matched.some(record => record.meta.requireAuth)) {
        if (store.getters.IS_LOGIN)
            next();
        else
            next({
                path: '/login',
                query: {redirect: to.fullPath}
            })
    } else {
        next();
    }
}))

export default router
