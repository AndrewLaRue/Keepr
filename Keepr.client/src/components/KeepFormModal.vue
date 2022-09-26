<template>
  <div class="modal fade" id="keepFormModal" tabindex="-1" aria-labelledby="keepFormModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="keepFormModalLabel">Create a Keep</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form class="account-form" @submit.prevent="createKeep">
            <div class="col-md-12">
              <label for="title">Title:</label>
              <input type="text" class="form-control" v-model="editable.title" required name="title">
            </div>
            <div class="col-md-12">
              <label for="img">Image:</label>
              <input type="url" class="form-control" v-model="editable.img" required name="img">
            </div>
            <div class="col-md-12">
              <label for="description">Description:</label>
              <textarea v-model="editable.description" class="form-control" name="description" id=""
                required></textarea>
            </div>

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
import { keepsService } from '../services/KeepsService.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
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