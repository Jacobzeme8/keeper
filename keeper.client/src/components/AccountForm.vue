<template>
  <form @submit.prevent="editAccount()" class="p-4">
    <label for="">Name</label>
    <input minlength="3" maxlength="100" class="form-control" v-model="account.name" type="text">
    <label for="">Picture</label>
    <input minlength="3" maxlength="150" class="form-control" v-model="account.picture" type="text">
    <label for="">Cover Image</label>
    <input minlength="3" maxlength="150" class="form-control" v-model="account.coverImg" type="text">
    <button data-bs-toggle="modal" class="btn btn-success mt-3">Save Account Info</button>
  </form>
</template>


<script>
import { onMounted, ref } from "vue";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { accountService } from "../services/AccountService";
import { computed } from "@vue/reactivity";

export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      async editAccount() {
        try {
          await accountService.editAccount(this.account)
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