<template>
  <div>
    <h2>Products</h2>    

    <div class="w3-row margin-bottom-15">
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
        <div class="align-v-center" v-if="state.includeDamaged">{{ new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(state.damageCost) }}</div>
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

      const router = useRouter();

      const {
        getProductLites,
        getProductDetail,
        listAllTypes,
        listAllLocations
      } = repository();

      onMounted(async () => {
        await refreshProducts()
        state.types = await listAllTypes();
        state.locations = await listAllLocations();
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
              state.products = await getProductLites(state.includeDamaged);
              state.products = state.products.filter(product => product.type == state.searchType);

              totalDamageCost();
              break;

            case 'location':
              state.products = await getProductLites(state.includeDamaged);
              state.products = state.products.filter(product => product.location == state.searchLocation);

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
          console.log(state.damageCost);
        })
      }

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
        totalDamageCost
      }
    }
  }
</script>

<style scoped>
</style>