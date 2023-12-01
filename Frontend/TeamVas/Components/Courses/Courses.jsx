import React from 'react';
import './Courses.module.css';

const dummyCourses = [
  { id: 1, title: 'Course 1' },
  { id: 2, title: 'Course 2' },
  { id: 3, title: 'Course 3' },
  { id: 4, title: 'Course 4' },
  { id: 5, title: 'Course 5' },
];

const Courses = () => {
  return (
    <div className="courses-container">
      <h1 className="courses-heading">Your Courses</h1>
      <ul className="courses-list">
        {dummyCourses.map((course) => (
          <li key={course.id} className="course-item">
            {course.title}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Courses;