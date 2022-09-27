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
              <label for="name">Name:</label>
              <input type="text" class="form-control" v-model="editable.name" required name="name">
            </div>
            <div class="col-md-12">
              <label for="Image">Image:</label>
              <input type="url" class="form-control" v-model="editable.img" required name="img">
            </div>
            <div class="col-md-12">
              <label for="description">Description:</label>
              <textarea v-model="editable.description" class="form-control" name="description" id=""
                required></textarea>
            </div>
            <label for="isPrivate">Private? </label>
            <input type="checkbox" class="mx-1" v-model="editable.isPrivate" name="isPrivate" placeholder="isPrivate">
            <div class="text-center">
              <button type="submit" data-bs-dismiss="modal" class="btn btn-primary mt-2 selectable">Save</button>
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
import { useRoute } from 'vue-router';

export default {
  setup() {
    const route = useRoute();
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          if (editable.value.isPrivate == null) { editable.value.isPrivate = false }
          await vaultsService.createVault(editable.value)
          Pop.success('You created a Vault.')
          editable.value = {}
          await vaultsService.getVaultsByProfileId(route.params.profileId)
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