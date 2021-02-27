import request from "@/utils/request";
export function getGoods(params) {
    return request({
        url: '/goods',
        method: 'get',
        params: params
    })
}

export function publishGoods(data) {
    return request({
        url: '/goods',
        method: 'post',
        data: data
    })
}