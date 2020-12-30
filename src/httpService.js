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
        return axios.get(baseURL, {
            params: { page }
        })
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
    getRecommend(wines_id) {
        console.log(wines_id)
        return axios.post(`${baseURL}/recommendation`, {
            Wines: wines_id
        })
    },
    getFilter(color, wine_type, country, vintage) {
        console.log(color, wine_type, country, vintage);
        return axios.get(`${baseURL}/filter`, {
            params: {
                color,
                wine_type,
                country,
                vintage
            }
        })
    },
    create(data) {
        return this.execute('post', '/', data)
    },
    update(id, data) {
        return this.execute('put', `/${id}`, data)
    },
}
