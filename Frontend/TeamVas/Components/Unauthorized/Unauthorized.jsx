import React from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';

const Unauthorized = () => {
  return (
    <div>
      <h1>Unauthorized Access</h1>
      <p>You do not have permission to view this page.</p>
      <Link to="/">Go Back to Home</Link>
    </div>
  );
};

export default Unauthorized;