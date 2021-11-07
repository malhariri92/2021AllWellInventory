<template>
  <div class="w3-modal" style="display:block;" v-show="showLogsModal === true">
    <div class="w3-modal-content w3-light-grey w3-round-xxlarge w3-padding-16" style="display: flex;">
      <div class="w3-container w3-col ">
          <a @click="close" class="w3-display-topright w3-margin w3-text-grey w3-hover-text-black">
            <font-awesome-icon icon="window-close" class="icons w3-xlarge" /></a>
         <h2><b>Assign Logs</b></h2>
       <h5><b>Product Name: </b>{{ state.product.name }}</h5>
      <h5><b>Serial Number: </b>{{ state.product.serialNo }}</h5>
      <table class="w3-table-all w3-margin-bottom">
      <tr class="w3-blue">
        <th class="w3-center cell-v-center">Employee Name</th>
        <th class="w3-center cell-v-center">Assigned Date</th>
        <th class="w3-center cell-v-center">Returned Date</th>
      </tr>
      
      <tr v-for="(log, id) in state.assignLogs" :key="id">
        <td class="w3-center cell-v-center">{{ log.employee.fName }} {{ log.employee.lName }}</td>
        <td class="w3-center cell-v-center">{{ log.assignedDate }}</td>
        <td v-show="log.returnedDate != null" class="w3-center cell-v-center">{{ log.returnedDate }}</td>
        <td v-show="log.returnedDate == null" class="w3-center cell-v-center">
            <button class="w3-button w3-round-large w3-card-4 color-yellow" @click="returnItem(productId)">Return Now</button>
        </td>
      </tr>
    </table>

     <div  v-show="(typeof(state.lastLog) === 'undefined')" >
        <p class="w3-center cell-v-center"> Looks like this product has not been assigned to anyone recently!
          </p>
    </div>

    <div v-show="checkLogs() && store.userState.user.isAdmin"
       class="w3-center cell-v-center w3-container w3-margin">
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
      <button class="w3-button w3-round-large w3-card-4 color-yellow" @click="assign(productId)">Assign Now</button>
    </div>

    </div>
    </div>
  </div>
</template>

<script>
  import { reactive, onMounted, inject } from 'vue';
  import { repository } from '@/store/repository.js';

  export default {
    name: 'assignLogs',
    props: ['showLogsModal', 'productId'],
    emits: ['closeLogsModal'],
    
    setup(props, context) {
      const state = reactive({
          assignLogs: [],
          product: {},
          lastLog: {},
          employees: {},
          employeeId: 0,
          isEmployeeSelected: true,
      });

      const store = inject('store');

      const { 
        getAssignLogs,
        returnProduct,
        getEmployees, 
        assignProduct,
        getProductDetail, } = repository();
      
      onMounted(async () => {
        state.product = await getProductDetail(props.productId);
        state.assignLogs = await getAssignLogs(props.productId);
        if (typeof(state.assignLogs) !== 'undefined'){
          state.lastLog = state.assignLogs[state.assignLogs.length -1];
        }

        state.employees = await getEmployees();      
        formatDates();
        
      });
        
      

    async function returnItem(productId){
       state.assignLogs = await returnProduct(productId);
      if (typeof(state.assignLogs) !== 'undefined'){
          state.lastLog = state.assignLogs[state.assignLogs.length -1];
        }
       formatDates();
    }

    function formatDates(){
       if (typeof(state.lastLog) !== 'undefined'){
        state.assignLogs.forEach(element => {
          if(element['returnedDate']){
              element['returnedDate'] = element['returnedDate'].replace('T', ' ').slice(0, 19);
          }
          
          if(element['assignedDate']){
              element['assignedDate'] = element['assignedDate'].replace('T', ' ').slice(0, 19);
          }
        });
       }
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
        if (typeof(state.assignLogs) !== 'undefined'){
          state.lastLog = state.assignLogs[state.assignLogs.length -1];
        }
      }
    }

    function checkLogs() {
      if (typeof(state.lastLog) == 'undefined' )
      {
        return true;
      }
      if(state.lastLog.returnedDate !== null)
      {
        return true;
      }
      return false;
    }
    
    function close() {
      context.emit('closeLogsModal', false);
    }
      return {
        state,
        getAssignLogs,
        close,
        returnItem,
        assign,
        checkLogs,
        store
      }
    }
  }
</script>

<style scoped>
</style>