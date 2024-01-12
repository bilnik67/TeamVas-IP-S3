<template>
    <div class="add-course-container">
      <h1 class="page-title">Edit Course</h1>
      <form @submit.prevent="editCourse" class="add-course-form">
        <div class="form-group">
          <label for="courseName" class="form-label">Course Name</label>
          <input v-model="editedCourse.name" id="courseName" class="form-input" placeholder="Course Name">
        </div>
        <div class="form-group">
          <label for="courseDescription" class="form-label">Course Description</label>
          <textarea v-model="editedCourse.description" id="courseDescription" class="form-input" placeholder="Course Description"></textarea>
        </div>
        <button type="submit">Save Changes</button>
      </form>
    </div>
  </template>
  
  <script>
import CourseRepository from '../../Repository/CourseRepository';
import { useToast } from "vue-toastification";

export default {
  name: 'EditCourse',
  data() {
    return {
      editedCourse: {
        name: '',
        description: '',
      },
      courseId: null, 
    };
  },
  created() {
    this.courseId = this.$route.params.courseId;
    this.fetchCourseDetails();
  },
  methods: {
    async fetchCourseDetails() {
      try {
        const courseDetails = await CourseRepository.getCourseById(this.courseId);
        this.editedCourse = courseDetails;
      } catch (error) {
        console.error('There was an error fetching course details:', error);
      }
    },
    async editCourse() {
      const toast = useToast();
      try {
        await CourseRepository.updateCourse(this.courseId, this.editedCourse);
        toast.success("Course edited successfully.");
        this.$router.push('/courses'); 
      } catch (error) {
        console.error('There was an error editing the course:', error);
        toast.error("Failed to edit the course.");
      }
    },
  },
};
</script>