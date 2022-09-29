<template>
  <div @click="setActiveKeep(keep.id)" class="card item text-bg-dark text-bg-dark min-ht">
    <img :src="keep.img" class="card-img" alt="...">
    <div class="card-img-overlay d-flex justify-content-end align-items-between flex-column">
      <span class="d-flex justify-content-between">
        <span class="glass-card pt-1 px-1 selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
          {{keep.name}}
        </span>
        <img @click="goToProfile" :src="keep.creator?.picture" class="rounded-circle selectable pe-1" alt="" height="35"
          title="Go to Profile page.">
      </span>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState.js'
import { router } from '../router.js'
import { accountService } from '../services/AccountService.js'
import { keepsService } from '../services/KeepsService.js'
import { profilesService } from '../services/ProfilesService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  props: {
    keep: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    const router = useRouter();
    const route = useRoute();
    return {
      account: computed(() => AppState?.account),
      profile: computed(() => AppState?.activeProfile),

      async getVaultsByAccountId() {
        try {
          if (this.account.id) {
            let accountId = await accountService.getAccount()
            await accountService.getVaultsByAccountId(accountId)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      async goToProfile() {
        try {
          // let profile = await profilesService.getProfileById(props.keep.creatorId)
          router.push({ name: 'Profile', params: { profileId: props.keep.creatorId } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      async setActiveKeep(id) {
        try {
          await keepsService.setActiveKeep(id)
          this.getVaultsByAccountId()
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