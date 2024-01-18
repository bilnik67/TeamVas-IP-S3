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
  import Assignments from '../Components/Assignments/Assignment.jsx';
  import HandInAssignment from '../Components/Assignments/HandInAssignment.jsx';
  import Chat from '../Components/Chat/Chat.jsx';
  import { ReactKeycloakProvider } from '@react-keycloak/web';


  function App() {
     
    return (
      <ReactKeycloakProvider authClient={keycloak}>
        <BrowserRouter>
          <Layout>
            <Routes>
            <Route path="/" element={<Homepage />} />

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
                <Route path="/messages" element={
                <AuthenticatedRoute>
                  <Chat />
                </AuthenticatedRoute>
                } />
                <Route path="/assignments" element={
                  <AuthenticatedRoute>
                    <Assignments />
                  </AuthenticatedRoute>
                } />
                <Route path="/handinassignment/:assignmentId" element={
                  <AuthenticatedRoute>
                    <HandInAssignment />
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
