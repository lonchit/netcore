import Vue from 'vue';
import VueRouter from 'vue-router';
import AuthStore from '@/stores/auth-store';
import accountLayout from '@/account/account-layout.vue';
import newsLayout from '@/news/news-layout.vue';
import layout from '@/home/layout.vue';


Vue.use(VueRouter);

const router = new VueRouter({
    mode: 'history',
    routes: [
        { path: '/', redirect: '/home' },
        {
            path: '/account',
            component: accountLayout,
            children: [
                { path: 'login', component: require('@/account/views/login/login.vue').default },
                { path: 'register', component: require('@/account/views/register/register.vue').default },
                { path: 'forgot-password', component: require('@/account/views/manage/forgot-password.vue').default },
                { path: 'reset-password', component: require('@/account/views/manage/reset-password.vue').default }
            ]
        },
        {
            path: '/admin',
            component: layout,
            meta: { requiresAuth: true },
            children: [
                { path: 'user-list', component: require('@/admin/views/users/user-list.vue').default },
                { path: 'role-list', component: require('@/admin/views/roles/role-list.vue').default },
                { path: 'create-news', component: require('@/admin/views/news/create/create-news.vue').default },
                { path: 'exceptions', component: require('@/admin/views/exceptions/list/exceptions-list.vue').default }
            ]
        },
        {
            path: '/home',
            component: layout,
            meta: { requiresAuth: true },
            children: [
                { path: '', component: require('@/home/views/home-page.vue').default },
                { path: 'news-list', component: require('@/home/views/news/list/news-list.vue').default }
            ]
        },

    ]
});

router.beforeEach((to: any, from: any, next: any) => {
    if (to.matched.some((record: any) => record.meta.requiresAuth)) {
        // this route requires auth, check if logged in
        // if not, redirect to login page.
        if (!AuthStore.isSignedIn()) {
            next({
                path: '/account/login',
                query: { redirect: to.fullPath }
            });
        }
    }
    next(); // make sure to always call next()!
});

export default router;