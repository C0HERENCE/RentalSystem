import request from "@/utils/request";

export function login(username, password) {
    return request({
        url: '/user/login',
        method: 'post',
        data: {
            username,password
        }
    })
}

export function register(data) {
    return request({
        url: '/user',
        method: 'post',
        data: data
    })
}