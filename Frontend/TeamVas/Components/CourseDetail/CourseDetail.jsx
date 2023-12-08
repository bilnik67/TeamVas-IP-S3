import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import styles from './CourseDetail.module.css';
import { fetchCourseDetails } from '../../Repository/CourseRepository.jsx';

const CourseDetail = () => {
  const { courseId } = useParams();
  const [course, setCourse] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
        try {
          const courseData = await fetchCourseDetails(courseId);
          setCourse(courseData);
        } catch (error) {
          console.error(`Error fetching course details for course ${courseId}:`, error);
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