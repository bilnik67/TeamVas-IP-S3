<template>
    <div>
      <h2>Assignment Details</h2>
      <p v-if="assignment">Title: {{ assignment.title }}</p>
      <p v-if="error">Error: {{ error }}</p>
    </div>
  </template>
  
  <script>
  import AssignmentRepository from '../../Repository/AssignmentRepository';
  
  export default {
    data() {
      return {
        assignment: null,
        error: null
      };
    },
    async created() {
      try {
        const assignmentId = this.$route.params.assignmentId; // assuming you're using Vue Router
        this.assignment = await AssignmentRepository.getAssignmentById(assignmentId);
      } catch (e) {
        this.error = e.message;
      }
    }
  };
  </script>
  
  <style>
  </style>