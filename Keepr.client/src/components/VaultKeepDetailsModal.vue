<template>
  <div class="modal fade" id="vaultKeepDetailsModal" tabindex="-1" aria-labelledby="vaultKeepModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-xl  modal-dialog-centered">
      <div class="modal-content container-fluid">
        <div class="row">
          <div class="col-12 col-md-6">
            <img class="img-size rounded" :src="vaultKeep?.img" alt="">
          </div>
          <div class="col-12 col-md-6 d-flex flex-column">
            <div class="modal-header fs-3">
              <span class="mx-3" title="Total Views">
                <i class="mdi mdi-eye text-secondary"></i>
                <span>{{vaultKeep?.views}}</span>
              </span>
              <span class="mx-3" title="Vaults it in">
                <i class="mdi mdi-ethernet mdi-flip-v text-primary"></i>
                <span>{{vaultKeep?.kept}}</span>
              </span>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body row">
              <div class="fs-2 modal-title">{{vaultKeep?.name}}</div>
              <p>
                {{vaultKeep?.description}}
              </p>
            </div>
            <div class="row my-3">
              <div class="col-4">
                <button v-if="account.id == profile.id" class="btn btn-outline-danger" title="Remove from Vault"
                  data-bs-dismiss="modal">
                  <i @click="deleteVaultKeep(vaultKeep?.vaultKeepId)" class="mdi mdi-delete"
                    data-bs-dismiss="modal"></i>
                </button>
              </div>
              <div class="col-8 text-end">
                <img @click="goToProfile" data-bs-dismiss="modal" :src="vaultKeep?.creator?.picture"
                  class="rounded-circle selectable" alt="" height="35" title="Go to Profile page.">
                <span :title="vaultKeep.creator?.name">
                  {{vaultKeep.creator?.name}}
                </span>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { accountService } from '../services/AccountService.js'
import { keepsService } from '../services/KeepsService.js';
import { profilesService } from '../services/ProfilesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  // props: {
  //   vaultKeep: { type: Object, required: true }
  // },
  setup() {
    const router = useRouter();
    const route = useRoute();
    // const selectedVault = ref({})
    onMounted(() => {
      // getMyVaults();
    });
    return {
      // selectedVault,

      // keep: computed(() => AppState.activeKeep),
      vaultKeep: computed(() => AppState.activeKeep),
      profileVaults: computed(() => AppState?.profileVaults),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState?.account),

      async goToProfile() {
        try {
          await profilesService.getProfileById(this.vaultKeep.creator.id)
          router.push({ name: 'Profile', params: { profileId: this.vaultKeep.creator.id } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      // async addToVault(keepId) {
      //   try {
      //     if (!selectedVault.value.id) {
      //       // logger.log('blank')
      //     } else {
      //       // logger.log(selectedVault.value)
      //       let vaultKeep = { vaultId: selectedVault.value.id, keepId }
      //       await keepsService.addToVault(vaultKeep)
      //       Pop.toast(`Added to Vault "${selectedVault.value.name}"`)
      //       selectedVault.value = {}
      //     }
      //   } catch (error) {
      //     logger.error(error)
      //     Pop.toast(error.message, 'error')
      //   }

      // },

      // async deleteKeep(id) {
      //   try {
      //     const yes = await Pop.confirm('Delete the keep?')
      //     if (!yes) { return }
      //     await keepsService.deleteKeep(id)
      //   } catch (error) {
      //     logger.error('[Deleting Keep]', error)
      //     Pop.error(error)
      //   }
      // },

      async deleteVaultKeep(id) {
        try {
          const yes = await Pop.confirm('Remove the keep?')
          if (!yes) { return }
          await keepsService.deleteVaultKeep(id)
          // await keepsService.getVaultKeeps(route.params.vaultId)
        } catch (error) {
          logger.error('[Deleting Keep]', error)
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.img-size {
  max-width: 100%;
  max-height: 80vh;
  margin-top: 1em;
  margin-bottom: 1em;
}
</style>