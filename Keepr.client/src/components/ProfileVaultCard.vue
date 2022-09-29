<template>

  <div @click="goToVault" class="card item selectable text-bg-dark min-ht">
    <img :src="vault.img" class="card-img" alt="...">
    <div class="card-img-overlay d-flex justify-content-end align-items-between flex-column">

      <span class="d-flex justify-content-between">
        <span class="glass-card img-text pt-1 px-1">
          {{vault.name}} <i v-if="vault.isPrivate" class="mdi mdi-lock" title="Private"></i>
        </span>
      </span>
    </div>
  </div>

</template>


<script>
import { router } from '../router.js'
import { vaultsService } from '../services/VaultsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  props: {
    vault: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    return {


      async goToVault() {
        try {
          await vaultsService.setActiveVault(props.vault.id)
          router.push({ name: 'Vault', params: { vaultId: props.vault.id } })
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