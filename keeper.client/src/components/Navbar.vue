<template>
  <!-- <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul>
      <Login />
    </div>
  </nav> -->


  <div class="container-fluid backround-mock nav-shadow">
    <div class="row">
      <div class="col-12 d-flex justify-content-between align-items-center">
        <div class="d-flex align-items-center">
          <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
            <div class="d-flex flex-column align-items-center p-1">
              <h1>Home</h1>
            </div>
          </router-link>
        </div>
        <div class="d-flex align-items-center">
          <div>
            <button data-bs-toggle="modal" data-bs-target="#vault-form" class="btn btn-success me-3">Create
              Vault</button>
            <button data-bs-toggle="modal" data-bs-target="#keep-form" class="btn btn-success me-3">Create Keep</button>
          </div>
          <Login />
        </div>
      </div>
    </div>
  </div>


  <ModalComponent id="vault-form">
    <VaultForm />
  </ModalComponent>

  <ModalComponent id="keep-form">
    <KeepForm />
  </ModalComponent>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
.nav-shadow {
  border-bottom: 2px solid black;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
