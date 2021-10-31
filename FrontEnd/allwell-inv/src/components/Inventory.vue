<template>
  <div>
      <div class="w3-display-container">
    <div v-if="store.userState.user !== null">
    <h2>Products</h2>        
      <button v-if="store.userState.user.isAdmin" class="w3-button w3-blue w3-round-xxlarge w3-display-topright w3-margin-right w3-hover-text-black" @click="showDetails(0)">
        <b> <font-awesome-icon icon="plus-circle" class="icons" />  Add New Product</b>
      </button>
      </div>

    <table class="w3-table-all w3-hoverable">
      <tr style="background-color: var(--blue)">
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
  import { reactive, onMounted, inject } from 'vue';
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

      const store = inject('store');

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
        store
      }
    }
  }
</script>

<style scoped>
</style>