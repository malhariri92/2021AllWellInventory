<template>
  <div>
    <div class="w3-half w3-container w3-light-grey w3-round-xxlarge w3-panel">
        <h1 class="w3-center">Add/Edit Employee</h1>
        <label>Employee ID</label>
        <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <label>First Name</label>
        <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <label>Last Name</label>
        <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <label>Username</label>
        <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <label>Password</label>
        <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="password">
        <label>Password</label>
        <input class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="password">
        <p class="w3-right-align"><input class="w3-check " type="checkbox">
        <label>Is Admin</label></p>

        <button class="w3-btn w3-blue w3-round-xxlarge" style="width: 100%;">Add/Edit Employee</button>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'

export default {
  name: 'employeeAddEdit',
  
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