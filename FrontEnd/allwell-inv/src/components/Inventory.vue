<template>
  <div>
      <div class="w3-display-container">
    <div>
    <h2><b>Inventory</b></h2>        
      <button class="w3-button w3-blue w3-round-xxlarge w3-display-topright w3-margin-right w3-hover-text-black" @click="showDetails(0)">
        <b> <font-awesome-icon icon="plus-circle" class="icons" /> Product</b>
      </button>
      </div>

    <table class="w3-table-all w3-hoverable">
      <tr class="w3-blue">
        <th class="w3-center cell-v-center">Name</th>
        <th class="w3-center cell-v-center">Type</th>
        <th class="w3-center cell-v-center">Location</th>
        <th class="w3-center cell-v-center">Damaged</th>
      </tr>

      <tr v-for="(product, id) in state.products" :key="id" v-on:click="showDetails(product.id)" class="w3-hover-pale-yellow" style="cursor:pointer">
        <td class="w3-center cell-v-center" style="width: 20%">{{ product.name }}</td>
        <td class="w3-center cell-v-center" style="width: 20%">{{ product.type }}</td>
        <td class="w3-center cell-v-center" style="width: 20%">{{ product.location }}</td>
        <td class="w3-center cell-v-center" style="width: 20%"><font-awesome-icon icon="check" class="icons w3-text-red" v-if="product.damaged === true" /></td>
      </tr>      
    </table>

    </div>
    <ProductDetails :showModal="state.showDetails" @closeDetailModal="closeDetailModal" :productId="state.selectedProductId"/>
  </div>
</template>

<script>
  import { reactive, onMounted } from 'vue';
  import { repository } from '@/store/repository.js';
  import ProductDetails from '@/components/ProductAddEdit.vue';

  export default {
    name: 'inventory',
    components: {ProductDetails},
    setup() {
      const state = reactive({
        products: [],
        showDetails: false,
        product: {},
        selectedProductId: 0,
      });

      const {
        getProductLites,
      } = repository();

      onMounted(async () => {
        state.products = await getProductLites();
      });

      async function showDetails(productId) {
        state.selectedProductId = productId;
       state.showDetails = true;

      }
      
      async function closeDetailModal(success) {
        if (success === false) {
          state.showDetails = false;
        }
        state.showDetails = false;
        state.products = await getProductLites();
      }
      
      return {
        state,
        showDetails,
        closeDetailModal,
      }
    }
  }
</script>

<style scoped>
</style>