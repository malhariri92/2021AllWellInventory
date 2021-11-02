<template>
  <div>   
    <div class="w3-display-container">
    <div v-if="store.userState.user !== null">
    <h2>Products</h2>        
      <button v-if="store.userState.user.isAdmin" class="w3-button w3-blue w3-round-xxlarge w3-display-topright w3-margin-right w3-hover-text-black" @click="showDetails(0)">
        <b> <font-awesome-icon icon="plus-circle" class="icons" />  Add New Product</b>
      </button>
      </div>
    <div class="w3-row margin-bottom-10">
      <div class="w3-quarter">
        <div class="w3-right-align align-v-center">Search by 
          <select class="margin-left-5 align-v-center" v-model="state.searchBy" @change="clearString">
            <option value="name">Name</option>
            <option value="type">Type</option>
            <option value="location">Location</option>
          </select>:
        </div>
      </div>

      <div class="w3-quarter align-v-center">
        <div class="w3-left-align" v-if="state.searchBy === 'name'">
          <input type="text" v-model="state.searchString" @keyup="search">
        </div>

        <div class="w3-left-align" v-if="state.searchBy === 'type'">
          <select class="margin-left-5 align-v-center" v-model="state.searchType" @change="search">
            <option value="" selected disabled hidden>Choose type</option>
            <option v-for="(type, id) in state.types" :key="id" :value="type.name">{{ type.name }}</option>
          </select>
        </div>

        <div class="w3-left-align" v-if="state.searchBy === 'location'">
          <select class="margin-left-5 align-v-center" v-model="state.searchLocation" @change="search">
            <option value="" selected disabled hidden>Choose location</option>
            <option v-for="(location, id) in state.locations" :key="id" :value="location.name">{{ location.name }}</option>
          </select>
        </div>
      </div>

      <div class="w3-quarter w3-center">
        <input class="w3-check" type="checkbox" name="damaged" v-model="state.includeDamaged" @change="refreshProducts()">
        <label class="" for="damaged">Include Damaged</label>
      </div>

      <div class="w3-quarter w3-left-align">
        <button class="w3-button w3-round-large color-red" @click="clearSearch">Clear</button>
      </div>
      
    </div>

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
        types: [],
        locations: [],
        sort: {
          onName: 0,
          onType: 0,
          onLocation: 0
        },
        searchBy: 'name',
        searchString: '',
        searchType: 0,
        searchLocation: 0,
        includeDamaged: false,
        damageCost: 0
      });

      const {
        getProductLites,
        getProductDetail,
        listAllTypes,
        listAllLocations
      } = repository();

      const store = inject('store');

      onMounted(async () => {
        await refreshProducts()
        state.types = await listAllTypes();
        state.locations = await listAllLocations();
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

      async function clearString() {
        state.searchString = '';
        state.products = await getProductLites(state.includeDamaged);
      }

      async function search() {
        switch (state.searchBy) {
          case 'name':
            if (state.searchString.trim() !== '') {
              let pattern = new RegExp('^' + state.searchString, 'i');

              let results = [];

              state.products.forEach(product => {
                if (product.name.match(pattern)) {
                  results.push(product);
                }
              })

              state.products = results;
            } else {
              state.products = await getProductLites(state.includeDamaged);
            }

            break;

            case 'type':
              state.products = await getProductLites(state.includeDamaged);
              state.products = state.products.filter(product => product.type == state.searchType);

              break;

            case 'location':
              state.products = await getProductLites(state.includeDamaged);
              state.products = state.products.filter(product => product.location == state.searchLocation);

            break;
        }
      }

      async function refreshProducts() {
        state.products = await getProductLites(state.includeDamaged);
        search()
      }

      // function totalDamageCost() {
        // state.products.forEach(product => {
          // if (product.)
        // })
      // }

      function clearSearch() {
        state.searchBy = 'name';
        state.searchString = '';
        state.includeDamaged = false;

        search();
      }

      return {
        state,
        showDetails,
        sortByName,
        sortByType,
        sortByLocation,
        clearString,
        search,
        refreshProducts,
        clearSearch,
        totalDamageCost,
        closeDetailModal,
        store,
      }
    }
  }
</script>

<style scoped>
</style>