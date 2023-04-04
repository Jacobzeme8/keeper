<template>
  <header class="backround-mock">
    <Navbar />
  </header>
  <main class="backround-mock">
    <router-view />
  </main>
  <footer class="backround-mock">
    <b>Made with C#, JS, and Vue </b>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

.backround-mock {
  background-color: #FEF6F0;
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
