
<template>
    <div class="assignments-container">
      <h1>All Assignments</h1>
      <ul class="assignments-list">
        <li v-for="assignment in assignments" :key="assignment.id" class="assignment-item">
          {{ assignment.title }}
          <router-link :to="'/view-assignment/' + assignment.id" class="view-assignment-link view-assignment-btn">View submissions</router-link>
          <button @click="deleteAssignment(assignment.id)" class="delete-assignment-btn">Delete</button>
        </li>
      </ul>
      <router-link to="/add-assignment" class="add-assignment-btn">Add New Assignment</router-link>
    </div>  
  </template>
  
  <script>
  import AssignmentRepository from '../../Repository/AssignmentRepository';
  import { useToast } from "vue-toastification";

  export default {
    name: 'AssignmentsPage',
    data() {
    return {
      assignments: []
    };
  },
  created() {
    this.fetchAssignments();
  },
  methods: {
    async fetchAssignments() {
        this.assignments = await AssignmentRepository.getAllAssignments();
    },
    async deleteAssignment(assignmentId) {
      const toast = useToast();  

      this.$swal({
        title: 'Are you sure?',
        text: 'You will not be able to recover this assignment!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then(async (result) => {
        if (result.isConfirmed) {
          try {
            await AssignmentRepository.deleteAssignment(assignmentId);
            this.fetchAssignments();  
            toast.success("Assignment deleted successfully.");  
          } catch (error) {
            console.error('There was an error deleting the assignment:', error);
            toast.error("Failed to delete the assignment.");  
          }
        }
      });
    },
  }
  }
  </script>

<style src="./Assignments.css">
</style>
  