

<template>
  <div class="hello">
    <div v-if="hasTeacherRole">
      <h1>Hey Teacher,</h1>
      <p>Welcome back, teacher!</p>
      <button @click="logout" class="auth-button">Sign out</button>
    </div>
    <div v-else>
      <p>You do not have permission to view this page.</p>
      <p>If you are a teacher, please logout and login with a teacher account:</p>
      <button @click="logout" class="auth-button">Sign out</button>
    </div>
  </div>
</template>



<script>

export default {
  name: 'HelloWorld',
  data() {
    return {
      authenticated: false, // Initialize authenticated as false
    };
  },
  computed: {
    keycloak() {
      return this.$keycloak;
    },
    hasTeacherRole() {
      return this.authenticated && this.keycloak.hasRealmRole('Teacher');
    }
  },
  methods: {
    login() {
      this.keycloak.login();
    },
    logout() {
      this.keycloak.logout();
    }
  },
  created() {
    this.keycloak.onReady = (authenticated) => {
      this.authenticated = authenticated;
    };
    this.keycloak.onAuthSuccess = () => {
      this.authenticated = true;
    };
    this.keycloak.onAuthLogout = () => {
      this.authenticated = false;
    };
  }
};
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.auth-button {
  background-color: #42b983;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 10px;
}

.auth-button:hover {
  background-color: #369f77;
}
</style>
