<template>
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl  modal-dialog-centered">
      <div class="modal-content container-fluid">
        <div class="row">
          <div class="col-6">
            <img class="img-size rounded" :src="keep.img" alt="">
          </div>
          <div class="col-6 d-flex flex-column">
            <div class="modal-header fs-3">
              <i class="mdi mdi-delete text-danger mx-3"></i>
              <span class="mx-3" title="Total Views">
                <i class="mdi mdi-eye text-secondary"></i>
                13
              </span>
              <span class="mx-3" title="Vaults it in">
                <i class="mdi mdi-ethernet mdi-flip-v text-primary"></i>
                9
              </span>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body row">
              <div class="fs-2 modal-title">{{keep.name}}</div>
              <p>
                {{keep.description}}
              </p>
            </div>
            <div class="row my-3">
              <div class="col-4">
                <select name="vaults" class="form-control">
                  <option value="">Add to Vault</option>
                  <option value="saab">Saab</option>
                  <option value="mercedes">Mercedes</option>
                  <option value="audi">Audi</option>
                </select>
              </div>
              <div class="col-8 text-end">
                <img @click="goToProfile" data-bs-dismiss="modal" :src="keep.creator?.picture"
                  class="rounded-circle selectable" alt="" height="35" title="Go to Profile page.">
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
import { onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { accountService } from '../services/AccountService.js'
import { profilesService } from '../services/ProfilesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  // props: {
  //   keep: { type: Object, required: true }
  // },
  setup() {
    onMounted(() => {
      // getMyVaults();
    });
    return {

      keep: computed(() => AppState.activeKeep),
      profileVaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.activeProfile),

      async goToProfile() {
        try {
          await profilesService.getProfileById(this.keep.creator.id)
          router.push({ name: 'Profile', params: { profileId: this.keep.creator.id } })
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
.img-size {
  max-width: 100%;
  max-height: 80vh;
  margin-top: 1em;
  margin-bottom: 1em;
}
</style>