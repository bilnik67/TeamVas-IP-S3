const baseURL = '/Courses';

export const fetchCourses = async () => {
  try {
    const response = await fetch(baseURL);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error("There was a problem fetching courses:", error);
    throw error;
  }
};


export const fetchCourseDetails = async (courseId) => {
  try {
    const response = await fetch(`${baseURL}/${courseId}`);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error(`There was a problem fetching details for course ${courseId}:`, error);
    throw error;
  }
};
