<template>
  <div class="w3-modal" style="display:block;" v-show="showModal === true">
    <div class="w3-modal-content" style="width:50%;">
    <div class="w3-container w3-col w3-light-grey w3-round-xxlarge w3-padding-16">
      <a @click="close" class="w3-display-topright w3-margin"><font-awesome-icon icon="times" class="icons w3-text-grey w3-xxlarge" /></a>
      <div class="w3-cell-row">
        <div class="w3-cell">
          <h2><b>Add/Edit Product</b></h2>
        </div> 
      </div>
      <label class="w3-left w3-margin-left">Product Name</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label class="w3-left w3-margin-left">Type</label>
      <select class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom">
        <option value="" disabled selected>Choose your product type</option>
        <option value="1">Option 1</option>
        <option value="2">Option 2</option>
        <option value="3">Option 3</option>
      </select>
      <label class="w3-left w3-margin-left">Cost</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label class="w3-left w3-margin-left">Location</label>
      <select class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom">
        <option value="" disabled selected>Choose your location</option>
        <option value="1">Option 1</option>
        <option value="2">Option 2</option>
        <option value="3">Option 3</option>
      </select>
      <label class="w3-left w3-margin-left">Condition</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label class="w3-left w3-margin-left">Serial Number</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <P><input class="w3-check " type="checkbox"> <label>Damaged</label></P>
      <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;"><b>Add/Edit Product</b></button>
    </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue';

export default {
  name: 'productAddEdit',
  props: ['showModal'],
  emits: ['closeDetailModal'],
  setup(props, context) {
    const state = reactive({

    });
    function close() {
      context.emit('closeDetailModal', false);
    }
    return {
      state,
      close,
    }
  }
}
</script>

<style scoped>
</style>