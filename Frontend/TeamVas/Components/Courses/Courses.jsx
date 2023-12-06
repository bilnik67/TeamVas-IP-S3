import React from 'react';
import './Courses.module.css';
import { MDBTable, MDBTableBody, MDBTableHead, MDBBtn } from 'mdb-react-ui-kit';


const coursesData = [
  {
    category: 'S3 Portfolio - GP and IP',
    courses: [
      { title: 'Portfolio S3 DB - Bachelor', date: 'Jan 19, 2024', points: '0 pts' },
      { title: 'Portfolio S3 DB - Associate Degree', date: 'Jan 19, 2024', points: '0 pts' },
    ],
  },
  {
    category: 'Assignments Dropbox IP',
    courses: [
      { title: 'Feedback Research', date: 'Nov 9', points: '0 pts' },
      { title: 'Portfolio Feedback 1', date: 'Nov 10', points: '0 pts' },
    ],
  },
];

const Course = ({ title, date, points }) => (
  <div className="course">
    <h4>{title}</h4>
    <p>{date} | {points}</p>
  </div>
);

const CourseCategory = ({ category, courses }) => (
  <section>
    <h3>{category}</h3>
    {courses.map((course, index) => <Course key={index} {...course} />)}
  </section>
);


const Courses = () => {
    return (
      <div>
        {coursesData.map((category, categoryIndex) => (
          <div key={categoryIndex} className="categoryTable">
            <h2>{category.category}</h2>
            <MDBTable align='middle'>
              <MDBTableHead light>
                <tr>
                  <th scope='col'>#</th>
                  <th scope='col'>Title</th>
                  <th scope='col'>Date</th>
                  <th scope='col'>Points</th>
                  <th scope='col'>Actions</th>
                </tr>
              </MDBTableHead>
              <MDBTableBody>
                {category.courses.map((item, itemIndex) => (
                  <tr key={`${categoryIndex}-${itemIndex}`}>
                    <th scope='row'>{itemIndex + 1}</th>
                    <td>{item.title}</td>
                    <td>{item.date}</td>
                    <td>{item.points}</td>
                    <td>
                      <MDBBtn color='link' size='sm'>
                        <i className='fas fa-times'></i>
                      </MDBBtn>
                    </td>
                  </tr>
                ))}
              </MDBTableBody>
            </MDBTable>
          </div>
        ))}
      </div>
    );
  }
  
  export default Courses;

