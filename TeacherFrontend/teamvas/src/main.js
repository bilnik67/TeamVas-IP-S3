import { createApp } from 'vue';
import App from './App.vue';
import KeycloakPlugin from './plugins/keycloak.js';
import router from './router/index.js';

const app = createApp(App);

app.use(KeycloakPlugin);

app.use(router);

app.mount('#app');

export {app};