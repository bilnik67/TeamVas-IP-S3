import React, { useEffect, useState } from "react";
import { useKeycloak } from "@react-keycloak/web";
import Teamvaslogo from '../../public/Images/TeamVasLogo.png';
import Unauthorized from "../../Components/Unauthorized/Unauthorized";

const AuthenticatedRoute = ({ children }) => {
  const { keycloak, initialized } = useKeycloak();

  if (!initialized) {
    return (
      <img
        src={Teamvaslogo}
        className="h-1/4 animate-pulse"
      />
    );
  }

  if (!keycloak.authenticated) {
    return <Unauthorized></Unauthorized>
    }

  return children;
};

export default AuthenticatedRoute;