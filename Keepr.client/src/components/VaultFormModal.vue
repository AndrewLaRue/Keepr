<template>
  <div class="modal fade" id="vaultFormModal" tabindex="-1" aria-labelledby="vaultFormModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="vaultFormModalLabel">Create a Vault</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form class="account-form" @submit.prevent="createVault">
            <div class="col-md-12">
              <label for="title">Title:</label>
              <input type="text" class="form-control" v-model="editable.title" required name="title">
            </div>
            <div class="col-md-12">
              <label for="Image">Image:</label>
              <input type="url" class="form-control" v-model="editable.img" required name="img">
            </div>
            <label for="isPrivate">Private? </label>
            <input type="checkbox" class="mx-1" v-model="editable.isPrivate" name="isPrivate" placeholder="isPrivate">
            <div class="text-center">
              <button type="submit" class="btn btn-primary mt-2 selectable">Save</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          await vaultsService.createVault(editable.value)
        } catch (error) {
          logger.error('[Creating a vault]', error)
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>