<template>
  <div class="row img-text">
    <div class="col-10 mt-5">
      <h1 class="">{{vault.name}}</h1>
      <p class="fs-4">Keeps: <span>4</span></p>
    </div>
    <div class="col-2">
      <button class="btn btn-danger mt-5">Delete Vault</button>
    </div>
  </div>
  <!-- <VaultKeepDetailsModal /> -->
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import VaultKeepDetailsModal from '../components/VaultKeepDetailsModal.vue';

export default {
  setup() {
    return {
      vault: computed(() => AppState.activeVault),
    };
  },
  components: { VaultKeepDetailsModal }
}
</script>


<style lang="scss" scoped>

</style>