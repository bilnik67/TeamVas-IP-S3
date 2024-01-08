  import { useState } from 'react'
  import reactLogo from './assets/react.svg'
  import viteLogo from '/vite.svg'
  import NavBar from '../Components/NavBar/NavBar.jsx'
  import { Routes, Route, BrowserRouter } from 'react-router-dom';
  import './App.css'
  import Homepage from '../Components/HomePage/HomePage.jsx';
  import Courses from '../Components/Courses/Courses.jsx';
  import Layout from '../Components/Layout/Layout.jsx'
  import keycloak from './Utils/useAuth.jsx';
  import CourseDetail from '../Components/CourseDetail/CourseDetail.jsx'
  import AuthenticatedRoute from './Utils/AuthenticatedRoute';
  import { ReactKeycloakProvider } from '@react-keycloak/web';

  function App() {
     
    return (
      <ReactKeycloakProvider authClient={keycloak}>
        <BrowserRouter>
          <Layout>
            <Routes>
              {/* Always visible route */}
            <Route path="/" element={<Homepage />} />

            {/* Conditionally visible routes */}
              <>
                <Route path="/courses" element={
                <AuthenticatedRoute>
                  <Courses />
                </AuthenticatedRoute>
                } />
                <Route path="/course/:courseId" element={
                <AuthenticatedRoute>
                  <CourseDetail />
                </AuthenticatedRoute>
                } />
              </>
            </Routes>
          </Layout>
        </BrowserRouter>
      </ReactKeycloakProvider>

    )
  }

  export default App
