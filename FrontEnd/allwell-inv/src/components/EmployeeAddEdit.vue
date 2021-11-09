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
        <label class="w3-left w3-margin-left"  v-if="state.ogPassword !== state.employee.password">Verify Password</label>
        <div class="input-container w3-animate-opacity" v-if="state.ogPassword !== state.employee.password">
          <input v-model="state.verify" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" @keyup="verifyPassword"  type="password">
          <i class="icon"><font-awesome-icon :icon="state.passMatch" class="icons w3-xlarge" :class="state.passMatchColor" /></i>
        </div>
        <p><input v-model="state.employee.isAdmin" class="w3-check" type="checkbox"> <label>Admin</label></p>
        <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;" @click="updateEmployee"><b>{{ state.title }}</b></button>
        </div>
    </div>
  </div>
</template>


<script>
import { reactive, watch, inject} from 'vue';
import { repository } from '@/store/repository.js';

export default {
  name: 'employeeAddEdit',
  props: ['showModal', 'employeeId'],

  emits: ['closeDetailModal'],

  setup(props, context) {
    const state = reactive({
      employee: {},
      verify: '',
      title: '',
      ogPassword: '',
      passMatch: 'times-circle',
      passMatchColor: 'w3-animate-opacity w3-text-red'
    });

    const{
      putEmployeeDetail,
      getEmployeeDetail,
      postEmployee,
    }= repository();

    const store = inject('store');
    watch(props, async function() {
        if (props.showModal === true) {
          if (props.employeeId !== 0) {
            state.employee = await getEmployeeDetail(props.employeeId);
            state.title = 'Edit';
            state.ogPassword = state.employee.password;
          }
          else {
            state.employee = {};
            state.title = 'Add';
           }
        }
    });

    function verifyPassword() {
      if (state.employee.password === state.verify){
        state.passMatchColor = 'w3-text-green';
        state.passMatch = 'check-circle';
      }
      else {
        state.passMatchColor = 'w3-text-red';
        state.passMatch = 'times-circle';
      }
    }

    function close() {
      state.ogPassword = '';
      state.passMatch = 'times-circle';
      state.passMatchColor = 'w3-animate-opacity w3-text-red';
      state.verify = '';
      context.emit('closeDetailModal', false);
      
                  console.log(state.ogPassword);

    }

    async function updateEmployee() {
      let success = false;
      if (state.ogPassword === state.employee.password || state.employee.password === state.verify) {
      if (props.employeeId === 0) {
        if(typeof(state.employee.isAdmin) === 'undefined')
        {
          state.employee.isAdmin = false;
        }
        success = await postEmployee(state.employee.fName, state.employee.lName,
                                      state.employee.username, state.employee.password, state.employee.isAdmin);
                                      
      }
      else {
        if(typeof(state.employee.isAdmin) === 'undefined')
        {
            state.employee.isAdmin = false;
        }
      state.employee = await putEmployeeDetail(state.employee.id, state.employee.fName, state.employee.lName,
                                              state.employee.username, state.employee.password,
                                              state.employee.isAdmin);
      }
      context.emit('closeDetailModal', true);
      console.log(success);

    }
    else {
      state.verify = '';
      state.employee.password = '';
      alert("Passwords do not match");
      console.log("fail");
    }
  }

    return {
      state,
      close,
      updateEmployee,
      verifyPassword,
      store
    }
  }
}

</script>

<style scoped>
* {box-sizing: border-box;}

/* Style the input container */
.input-container {
  display: flex;
  width: 100%;
}

/* Style the form icons */
.icon {
  margin-top: 8px;
  min-width: 50px;
}
</style>