import React from 'react';
import styles from './NavBar.module.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBook, faBriefcase, faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { Link } from 'react-router-dom';
import Teamvaslogo from '../../public/Images/TeamVasLogo.png';
import { useKeycloak } from '@react-keycloak/web';

const NavBar = () => {
  const { keycloak } = useKeycloak();
    return (
      <nav className={styles.navbar}>
        <Link to="/">
            <img src={Teamvaslogo} alt="Logo" className={styles.logo} /> 
        </Link>
        <Link to="/Courses" className={styles.navItem}>
            <FontAwesomeIcon icon={faBook} className={styles.navIcon} />
            <span>Courses</span>
        </Link>
        <Link to="/" className={styles.navItem}>
            <FontAwesomeIcon icon={faBriefcase} className={styles.navIcon} />
            <span>Portfolio</span>
        </Link>
        <div className={styles.userSection}>
          <button>
            <div onClick={() => keycloak.logout()}>
              logout
            </div>
          </button>
          <button>
            <div onClick={() => keycloak.login()}>
              login
            </div>
          </button>

          <FontAwesomeIcon icon={faUserCircle} className={styles.userIcon} />
          <span className={styles.userName} onClick={() => keycloak.accountManagement()}>
            {keycloak.tokenParsed?.preferred_username ?? 'Unknown User'}
          </span>
        </div>
      </nav>
    );
};

export default NavBar;