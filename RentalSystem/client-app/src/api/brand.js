import request from "@/utils/request";

export function getBrands() {
    return request({
        url: '/brand',
        method: 'get',
    })
}