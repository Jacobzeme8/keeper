<template>
  <div class="container mt-5">
    <div class="row">
      <div class="col-12 d-flex flex-column align-items-center">
        <img :src="account.coverImg" class="img-fluid cover-img" alt="">
        <div>
          <img :src="account.picture" class="picture img-fluid rounded-circle" alt="">
        </div>
      </div>
    </div>
  </div>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h1>My Vaults</h1>
      </div>

    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
export default {
  setup() {

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
.cover-img {
  height: 40vh;
  width: 100%;
  object-fit: cover;

}

.picture {
  height: 10vh;
  widows: 10vh;
  transform: translateY(-5vh);
}
</style>
