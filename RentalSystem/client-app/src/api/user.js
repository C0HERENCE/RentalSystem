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

export function getUserInfo(id) {
    return request({
        url: '/user' + (id!=null ? '/'+id : ''),
        method: 'get'
    })
}

export function setUserInfo(description) {
    return request({
        url: '/user',
        method: 'put',
        data: {
            description
        }
    })
}

export function resetPassword(old_password, new_password) {
    return request({
        url: '/user/password',
        method: 'put',
        data: {
            old_password,
            new_password
        }
    })
}