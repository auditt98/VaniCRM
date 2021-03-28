import Vue from 'vue'
import VueRouter from 'vue-router'
import {authenticationService} from "@/service/authentication.service";

Vue.use(VueRouter)

const routes = [
    {
        path: '/task-list',
        name: 'TaskList',
        component: () => import('../components/task/TaskList.vue')
    },
    {
        path: '/task-update',
        name: 'TaskUpdate',
        component: () => import('../components/task/TaskUpdate.vue')
    },
    {
        path: '/task-detail',
        name: 'TaskDetail',
        component: () => import('../components/task/TaskDetail.vue')
    },
    {
        path: '/group-list',
        name: 'GroupList',
        component: () => import('../components/group/GroupList.vue')
    },
    {
        path: '/group-update',
        name: 'GroupUpdate',
        component: () => import('../components/group/GroupUpdate.vue')
    },
    {
        path: '/reset_password',
        name: 'ResetPassword',
        component: () => import('../components/login/ResetPassword.vue')
    },

    {
        path: '/',
        name: 'DealDetail',
        component: () => import('../components/deal/DealDetail.vue')
    },
    {
        path: '/users',
        name: 'users',
        component: () => import('../components/user/UserList.vue')
    },
    {
        path: '/user-update',
        name: 'user-update',
        component: () => import('../components/user/UserUpdate.vue')
    },
    {
        path: '/userPage',
        name: 'UserPage',
        component: () => import('../components/user/UserPage.vue')
    },
    {
        path: '/leadUpdate',
        name: 'LeadUpdate',
        component: () => import('../components/lead/LeadUpdate.vue')
    },
    {
        path: '/leadDetail',
        name: 'LeadDetail',
        component: () => import('../components/lead/LeadDetail.vue')
    },
    {
        path: '/leadList',
        name: 'LeadList',
        component: () => import('../components/lead/LeadList.vue')
    },
    {
        path: '/dashboard',
        name: 'Dashboard',
        component: () => import('../components/dashboard/Dashboard.vue')
    },
    {
        path: '/login',
        name: 'LoginPage',
        component: () => import('../components/login/LoginPage.vue')
    },
    {
        path: '/about',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import('../views/About.vue')
    },
    // otherwise redirect to home
    {path: '*', redirect: '/'}
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
router.beforeEach((to, from, next) => {
    const {authorize} = to.meta;
    const currentUser = authenticationService.currentUserValue;
    if (authorize) {
        if (!currentUser) {
            return next({path: '/login', query: {returnUrl: to.path}});
        }
        console.log(authorize);
        console.log(currentUser.group);
        if (authorize.length && !authorize.includes(currentUser.group)) {
            alert('Khong co quyen!!');
            return next({path: '/'});
        }
    }

    next();
})
