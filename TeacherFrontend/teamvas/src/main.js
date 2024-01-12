import { createApp } from 'vue';
import App from './App.vue';
import KeycloakPlugin from './plugins/keycloak.js';
import router from './router/index.js';
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

const app = createApp(App);

app.use(KeycloakPlugin);

app.use(Toast);

app.use(VueSweetalert2);

app.use(router);


app.mount('#app');

export {app};