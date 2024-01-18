import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './Assignment.module.css';
import { getAllAssignments, getAllSubmittedAssignments } from '../../Repository/AssignmentRepository';

const AssignmentTile = ({ title, onClick, onHandInClick }) => (
    <div className={styles.assignmentCard}>
      <div className={styles.assignmentTile}>
        <h2 className={styles.assignmentTitle}>{title}</h2>
      </div>
      <button className={styles.handInButton} onClick={onHandInClick}>Hand In</button>
    </div>
  );
  
  const Assignments = () => {
    const [assignments, setAssignments] = useState([]);
    const navigate = useNavigate();
  
    useEffect(() => {
      const fetchData = async () => {
        try {
          const assignmentsData = await getAllAssignments();
          setAssignments(assignmentsData);
        } catch (error) {
          console.error("Error fetching assignments:", error);
        }
      };
  
      fetchData();
    }, []);
  
    const handleAssignmentClick = async (assignment) => {
      console.log(`Assignment clicked: ${assignment.title}`);
      try {
        const submittedAssignmentsData = await getAllSubmittedAssignments(assignment.id);
        console.log("Submitted Assignments:", submittedAssignmentsData);
      } catch (error) {
        console.error("Error fetching submitted assignments:", error);
      }
    };
  
    const handleHandInClick = (assignment) => {
      console.log(`Hand In clicked for assignment: ${assignment.title}`);
      navigate(`/handinassignment/${assignment.id}`);
    };
  
    return (
      <div className={styles.assignmentsContainer}>
        <h1 className={styles.title}>Assignments</h1>
        <div className={styles.masonryLayout}>
          {assignments.map((assignment, index) => (
            <AssignmentTile
              key={index}
              title={assignment.title}
              onClick={() => handleAssignmentClick(assignment)}
              onHandInClick={() => handleHandInClick(assignment)}
            />
          ))}
        </div>
      </div>
    );
  }
  
  export default Assignments;