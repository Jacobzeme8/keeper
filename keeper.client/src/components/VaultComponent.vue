<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 p-0 vault-view">
        <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
          <img :src="vault.img" class="img-fluid rounded vault-img" alt="">
        </router-link>
        <h3 class=" ps-2 shadow text-light vault-name">{{ vault.name }}</h3>
        <i v-if="vault.isPrivate" class="mdi mdi-lock fs-4 private text-light shadow"></i>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
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