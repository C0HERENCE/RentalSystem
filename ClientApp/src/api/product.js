import request from "./request";

export function publishProduct(pp) {
    return request({
        url: '/api/product/add',
        method: 'post',
        data: pp
    })
}

export function getProductCheck(id) {
    return request({
        url: '/api/product/checked',
        method: 'get',
        params: {id}
    })
}

export function getAllProduct() {
    return request({
        url: '/api/product/all',
        method: 'get',
    })
}

export function  getProductById(id) {
    return request({
        url: '/api/product/' + id,
        method:"get"
    })
}

export function deleteProductById(id) {
    return request({
        url: '/api/product/delete/' + id,
        method: "post"
    })
}

export function search(keywords) {
    return request({
        url: '/api/product/search',
        params: {
            keywords
        }
    })
}

export function guess() {
    return request({
        url: '/api/product/guess'
    })
}

export function likeProduct(id) {
    return request({
        url: '/api/product/like/' + id,
        method: 'post'
    })
}


export function dislikeProduct(id) {
    return request({
        url: '/api/product/dislike/' + id,
        method: 'post'
    })
}