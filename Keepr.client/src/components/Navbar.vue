<template>
  <div class="row my-nav justify-content-between align-items-center py-2">
    <div class="col-4 col-md-1">
      <router-link :to="{ name: 'Home' }">
        <img class="rounded-circle img-fluid" alt="logo" src="../assets/img/keep-logo.png" />
      </router-link>
    </div>
    <div class="col-8 col-md-11 text-end no-flow">
      <Login />
    </div>
  </div>
</template>

<script>
import Login from './Login.vue';
export default {
  setup() {
    return {};
  },
  components: { Login }
};
</script>

<style scoped>
.no-flow {
  /* overflow-x: hidden; */
}

.my-nav {
  background-color: grey;
}
</style>
