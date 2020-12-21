// import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
    baseURL: 'http://localhost:5000/api/wine',
    json: true
})

export default {
    async execute(method, resource, data) {
        // const accessToken = await Vue.prototype.$auth.getAccessToken()
        // console.log("token", accessToken);
        return client({
            method,
            url: resource,
            data,
            headers: {
                // Authorization: `Bearer ${accessToken}`
            }
        }).then(req => {
            return req.data
        })
    },
    getAll() {
        return this.execute('get', '/')
    },
    getById(id) {
        return this.execute('get', `/${id}`)
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
    // getters: {
    //     getWines: this.getAll().then((data) => { console.log("my", data)}),
    //     // getPages: (state) => Math.ceil(state.wineCount / API_URLS.LIMIT),
    // }
}