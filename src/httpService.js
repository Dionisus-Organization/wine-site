// import Vue from 'vue'
import axios from 'axios'

const baseURL = 'http://localhost:5000/wine';

const client = axios.create({
    baseURL: baseURL,
    json: true
})

export default {
    async execute(method, resource, data) {
        return client({
            method,
            url: resource,
            data,
            headers: {
            }
        }).then(req => {
            return req.data
        })
    },
    getAll(page) {
        return axios.get(`${baseURL}?page=${page}`)
        .then(req => {
            return req.data
        })
    },
    getById(id) {
        return this.execute('get', `/${id}`)
    },
    getCount() {
        return this.execute('get', '/number-of-records')
    },
    // getByColor(color) {
    //     return this.execute('get', `/${color}`)
    // },
    create(data) {
        return this.execute('post', '/', data)
    },
    update(id, data) {
        return this.execute('put', `/${id}`, data)
    },
}
