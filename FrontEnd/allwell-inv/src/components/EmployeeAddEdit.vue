<template>
  <div class="w3-modal" style="display:block;" v-show="showModal === true">
    <div class="w3-modal-content" style="width:50%;">
    <div class="w3-container w3-col w3-light-grey w3-round-xxlarge w3-padding-16">
      <a @click="close" class="w3-display-topright w3-margin"><font-awesome-icon icon="times" class="icons w3-text-grey w3-xxlarge" /></a>
      <div class="w3-cell-row">
        <div class="w3-cell">
          <h2><b>Add/Edit Employee</b></h2>
        </div> 
      </div>
      <label class="w3-left w3-margin-left">First Name</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label class="w3-left w3-margin-left">Last Name</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label class="w3-left w3-margin-left">Username</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
      <label class="w3-left w3-margin-left">Password</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="password">
      <label class="w3-left w3-margin-left">Password</label>
      <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="password">
      <P><input class="w3-check " type="checkbox"> <label>Is Admin</label></P>
      <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;"><b>Add/Edit Employee</b></button>
    </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'

export default {
  name: 'employeeAddEdit',
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