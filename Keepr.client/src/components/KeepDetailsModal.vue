<template>
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl  modal-dialog-centered">
      <div class="modal-content container-fluid">
        <div class="row">
          <div class="col-12 col-md-6">
            <img class="img-size rounded" :src="keep.img" alt="">
          </div>
          <div class="col-12 col-md-6 d-flex flex-column">
            <div class="modal-header fs-3">
              <i v-if="account?.id == keep.creatorId" @click="deleteKeep(keep.id)"
                class="mdi mdi-delete text-danger mx-3 selectable" data-bs-dismiss="modal" title="Delete Keep"></i>
              <span class="mx-3" title="Total Views">
                <i class="mdi mdi-eye text-secondary pe-1"></i>
                <span>{{keep.views}}</span>
              </span>
              <span class="mx-3" title="Vaults it in">
                <i class="mdi mdi-ethernet mdi-flip-v text-primary pe-1"></i>
                <span>{{keep.kept}}</span>
              </span>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body row">
              <div class="fs-2 modal-title">{{keep.name}}</div>
              <p>
                {{keep.description}}
              </p>
            </div>
            <div class="row my-3 align-items-center">
              <div class="col-4">
                <label for="vaults">Add to Vault</label>
                <select name="vaults" class="form-select selectable pt-1" aria-label="Vault Dropdown"
                  v-model="selectedVault" @click="addToVault(keep.id)">
                  <option selected value="">Add to Vault</option>
                  <option v-for="v in accountVaults" :key="v.id" :vault="v" :value="v" :aria-label="v.name">
                    {{v.name}}</option>
                </select>
              </div>
              <div class="col-8 text-end">
                <img @click="goToProfile" data-bs-dismiss="modal" :src="keep.creator?.picture"
                  class="rounded-circle selectable pe-1" alt="" height="35" title="Go to Profile page.">
                <span :title="keep.creator?.name">
                  {{keep.creator?.name}}
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
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { accountService } from '../services/AccountService.js'
import { keepsService } from '../services/KeepsService.js';
import { profilesService } from '../services/ProfilesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  // props: {
  //   vault: { type: Object, required: true }
  // },
  setup() {
    const selectedVault = ref({})
    onMounted(() => {
      // getVaultsByAccountId()
    })
    // watchEffect(() => {

    // })
    return {
      selectedVault,

      keep: computed(() => AppState.activeKeep),
      accountVaults: computed(() => AppState?.accountVaults),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState?.account),

      async goToProfile() {
        try {
          await profilesService.getProfileById(this.keep.creator.id)
          router.push({ name: 'Profile', params: { profileId: this.keep.creator.id } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },



      async addToVault(keepId) {
        try {
          if (!selectedVault.value.id) {
            // logger.log('blank')
          } else {
            // logger.log(selectedVault.value)
            let vaultKeep = { vaultId: selectedVault.value.id, keepId }
            await keepsService.addToVault(vaultKeep)
            Pop.toast(`Added to Vault "${selectedVault.value.name}"`)
            selectedVault.value = {}
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }

      },

      async deleteKeep(id) {
        try {
          const yes = await Pop.confirm('Delete the keep?')
          if (!yes) { return }
          await keepsService.deleteKeep(id)
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

.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}

.dropdown-menu.show {
  transform: scale(1);
}
</style>