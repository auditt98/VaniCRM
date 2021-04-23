import Vue from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery/src/jquery.js'
import 'bootstrap/dist/js/bootstrap.min.js'
import '../public/main.css'
import '../public/css/font-awesome.min.css'
import Popover from 'vue-js-popover'
import VPopover from 'vue-js-popover'
import InfiniteLoading from 'vue-infinite-loading';

Vue.use(InfiniteLoading, { /* options */ });
Vue.use(Popover)
Vue.use(VPopover, { tooltip: true })

import { format, parseISO } from 'date-fns';
import {DATE_FORMAT, DATE_TIME_FORMAT} from "@/config/config";
import 'vue-select/dist/vue-select.css';
import Vuetify from 'vuetify'
import DaySpanVuetify from 'dayspan-vuetify'

import Notifications from 'vue-notification'
import vSelect from 'vue-select'
import Vuelidate from 'vuelidate'
import velocity      from 'velocity-animate'
import VueExpandableImage from 'vue-expandable-image'
Vue.use(VueExpandableImage)

Vue.use(Notifications, { velocity })
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
Vue.use(VueScrollTo, {
  container: "body",
  duration: 500,
  easing: "ease",
  offset: -100,
  force: true,
  cancelable: true,
  onStart: false,
  onDone: false,
  onCancel: false,
  x: false,
  y: true
})
Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
