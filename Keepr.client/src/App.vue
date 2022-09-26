<template>
  <header class="container-fluid">
    <Navbar />
  </header>
  <main>
    <div class="container-fluid bg-stone">
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
</style>
