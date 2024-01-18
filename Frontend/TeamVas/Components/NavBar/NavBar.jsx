import React from 'react';
import styles from './NavBar.module.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBook, faUserCircle, faComment, faFilePen} from '@fortawesome/free-solid-svg-icons';
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
        <Link to="/Assignments" className={styles.navItem}>
            <FontAwesomeIcon icon={faFilePen} className={styles.navIcon} />
            <span>Assignments</span>
        </Link>
        <Link to="/messages" className={styles.navItem}>
            <FontAwesomeIcon icon={faComment} className={styles.navIcon} />
            <span>Messages</span>
        </Link>

        <div className={styles.userSection}>

            <div className={styles.centerUserIcon}>
              <FontAwesomeIcon icon={faUserCircle} className={styles.navIcon} />
            </div>
          </div>

          <span className={styles.userName} onClick={() => keycloak.accountManagement()}>
            {keycloak.tokenParsed?.preferred_username ?? 'Unknown User'}
          </span>
          <div className={styles.centerUserSection}>
            {keycloak.authenticated ? (
              <>
                <button>
                  <div onClick={() => keycloak.logout()}>Logout</div>
                </button>
              </>
            ) : (
              <>
                <button>
                  <div onClick={() => keycloak.login()}>Login</div>
                </button>
              </>
            )}
        </div>
    </nav>
    );
};

export default NavBar;