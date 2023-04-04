<template>
  <div class="container mt-5">
    <div class="row">
      <div class="col-12 d-flex flex-column align-items-center">
        <img :src="account.coverImg" class="img-fluid cover-img" alt="">
        <div>
          <img data-bs-target="#account-form" data-bs-toggle="modal" :src="account.picture"
            class="picture img-fluid rounded-circle selectable" alt="">
        </div>
      </div>
    </div>
  </div>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h1>My Vaults</h1>
      </div>
      <div v-for="vault in vaults" class="col-3 my-2">
        <VaultComponent :vault="vault" />
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h1>My Keeps</h1>
      </div>
      <section class="bricks">
        <div v-for="keep in keeps">
          <KeepsCard :keep="keep" />
        </div>
      </section>
    </div>
  </div>

  <ModalComponent id="account-form">
    <AccountForm />
  </ModalComponent>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
import { keepsService } from "../services/KeepsService"
import ModalComponent from "../components/ModalComponent.vue"
export default {
  setup() {
    async function getMyInfo() {
      try {
        await vaultsService.getMyVaults();
        await getMyKeeps();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    async function getMyKeeps() {
      try {
        await keepsService.getMyKeeps();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    onMounted(() => {
      getMyInfo();
    });
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { ModalComponent }
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
