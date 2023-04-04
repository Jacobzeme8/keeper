<template>
  <div class="container" v-if="vault">
    <div class="row">
      <div class="col-8 m-auto my-4 d-flex flex-column top-row align-items-center">
        <img class="img-fluid cover-img" :src="vault.img" alt="">
        <h2 class="m-auto name text-light shadow mb-3">{{ vault.name }}</h2>
        <h4 class="m-auto name2 text-light shadow mb-2">Made By {{ vault.creator.name }}</h4>
      </div>
      <div class="col-8 m-auto d-flex justify-content-center">
        <p v-if="keeps" class="fs-3"><i class="mdi mdi-alpha-k-box-outline"></i> {{ keeps.length }}</p>
      </div>
    </div>
    <div class="row">
      <section class="bricks">
        <div v-for="keep in keeps">
          <KeepsCard :keep="keep">
            <button :title="`Remove ${keep.name} from ${vault.name}`" @click="deleteVaultKeep(keep.vaultKeepId)"
              v-if="account.id == vault.creatorId" class="delete-button btn btn-danger m-0 p-1">remove from vault</button>
          </KeepsCard>
        </div>
      </section>
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
import { keepsService } from "../services/KeepsService";
import { onMounted, onUnmounted } from "vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    async function getKeepsInVault() {
      try {
        const vaultId = route.params.vaultId
        await keepsService.getKeepsInVault(vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function getVaultById() {
      try {
        const vaultId = route.params.vaultId
        await vaultsService.getVaultById(vaultId)
      } catch (error) {
        router.push({ name: 'Home' })
        logger.error(error)
        Pop.error('Back to the home page you go nerd!')
      }
    }

    onMounted(() => {
      getKeepsInVault()
      getVaultById()
    })

    onUnmounted(() => {
      AppState.keeps = null,
        AppState.vaults = null
    })

    return {
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),

      async deleteVaultKeep(vaultKeepId) {
        try {
          if (await Pop.confirm("keep from the vault?")) {
            await vaultKeepsService.deleteVaultKeep(vaultKeepId)
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
$gap: .5em;

.delete-button {
  position: absolute;
  top: 20px;
  right: 20px;
}

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

.cover-img {
  width: 100%;
  object-fit: cover;
  height: 40vh;
}

.top-row {
  position: relative;
}

.name {
  position: absolute;
  bottom: 30px;
}

.name2 {
  position: absolute;
  bottom: 2px;
}

.shadow {
  text-shadow: 2px 2px 4px black;
}
</style>