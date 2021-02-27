import request from "@/utils/request";

export function getCategories() {
    return request({
        url: '/category',
        method: 'get',
    })
}
export function getChildCategories(id) {
    return request({
        url: '/category/' + id,
        method: 'get',
    })
}

export function getCategoryDetail(id) {
    return request({
        url: '/category/detail/' + id,
        method: 'get',
    })
}