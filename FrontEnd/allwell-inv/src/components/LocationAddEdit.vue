<template>
  <div class="w3-modal w3-animate-opacity" style="display:block; padding-top:50px;" v-show="showModal === true">
    <div class="w3-modal-content" style="max-width:500px">
      <div class="w3-container w3-col w3-light-grey w3-round-xxlarge w3-padding-16 w3-margin-bottom">
        <a @click="close" class="w3-display-topright w3-margin w3-text-grey w3-hover-text-black"><font-awesome-icon icon="window-close" class="icons w3-xlarge" /></a>
        <div class="w3-cell-row">
          <div class="w3-cell">
            <h2><b>{{ state.title }} Location</b></h2>
          </div> 
        </div> 

        <label class="w3-left w3-margin-left">Name</label>
        <input v-model="state.location.name" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        
        <div v-if="!state.isValidName">
          <p class="font-color-red">Location name is required!</p>
        </div>

        <label class="w3-left w3-margin-left">City</label>
        <input v-model="state.location.city" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        
        <div v-if="!state.isValidCity">
          <p class="font-color-red">City is required!</p>
        </div>

        <label class="w3-left w3-margin-left">County</label>
        <input  v-model="state.location.county" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        
        <div v-if="!state.isValidCounty">
          <p class="font-color-red">County is required!</p>
        </div>

        <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;" @click="updateLocation"><b>{{ state.title }}</b></button>
      </div>
    </div>
  </div>
</template>


<script>
  import { reactive, watch } from 'vue';
  import { repository } from '@/store/repository.js';

  export default {
    name: 'locationAddEdit',
    props: ['showModal', 'locationId'],

    emits: ['closeDetailModal'],

    setup(props, context) {
      const state = reactive({
        location: {},
        title: '',
        isValidName: true,
        isValidCity: true,
        isValidCounty: true,
      });

      const{
        putLocationDetail,
        getLocationDetail,
        postLocation
      }= repository();


      watch(props, async function() {
        if (props.showModal === true) {
          if (props.locationId !== 0) {
            state.location = await getLocationDetail(props.locationId);
            state.title = 'Edit';
          } else {
            state.location = {};
            state.title = 'Add';
            }
        }
      });

      function close() {
        setErrorMessages();
        context.emit('closeDetailModal', false);
      }

      function closeLogsModal(success) {
        if (success === false) {
          state.showLogsModal = false;
        }
        state.showDetails = false;
      }

      async function updateLocation() {
        let success = false;
        setErrorMessages();

        if(!validateData()) return;
          if (props.locationId === 0) {
            success = await postLocation(state.location.name, state.location.city, state.location.county);                           
            console.log(success)
          } else {
          state.location = await putLocationDetail(state.location.locationId, state.location.name, state.location.city,
                                                  state.location.county);
          }
        context.emit('closeDetailModal', true);
      }
    
      function validateData() {
        if(typeof(state.location.name) === 'undefined' || state.location.name === '') {
          state.isValidName = false;
          return false;
        }

        if(typeof(state.location.city) === 'undefined' || state.location.city === '') {
          state.isValidCity = false;
          return false;
        }
        
        if(typeof(state.location.county) === 'undefined' || state.location.county === '') {
          state.isValidCounty = false;
          return false;
        }

        return true;
      }

      function setErrorMessages() {
        state.isValidName = true;
        state.isValidCity = true;
        state.isValidCounty = true;
      }

      return {
        state,
        close,
        closeLogsModal,
        updateLocation
      }
    }
  }
</script>

<style scoped>
</style>