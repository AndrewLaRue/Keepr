<template>

  <div class="row justify-content-center">
    <div class="col-4 glass-card img-text mt-5">
      <AccountForm />
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.form-bg {
  background-size: 100vh;
  margin-top: 15vh;
  padding-bottom: 6vh;

}
</style>
