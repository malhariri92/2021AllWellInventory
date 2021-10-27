<template>
  <div>
      <div class="w3-display-container"> 
    <div>
    <h2>Types</h2>        
      <button class="w3-button w3-blue w3-round-xxlarge w3-display-topright w3-margin-right w3-hover-text-black" @click="showDetails(0)">
        <b> <font-awesome-icon icon="plus-circle" class="icons" /> Add Type</b>
      </button>
      </div>

    <table class="w3-table-all w3-hoverable">
      <tr style="background-color: var(--blue)">
        <th class="w3-center cell-v-center">Name</th>
      </tr>

      <tr v-for="(type, id) in state.types" :key="id" v-on:click="showDetails(type.typeId)" class="w3-hover-pale-yellow" style="cursor:pointer">
        <td class="w3-center cell-v-center" style="width: 20%">{{ type.name }}</td>
      </tr>      
    </table>

    </div>
    <TypeDetails :showModal="state.showDetails" @closeDetailModal="closeDetailModal" :typeId="state.selectedTypeId"/>
  </div>
</template>

<script>
  import { reactive, onMounted } from 'vue';
  import { repository } from '@/store/repository.js';
  import TypeDetails from '@/components/TypeAddEdit.vue';

  export default {
    name: 'inventory',
    components: {TypeDetails},
    setup() {
      const state = reactive({
        types: [],
        showDetails: false,
        type: {},
        selectedTypeId: 0,
      });

      const {
        getTypes,
      } = repository();

      onMounted(async () => {
        state.types = await getTypes();
      });

      async function showDetails(typeId) {
        state.selectedTypeId = typeId;
       state.showDetails = true;

      }
      
      async function closeDetailModal(success) {
        if (success === false) {
          state.showDetails = false;
        }
        state.showDetails = false;
        state.types = await getTypes();
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