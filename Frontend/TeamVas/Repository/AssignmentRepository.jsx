import axios from 'axios';

const baseURL = '/Assignments';

export const getAllAssignments = async () => {
  try {
    const response = await axios.get(baseURL);
    return response.data;
  } catch (error) {
    console.error("There was a problem fetching assignments:", error);
    throw error;
  }
};

export const getAllSubmittedAssignments = async (assignmentId) => {
  try {
    const response = await axios.get(`${baseURL}/${assignmentId}/submissions`);
    return response.data;
  } catch (error) {
    console.error(`There was a problem fetching submitted assignments for assignment ${assignmentId}:`, error);
    throw error;
  }
};
export const postSubmittedAssignment = async (assignmentId, content) => {
    try {
      const response = await axios.post(`${baseURL}/${assignmentId}/submissions`, { content });
      return response.data;
    } catch (error) {
      console.error(`There was a problem posting the assignment submission for assignment ${assignmentId}:`, error);
      throw error;
    }
  };