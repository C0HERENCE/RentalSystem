import request from "./request";

export function getUserProfileById() {
    return request({
        url: '/api/user/profile',
        method: 'get'
    })
}

export function getMyFavourite() {
    return request({
        url: '/api/user/favourite',
        method: 'get'
    })
}


export function login(u, p) {
    return request({
        url: '/api/user/login',
        method: 'post',
        data: {
            schoolNum: u,
            password: p
        }
    })
}

export function register(u) {
    return request({
        url: '/api/user/register',
        method: 'post',
        data: u
    })
}

export function captcha(email) {
    return request({
        url: '/api/user/sendmail',
        method: 'post',
        params: {
            email: email
        }
    })
}


export function addMoney(money) {
    return request({
        url: '/api/user/money',
        method: 'post',
        data: {money}
    })
}

export function modifyInfo(form) {
    return request({
        url: '/api/user/modifyInfo',
        method: 'post',
        data: form
    })
}

//修改密码
export function modify(form) {
    return request({
        url: '/api/user/modify',
        method: 'post',
        data: form
    })
}

export function moneyHistory(from, end) {
    return request({
        url: '/api/user/moneyHistory',
        params: {
            from: from,
            end: end
        }
    })
}