<template>
  <div>
      <div class="w3-display-container"> 
        <div>
          <h2><b>Employees</b></h2>        
          <button class="w3-button w3-blue w3-round-xxlarge w3-display-topright w3-margin-right w3-hover-text-black" @click="showDetails(0)">
            <b><font-awesome-icon icon="plus-circle" class="icons"/>Employee</b>
          </button>
        </div>

        <table class="w3-table-all w3-hoverable">
          <tr class="w3-blue">
            <th class="w3-center cell-v-center">Name</th>
            <th class="w3-center cell-v-center">Username</th>
            <th class="w3-center cell-v-center">Admin</th>
          </tr>

          <tr v-for="(employee, id) in state.employees" :key="id" v-on:click="showDetails(employee.id)" class="w3-hover-pale-yellow" style="cursor:pointer">
            <td class="w3-center" style="width: 20%">{{ employee.fName }} {{ employee.lName }}</td>
            <td class="w3-center" style="width: 20%">{{ employee.username }}</td>
            <td class="w3-center" style="width: 20%"><font-awesome-icon icon="check" class="icons w3-text-green" v-if="employee.isAdmin === true" /></td>
          </tr>      
        </table>
      </div>
    <EmployeeDetails :showModal="state.showDetails" @closeDetailModal="closeDetailModal" :employeeId="state.selectedEmployeeId"/>
  </div>
</template>

<script>
  import { reactive, onMounted } from 'vue';
  import { repository } from '@/store/repository.js';
  import EmployeeDetails from '@/components/EmployeeAddEdit.vue';

  export default {
    name: 'inventory',
    components: {EmployeeDetails},
    setup() {
      const state = reactive({
        employees: [],
        showDetails: false,
        employee: {},
        selectedEmployeeId: 0,
      });

      const {
        getEmployees,
      } = repository();

      onMounted(async () => {
        state.employees = await getEmployees();
      });

      async function showDetails(employeeId) {
        state.selectedEmployeeId = employeeId;
        state.showDetails = true;
      }
      
      async function closeDetailModal() {
        state.showDetails = false;
        state.employees = await getEmployees();
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