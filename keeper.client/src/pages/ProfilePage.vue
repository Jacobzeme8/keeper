<template>
  <div v-if="profile" class="container mt-5">
    <div class="row">
      <div class="col-12 d-flex flex-column align-items-center">
        <img :src="profile.coverImg" class="img-fluid cover-img" alt="">
        <div>
          <img :src="profile.picture" class="picture img-fluid rounded-circle" alt="">
        </div>
        <h1>{{ profile.name }}</h1>
        <h2 v-if="keeps && vaults">{{ keeps.length }} Keeps / {{ vaults.length }} Vaults</h2>
      </div>
    </div>
  </div>
  <div v-if="profile" class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h1>{{ profile.name }}'s Vaults</h1>
      </div>
      <div v-for="vault in vaults" class="col-md-3 col-sm-6 my-2">
        <VaultComponent :vault="vault" />
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h1>{{ profile.name }}'s Keeps</h1>
      </div>
      <section class="bricks">
        <div v-for="keep in keeps">
          <KeepsCard :keep="keep" />
        </div>
      </section>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect, onUnmounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
import { keepsService } from "../services/KeepsService"
import { profilesService } from "../services/ProfilesService"
import { useRoute } from "vue-router"
export default {
  setup() {

    const route = useRoute();

    async function getProfileVaults() {
      try {
        const id = route.params.profileId
        await vaultsService.getProfileVaults(id)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function getProfileKeeps() {
      try {
        const id = route.params.profileId
        await keepsService.getProfileKeeps(id)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function getProfile() {
      try {
        const id = route.params.profileId
        await profilesService.getProfile(id)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function getOnlyMyVaults() {
      try {
        await vaultsService.getOnlyMyVaults()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    watchEffect(() => {
      if (AppState.account.id) {
        getOnlyMyVaults()
      }
    })

    onMounted(() => {

      getProfile()
      getProfileVaults()
      getProfileKeeps()
    })

    onUnmounted(() => {
      AppState.keeps = null,
        AppState.vaults = null
    })

    return {
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
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
