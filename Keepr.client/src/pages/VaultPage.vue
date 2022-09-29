<template>
  <div class="row img-text">
    <div class="col-6 col-md-10 mt-5">
      <h1 class="">{{vault?.name}}</h1>
      <p class="fs-4">Keeps: <span>{{vaultKeeps.length}}</span></p>
    </div>
    <div class="col-6 col-md-2">
      <button v-if="account?.id == keep.creatorId" @click="deleteVault(vault?.id)" class="btn btn-danger mt-5">Delete
        Vault</button>
    </div>
  </div>
  <div class="row">
    <div class="col-12">
      <div class="masonry">

        <VaultKeepCard v-for="vk in vaultKeeps" :key="vk.id" :vaultKeep="vk" />
      </div>
    </div>
  </div>
  <VaultKeepDetailsModal />
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import VaultKeepDetailsModal from '../components/vaultKeepDetailsModal.vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { useRoute, useRouter } from 'vue-router';
import { onMounted } from 'vue';
import { keepsService } from '../services/KeepsService.js';

export default {
  // props: {
  //   vault: { type: Object, required: true }
  // },
  setup() {

    const route = useRoute();
    const router = useRouter();


    async function getActiveVault() {
      try {
        if (!AppState.activeVault) {
          await vaultsService.setActiveVault(route.params.id);
        }
      }
      catch (error) {
        logger.log(error);
        router.push({ name: 'Home' })
      }
    }



    onMounted(() => {
      getActiveVault();
      // getVaultKeeps()
    });
    return {
      vault: computed(() => AppState.activeVault),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      // vaultKeep: computed(() => AppState.activeVaultKeep),
      keep: computed(() => AppState.activeKeep),

      async getVaultKeeps() {
        try {
          await keepsService.getVaultKeeps(props.vault.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },


      async deleteVault(vaultId) {
        try {
          const yes = await Pop.confirm('Delete the vault?')
          if (!yes) { return }
          await vaultsService.deleteVault(vaultId)
          Pop.success('Vault deleted')
          router.push({ name: 'Profile', params: { profileId: this.profile.id } })
        } catch (error) {
          logger.error('[Deleting Vault]', error)
          Pop.error(error)
        }
      },
    };
  },
  components: { VaultKeepDetailsModal }
}
</script>


<style lang="scss" scoped>

</style>