<template>
  <div class="w3-modal" style="display:block;" v-show="showLogsModal === true">
    <div class="w3-modal-content w3-light-grey w3-round-xxlarge w3-padding-16" style="width:40%; display: flex;">
      <div class="w3-container w3-col ">
         <a @click="close" class="w3-display-topright w3-margin"><font-awesome-icon icon="times" class="icons w3-text-grey w3-xxlarge" /></a>
         <h2><b>Assign Logs</b></h2>
       <h5><b>Product Name: </b>{{ product.name }}</h5>
      <h5><b>Serial Number: </b>{{ product.serialNo }}</h5>
      <table class="w3-table-all">
      <tr style="background-color: var(--blue)">
        <th class="w3-center cell-v-center">Employee Name</th>
        <th class="w3-center cell-v-center">Assigned Date</th>
        <th class="w3-center cell-v-center">Returned Date</th>
      </tr>

      <tr v-for="(log, id) in state.assignLogs" :key="id">
        <td class="w3-center cell-v-center">{{ log.employee.fName }} {{ log.employee.lName }}</td>
        <td class="w3-center cell-v-center">{{ log.assignedDate }}</td>
        <td v-show="log.returnedDate != null" class="w3-center cell-v-center">{{ log.returnedDate }}</td>
        <td v-show="log.returnedDate == null" class="w3-center cell-v-center">
            <button class="w3-button w3-round-large w3-card-4 color-yellow" @click="returnItem(product.id)">Return Now</button>
        </td>
      </tr>
    </table>
     
    <div v-show="state.lastLog.returnedDate != null" class="w3-center cell-v-center w3-container w3-margin">
      <h2><b>Assign Product</b></h2>
      <select v-model="state.employeeId" class="w3-input w3-round-xxlarge w3-border-0 w3-margin-bottom" required>
      <option :value="0" disabled selected>Select An Employee</option>
      <option v-for="employee in state.employees" :value="employee.id" :key="employee.id">
        {{ employee.fName }} {{ employee.lName }}
      </option>
    </select>
    <div v-if="!state.isEmployeeSelected">
        <p class="font-color-red">Please select an employee!</p>
      </div>
      <button class="w3-button w3-round-large w3-card-4 color-yellow" @click="assign(product.id)">Assign Now</button>
    </div>

    </div>
    </div>
  </div>
</template>

<script>
  import { reactive, toRefs, onMounted } from 'vue';
  import { repository } from '@/store/repository.js';

  export default {
    name: 'assignLogs',
    props: ['showLogsModal', 'product'],
    emits: ['closeLogsModal'],
    
    setup(props, context) {
      const state = reactive({
          assignLogs: {},
          product: toRefs(props).product,
          lastLog: {},
          employees: {},
          employeeId: 0,
          isEmployeeSelected: true,
      });

      const { getAssignLogs, returnProduct, getEmployees, assignProduct } = repository();
      
      onMounted(async () => {
        state.assignLogs = await getAssignLogs(state.product.id);
        state.lastLog= state.assignLogs.at(-1);
        state.employees = await getEmployees();
        formatDates();
      });
        
      

    async function returnItem(productId){
       state.assignLogs = await returnProduct(productId);
       state.lastLog= state.assignLogs.at(-1);
       formatDates();
    }

    function formatDates(){
        state.assignLogs.forEach(element => {
          if(element['returnedDate']){
              element['returnedDate'] = element['returnedDate'].replace('T', ' ').slice(0, 19);
          }
          
          if(element['assignedDate']){
              element['assignedDate'] = element['assignedDate'].replace('T', ' ').slice(0, 19);
          }
        });
    }

    async function assign(productId)
    {
      if(state.employeeId == 0){
        state.isEmployeeSelected = false;
      }
      else{
        state.isEmployeeSelected = true;

        state.assignLogs = await assignProduct(state.employeeId, productId);
        formatDates();
        state.lastLog= state.assignLogs.at(-1);
      }
    }

    function close() {
      context.emit('closeLogsModal', false);
    }
      return {
        state,
        getAssignLogs,
        close,
        returnItem,
        assign
      }
    }
  }
</script>

<style scoped>
</style>