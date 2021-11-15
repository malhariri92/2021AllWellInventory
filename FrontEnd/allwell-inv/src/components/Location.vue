<template>
  <div>
    <div class="w3-display-container"> 
      <div>
        <h2><b>Locations</b></h2>        
          <button class="w3-button w3-blue w3-round-xxlarge w3-display-topright w3-margin-right w3-hover-text-black" @click="showDetails(0)">
            <b> <font-awesome-icon icon="plus-circle" class="icons" /> Location</b>
          </button>
          </div>

        <table class="w3-table-all w3-hoverable">
          <tr class="w3-blue">
            <th class="w3-center cell-v-center">Name</th>
            <th class="w3-center cell-v-center">City</th>
            <th class="w3-center cell-v-center">County</th>
          </tr>

          <tr v-for="(location, id) in state.locations" :key="id" v-on:click="showDetails(location.locationId)" class="w3-hover-pale-yellow" style="cursor:pointer">
            <td class="w3-center cell-v-center" style="width: 20%">{{ location.name }}</td>
            <td class="w3-center cell-v-center" style="width: 20%">{{ location.city }}</td>
            <td class="w3-center cell-v-center" style="width: 20%">{{ location.county }}</td>
          </tr>      
        </table>
      </div>
    <LocationDetails :showModal="state.showDetails" @closeDetailModal="closeDetailModal" :locationId="state.selectedLocationId"/>
  </div>
</template>

<script>
  import { reactive, onMounted } from 'vue';
  import { repository } from '@/store/repository.js';
  import LocationDetails from '@/components/LocationAddEdit.vue';

  export default {
    name: 'inventory',
    components: {LocationDetails},
    setup() {
      const state = reactive({
        locations: [],
        showDetails: false,
        location: {},
        selectedLocationId: 0,
      });

      const {
        getLocations,
      } = repository();

      onMounted(async () => {
        state.locations = await getLocations();
      });

      async function showDetails(locationId) {
        state.selectedLocationId = locationId;
        state.showDetails = true; 
      }
      
      async function closeDetailModal() {
        state.showDetails = false;
        state.locations = await getLocations();
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