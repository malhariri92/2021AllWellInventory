<template>
  <div>   
    <div class="w3-display-container">
      <div v-if="store.userState.user !== null">
        <h2><b>Inventory</b></h2>        
          <button v-if="store.userState.user.isAdmin" class="w3-button w3-blue w3-round-xxlarge w3-display-topright w3-margin-right w3-hover-text-black" @click="showDetails(0)">
            <b><font-awesome-icon icon="plus-circle" class="icons"/> Product</b>
          </button>
      </div>
    <div class="w3-table">
      <div class="w3-row w3-border-top w3-medium w3-padding">
        Search by 
          <select class="w3-padding-small w3-round-xxlarge" v-model="state.searchBy" @change="clearString">
            <option value="name">Name</option>
            <option value="type">Type</option>
            <option value="location">Location</option>
          </select> : 

        <span v-if="state.searchBy === 'name'">
          <input type="text" class="w3-margin-right w3-padding-small w3-round-xxlarge" style="border-width:1px;" v-model="state.searchString" @keyup="search">
        </span>

        <span v-if="state.searchBy === 'type'">
          <select class="w3-padding-small w3-round-xxlarge w3-margin-right" v-model="state.searchType" @change="search">
            <option v-for="(type, id) in state.types" :key="id" :value="type.name">{{ type.name }}</option>
          </select>
        </span>

        <span  v-if="state.searchBy === 'location'">
          <select class="w3-padding-small w3-round-xxlarge w3-margin-right" v-model="state.searchLocation" @change="search">
            <option v-for="(location, id) in state.locations" :key="id" :value="location.name">{{ location.name }}</option>
          </select>
        </span>      

      <span style="white-space:nowrap;">
        <input class="w3-check" type="checkbox" name="damaged" v-model="state.includeDamaged" @change="refreshProducts()">
        <label class="w3-margin-right" for="damaged"> Damaged</label>
        <button class="w3-button w3-border w3-padding-small w3-round-xxlarge" @click="clearSearch">Clear Search</button>
      </span>

      </div>
      <div class="w3-row w3-red w3-sepia-min w3-padding-small" v-if="state.includeDamaged">Damaged value: {{ new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(state.damageCost) }}</div>
    </div>

    <table class="w3-table-all w3-border-0">
      <tr class="w3-blue">
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
        clearString();
        await refreshProducts();
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

            totalDamageCost();
            break;

            case 'type':
              if (state.searchType !== 0) {
                state.products = await getProductLites(state.includeDamaged);
                state.products = state.products.filter(product => product.type == state.searchType);
              }

              totalDamageCost();
              break;

            case 'location':
              if (state.searchLocation !== 0) {
                state.products = await getProductLites(state.includeDamaged);
                state.products = state.products.filter(product => product.location == state.searchLocation);
              }

              totalDamageCost();
              break;
        }
      }

      async function refreshProducts() {
        state.products = await getProductLites(state.includeDamaged);
        search()
      }

      function totalDamageCost() {
        state.damageCost = 0;

        state.products.forEach(product => {
          if (product.damaged) {
            state.damageCost += product.cost;
          }
        })
      }

      function clearSearch() {
        state.searchBy = 'name';
        state.searchString = '';
        state.searchType = '';
        state.searchLocation = '';
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
        closeDetailModal,
        store,
      }
    }
  }
</script>

<style scoped>
</style>