import axios from 'axios';

const baseURL = '/Courses';

export const fetchCourses = async () => {
  try {
    const response = await axios.get(baseURL);
    return response.data;
  } catch (error) {
    console.error("There was a problem fetching courses:", error);
    throw error;
  }
};

export const fetchCourseDetails = async (courseId) => {
  try {
    const response = await axios.get(`${baseURL}/${courseId}`);
    return response.data;
  } catch (error) {
    console.error(`There was a problem fetching details for course ${courseId}:`, error);
    throw error;
  }
};