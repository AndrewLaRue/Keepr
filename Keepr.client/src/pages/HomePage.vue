<template>
  <div class="row">
    <div class="col-12">
      <div class="masonry">

        <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
  <KeepDetailsModal />
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import KeepDetailsModal from '../components/KeepDetailsModal.vue';
import { accountService } from '../services/AccountService.js';
export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        logger.error(error);
        Pop.toast(error.message, "error");
      }
    };



    onMounted(() => {
      getAllKeeps();
      // getMyVaults();
    });
    return {
      keeps: computed(() => AppState.keeps),
      keep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState?.profileVaults),

      async getMyVaults() {
        try {
          await accountService.getMyVaults()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    };
  },
  components: { KeepDetailsModal }
}
</script>


<style lang="scss" scoped>

</style>