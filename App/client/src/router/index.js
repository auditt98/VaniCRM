import Vue from 'vue'
import VueRouter from 'vue-router'
import {route} from "../utils/routes.js"
import BaseLayout from "../components/uicomponents/BaseLayout.vue"
// import login from '../components/views/'

Vue.use(VueRouter)

//define routes
//define nested route manually (for layout injection) 
const routes = [
  route('Login', null, '/login','account'),
  route('Register', null, '/register', 'account'),
  {
    path: '/',
    name: 'Home',
    component: BaseLayout,
    children: [

    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
