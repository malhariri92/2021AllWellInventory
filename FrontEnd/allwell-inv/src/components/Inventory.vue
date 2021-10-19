<template>
  <div>
    <h2>Products</h2>    
    <table class="w3-table-all">
      <tr style="background-color: var(--blue)">
        <th class="w3-center cell-v-center"></th>
        <th class="w3-center cell-v-center" @click="sortByName">Name
          <img src="@/assets/images/downIcon.png" style="height: .75em; width: .75em" v-show="state.sort.onName === 1">
          <img src="@/assets/images/upIcon.png" style="height: .75em; width: .75em" v-show="state.sort.onName === -1">
        </th>
        <th class="w3-center cell-v-center" @click="sortByType">Type
          <img src="@/assets/images/downIcon.png" style="height: .75em; width: .75em" v-show="state.sort.onType === 1">
          <img src="@/assets/images/upIcon.png" style="height: .75em; width: .75em" v-show="state.sort.onType === -1">
        </th>
        <th class="w3-center cell-v-center" @click="sortByLocation">Location
          <img src="@/assets/images/downIcon.png" style="height: .75em; width: .75em" v-show="state.sort.onLocation === 1">
          <img src="@/assets/images/upIcon.png" style="height: .75em; width: .75em" v-show="state.sort.onLocation === -1">
        </th>
        
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
</template>

<script>
  import { reactive, onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  import { repository } from '@/store/repository.js';

  export default {
    name: 'inventory',

    setup() {
      const state = reactive({
        products: [],
        sort: {
          onName: 0,
          onType: 0,
          onLocation: 0
        },
        searchCategory: '',
        searchString: ''
      });

      const router = useRouter();

      const {
        getProductLites,
        getProductDetail
      } = repository();

      onMounted(async () => {
        state.products = await getProductLites();
      });

      async function showDetails(productId) {
        console.log(await getProductDetail(productId));

        router.push('/productAddEdit');
      }

//#region 'sorting'
      function resetSortObject() {
        state.sort.onName = 0;
        state.sort.onType = 0;
        state.sort.onLocation = 0;
      }

      function getSortOrderDescending(property) {
        return function (a, b) {
          if (a[property] < b[property]) {
            return 1;
          } else if (a[property] > b[property]) {
            return -1;
          }
          return 0;
        }
      }

      function getSortOrderAscending(property) {
        return function (a, b) {
          if (a[property] > b[property]) {
            return 1;
          } else if (a[property] < b[property]) {
            return -1;
          }
          return 0;
        }
      }

      function sortByName() {
        switch (state.sort.onName) {
          case 0:
            state.products.sort(getSortOrderAscending('name'));
            resetSortObject();
            state.sort.onName = 1;
            break;
          case 1:
            state.products.sort(getSortOrderDescending('name'));
            resetSortObject();
            state.sort.onName = -1;
            break;
          case -1:
            state.products.sort(getSortOrderAscending('name'));
            resetSortObject();
            state.sort.onName = 1;
            break;
        }
      }

      function sortByType() {
        switch (state.sort.onType) {
          case 0:
            state.products.sort(getSortOrderAscending('type'));
            resetSortObject();
            state.sort.onType = 1;
            break;
          case 1:
            state.products.sort(getSortOrderDescending('type'));
            resetSortObject();
            state.sort.onType = -1;
            break;
          case -1:
            state.products.sort(getSortOrderAscending('type'));
            resetSortObject();
            state.sort.onType = 1;
            break;
        }
      }

      function sortByLocation() {
        switch (state.sort.onLocation) {
          case 0:
            state.products.sort(getSortOrderAscending('location'));
            resetSortObject();
            state.sort.onLocation = 1;
            break;
          case 1:
            state.products.sort(getSortOrderDescending('location'));
            resetSortObject();
            state.sort.onLocation = -1;
            break;
          case -1:
            state.products.sort(getSortOrderAscending('location'));
            resetSortObject();
            state.sort.onLocation = 1;
            break;
        }
      }
//#endregion

      return {
        state,
        showDetails,
        sortByName,
        sortByType,
        sortByLocation
      }
    }
  }
</script>

<style scoped>
</style>