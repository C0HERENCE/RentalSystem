import request from "@/api/request";

export function GetAllStudents() {
    return request({
        url: '/api/admin/students',
    })
}

export function GetAllUsers() {
    return request({
        url: '/api/admin/users',
    })
}

export function GetClasses() {
    return request({
        url: '/api/admin/classes',
    })
}

export function GetOrders() {
    return request({
        url: '/api/admin/orders',
    })
}

export function GetDemands() {
    return request({
        url: '/api/admin/demands',
    })
}

export function GetCategories() {
    return request({
        url: '/api/admin/categories',
    })
}

export function GetProducts() {
    return request({
        url: '/api/admin/products',
    })
}

export function AddClasses(grade, major) {
    return request({
        url: '/api/admin/addclasses',
        method: 'post',
        params: {
            grade,
            major
        }
    })
}

export function UpdateCategory(id, newName, enabled) {
    return request({
        url: '/api/admin/updateCategory',
        method: 'post',
        params: {
            id,
            newName,
            enabled
        }
    })
}

export function AddCategories(name) {
    return request({
        url: '/api/admin/addcategories',
        method: 'post',
        params: {
            name
        }
    })
}

export function UpdateStudentById(id, name, schoolNum, IdCard, classId) {
    return request({
        url: '/api/admin/updatestudents',
        method: 'post',
        params: {
            id,
            name,
            schoolNum,
            IdCard,
            classId
        }
    })
}

export function UpdateUserById(id, enabled) {
    return request({
        url: '/api/admin/updateusers',
        method: 'post',
        params: {
            id,
            enabled
        }
    })
}

export function UpdateProductById(id, Checked, enabled, categoryId, keywords) {
    return request({
        url: '/api/admin/updateProduct',
        method: 'post',
        params: {
            id, Checked, enabled, categoryId, keywords
        }
    })
}

export function UpdateDemandById(id, Checked, enabled) {
    return request({
        url: '/api/admin/demands',
        method: 'post',
        params: {
            id, Checked, enabled
        }
    })
}

export function UpdateOrderById(id, status) {
    return request({
        url: '/api/admin/updateorders',
        method: 'post',
        params: {
            id, status
        }
    })
}