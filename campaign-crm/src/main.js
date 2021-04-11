import Vue from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery/src/jquery.js'
import 'bootstrap/dist/js/bootstrap.min.js'
import { format, parseISO } from 'date-fns';
import {DATE_FORMAT, DATE_TIME_FORMAT} from "@/config/config";
import 'vue-select/dist/vue-select.css';
import Vuetify from 'vuetify'
import DaySpanVuetify from 'dayspan-vuetify'

import vSelect from 'vue-select'
import Vuelidate from 'vuelidate'

Vue.use(Vuetify);

Vue.use(DaySpanVuetify, {
  methods: {
    getDefaultEventColor: () => '#1976d2'
  }
});
Vue.use(Vuelidate)
Vue.component('vc-select', vSelect)
const VueScrollTo = require('vue-scrollto');


Vue.filter('formatDateTime', value => {
  if (value) {
    return format(parseISO(value), DATE_TIME_FORMAT);
  }
  return '';
});
Vue.filter('formatDate', value => {
  if (value) {
    return format(parseISO(value), DATE_FORMAT);
  }
  return '';
});
Vue.use(VueScrollTo)
Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')