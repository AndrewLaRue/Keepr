<template>
  <div @click="setActiveKeep(keep.id)" class="card item text-bg-dark selectable min-ht" data-bs-toggle="modal"
    data-bs-target="#keepDetailsModal">
    <img :src="keep.img" class="card-img" alt="...">
    <div class="card-img-overlay d-flex justify-content-end align-items-between flex-column">
      <span class="d-flex justify-content-between">
        <span class="glass-card pt-1 px-1">
          {{keep.name}}
        </span>
      </span>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { accountService } from '../services/AccountService.js'
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  props: {
    keep: {
      type: Object,
      required: true,
    }
  },
  setup() {
    return {
      account: computed(() => AppState?.account),

      async getMyVaults() {
        try {
          await accountService.getMyVaults()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      async setActiveKeep(id) {
        try {
          await keepsService.setActiveKeep(id)
          if (this.account.id != null) {
            this.getMyVaults()
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>