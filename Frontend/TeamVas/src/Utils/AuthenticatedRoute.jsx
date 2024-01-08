import React, { useEffect, useState } from "react";
import { useKeycloak } from "@react-keycloak/web";
import Teamvaslogo from '../../public/Images/TeamVasLogo.png';

const AuthenticatedRoute = ({ children }) => {
  const { keycloak, initialized } = useKeycloak();

  if (!initialized) {
    return (
      <div className="h-screen blue flex justify-center content-center items-center">
        <img
          src={Teamvaslogo}
          className="h-1/4 animate-pulse"
        />
      </div>
    );
  }

  if (!keycloak.authenticated) {
    return <div>Privateroute</div>
    }

  return children;
};

export default AuthenticatedRoute;