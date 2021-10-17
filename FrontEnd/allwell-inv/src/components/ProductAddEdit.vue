<template>
  <div class="w3-modal w3-animate-opacity" style="display:block; padding-top:50px;" v-show="showModal === true">
    <div class="w3-modal-content" style="max-width:500px">
      <div class="w3-container w3-col w3-light-grey w3-round-xxlarge w3-padding-16 w3-margin-bottom">
        <button v-if="state.title === 'Edit'" class="w3-button w3-blue w3-round-xxlarge w3-display-topleft w3-margin w3-hover-text-black">
          <b> <font-awesome-icon icon="user-plus" class="icons" />  Assign</b>
        </button>
        <a @click="close" class="w3-display-topright w3-margin w3-text-grey w3-hover-text-black"><font-awesome-icon icon="window-close" class="icons w3-xlarge" /></a>
        <div class="w3-cell-row">
          <div class="w3-cell">
            <h2><b>{{ state.title }} Product</b></h2>
          </div> 
        </div> 
        <label class="w3-left w3-margin-left">Product Name</label>
        <input v-model="state.product.name" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
       
        <label class="w3-left w3-margin-left">Type</label>
        <select class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" v-model="state.selectedType" @change="typeChange">
          <option  v-if="state.title === 'Add'" selected disabled value>Choose a type</option>
          <option v-for="(type, typeId) in state.types" :key="typeId" :value="type.typeId">{{ type.name }}</option>
        </select>

        <label class="w3-left w3-margin-left">Location</label>
        <select class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" v-model="state.selectedLocation" @change="locationChange">
          <option v-if="state.title === 'Add'" selected disabled value>Choose a location</option>
          <option v-for="(location, locationId) in state.locations" :key="locationId" :value="location.locationId">{{ location.name }}</option>
        </select>

        <label class="w3-left w3-margin-left">Cost</label>
        <input  v-model="state.product.cost" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <label class="w3-left w3-margin-left">Condition</label>
        <input  v-model="state.product.condition" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <label class="w3-left w3-margin-left">Serial Number</label>
        <input  v-model="state.product.serialNo" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <p v-if="state.title === 'Edit'"><input  v-model="state.product.damaged" class="w3-check " type="checkbox"> <label>Damaged</label></p>
        <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;" @click="updateProduct"><b>{{ state.title }}</b></button>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, watch } from 'vue';
import { repository } from '@/store/repository.js';

export default {
  name: 'productAddEdit',

  props: ['showModal', 'productId'],

  emits: ['closeDetailModal'],

  setup(props, context) {
    const state = reactive({
      product: {},
      types: [],
      selectedType: 0,
      locations: [],
      selectedLocation: 0,
      title: ''
    });

    const{
      getTypes,
      getLocations,
      putProductDetail,
      getProductDetail,
      postProduct
    }= repository();


    watch(props, async function() {
        if (props.showModal === true) {
          if (props.productId !== 0) {
            state.product = await getProductDetail(props.productId);
            state.title = 'Edit';
          }
          else {
            state.product = {};
            state.title = 'Add';
           }

          state.types = await getTypes();
          state.locations = await getLocations();
          state.selectedType = state.product.typeId;
          state.selectedLocation = state.product.locationId;
        }
    });

    function close() {
      context.emit('closeDetailModal', false);
    }

    function typeChange() {
      state.product.typeId = state.selectedType;
    }

    function locationChange() {
      state.product.locationId = state.selectedLocation;

    }

    async function updateProduct() {
      context.emit('closeDetailModal', true);
      let success = false;
      if (props.productId === 0) {
        success = await postProduct(state.product.name, state.product.typeId,
                                      state.product.cost, state.product.locationId, state.product.condition,
                                      state.product.damaged, state.product.serialNo);
                                      
      }
      else {
      state.product = await putProductDetail(state.product.productId, state.product.name, state.product.typeId,
                                              state.product.cost, state.product.locationId, state.product.condition,
                                              state.product.damaged, state.product.serialNo);
      }
      console.log(success);

    }

    return {
      state,
      close,
      updateProduct,
      typeChange,
      locationChange,
    }
  }
}
</script>

<style scoped>
</style>