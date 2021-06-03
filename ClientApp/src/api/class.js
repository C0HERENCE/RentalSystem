import request from "./request";

export function getAllClasses() {
    return request({
        url: '/api/class/get',
        method: 'get'
    })
}