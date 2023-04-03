<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <form @submit.prevent="createVault()">
          <label class="form-label">Name</label>
          <input required v-model="editable.name" type="text" class="form-control" id="exampleInputPassword1">
          <label class="form-label">Description</label>
          <input required v-model="editable.description" type="text" class="form-control" id="exampleInputPassword1">
          <label class="form-label">Image</label>
          <input required v-model="editable.img" type="text" class="form-control" id="exampleInputPassword1">
          <label class="form-label me-2">Private</label>
          <input v-model="editable.isPrivate" type="checkbox" name="isPrivate" id="">
          <button data-bs-dismiss="modal" class="btn btn-success">Create Vault</button>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue";
import { vaultsService } from "../services/VaultsService";

export default {
  setup() {

    const editable = ref({})

    return {
      editable,
      async createVault() {
        const vaultData = editable.value
        await vaultsService.createVault(vaultData)
        editable.value = {}
      }

    }
  }
}
</script>


<style lang="scss" scoped></style>