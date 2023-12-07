import React from 'react';
import styles from './NavBar.module.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBook, faBriefcase } from '@fortawesome/free-solid-svg-icons';
import { Link } from 'react-router-dom';
import Teamvaslogo from '../../public/Images/TeamVasLogo.png';

const NavBar = () => {
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
      </nav>
    );
};

export default NavBar;