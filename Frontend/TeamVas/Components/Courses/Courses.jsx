import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './Courses.module.css';
import { fetchCourses } from '../../Repository/CourseRepository.jsx';

const CourseTile = ({ name, onClick }) => (
  <div className={styles.courseCard}>
    <div className={styles.courseTile} onClick={onClick}>
      <h2 className={styles.courseTitle}>{name}</h2>
    </div>
  </div>

);

const Courses = () => {
  const [courses, setCourses] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const coursesData = await fetchCourses();
        setCourses(coursesData);
      } catch (error) {
        console.error("Error fetching courses:", error);
      }
    };

    fetchData();
  }, []);

  const handleCourseClick = (course) => {
    console.log(`Course clicked: ${course.name}`);
    navigate(`/course/${course.id}`);
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