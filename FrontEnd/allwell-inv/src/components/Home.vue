<template>
  <div>
    <h1 class="color-red">All Well Inventory System</h1>
    <h2>This is a placeholder home page</h2>

    <button class="w3-button w3-card-4 w3-circle button-close"></button>
  </div>
</template>

<script>
  import { reactive } from 'vue';

  export default {
    name: 'home',

    setup() {
      const state = reactive({
        
      });

      return {
        state
      }
    }
  }
</script>

<style scoped>
button.button-close {
  background-color: var(--green);
}
</style>