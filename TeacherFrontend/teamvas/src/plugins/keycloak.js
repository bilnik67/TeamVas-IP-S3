import Keycloak from 'keycloak-js';

let keycloakConfig  = {
    url: "http://localhost:8080/", 
    realm: process.env.VUE_APP_KEYCLOAK_REALM, 
    clientId: process.env.VUE_APP_KEYCLOAK_CLIENT,
  };
  

const keycloak = new Keycloak(keycloakConfig);

export default {
  install: (app) => {
      app.config.globalProperties.$keycloak = keycloak;

      keycloak.init({ onLoad: 'login-required' })
          .then((auth) => {
              if (!auth) {
                  window.location.reload();
              } else {
                  localStorage.setItem('jwtToken', keycloak.token);
                  localStorage.setItem('jwtRefreshToken', keycloak.refreshToken);

                  keycloak.onTokenExpired = () => {
                      keycloak.updateToken(30).then((refreshed) => {
                          if (refreshed) {
                              console.log('Token was successfully refreshed');
                              localStorage.setItem('jwtToken', keycloak.token);
                              localStorage.setItem('jwtRefreshToken', keycloak.refreshToken);
                          } else {
                              console.warn('Token not refreshed, valid for ' + Math.round(keycloak.tokenParsed.exp + keycloak.timeSkew - new Date().getTime() / 1000) + ' seconds');
                          }
                      }).catch(() => {
                          console.error('Failed to refresh the token, or the session has expired');
                      });
                  };
              }
          })
          .catch((err) => {
              console.error('Authenticated Failed:', err);
          });
  },
};