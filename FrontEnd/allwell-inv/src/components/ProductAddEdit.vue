<template>
  <div class="w3-modal w3-animate-opacity" style="display:block; padding-top:50px;" v-show="showModal === true">
    <div v-if="store.userState.user !== null">
      <div class="w3-modal-content" style="max-width:500px">

        <div class="w3-container w3-col w3-light-grey w3-round-xxlarge w3-padding-16 w3-margin-bottom">
          <a @click="close" class="w3-display-topright w3-margin w3-text-grey w3-hover-text-black">
            <font-awesome-icon icon="window-close" class="icons w3-xlarge" />
          </a>
          
        <div v-if="store.userState.user.isAdmin">
        <button @click="showLogsModal()" 
          class="w3-button w3-blue w3-round-xxlarge w3-display-topleft w3-margin w3-hover-text-black">
          <b> <font-awesome-icon icon="user-plus" class="icons" />Assign</b>
        </button>

        <a @click="close" class="w3-display-topright w3-margin w3-text-grey w3-hover-text-black">
          <font-awesome-icon icon="window-close" class="icons w3-xlarge" /></a>
        <div class="w3-cell-row">
          <div class="w3-cell">
            <h2><b>{{ state.title }} </b></h2>
          </div> 
        </div> 
        <label class="w3-left w3-margin-left">Product Name</label>
        <input v-model="state.product.name" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <div v-if="!state.isValidName">
        <p class="font-color-red">Product name is required!</p>
        </div>
        <label class="w3-left w3-margin-left">Type</label>
        <select class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" v-model="state.selectedType" @change="typeChange">
          <option  v-if="state.title === 'Add Product'" selected disabled value>Choose a type</option>
          <option v-for="(type, typeId) in state.types" :key="typeId" :value="type.typeId">{{ type.name }}</option>
        </select>
        <div v-if="!state.isValidType">
        <p class="font-color-red">Please select a type!</p>
        </div>
        <label class="w3-left w3-margin-left">Location</label>
        <select class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" v-model="state.selectedLocation" @change="locationChange">
          <option v-if="state.title === 'Add Product'" selected disabled value>Choose a location</option>
          <option v-for="(location, locationId) in state.locations" :key="locationId" :value="location.locationId">{{ location.name }}</option>
        </select>
        <div v-if="!state.isValidLocation">
        <p class="font-color-red">Please select a location!</p>
        </div>
        <label class="w3-left w3-margin-left">Cost</label>
        <input  v-model="state.product.cost" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="number">
        <div v-if="!state.isValidCost">
        <p class="font-color-red">Cost is required!</p>
        </div>
        <label class="w3-left w3-margin-left">Condition</label>
        <input  v-model="state.product.condition" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">
        <div v-if="!state.isValidCondition">
        <p class="font-color-red">Condition is required!</p>
        </div>
        <label class="w3-left w3-margin-left">Serial Number</label>
        <input  v-model="state.product.serialNo" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text" required>
        <div v-if="!state.isValidSerial">
        <p class="font-color-red">Serial number is required!</p>
        </div>
        <p><input  v-model="state.product.damaged" class="w3-check " type="checkbox"> <label>Damaged</label></p>
        <button v-if="store.userState.user.isAdmin" class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;" 
        @click="updateProduct"><b>{{ state.title }}</b></button>
        </div>

            <div v-if="!state.isValidLocation">
              <p class="font-color-red">Please select a location!</p>
            </div>

            <label class="w3-left w3-margin-left">Cost</label>
            <input  v-model="state.product.cost" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">

            <div v-if="!state.isValidCost">
              <p class="font-color-red">Cost is required!</p>
            </div>

            <label class="w3-left w3-margin-left">Condition</label>
            <input  v-model="state.product.condition" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text">

            <div v-if="!state.isValidCondition">
              <p class="font-color-red">Condition is required!</p>
            </div>

            <label class="w3-left w3-margin-left">Serial Number</label>
            <input  v-model="state.product.serialNo" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom w3-padding" type="text" required>

            <div v-if="!state.isValidSerial">
              <p class="font-color-red">Serial number is required!</p>
            </div>

            <p><input  v-model="state.product.damaged" class="w3-check " type="checkbox"><label>Damaged</label></p>

            <button v-if="store.userState.user.isAdmin" class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;" 
                    @click="updateProduct"><b>{{ state.title }}</b></button>
          </div>

          <div v-if="(!store.userState.user.isAdmin && showModal)">
            <div class="w3-cell-row">
              <div class="w3-cell">
                <h2><b>{{ state.title }} </b></h2>
              </div> 
            </div> 

            <ul class="w3-ul w3-border w3-left-align" style="display: flex; flex-direction: column">
              <li><strong>Name:</strong> {{ state.product.name }}</li>
              <li v-if="state.types.length > 0" class="w3-left"><strong>Type:</strong> {{ state.types[state.product.typeId - 1].name }}</li>
              <li v-if="state.locations.length > 0" class="w3-left"><strong>Location:</strong> {{ state.locations[state.product.locationId - 1].name }}</li>
              <li class="w3-left"><strong>Cost:</strong> ${{ state.product.cost }}</li>
              <li class="w3-left"><strong>Condition:</strong> {{ state.product.condition }}</li>
              <li class="w3-left"><strong>Serial Number:</strong> {{ state.product.serialNo }}</li>
              <li class="w3-left"><strong>Damaged:</strong> {{ state.product.damaged ? 'Yes' : 'No' }}</li>
            </ul>      
          </div>
        </div>
      </div>
    </div>

    <AssignLogs :showLogsModal="state.showLogsModal" v-if="state.showLogsModal" @closeLogsModal="closeLogsModal" :productId="productId"/>
  </div>
