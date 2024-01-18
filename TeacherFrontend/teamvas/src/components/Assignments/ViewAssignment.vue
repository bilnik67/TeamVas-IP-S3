<template>
  <div>
    <h2 v-if="assignment">Title: {{ assignment.title }}</h2>
    <p v-if="error">Error: {{ error }}</p>

    <div v-if="submittedAssignments.length > 0">
      <h3>Submitted Assignments:</h3>
      <div v-for="(submission, index) in submittedAssignments" :key="index" class="submission-container">
        <div class="submission-box">
          <div v-if="submission.content.length > 100">
            <div class="submission-header">
              {{ showMore[index] ? submission.content : submission.content.slice(0, 100) + '...' }}
              <button @click="toggleReadMore(index)">{{ showMore[index] ? 'Read Less' : 'Read More' }}</button>
            </div>
            <div class="submission-date">Submitted on: {{ submission.submitted_on }}</div>
          </div>
          <div v-else>
            {{ submission.content }}
            <div class="submission-date">Submitted on: {{ submission.submitted_on }}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
  
<script>
import AssignmentRepository from '../../Repository/AssignmentRepository';

export default {
  data() {
    return {
      assignment: null,
      error: null,
      submittedAssignments: [],
      showMore: [], 
    };
  },
  async created() {
    try {
      const assignmentId = this.$route.params.assignmentId;
      this.assignment = await AssignmentRepository.getAssignmentById(assignmentId);

      this.submittedAssignments = await AssignmentRepository.getAllSubmittedAssignments(assignmentId);
      
      this.showMore = new Array(this.submittedAssignments.length).fill(false);
    } catch (e) {
      this.error = e.message;
    }
  },
  methods: {
    toggleReadMore(index) {
      this.showMore[index] = !this.showMore[index];
    },
  },
};
</script>

<style scoped>
.assignment-details {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}

h2 {
  font-size: 24px;
  margin-bottom: 20px;
}

.submission-item {
  margin-bottom: 20px;
}

.submission-content {
  font-size: 16px;
  line-height: 1.5;
}

.read-more-button {
  background-color: #007bff;
  color: #fff;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  font-size: 14px;
}

.read-more-button:hover {
  background-color: #0056b3;
}
.submission-box {
  background-color: #e5f7e8; 
  border: 1px solid #ccc;
  padding: 10px;
  margin-bottom: 10px;
  max-width: 600px; 
}
.submission-container {
  display: flex;
  justify-content: center; 
  align-items: center; 
  margin-bottom: 20px;
}

.submission-header {
  font-weight: bold;
  margin-bottom: 5px;
}

.submission-date {
  font-size: 0.8rem;
  color: #888;
}
</style>