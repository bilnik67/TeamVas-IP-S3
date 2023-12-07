import React, { useState, useEffect } from 'react';
import styles from './Courses.module.css';

const CourseTile = ({ name, onClick }) => (
  <div className={styles.courseCard}>
    <div className={styles.courseTile} onClick={onClick}>
      <h2 className={styles.courseTitle}>{name}</h2>
    </div>
  </div>

);

const Courses = () => {
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('https://localhost:7232/Courses');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setCourses(data);
      } catch (error) {
        console.error("There was a problem fetching course data:", error);
      }
    };

    fetchData();
  }, []);

  const handleCourseClick = (course) => {
    console.log(`Course clicked: ${course.name}`);
  };

  return (
    <div className={styles.coursesContainer}>
      <h1 className={styles.title}>Your Courses</h1>
      <div className={styles.masonryLayout}>
        {courses.map((course, index) => (
          <CourseTile key={index} {...course} onClick={() => handleCourseClick(course)} />
        ))}
      </div>
    </div>
  );
}

export default Courses;