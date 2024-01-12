
<template>
    <div class="courses-container">
      <h1>All Courses</h1>
      <ul class="courses-list">
        <li v-for="course in courses" :key="course.id" class="course-item">
          {{ course.name }}
          <router-link :to="'/edit-course/' + course.id" class="edit-course-link edit-course-btn">Edit</router-link>
          <button @click="deleteCourse(course.id)" class="delete-course-btn">Delete</button>
        </li>
      </ul>
      <router-link to="/add-course" class="add-course-btn">Add New Course</router-link>
    </div>  
  </template>
  
  <script>
  import CourseRepository from '../../Repository/CourseRepository';
  import { useToast } from "vue-toastification";

  export default {
    name: 'CoursesPage',
    data() {
    return {
      courses: []
    };
  },
  created() {
    this.fetchCourses();
  },
  methods: {
    async fetchCourses() {
        this.courses = await CourseRepository.getAllCourses();
    },
    async deleteCourse(courseId) {
      const toast = useToast();  

      this.$swal({
        title: 'Are you sure?',
        text: 'You will not be able to recover this course!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then(async (result) => {
        if (result.isConfirmed) {
          try {
            await CourseRepository.deleteCourse(courseId);
            this.fetchCourses();  
            toast.success("Course deleted successfully.");  
          } catch (error) {
            console.error('There was an error deleting the course:', error);
            toast.error("Failed to delete the course.");  
          }
        }
      });
    },
  }
  }
  </script>

<style src="./Courses.css">
</style>
  