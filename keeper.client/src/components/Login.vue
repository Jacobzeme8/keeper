<template>
  <span class="navbar-text">
    <button class="btn selectable text-uppercase my-2 my-lg-0" @click="login" v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown dropstart my-2 my-lg-0">
        <div type="button" class="bg-dark border-0 selectable no-select" data-bs-toggle="dropdown">
          <div class="backround-mock" v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" class="rounded-circle profile" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-left p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
          <div class="button-query d-flex flex-column" v-if="account.id">
            <button data-bs-toggle="modal" data-bs-target="#vault-form" class="btn btn-success form-button m-1">Create
              Vault</button>
            <button data-bs-toggle="modal" data-bs-target="#keep-form" class="btn btn-success form-button m-1">Create
              Keep</button>
          </div>
        </div>
      </div>
    </div>
  </span>

  <ModalComponent id="vault-form">
    <VaultForm />
  </ModalComponent>

  <ModalComponent id="keep-form">
    <KeepForm />
  </ModalComponent>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.profile {
  height: 45px;
  width: 45px;
  object-fit: cover;
}
</style>
