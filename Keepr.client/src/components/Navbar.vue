<template>
  <div class="row my-nav justify-content-center bg-stone align-items-center pb-5 py-md-2">
    <div class="col-12 col-md-1 text-center">
      <router-link :to="{ name: 'Home' }">
        <img class="rounded-circle" alt="logo" src="../assets/img/keep-logo.png" height="150" />
      </router-link>
    </div>
    <div class="col-12 col-md-3 offset-md-8 text-center">
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
.my-nav {
  height: 25vh;
}
</style>
