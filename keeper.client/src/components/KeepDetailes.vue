<template>
  <div v-if="keep" class="component ">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-6 p-0  background-mock">
          <img class="img-fluid keep-img rounded" :src="keep.img" alt="">
        </div>
        <div class="col-md-6 d-flex flex-column justify-content-around align-items-center">
          <p> <i class="mdi mdi-eye"></i>{{ keep.views }} <i class="mdi mdi-alpha-k-box-outline"></i>{{ keep.kept }}</p>
          <p>{{ keep.name }}</p>
          <p class="text-wrap">{{ keep.description }}</p>
          <div class="d-flex flex-row align-items-center pb-3">
            <p>{{ keep.creator.name }}</p>
            <div v-if="account.id != keep.creator.id">
              <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
                <img title="My account page" data-bs-dismiss="modal" class="img-fluid picture rounded-circle ms-4"
                  :src="keep.creator.picture" alt="">
              </router-link>
            </div>
            <div v-if="account.id == keep.creator.id">
              <router-link :to="{ name: 'Account' }">
                <img data-bs-dismiss="modal" :title="`${keep.creator.name}'s' profile page`"
                  class="img-fluid picture rounded-circle ms-4" :src="keep.creator.picture" alt="">
              </router-link>
            </div>
          </div>
          <div v-if="account.id" class="d-flex">
            <form @submit.prevent="addKeepToVault(keep.id)" class="d-flex flex-row pb-3">
              <select v-model="editable.vaultId" class="form-select" aria-label="Default select example">
                <option :value="vault.id" v-for="vault in vaults">{{ vault.name }}</option>
              </select>
              <button type="submit" class="btn btn-success">Save</button>
            </form>
          </div>
        </div>
      </div>
    </div>


  </div>
</template>


<script>
import { computed, onMounted, ref, onUnmounted } from "vue";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultKeepsService } from "../services/VaultKeepsService";
export default {
  setup() {

    const editable = ref({})

    onUnmounted(() => {
      AppState.activeKeep = null
    })

    return {
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
      editable,
      async addKeepToVault(keepId,) {
        try {
          editable.value.keepId = keepId
          editable.value.creatorId = this.account.id
          // logger.log(editable.value)
          await vaultKeepsService.addKeepToVault(editable.value)
          this.keep.kept++
          Pop.success("added keep to vault")
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
.backround-mock {
  background-color: #FEF6F0;
  ;
}

.picture {
  width: 45px;
  height: 45px;
}

.keep-img {
  height: 100%;
  object-fit: cover;
}
</style>