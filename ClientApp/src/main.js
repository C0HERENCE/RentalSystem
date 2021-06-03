import Vue from 'vue'
import ElementUI from 'element-ui';
import './assets/index.css';
import App from './App.vue'
import router from './router'
import store from './store'

import {BootstrapVue, IconsPlugin} from 'bootstrap-vue'

// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)

Vue.config.productionTip = false
Vue.use(ElementUI);

import VueCookies from 'vue-cookies'
Vue.use(VueCookies)

import Moment from 'moment'

Vue.filter('fTime', function (value) {
    return Moment(value).format('YYYY-MM-DD HH:mm:ss')
})

import VueQuillEditor from 'vue-quill-editor'

import 'quill/dist/quill.core.css' // import styles
import 'quill/dist/quill.snow.css' // for snow theme
import 'quill/dist/quill.bubble.css' // for bubble theme

Vue.use(VueQuillEditor, /* { default global options } */)

const signalR = require('@microsoft/signalr') // 1 装包，Microsoftsignalr npm install ，引入
Vue.prototype.$connection = new signalR.HubConnectionBuilder() // 2. 在vue的原型上加上一个signalr 连接对象
    .withUrl("/chathub") // 3 配置一个connection的过程
    .build();


import {Notification} from 'element-ui';

// 4 $connection对象 监听两个事件ReceiveMessage   NotifyMail
Vue.prototype.$connection.on("ReceiveMessage", (userId, userName, message) => {
    console.log(userId, userName, message)
    Notification.success({message: message.substr(0,20), title: userName + "给你发送了一条消息"})
    store.commit("AddUserMessage", { // 5. 通知Notification + 保存到vuex
        tid: userId,
        tname: userName,
        message: message,
        dir: 0
    })
})
Vue.prototype.$connection.on("NotifyMail", () => {
    Notification.success({message: "对方目前不在线，请再浏览一下商品详情，或通过手机号码联系对方。", title: "你的消息发送失败"})
})

if (store.state.isLogin) {
    Vue.prototype.$connection.start()
}

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')
