import request from "@/api/request";

export function publishDemand(form) {
    return request({
        url: '/api/demand/add',
        method: 'post',
        data: form
    })
}



export function getDemandCheck(id) {
    return request({
        url: '/api/demand/checked',
        method: 'get',
        params: {id}
    })
}

export function getAllDemands() {
    return request({
        url: '/api/demand/all',
        method: 'get',
    })
}