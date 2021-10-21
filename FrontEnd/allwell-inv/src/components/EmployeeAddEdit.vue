<template>
  <div class="w3-modal w3-animate-opacity" style="display:block; padding-top:50px;" v-show="showModal === true">
    <div class="w3-modal-content" style="max-width:500px">
      <div class="w3-container w3-col w3-light-grey w3-round-xxlarge w3-padding-16 w3-margin-bottom">
        <a @click="close" class="w3-display-topright w3-margin w3-text-grey w3-hover-text-black"><font-awesome-icon icon="window-close" class="icons w3-xlarge" /></a>
        <div class="w3-cell-row">
          <div class="w3-cell">
            <h2><b>{{ state.title }} Employee</b></h2>
          </div> 
        </div> 
        <label class="w3-left w3-margin-left">First Name</label>
        <input v-model="state.employee.fName" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <label class="w3-left w3-margin-left">Last Name</label>
        <input v-model="state.employee.lName" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <label class="w3-left w3-margin-left">Username</label>
        <input  v-model="state.employee.username" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <label class="w3-left w3-margin-left">Password</label> 
        <input v-model="state.employee.password" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="password">
        <label class="w3-left w3-margin-left">Verify Password</label>
        <input v-model="state.employee.password" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="password">
        
        <label class="w3-left w3-margin-left">Location</label>
        <select class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" v-model="state.selectedLocation" @change="locationChange">
          <option v-if="state.title === 'Add'" selected disabled value>Choose a location</option>
          <option v-for="(location, locationId) in state.locations" :key="locationId" :value="location.locationId">{{ location.name }}</option>
        </select>
        
        <p><input  v-model="state.employee.isAdmin" class="w3-check " type="checkbox"> <label>Admin</label></p>
        <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;" @click="updateEmployee"><b>{{ state.title }}</b></button>
        </div>
    </div>
  </div>
</template>


<script>
import { reactive, watch } from 'vue';
import { repository } from '@/store/repository.js';

export default {
  name: 'employeeAddEdit',
  props: ['showModal', 'employeeId'],

  emits: ['closeDetailModal'],

  setup(props, context) {
    const state = reactive({
      employee: {},
      title: '',
      locations: [],
      selectedLocation: 0
    });

    const{
      putEmployeeDetail,
      getEmployeeDetail,
      getEmployeeLocations,
      postEmployee
    }= repository();


    watch(props, async function() {
        if (props.showModal === true) {
          if (props.employeeId !== 0) {
            state.employee = await getEmployeeDetail(props.employeeId);
            console.log(props.employeeId);
            state.title = 'Edit';
          }
          else {
            state.employee = {};
            state.title = 'Add';
           }
           
          state.locations = await getEmployeeLocations();
          state.selectedLocation = state.employee.locationId;
        }
    });

    function close() {
      context.emit('closeDetailModal', false);
    }

    function locationChange() {
      state.employee.locationId = state.selectedLocation;

    }


    function showLogsModal() {
      state.showLogsModal = true;
    }

    function closeLogsModal(success) {
      if (success === false) {
        state.showLogsModal = false;
      }
      state.showDetails = false;
    }

    async function updateEmployee() {
      let success = false;
      if (props.employeeId === 0) {
        if(typeof(state.employee.isAdmin) === 'undefined')
        {
          state.employee.isAdmin = false;
        }
        success = await postEmployee(state.employee.fName, state.employee.lName,
                                      state.employee.username, state.employee.password, state.employee.isAdmin, state.employee.LocationId);
                                      
      }
      else {
      state.employee = await putEmployeeDetail(state.employee.id, state.employee.fName, state.employee.lName,
                                              state.employee.username, state.employee.password,
                                              state.employee.isAdmin, state.employee.LocationId);
      }
      context.emit('closeDetailModal', true);
      console.log(success);

    }

    return {
      state,
      close,
      showLogsModal,
      closeLogsModal,
      updateEmployee,
      locationChange
    }
  }
}

</script>

<style scoped>
</style>