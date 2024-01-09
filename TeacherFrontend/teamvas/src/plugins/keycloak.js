import Keycloak from 'keycloak-js';

let keycloakConfig  = {
    url: process.env.VUE_APP_KEYCLOAK_URL, 
    realm: process.env.VUE_APP_KEYCLOAK_REALM, 
    clientId: process.env.VUE_APP_KEYCLOAK_CLIENT,
  };
  

const keycloak = new Keycloak(keycloakConfig);

export default {
  install: (app) => {
    app.config.globalProperties.$keycloak = keycloak;

    keycloak
      .init({ onLoad: 'login-required' })
      .then((auth) => {
        if (!auth) {
          window.location.reload();
        } else {
          app.config.globalProperties.$keycloak.hasRealmRole = (Teacher) => {
            return keycloak.hasResourceRole(Teacher) || keycloak.hasRealmRole(Teacher);
          };
        }
      })
      .catch((err) => {
        console.error('Authenticated Failed:', err);
      });
  },
};