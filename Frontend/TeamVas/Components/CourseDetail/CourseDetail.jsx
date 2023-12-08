import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import styles from './CourseDetail.module.css';

const CourseDetail = () => {
  const { courseId } = useParams();
  const [course, setCourse] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`https://localhost:7232/Courses/${courseId}`);
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setCourse(data);
      } catch (error) {
        console.error("There was a problem fetching course data:", error);
      }
    };

    fetchData();
  }, [courseId]); 

  return (
    <div className={styles.container}>
      {course && (
        <div className={styles.courseHeader}>
          <h2 className={styles.courseTitle}>{course.name}</h2>
        </div>
      )}
      {course && (
        <p className={styles.courseDescription}>{course.description}</p>
      )}
    </div>
  );
};

export default CourseDetail;