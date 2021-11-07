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
        <label class="w3-left w3-margin-left">City</label>
        <input v-model="state.location.city" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <label class="w3-left w3-margin-left">County</label>
        <input  v-model="state.location.county" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <div v-if="state.error === true">
            <p class="w3-text-red w3-animate-opacity">Location name, city, and county required.</p>
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
      error: false,
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
          }
          else {
            state.location = {};
            state.title = 'Add';
           }
        }
    });

    function close() {
      state.error = false;
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
      if (typeof(state.location.name, state.location.city, state.location.county) === 'undefined') {
          state.error = true;
          console.log("fail");
      }
      else if (props.locationId === 0) {
        success = await postLocation(state.location.name, state.location.city, state.location.county);                           
        context.emit('closeDetailModal', true);
        console.log(success);
      }
      else if (props.locationId !== 0) {
        state.location = await putLocationDetail(state.location.locationId, state.location.name, state.location.city,
                                                state.location.county);
        context.emit('closeDetailModal', true);
        console.log(success);
      }
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