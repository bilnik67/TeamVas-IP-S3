import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { postSubmittedAssignment } from '../../Repository/AssignmentRepository.jsx';
import { useParams } from 'react-router-dom';
import styles from './Assignment.module.css';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const HandInAssignment = () => {
  const [assignmentText, setAssignmentText] = useState('');
  const navigate = useNavigate();
  const { assignmentId } = useParams();

  const handleHandInClick = async () => {
    try {
      await postSubmittedAssignment(assignmentId, assignmentText);

      toast.success('Assignment successfully submitted!', {
        position: 'top-right',
        autoClose: 2000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });

      navigate('/assignments');
    } catch (error) {
      console.error("Error posting the assignment:", error);
    }
  };

  return (    
    <div className={styles.handInAssignmentContainer}>
      <h1 className={styles.title}>Hand In Assignment</h1>
      <div className={styles.assignmentTextArea}>
        <textarea
          rows="6"
          cols="50"
          placeholder="Enter your assignment text here..."
          value={assignmentText}
          onChange={(e) => setAssignmentText(e.target.value)}
        ></textarea>
      </div>
      <button className={styles.handInButton} onClick={handleHandInClick}>Hand In Assignment</button>
      <ToastContainer
        position="top-right"
        autoClose={2000}
        hideProgressBar={false}
        closeOnClick
        pauseOnHover
        draggable
        progress={undefined}
        toastClassName={styles.toast} 
        bodyClassName={styles.toastBody} 
    />
    </div>
  );
}

export default HandInAssignment;