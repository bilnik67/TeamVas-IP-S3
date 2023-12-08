  import { useState } from 'react'
  import reactLogo from './assets/react.svg'
  import viteLogo from '/vite.svg'
  import NavBar from '../Components/NavBar/NavBar.jsx'
  import { Routes, Route } from 'react-router-dom';
  import './App.css'
  import Homepage from '../Components/HomePage/HomePage.jsx';
  import Courses from '../Components/Courses/Courses.jsx';
  import Layout from '../Components/Layout/Layout.jsx'
  import CourseDetail from '../Components/CourseDetail/CourseDetail.jsx'

  function App() {

    return (
      <Layout>
        <Routes>
          <Route path="/" element={<Homepage />} />
          <Route path="/courses" element={<Courses />} />
          <Route path="/course/:courseId" element={<CourseDetail />} />
        </Routes>
      </Layout>

    )
  }

  export default App
