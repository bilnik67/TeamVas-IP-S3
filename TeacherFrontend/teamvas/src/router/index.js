import { createRouter, createWebHistory } from 'vue-router';
import HelloWorld from '../components/HelloWorld.vue'
import NotFound from '../components/Notfound/Notfound.vue'; 
import Courses from '../components/Courses/Courses.vue';
import NoAccess from '../components/NoAccess/NoAccess.vue';
import Addcourse from '../components/Courses/AddCourse.vue';
import EditCourse from '../components/Courses/EditCourse.vue';
import { app } from '../main.js';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HelloWorld,
  },
  {
    path: '/no-access',
    name: 'NoAccess',
    component: NoAccess,
  
  },
  {
    path: '/courses',
    name: 'Courses',
    component: Courses,
    meta: { requiresAuth: true }
  },
  {
    path: '/add-course',
    name: 'AddCourse',
    component: Addcourse,
    meta: { requiresAuth: true }
  },
  {
    path: '/edit-course/:courseId',
    name: 'EditCourse',
    component: EditCourse,
    meta: { requiresAuth: true }
  },
  {
    path: '/assignments',
    name: 'Assignments',
    component: HelloWorld,
    meta: { requiresAuth: true }

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

    if (!app.config.globalProperties.$keycloak.hasRealmRole('Teacher')) {
    console.log('No access');
    next({ name: 'NoAccess' });
    } else {
    console.log('Access');
    next();
    }
});

export default router;