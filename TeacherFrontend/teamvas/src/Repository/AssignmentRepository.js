import axios from 'axios';

const API_URL = 'https://localhost:7232/Assignments'; 

const AssignmentRepository = {
    async addAssignment(assignmentDto) {
        try {
            const token = localStorage.getItem('jwtToken'); 
            const headers = {
                Authorization: 'Bearer ' + token
            };
            const response = await axios.post(API_URL, assignmentDto, { headers: headers });
            return response.data;
        } catch (error) {
            console.error('There was an error adding the assignment:', error);
        }
    },
    async updateAssignment(assignmentId, assignmentDto) {
        try {
            const token = localStorage.getItem('jwtToken'); 
            const headers = {
                Authorization: 'Bearer ' + token
            };
            const response = await axios.put(`${API_URL}/${assignmentId}`, assignmentDto, { headers: headers });
            return response.data;
        } catch (error) {
            console.error('There was an error updating the assignment:', error);
        }
    },
    async deleteAssignment(assignmentId) {
        try {
            const token = localStorage.getItem('jwtToken'); 
            const headers = {
                Authorization: 'Bearer ' + token
            };
            const response = await axios.delete(`${API_URL}/${assignmentId}`, { headers: headers });
            return response.data;
        } catch (error) {
            console.error('There was an error deleting the assignment:', error);
        }
    },
    async getAllAssignments() {
        try {
            const response = await axios.get(API_URL);
            return response.data;
        } catch (error) {
            console.error('There was an error fetching the assignments:', error);
        }
    },
    async getAssignmentById(assignmentId) {
        try {
            const response = await axios.get(`${API_URL}/${assignmentId}`);
            return response.data;
        } catch (error) {
            console.error('There was an error fetching the assignment:', error);
        }
    }
}
export default AssignmentRepository;