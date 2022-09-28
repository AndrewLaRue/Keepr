<template>
  <header class="container-fluid">
    <Navbar />
  </header>
  <main>
    <div class="container-fluid big-stone">
      <router-view />
    </div>
  </main>
</template>

<script>
import { computed } from 'vue';
import { AppState } from './AppState';

export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.big-stone {
  background-image: url(https://wallpaperaccess.com/full/7885018.jpg);
  background-repeat: no-repeat;
  background-attachment: fixed;
  min-height: 75vh;
}
</style>
