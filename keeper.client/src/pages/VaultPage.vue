<template>
  <div class="container">
    <div class="row">
      <section class="bricks">
        <div v-for="keep in keeps">
          <KeepsCard :keep="keep" />
        </div>
      </section>
    </div>
  </div>
</template>


<script>
import { useRoute } from "vue-router";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
import { keepsService } from "../services/KeepsService";
import { onMounted } from "vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
export default {
  setup() {
    const route = useRoute();
    async function getKeepsInVault() {
      try {
        const vaultId = route.params.vaultId
        await keepsService.getKeepsInVault(vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    onMounted(() => {
      getKeepsInVault()
    })

    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>


<style lang="scss" scoped>
$gap: .5em;

.bricks {
  columns: 300px;
  column-gap: $gap;

  // overflow: hidden;


  &>div {
    margin-top: $gap;
    display: inline-block;
    white-space: nowrap;
  }
}
</style>