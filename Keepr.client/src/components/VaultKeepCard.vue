<template>
  <div @click="setActive()" class="card item text-bg-dark selectable text-bg-dark min-ht" data-bs-toggle="modal"
    data-bs-target="#vaultKeepDetailsModal">
    <img :src="vaultKeep.img" class="card-img" alt="...">
    <div class="card-img-overlay d-flex justify-content-end align-items-between flex-column">
      <span class="d-flex justify-content-between">
        <span class="glass-card pt-1 px-1">
          {{vaultKeep.name}}
        </span>
        <img :src="vaultKeep.creator?.picture" class="rounded-circle" alt="" height="35"
          :title="vaultKeep.creator?.name">
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
    vaultKeep: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState?.account),

      async getMyVaults() {
        try {
          await accountService.getVaultsByAccountId()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      async setActive() {
        try {
          await keepsService.setActiveVaultKeep(props.vaultKeep)
          // await keepsService.setActiveKeep(props.vaultKeep.id)
          // await keepsService.setActiveVaultKeep(vkId)
          props.vaultKeep.views++
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