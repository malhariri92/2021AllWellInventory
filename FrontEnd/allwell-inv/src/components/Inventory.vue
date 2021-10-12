<template>
  <div>
    <div>
    <h2>Products</h2>
    <table class="w3-table-all">
      <tr style="background-color: var(--blue)">
        <th class="w3-center cell-v-center"></th>
        <th class="w3-center cell-v-center">Name</th>
        <th class="w3-center cell-v-center">Type</th>
        <th class="w3-center cell-v-center">Location</th>
        <th class="w3-center cell-v-center">Damaged</th>
      </tr>

      <tr v-for="(product, id) in state.products" :key="id">
        <td class="w3-center cell-v-center" style="width: 20%">
          <button class="w3-button w3-round-large w3-card-4 color-yellow" @click="showDetails(product.id)">Details</button>
        </td>
        <td class="w3-center cell-v-center" style="width: 20%">{{ product.name }}</td>
        <td class="w3-center cell-v-center" style="width: 20%">{{ product.type }}</td>
        <td class="w3-center cell-v-center" style="width: 20%">{{ product.location }}</td>
        <td class="w3-center cell-v-center" style="width: 20%">{{ product.damaged }}</td>
      </tr>
    </table>
    </div>
    <ProductDetails :showModal="state.showDetails" @closeDetailModal="closeDetailModal" :product="state.product"/>
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
        product: {}
      });

      const {
        getProductLites,
        getProductDetail
      } = repository();

      onMounted(async () => {
        state.products = await getProductLites();
      });

      async function showDetails(productId) {
        state.product = await getProductDetail(productId);
       

      state.showDetails = true;
      }
      function closeDetailModal(success) {
        if (success === false) {
          state.showDetails = false;
        }
        state.showDetails = false;
      }
      return {
        state,
        showDetails,
        closeDetailModal
      }
    }
  }
</script>

<style scoped>
</style>