<template>
  <div v-if="keep" class="component">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-6 p-0">
          <img class="img-fluid keep-img rounded" :src="keep.img" alt="">
        </div>
        <div class="col-md-6 d-flex flex-column justify-content-around align-items-center">
          <p> <i class="mdi mdi-eye"></i>{{ keep.views }} <i class="mdi mdi-alpha-k-box-outline"></i>{{ keep.kept }}</p>
          <p>{{ keep.name }}</p>
          <p class="text-wrap">{{ keep.description }}</p>
          <p>{{ keep.creator.name }}</p>
          <div>
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </div>
    </div>


  </div>
</template>


<script>
import { computed, onMounted } from "vue";
import { AppState } from "../AppState";
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped>
.keep-img {
  height: 100%;
  object-fit: cover;
}
</style>