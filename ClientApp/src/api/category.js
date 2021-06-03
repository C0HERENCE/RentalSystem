import request from "./request";

export function getCategories() {
    return request({
        url: '/api/category/get',
        method: 'get'
    })
}