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

export function getGoodByCategoryId(id, page, limit) {
    return request({
        url: '/goods?categoryId=' + id +'&page='+page +'&limit='+limit,
        method: 'get',
    })
}

export function getUserGoods() {
    return request({
        url: '/goods/user',
        method: 'get'
    })
}