<template>
  <form @submit.prevent="createKeep()" class="p-4">
    <label for="">Name</label>
    <input v-model="editable.name" class="form-control" type="text">
    <label for="">Description</label>
    <input v-model="editable.description" class="form-control" type="text">
    <label for="">Image URL</label>
    <input v-model="editable.img" class="form-control" type="text">
    <button class="btn btn-success" data-bs-dismiss="modal">Create Keep</button>
  </form>
</template>


<script>
import { ref } from "vue";
import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { useRoute } from "vue-router";

export default {
  setup() {
    const editable = ref({})
    const route = useRoute()
    return {
      editable,
      route,
      async createKeep() {
        try {
          const keepData = editable.value
          await keepsService.createKeep(keepData, route)
          editable = {}
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