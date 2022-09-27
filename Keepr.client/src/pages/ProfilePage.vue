<template>


  <!-- SECTION Profile details -->
  <div class="row align-items-center">
    <div class="col-3 my-4">
      <img class="rounded pro-img" :src="profile.picture" alt="" />
    </div>
    <div class="col img-text">
      <h1>{{ profile.name }}</h1>
      <h2>Vaults: <span>{{vaults.length}}</span></h2>
      <h3>Keeps: <span>{{keeps.length}}</span></h3>
    </div>
  </div>


  <!-- SECTION Profile Vaults -->
  <div class="row">
    <div class="col-12 fs-1 img-text ps-5">
      Vaults <i class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal"
        data-bs-target="#vaultFormModal"></i>
    </div>
    <div class="col-12">
      <div class="masonry">

        <ProfileVaultCard v-for="v in vaults" :key="v.id" :vault="v" />
      </div>
    </div>

  </div>


  <!-- SECTION Profile Keeps -->
  <div class="row">
    <div class="col-12 fs-1 img-text ps-5">
      Keeps <i class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal" data-bs-target="#keepFormModal"></i>
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
import { useRoute } from 'vue-router';
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

export default {
  setup() {
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
    async function getVaultsByProfileId() {
      try {
        await vaultsService.getVaultsByProfileId(route.params.profileId);
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
      getVaultsByProfileId();
    });
    return {
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),



    };
  },
  components: { ProfileKeepCard, ProfileVaultCard, VaultFormModal, KeepFormModal, KeepDetailsModal }
}

</script>


<style lang="scss" scoped>
.pro-img {
  width: 80%;
}

.masonry {
  /* Masonry container */
  column-count: 4;
  column-gap: 1em;
  direction: row;
  margin-top: 3em;
}

.item {
  /* Masonry bricks or child elements */
  background-color: #eee;
  display: inline-block;
  margin: 0 0 1em;
  width: 100%;
}
</style>