import React, { useState, useEffect, useRef } from 'react';
import Keycloak from 'keycloak-js';


const keycloak = new Keycloak({
    url: "http://localhost:8080/",
    realm: import.meta.env.VITE_KEYCLOAK_REALM,
    clientId: import.meta.env.VITE_KEYCLOAK_CLIENT,
});

export default keycloak;