<template>
  <form @submit.prevent="editAccount()" class="p-4">
    <label for="">Name</label>
    <input class="form-control" v-model="editable.name" type="text">
    <label for="">Picture</label>
    <input class="form-control" v-model="editable.picture" type="text">
    <label for="">Cover Image</label>
    <input class="form-control" v-model="editable.coverImg" type="text">
    <button data-bs-toggle="modal" class="btn btn-success mt-3">Save Account Info</button>
  </form>
</template>


<script>
import { ref } from "vue";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { accountService } from "../services/AccountService";

export default {
  setup() {
    const editable = ref(AppState.account)
    return {
      editable,
      async editAccount() {
        try {
          await accountService.editAccount(editable.value)
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>