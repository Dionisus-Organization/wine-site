import Vue from 'vue'
import App from './App.vue'
import { ValidationProvider } from 'vee-validate/dist/vee-validate.full.esm';
import VuejsPaginate from 'vuejs-paginate';
import BootstrapVue from 'bootstrap-vue'
import './custom.scss';

import router from './router'

Vue.config.productionTip = false

Vue.use(BootstrapVue);

Vue.component('ValidationProvider', ValidationProvider);
Vue.component('paginate', VuejsPaginate);

new Vue({
    router,
    render: h => h(App),
}).$mount('#app')
