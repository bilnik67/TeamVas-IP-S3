<template>
    <div class="add-assignment-container">
      <h1 class="page-title">Add New Assignment</h1>
      <form @submit.prevent="addNewAssignment" class="add-assignment-form">
        <div class="form-group">
          <label for="assignmentTitle" class="form-label">Assignment Title</label>
          <input v-model="newAssignment.title" id="assignmentTitle" class="form-input" placeholder="Enter Assignment Title">
        </div>
        <div class="form-group">
          <label for="assignmentDescription" class="form-label">Assignment Description</label>
          <textarea v-model="newAssignment.description" id="assignmentDescription" class="form-input" placeholder="Enter Assignment Description"></textarea>
        </div>
        <button type="submit" class="submit-button">Add Assignment</button>
      </form>
    </div>
  </template>
  
  <script>
  import AssignmentRepository from '../../Repository/AssignmentRepository';
  import { useToast } from "vue-toastification";
  
  export default {
    title: 'AddAssignment',
    data() {
      return {
        newAssignment: {
          title: '',
        },
      };
    },
    methods: {
      async addNewAssignment() {
        const toast = useToast();
        try {
          await AssignmentRepository.addAssignment(this.newAssignment);
          toast.success("Assignment added successfully.");
          this.newAssignment = { title: '' };
          this.$router.push('/assignments');
        } catch (error) {
          console.error('There was an error adding the assignment:', error);
          toast.error("Failed to add the assignment.");
        }
      },
    },
  };
  </script>