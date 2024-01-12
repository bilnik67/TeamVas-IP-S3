<template>
  <div class="hello">
    <h1>Hey Teacher,</h1>
    <p>Welcome back, teacher!</p>
    <button @click="logout" class="auth-button">Sign out</button>
  </div>
</template>

<script>

export default {
  name: 'HelloWorld',
  methods: {
    login() {
      this.$keycloak.login();
    },
    logout() {
      this.$keycloak.logout();
    }
  },
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
