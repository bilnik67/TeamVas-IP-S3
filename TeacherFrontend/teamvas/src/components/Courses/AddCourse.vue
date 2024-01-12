<template>
    <div class="add-course-container">
      <h1 class="page-title">Add New Course</h1>
      <form @submit.prevent="addNewCourse" class="add-course-form">
        <div class="form-group">
          <label for="courseName" class="form-label">Course Name</label>
          <input v-model="newCourse.name" id="courseName" class="form-input" placeholder="Enter Course Name">
        </div>
        <div class="form-group">
          <label for="courseDescription" class="form-label">Course Description</label>
          <textarea v-model="newCourse.description" id="courseDescription" class="form-input" placeholder="Enter Course Description"></textarea>
        </div>
        <button type="submit" class="submit-button">Add Course</button>
      </form>
    </div>
  </template>
  
  <script>
  import CourseRepository from '../../Repository/CourseRepository';
  import { useToast } from "vue-toastification";
  
  export default {
    name: 'AddCourse',
    data() {
      return {
        newCourse: {
          name: '',
        },
      };
    },
    methods: {
      async addNewCourse() {
        const toast = useToast();
        try {
          await CourseRepository.addCourse(this.newCourse);
          toast.success("Course added successfully.");
          this.newCourse = { name: '' };
          this.$router.push('/courses');
        } catch (error) {
          console.error('There was an error adding the course:', error);
          toast.error("Failed to add the course.");
        }
      },
    },
  };
  </script>