import Vue from 'vue'
import VueRouter from 'vue-router'
import {authenticationService} from "@/service/authentication.service";

Vue.use(VueRouter)

const routes = [
    {
        path: '/deal-detail',
        name: 'DealDetail',
        component: () => import('../components/deal/DealDetail.vue')
    },
    {
        path: '/deal-create',
        name: 'DealCreate',
        component: () => import('../components/deal/DealCreateEdit.vue')
    },
    {
        path: '/deal-update',
        name: 'DealUpdate',
        component: () => import('../components/deal/DealCreateEdit.vue')
    },
    {
        path: '/deals',
        name: 'DealList',
        component: () => import('../components/deal/DealList.vue')
    },
    {
        path: '/meeting-create',
        name: 'MeetingCreate',
        component: () => import('../components/meeting/MeetingCreateEdit.vue')
    },
    {
        path: '/meeting-update',
        name: 'MeetingUpdate',
        component: () => import('../components/meeting/MeetingCreateEdit.vue')
    },
    {
        path: '/meeting-detail',
        name: 'MeetingDetail',
        component: () => import('../components/meeting/MeetingDetail.vue')
    },
    {
        path: '/call-create',
        name: 'CallCreate',
        component: () => import('../components/call/CallCreateEdit.vue')
    },
    {
        path: '/call-update',
        name: 'CallUpdate',
        component: () => import('../components/call/CallCreateEdit.vue')
    },
    {
        path: '/call-detail',
        name: 'CallDetail',
        component: () => import('../components/call/CallDetail.vue')
    },
    {
        path: '/report',
        name: 'Report',
        component: () => import('../components/report/Report.vue')
    },
    {
        path: '/task-list',
        name: 'TaskList',
        component: () => import('../components/task/TaskList.vue')
    },
    {
        path: '/task-create',
        name: 'TaskCreate',
        component: () => import('../components/task/TaskUpdate.vue')
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
        path: '/reset-password',
        name: 'ResetPassword',
        component: () => import('../components/login/ResetPassword.vue')
    },

    {
        path: '/',
        redirect: '/dashboard'
    },
    {
        path: '/users',
        name: 'Users',
        component: () => import('../components/user/UserList.vue')
    },
    {
        path: '/user-create',
        name: 'UserCreate',
        component: () => import('../components/user/UserCreate.vue')
    },
    {
        path: '/user-update',
        name: 'UserUpdate',
        component: () => import('../components/user/UserUpdate.vue')
    },
    {
        path: '/user-page',
        name: 'UserPage',
        component: () => import('../components/user/UserPage.vue')
    },
    {
        path: '/lead-create',
        name: 'LeadCreate',
        component: () => import('../components/lead/LeadCreate.vue')
    },
    {
        path: '/lead-detail',
        name: 'LeadDetail',
        component: () => import('../components/lead/LeadDetail.vue')
    },
    {
        path: '/leads',
        name: 'LeadList',
        component: () => import('../components/lead/LeadList.vue')
    },
    {
        path: '/lead-update',
        name: 'LeadUpdate',
        component: () => import('../components/lead/LeadCreate.vue')
    },
    //campaign
    {
        path: '/campaign-create',
        name: 'CampaignCreate',
        component: () => import('../components/campaign/CampaignCreate.vue')
    },
    {
        path: '/campaign-detail',
        name: 'CampaignDetail',
        component: () => import('../components/campaign/CampaignDetail.vue')
    },
    {
        path: '/campaigns',
        name: 'CampaignList',
        component: () => import('../components/campaign/CampaignList.vue')
    },
    //end campaign
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
    {
        path: '/contacts',
        name: 'ContactList',
        component: () => import('../components/contact/ContactList.vue')
    },
    {
        path: '/contact-update',
        name: 'ContactUpdate',
        component: () => import('../components/contact/ContactUpdate.vue')
    },
    {
        path: '/contact-create',
        name: 'ContactCreate',
        component: () => import('../components/contact/ContactUpdate.vue')
    },
    {
        path: '/contact-detail',
        name: 'ContactDetail',
        component: () => import('../components/contact/ContactDetail.vue')
    },
    {
        path: '/accounts',
        name: 'AccountList',
        component: () => import('../components/account/AccountList.vue')
    },
    {
        path: '/account-create',
        name: 'AccountCreate',
        component: () => import('../components/account/AccountCreate.vue')
    },
    {
        path: '/account-update',
        name: 'AccountUpdate',
        component: () => import('../components/account/AccountCreate.vue')
    },
    {
        path: '/account-detail',
        name: 'AccountDetail',
        component: () => import('../components/account/AccountDetail.vue')
    },
    {
        path: '/meeting-create',
        name: 'MeetingCreate',
        component: () => import('../components/meeting/MeetingCreateEdit.vue')
    },
    {
        path: '/meeting-update',
        name: 'MeetingEdit',
        component: () => import('../components/meeting/MeetingCreateEdit.vue')
    },
    {
        path: '/meeting-detail',
        name: 'MeetingDetail',
        component: () => import('../components/meeting/MeetingDetail.vue')
    },
    {
        path: '/test',
        name: 'test',
        component: () => import('../components/user/test.vue')
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
        if (authorize.length && !authorize.includes(currentUser.group)) {
            alert('Khong co quyen!!');
            return next({path: '/'});
        }
    }

    next();
})
