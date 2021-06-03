import request from "@/api/request";

export function makeNewOrder(productId, day) {
    return request({
        url: '/api/order/new',
        method: 'post',
        data: {
            productId: productId,
            day: day
        }
    })
}

export function confirmOrder(orderId, msg) {
    return request({
        url: 'api/order/confirm',
        method: 'post',
        data: {
            orderId,msg
        }
    })
}

export function backOrder(orderId, yaMoney) {
    return request({
        url: 'api/order/back',
        method: 'post',
        data: {
            orderId, yaMoney
        }
    })
}

export function rateOrder(orderId, value) {
    return request({
        url: 'api/order/rate',
        method: 'post',
        params: {
            orderId, value
        }
    })
}