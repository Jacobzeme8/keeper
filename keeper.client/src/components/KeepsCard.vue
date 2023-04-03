<template>
  <div class="m-2 rounded">
    <div class="keep-view">
      <img @click="getActiveKeep(keep.id)" data-bs-toggle="modal" data-bs-target="#keepModal"
        class="rounded img-fluid selectable" :src="keep.img" alt="">
      <div class="profile-info ps-2">
        <h3 class="text-wrap text-light shadow">{{ keep.name }}</h3>
      </div>
    </div>
  </div>






  <ModalComponent id="keepModal">
    <KeepDetailes />
  </ModalComponent>
</template>


<script>
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";



export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      async getActiveKeep(keepId) {
        try {
          await keepsService.getActiveKeep(keepId)
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      }
    };
  },
}
</script>


<style lang="scss" scoped>
.keep-view {
  position: relative;
}

.profile-info {
  position: absolute;
  bottom: 2px;
}

.shadow {
  text-shadow: 2px 2px 4px black;
}
</style>