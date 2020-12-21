import Vue from 'vue'
import Router from 'vue-router'

import Auth from '@okta/okta-vue'

Vue.use(Auth, {
    issuer: 'https://dev-914982.okta.com/oauth2/default',
    clientId: '0oatq53f87ByM08MQ4x6',
    redirectUri: 'https://localhost:5001/implicit/callback',
    scope: 'openid profile email'
})

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
            path: '/card/:wine_id/:vintage',
            name: 'oneCard',
            component: () => import('../dashboards/WineCard.vue'),
            props: true
        },
        {
            path: '/implicit/callback',
            component: Auth.handleCallback()
        }
    ]
})

router.beforeEach(Vue.prototype.$auth.authRedirectGuard())

export default router
