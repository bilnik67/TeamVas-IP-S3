import { createRouter, createWebHistory } from 'vue-router';
import HelloWorld from '../components/HelloWorld.vue'
import NotFound from '../components/Notfound/Notfound.vue'; 
import Courses from '../components/Courses/Courses.vue';
import NoAccess from '../components/NoAccess/NoAccess.vue';
import { app } from '../main.js';
import keycloak from '@/plugins/keycloak';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HelloWorld
  },
  
  {
    path: '/no-access',
    name: 'NoAccess',
    component: NoAccess
  },
  {
    path: '/courses',
    name: 'Courses',
    component: Courses,
    meta: { requiresAuth: true }
  },
  {
    path: '/assignments',
    name: 'Assignments',
    component: HelloWorld
  },
  {
    path: '/:pathMatch(.*)*', 
    name: 'NotFound',
    component: NotFound
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});


router.beforeEach((to, from, next) => {
    const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
  
    if (!requiresAuth) {
      next();
      return;
    }

    var role = app.config.globalProperties.$keycloak.realmAccess.roles[1];
    console.log(role);
  
    if (!app.config.globalProperties.$keycloak.authenticated) {
      console.log('Not authenticated, initiating login');
      keycloak.login();
      return;
    }

        if (!app.config.globalProperties.$keycloak.hasRealmRole(role)) {
        console.log('No access');
        next({ name: 'NoAccess' });
        } else {
        console.log('Access');
        next();
        }
    });

export default router;
