<template>
    <div class="no-access">
      <h1>Access Denied</h1>
      <p>You do not have permission to view this page.</p>
      <button @click="logout" class="auth-button">Sign out</button>
    </div>
  </template>
  
  <script>
  export default {
    name: 'NoAccessPage',
    methods: {
      logout() {
        this.$keycloak.logout();
      }
    }
  };
  </script>

<style scoped>
/* Your CSS here */
.no-access {
  text-align: center;
  margin-top: 50px;
}

</style>