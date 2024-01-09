
<template>
  <nav class="navbar">
    <ul class="nav-items">
      <ul class="nav-item"><router-link to="/" class="nav-button">Home</router-link></ul>
      <ul class="nav-item"><router-link to="/courses" class="nav-button">Courses</router-link></ul>
      <ul class="nav-item"><router-link to="/assignments" class="nav-button">Assignments</router-link></ul>
    </ul>
  </nav>
</template>

<script>
export default {
  name: 'AppNavbar'
}
</script>

<style src="./AppNavbar.css">
</style>