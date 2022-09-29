<template>
  <form class="account-form" @submit.prevent="handleSubmit">
    <div class="col-md-12">
      <label for="name">Name:</label>
      <input type="text" class="form-control" v-model="editable.name" required name="name" placeholder="Name..."
        aria-label="Name">
    </div>
    <div class="col-md-12">
      <label for="email">Email:</label>
      <input type="text" class="form-control" v-model="editable.email" required name="email" placeholder="Email..."
        aria-label="Email">
    </div>

    <div class="col-md-12">
      <label for="picture">Picture:</label>
      <input type="url" class="form-control" v-model="editable.picture" required name="picture" placeholder="Picture..."
        aria-label="Image Url">
    </div>


    <div class="text-center">
      <button type="submit" class="btn btn-primary mt-2 selectable">Save</button>
    </div>
  </form>
</template>


<script>
import { ref, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { accountService } from '../services/AccountService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      if (!AppState.account) { return }
      editable.value = { ...AppState.account }
    })
    return {
      editable,
      async handleSubmit() {
        try {
          await accountService.editAccount(editable.value)
          router.push({ name: 'Profile', params: { profileId: editable.value.id } })
        } catch (error) {
          logger.error('[Editing Account]', error)
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>