</template>

<script>
  import { reactive, watch, inject } from 'vue';
  import { repository } from '@/store/repository.js';
  import AssignLogs from '@/components/AssignLogs.vue';

  export default {
    name: 'productAddEdit',
    components: {AssignLogs},
    props: ['showModal', 'productId'],
    emits: ['closeDetailModal'],

    setup(props, context) {
    const state = reactive({
      showLogsModal: false,
      product: {},
      types: [],
      selectedType: 0,
      locations: [],
      selectedLocation: 0,
      title: '',
      isValidType: true,
      isValidLocation: true,
      isValidName: true,
      isValidCost: true,
      isValidCondition: true,
      isValidSerial: true,
    });

    const store = inject('store');

    const{
      getTypes,
      getLocations,
      putProductDetail,
      getProductDetail,
      postProduct,
      selectedLocation,
      selectedType
    }= repository();


    watch(props, async function() {
      if (props.showModal === true) {
        if (props.productId !== 0) {
          state.product = await getProductDetail(props.productId);

          if(store.userState.user.isAdmin){
            state.title = 'Edit Product';
          } else{
            state.title = 'Product Details';
          }
        } else {
          state.product = {};
          state.title = 'Add Product';
          }

        state.types = await getTypes();
        state.locations = await getLocations();
        state.selectedType = state.product.typeId;
        state.selectedLocation = state.product.locationId;
      }
    });

    function close() {
      setErrorMessages();
      context.emit('closeDetailModal', false);
    }

    function showLogsModal() {
      state.showLogsModal = true;
    }

    function closeLogsModal() {
      state.showDetails = false;
    }

    function typeChange() {
      state.product.typeId = state.selectedType;
    }

    function locationChange() {
      state.product.locationId = state.selectedLocation;
    }

    async function updateProduct() {
      let success = false;
      setErrorMessages();

      if(!validateData()) return;

      if (props.productId === 0) {
        if(typeof(state.product.damaged) === 'undefined') {
          state.product.damaged = false;
        }

        success = await postProduct(state.product.name, state.product.typeId,
                                      state.product.cost, state.product.locationId, state.product.condition,
                                      state.product.damaged, state.product.serialNo);
      } else {
        state.product = await putProductDetail(state.product.productId, state.product.name, state.product.typeId,
                                                state.product.cost, state.product.locationId, state.product.condition,
                                                state.product.damaged, state.product.serialNo);
        }
        context.emit('closeDetailModal', true);
      }

      function validateData() {
        if(typeof(state.product.name) === 'undefined' || state.product.name === '') {
          state.isValidName = false;
          return false;
        }
  
        if(typeof(state.selectedType) === 'undefined') {
          state.isValidType = false;
          return false;
        }
  
        if(typeof(state.selectedLocation) === 'undefined') {
          state.isValidLocation = false;
          return false;
        }
  
        if(typeof(state.product.cost) === 'undefined' || state.product.cost === '') {
          state.isValidCost = false;
          return false;
        }
  
        if(typeof(state.product.condition) === 'undefined' || state.product.condition === '') {
          state.isValidCondition= false;
          return false;
        }
  
        if(typeof(state.product.serialNo) === 'undefined' || state.product.serialNo === '') {
          state.isValidSerial = false;
          return false;
        }
  
        return true;
      }
  
      function setErrorMessages() {
        state.isValidType = true;
        state.isValidLocation = true;
        state.isValidName = true;
        state.isValidCost = true;
        state.isValidSerial = true;
        state.isValidCondition= true;
      }
  
      return {
        state,
        close,
        showLogsModal,
        closeLogsModal,
        updateProduct,
        typeChange,
        locationChange,
        selectedLocation,
        selectedType,
        store
      }
    }
  }
</script>

<style scoped>
</style>