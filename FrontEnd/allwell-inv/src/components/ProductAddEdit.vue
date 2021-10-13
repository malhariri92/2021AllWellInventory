<template>
  <div class="w3-modal w3-animate-opacity" style="display:block; padding-top:50px;" v-show="showModal === true">
    <div class="w3-modal-content" style="max-width:500px">
      <div class="w3-container w3-col w3-light-grey w3-round-xxlarge w3-padding-16 w3-margin-bottom">
        <button class="w3-button w3-blue w3-round-xxlarge w3-display-topleft w3-margin w3-hover-text-black" v-if="state.product !== {}">
          <b> <font-awesome-icon icon="user-plus" class="icons" />  Assign</b>
        </button>
        <a @click="close" class="w3-display-topright w3-margin w3-text-grey w3-hover-text-black"><font-awesome-icon icon="window-close" class="icons w3-xlarge" /></a>
        <div class="w3-cell-row">
          <div class="w3-cell">
            <h2><b>Add/Edit Product</b></h2>
          </div> 
        </div> 
        {{ state.product.id }}
        <label class="w3-left w3-margin-left">Product Name</label>
        <input v-model="state.product.name" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <label  class="w3-left w3-margin-left">Type</label>



        <label class="w3-left w3-margin-left">Cost</label>
        <input  v-model="state.product.cost" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <label class="w3-left w3-margin-left">Location</label>



        <label class="w3-left w3-margin-left">Condition</label>
        <input  v-model="state.product.condition" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <label class="w3-left w3-margin-left">Serial Number</label>
        <input  v-model="state.product.serialNo" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" type="text">
        <p><input  v-model="state.product.damaged" class="w3-check " type="checkbox"> <label>Damaged</label></p>
        <button class="w3-button w3-blue w3-round-xxlarge" style="width: 100%;" @click="updateProduct"><b>Edit Product</b></button>
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
    });

    const{
      putProductDetail,
      getProductDetail
    }= repository();


    watch(props, async function() {
        if (props.showModal === true) {
          state.product = await getProductDetail(props.productId);
          console.log(state.product);
        }
    });

    function close() {
      context.emit('closeDetailModal', false);
    }

    async function updateProduct() {
      state.product = await putProductDetail(state.product.productId, state.product.name, state.product.typeId,
                                              state.product.cost, state.product.locationId, state.product.condition,
                                              state.product.damaged, state.product.serialNo);
      context.emit('closeDetailModal', true);
    }

    return {
      state,
      close,
      updateProduct
    }
  }
}
</script>

<style scoped>
</style>