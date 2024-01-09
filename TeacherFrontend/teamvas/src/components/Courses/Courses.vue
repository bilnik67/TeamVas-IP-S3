

<template>
    <div class="courses-container">
      <h1>All Courses</h1>
      <ul class="courses-list">
        <li v-for="course in courses" :key="course.id" class="course-item">
          {{ course.name }}
          <button @click="editCourse(course)" class="edit-course-btn">Edit</button>
          <button @click="deleteCourse(course.id)" class="delete-course-btn">Delete</button>
        </li>
      </ul>
      <button @click="addCourse" class="add-course-btn">Add Course</button>
    </div>  
  </template>
  
  <script>
  export default {
    name: 'CoursesPage',
    data() {
      return {
        courses: [
          { id: 1, name: 'Introduction to Vue.js' },
          { id: 2, name: 'Component Composition' },
          { id: 3, name: 'Vue Router for Single Page Applications' },
          { id: 4, name: 'State Management with Vuex' },
          { id: 5, name: 'Building Performant Vue Apps' },
          { id: 6, name: 'Testing Vue Components' }
        ]
      }
    },
    methods: {
      addCourse() {
        alert('Add course functionality to be implemented');
      }
    }
  }
  </script>

<style src="./Courses.css">
</style>
  