<template>
  <div class="modal fade" id="keepFormModal" tabindex="-1" aria-labelledby="keepFormModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="keepFormModalLabel">Create a Keep</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form class="account-form" @submit.prevent="createKeep">
            <div class="col-md-12">
              <label for="name">Name:</label>
              <input type="text" class="form-control" v-model="editable.name" required name="name" aria-label="Name"
                placeholder="Name..." minlength="2" maxlength="50">
            </div>
            <div class="col-md-12">
              <label for="img">Image:</label>
              <input type="url" class="form-control" v-model="editable.img" required name="img" aria-label="Image Url"
                placeholder="Image Url..." minlength="2" maxlength="250">
            </div>
            <div class="col-md-12">
              <label for="description">Description:</label>
              <textarea v-model="editable.description" class="form-control" name="description" id="" required
                placeholder="Description..." aria-label="Description"></textarea>
            </div>

            <div class="text-center">
              <button type="submit" data-bs-dismiss="modal" class="btn btn-primary mt-2 selectable"
                aria-label="Save Button">Save</button>
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
import { keepsService } from '../services/KeepsService.js';
import { useRoute } from 'vue-router';

export default {
  setup() {
    const route = useRoute();
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          Pop.success('You created a Keep.')
          editable.value = {}
          await keepsService.getKeepsByProfileId(route.params.profileId)
        } catch (error) {
          logger.error('[Creating a Keep]', error)
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>