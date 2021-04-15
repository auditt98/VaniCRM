import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
    {
        path: '/calendar',
        name: 'Calendar',
        component: () => import('../components/calendar/Calendar.vue'),
        meta: { title: 'Calendar' }
    },
    {
        path: '/deal-detail',
        name: 'DealDetail',
        component: () => import('../components/deal/DealDetail.vue'),
        meta: { title: 'Deal Detail' }
    },
    {
        path: '/deal-create',
        name: 'DealCreate',
        component: () => import('../components/deal/DealCreateEdit.vue'),
        meta: { title: 'Create Deal' }
    },
    {
        path: '/deal-update',
        name: 'DealUpdate',
        component: () => import('../components/deal/DealCreateEdit.vue'),
        meta: { title: 'Update Deal' }
    },
    {
        path: '/deals',
        name: 'DealList',
        component: () => import('../components/deal/DealList.vue'),
        meta: { title: 'List Deal' }
    },
    {
        path: '/meeting-create',
        name: 'MeetingCreate',
        component: () => import('../components/meeting/MeetingCreateEdit.vue'),
        meta: { title: 'Create Meeting' }
    },
    {
        path: '/meeting-update',
        name: 'MeetingUpdate',
        component: () => import('../components/meeting/MeetingCreateEdit.vue'),
        meta: { title: 'Update Meeting' }
    },
    {
        path: '/meeting-detail',
        name: 'MeetingDetail',
        component: () => import('../components/meeting/MeetingDetail.vue'),
        meta: { title: 'Meeting Detail' }
    },
    {
        path: '/call-create',
        name: 'CallCreate',
        component: () => import('../components/call/CallCreateEdit.vue'),
        meta: { title: 'Create Call' }
    },
    {
        path: '/call-update',
        name: 'CallUpdate',
        component: () => import('../components/call/CallCreateEdit.vue'),
        meta: { title: 'Update Call' }
    },
    {
        path: '/call-detail',
        name: 'CallDetail',
        component: () => import('../components/call/CallDetail.vue'),
        meta: { title: 'Call Detail' }
    },
    {
        path: '/report',
        name: 'Report',
        component: () => import('../components/report/Report.vue'),
        meta: { title: 'Report' }
    },
    {
        path: '/tasks',
        name: 'TaskList',
        component: () => import('../components/task/TaskList.vue'),
        meta: { title: 'List Task' }
    },
    {
        path: '/task-create',
        name: 'TaskCreate',
        component: () => import('../components/task/TaskUpdate.vue'),
        meta: { title: 'Create Task' }
    },
    {
        path: '/task-update',
        name: 'TaskUpdate',
        component: () => import('../components/task/TaskUpdate.vue'),
        meta: { title: 'Update Task' }
    },
    {
        path: '/task-detail',
        name: 'TaskDetail',
        component: () => import('../components/task/TaskDetail.vue'),
        meta: { title: 'Task Detail' }
    },
    {
        path: '/group',
        name: 'GroupList',
        component: () => import('../components/group/GroupList.vue'),
        meta: { title: 'List Group' }
    },
    {
        path: '/group-update',
        name: 'GroupUpdate',
        component: () => import('../components/group/GroupUpdate.vue'),
        meta: { title: 'Update Group' }
    },
    {
        path: '/reset-password',
        name: 'ResetPassword',
        component: () => import('../components/login/ResetPassword.vue'),
        meta: { title: 'Reset Password' }
    },

    {
        path: '/',
        redirect: '/dashboard-sale'
    },
    {
        path: '/users',
        name: 'Users',
        component: () => import('../components/user/UserList.vue'),
        meta: { title: 'List User' }
    },
    {
        path: '/user-create',
        name: 'UserCreate',
        component: () => import('../components/user/UserCreate.vue'),
        meta: { title: 'Create User' }
    },
    {
        path: '/user-update',
        name: 'UserUpdate',
        component: () => import('../components/user/UserUpdate.vue'),
        meta: { title: 'Update User' }
    },
    {
        path: '/user-page',
        name: 'UserPage',
        component: () => import('../components/user/UserPage.vue'),
        meta: { title: 'User Page' }
    },
    {
        path: '/lead-create',
        name: 'LeadCreate',
        component: () => import('../components/lead/LeadCreate.vue'),
        meta: { title: 'Create Lead' }
    },
    {
        path: '/lead-detail',
        name: 'LeadDetail',
        component: () => import('../components/lead/LeadDetail.vue'),
        meta: { title: 'Lead Detail' }
    },
    {
        path: '/leads',
        name: 'LeadList',
        component: () => import('../components/lead/LeadList.vue'),
        meta: { title: 'Lead List' }
    },
    {
        path: '/lead-update',
        name: 'LeadUpdate',
        component: () => import('../components/lead/LeadCreate.vue'),
        meta: { title: 'Update Lead' }
    },
    //campaign
    {
        path: '/campaign-create',
        name: 'CampaignCreate',
        component: () => import('../components/campaign/CampaignCreate.vue'),
        meta: { title: 'Create Campaign' }
    },
    {
        path: '/campaign-detail',
        name: 'CampaignDetail',
        component: () => import('../components/campaign/CampaignDetail.vue'),
        meta: { title: 'Campaign Detail' }
    },
    {
        path: '/campaigns',
        name: 'CampaignList',
        component: () => import('../components/campaign/CampaignList.vue'),
        meta: { title: 'List Campaign' }
    },
    //end campaign
    {
        path: '/dashboard-sale',
        name: 'DashboardSale',
        component: () => import('../components/dashboard/Dashboard.vue'),
        meta: { title: 'Dashboard Sale' }
    },
    {
        path: '/dashboard-marketing',
        name: 'DashboardMarketing',
        component: () => import('../components/dashboard/Dashboard.vue'),
        meta: { title: 'Dashboard Marketing' }
    },
    {
        path: '/login',
        name: 'LoginPage',
        component: () => import('../components/login/LoginPage.vue'),
        meta: { title: 'Login' }
    },
    {
        path: '/contacts',
        name: 'ContactList',
        component: () => import('../components/contact/ContactList.vue'),
        meta: { title: 'List Contact' }
    },
    {
        path: '/contact-update',
        name: 'ContactUpdate',
        component: () => import('../components/contact/ContactUpdate.vue'),
        meta: { title: 'Update Contact' }
    },
    {
        path: '/contact-create',
        name: 'ContactCreate',
        component: () => import('../components/contact/ContactUpdate.vue'),
        meta: { title: 'Create Contact' }
    },
    {
        path: '/contact-detail',
        name: 'ContactDetail',
        component: () => import('../components/contact/ContactDetail.vue'),
        meta: { title: 'Contact Detail' }
    },
    {
        path: '/accounts',
        name: 'AccountList',
        component: () => import('../components/account/AccountList.vue'),
        meta: { title: 'List Account' }
    },
    {
        path: '/account-create',
        name: 'AccountCreate',
        component: () => import('../components/account/AccountCreate.vue'),
        meta: { title: 'Create Account' }
    },
    {
        path: '/account-update',
        name: 'AccountUpdate',
        component: () => import('../components/account/AccountCreate.vue'),
        meta: { title: 'Update Account' }
    },
    {
        path: '/account-detail',
        name: 'AccountDetail',
        component: () => import('../components/account/AccountDetail.vue'),
        meta: { title: 'Account Detail' }
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
    /*const {authorize} = to.meta;
    const currentUser = authenticationService.currentUserValue;
    if (authorize) {
        if (!currentUser) {
            return next({path: '/login', query: {returnUrl: to.path}});
        }
        if (authorize.length && !authorize.includes(currentUser.group)) {
            alert('Khong co quyen!!');
            return next({path: '/'});
        }
    }*/

    next();
})
const DEFAULT_TITLE = 'CRM';
router.afterEach((to) => {
    Vue.nextTick(() => {
        document.title = to.meta.title || DEFAULT_TITLE;
    });
});