<template>
  <div class="m-2 rounded">
    <div class="keep-view">
      <img @click="getActiveKeep(keep.id)" data-bs-toggle="modal" data-bs-target="#keepModal"
        class="rounded img-fluid selectable d-flex" :src="keep.img" alt="">
      <slot class="top-card"></slot>
      <i :title="`Delete ${keep.name} Keep`" @click="deleteKeep(keep.id)"
        v-if="keep.creatorId == account.id && route.path == '/account'"
        class="mdi mdi-delete fs-4 text-danger delete selectable"></i>
      <div v-if="keep.creator" class="profile-info ps-2 d-flex ">
        <h3 class="text-wrap text-light shadow">{{ keep.name }}</h3>
        <img :title="keep.creator.name" v-if="!route.path.includes('profile', 'account')"
          class="rounded-circle profile-picture ms-3 me-1 mb-1" :src="keep.creator.picture" alt="">
      </div>
    </div>
  </div>






  <ModalComponent id="keepModal">
    <KeepDetailes />
  </ModalComponent>
</template>


<script>
import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";



export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    const route = useRoute()
    return {
      account: computed(() => AppState.account),
      route,
      async getActiveKeep(keepId) {
        try {
          await keepsService.getActiveKeep(keepId)
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      },
      async deleteKeep(keepId) {
        try {
          if (await Pop.confirm("delete this keep?")) {
            await keepsService.deleteKeep(keepId)
          }
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      }
    };
  },
}
</script>


<style lang="scss" scoped>
.profile-picture {
  width: 45px;
  height: 45px;
}

.delete {
  position: absolute;
  top: 5px;
  right: 20px;
}

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

.top-card {
  position: absolute;
  top: 2px;
}
</style>