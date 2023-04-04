<template>
  <div class="container-fluid">
    <div class="py-5">
      <section class="bricks">
        <div v-for="keep in keeps">
          <KeepsCard :keep="keep" />
        </div>
      </section>
    </div>
  </div>
</template>

<script>
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService"
import { computed, onMounted, watchEffect } from "vue";
import { AppState } from "../AppState";
import { vaultsService } from "../services/VaultsService";

export default {
  setup() {

    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    onMounted(() => {
      getAllKeeps()
    })

    watchEffect(() => {
      if (AppState.account.id) {
        getMyVaults()
      }
    })


    return {
      keeps: computed(() => AppState.keeps),

    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

$gap: .5em;

.bricks {
  columns: 250px;
  column-gap: $gap;

  // overflow: hidden;


  &>div {
    margin-top: $gap;
    display: inline-block;
    white-space: nowrap;
  }
}

@media (max-width: 576px) {
  .bricks {
    columns: 150px;
  }
}
</style>
