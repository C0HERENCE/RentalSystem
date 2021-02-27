import axios from "axios";
import store from "../store"

const service = axios.create({
    baseURL: 'http://localhost:5000/api/',
    timeout: 15000
})
service.defaults.headers = {"Content-Type" : "application/json"}

service.interceptors.request.use(config => {
    if (store.getters.IS_LOGIN) {
        config.headers['Authorization'] = 'Bearer ' + store.state.token;
    }
    return config;
}, error => {
    // Do something with request error
    console.log("请求错误：" + error) // for debug
    Promise.reject(error)
})

service.interceptors.response.use(response => {
    const res = response.data;
    if (res.status !== 200) {
        console.log("响应错误: " + res.message);
        if (res.status === 401) {
            console.log("无权限")
        }
        return Promise.reject('error')
    } else {
        return response.data
    }

}, error => {
    console.log('Error' + error)// for debug
        return Promise.reject(error)
})

export default service;