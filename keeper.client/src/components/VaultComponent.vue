<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 p-0 vault-view">
        <router-link name="Vault Page" :to="{ name: 'Vault', params: { vaultId: vault.id } }">
          <img :title="`Go to ${vault.name} vault page`" :src="vault.img" class="img-fluid rounded vault-img"
            :alt="vault.name">
        </router-link>
        <p class=" ps-2 shadow text-light vault-name fs-3">{{ vault.name }}</p>
        <i title="Private Vault" v-if="vault.isPrivate" class="mdi mdi-lock fs-4 private text-light shadow"></i>
        <i :title="`Delete  ${vault.name} Vault`" @click="deleteVault(vault.id)" v-if="vault.creatorId == account.id"
          class="mdi mdi-delete fs-4 text-danger delete selectable"></i>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from 'vue'
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { vaultsService } from "../services/VaultsService";

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      account: computed(() => AppState.account),
      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm("delete this vault?")) {
            await vaultsService.deleteVault(vaultId)
          }
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.delete {
  position: absolute;
  top: 5px;
  right: 20px;
}

.vault-img {
  height: 30vh;
  object-fit: cover;
  width: 100%;

}

.private {
  position: absolute;
  top: 20x;
  left: 20px;
}

.vault-view {
  position: relative;
}

.vault-name {
  position: absolute;
  bottom: 2px;
}

.shadow {
  text-shadow: 2px 2px 4px black;
}
</style>