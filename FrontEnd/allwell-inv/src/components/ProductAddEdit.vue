<template>
  <div>
    <div class="w3-half w3-container w3-light-grey w3-round-xxlarge w3-panel">
      <h1 class="w3-center">Add/Edit Product</h1>
      <label>Product ID</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label>Product Name</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label>Type ID</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label>Cost</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label>Worker ID</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label>Location ID</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label>Condition</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label>Serial Number</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <p class="w3-right-align">
          <input class="w3-check " type="checkbox">
          <label>Damaged</label>
      </p>

      <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;">Add/Edit Product</button>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'

export default {
  name: 'productAddEdit',
  
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
</style>