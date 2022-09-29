<template>


  <!-- SECTION Profile details -->
  <div class="row align-items-center">
    <div class="col-3 my-4">
      <img class="rounded pro-img" :src="profile.picture" alt="" />
    </div>
    <div class="col-8 col-md-6 img-text glass-card">
      <span class="mfs-6">
        <p class="mfs-2 mb-0">{{ profile.name }}</p>
        <p v-if="profile.id != account.id" class="mfs-3 mb-0">Vaults: <span>{{vaults.length}}</span></p>
        <p v-else class="mfs-3 mb-0">Vaults: <span>{{vaults.length}}</span></p>
        <p class="mfs-3 mb-0">Keeps: <span>{{keeps.length}}</span></p>
      </span>
    </div>
  </div>


  <!-- SECTION Profile Vaults -->
  <div class="row">
    <div class="col-12 fs-1 glass-pro-card img-text ps-5 mb-4">
      <span class=" p-2">
        Vaults <i v-if="profile.id == account.id" class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal"
          data-bs-target="#vaultFormModal"></i>
      </span>
    </div>
    <div class="col-12">
      <div class="masonry">

        <ProfileVaultCard v-for="v in vaults" :key="v.id" :vault="v" @click="getVaultKeeps(v.id)" />
      </div>
    </div>

  </div>


  <!-- SECTION Profile Keeps -->
  <div class="row">
    <div class="col-12 fs-1 glass-pro-card img-text ps-5 my-4">
      <span class="p-2">
        Keeps <i v-if="profile.id == account.id" class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal"
          data-bs-target="#keepFormModal"></i>
      </span>
    </div>
    <div class="col-12">
      <div class="masonry">

        <ProfileKeepCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
  <VaultFormModal />
  <KeepFormModal />
  <KeepDetailsModal />
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { profilesService } from '../services/ProfilesService.js';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import ProfileKeepCard from '../components/ProfileKeepCard.vue';
import ProfileVaultCard from '../components/ProfileVaultCard.vue';
import VaultFormModal from '../components/VaultFormModal.vue';
import KeepFormModal from '../components/KeepFormModal.vue';
import KeepDetailsModal from '../components/KeepDetailsModal.vue';
import { accountService } from '../services/AccountService.js';

export default {
  setup() {
    const router = useRouter();
    const route = useRoute();


    async function getKeepsByProfileId() {
      try {
        await keepsService.getKeepsByProfileId(route.params.profileId);
      }
      catch (error) {
        logger.error("[Getting Profile Keeps]", error);
        Pop.error(error);
      }
    }

    async function getVaults() {
      try {
        if (AppState.account.id == route.params.profileId) {
          // let accountId = await accountService.getAccount()
          await accountService.getVaultsByAccountId()
        } else {
          await vaultsService.getVaultsByProfileId(route.params.profileId);
        }
      }
      catch (error) {
        logger.error("[Getting Profile Vaults]", error);
        Pop.error(error);

      }
    }




    async function getProfileById() {
      try {
        await profilesService.getProfileById(route.params.profileId);
      }
      catch (error) {
        logger.error("[GettingProfile]", error);
        Pop.error(error);
        router.push({ name: "Home" });
      }
    }
    onMounted(() => {
      getProfileById();
      getKeepsByProfileId();
      getVaults();
    });
    return {
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      accountVaults: computed(() => AppState.accountVaults),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),

      async getVaultKeeps(vaultId) {
        try {
          await keepsService.getVaultKeeps(vaultId)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }

    };
  },
  components: { ProfileKeepCard, ProfileVaultCard, VaultFormModal, KeepFormModal, KeepDetailsModal }
}

</script>


<style lang="scss" scoped>
.pro-img {
  width: 80%;
}


.glass-pro-card {
  background: rgba(0, 0, 0, 0.50);
  backdrop-filter: blur(4px);
  border: solid #8d8b8b1f;
  // border-radius: 8px;
  // padding: 0;
}
</style>