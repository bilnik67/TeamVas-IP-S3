import axios from 'axios';

const API_URL = 'https://localhost:7232/Courses'; 

const CourseRepository = {
    async addCourse(courseDto) {
        try {
            const token = localStorage.getItem('jwtToken'); 
            const headers = {
                Authorization: 'Bearer ' + token
            };
            const response = await axios.post(API_URL, courseDto, { headers: headers });
            return response.data;
        } catch (error) {
            console.error('There was an error adding the course:', error);
        }
    },
    async updateCourse(courseId, courseDto) {
        try {
            const token = localStorage.getItem('jwtToken'); 
            const headers = {
                Authorization: 'Bearer ' + token
            };
            const response = await axios.put(`${API_URL}/${courseId}`, courseDto, { headers: headers });
            return response.data;
        } catch (error) {
            console.error('There was an error updating the course:', error);
        }
    },
    async deleteCourse(courseId) {
        try {
            const token = localStorage.getItem('jwtToken'); 
            const headers = {
                Authorization: 'Bearer ' + token
            };
            const response = await axios.delete(`${API_URL}/${courseId}` , { headers: headers });
            return response.data;
        } catch (error) {
            console.error('There was an error deleting the course:', error);
        }
    },
    async getAllCourses() {
        try {
            const response = await axios.get(API_URL);
            return response.data;
        } catch (error) {
            console.error('There was an error fetching the courses:', error);
        }
    },
    async getCourseById(courseId) {
        try {
            const response = await axios.get(`${API_URL}/${courseId}`);
            return response.data;
        } catch (error) {
            console.error('There was an error fetching the course:', error);
        }
    }
}
export default CourseRepository;