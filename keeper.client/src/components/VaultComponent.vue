<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 p-0 vault-view">
        <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
          <img :src="vault.img" class="img-fluid rounded" alt="">
        </router-link>
        <h3 class=" ps-2 shadow text-light vault-name">{{ vault.name }}</h3>
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