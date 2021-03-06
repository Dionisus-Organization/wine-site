import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

let router = new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            name: 'wineList',
            component: () => import('../dashboards/WineList.vue'),
            props: true
        },
        {
            path: '/card/:id/:vintage',
            name: 'oneCard',
            component: () => import('../dashboards/WineCard.vue'),
            props: true
        },
        {
            path: '/recommend',
            name: 'recommend',
            component: () => import('../dashboards/WineRatesTables.vue'),
            props: true
        },
    ]
})

export default router
