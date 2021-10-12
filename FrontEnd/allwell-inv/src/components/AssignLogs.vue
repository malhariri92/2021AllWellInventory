<template>
  <div class="w3-modal" style="display:block;" v-show="showLogsModal === true">
    <div v-if="showLogsModal === true">
      {{ getLogs(product.id) }}
    </div>
    <div class="w3-modal-content w3-light-grey w3-round-xxlarge w3-padding-16" style="width:50%; min-height:600px;">
      <div class="w3-container w3-col ">
         <a @click="close" class="w3-display-topright w3-margin"><font-awesome-icon icon="times" class="icons w3-text-grey w3-xxlarge" /></a>
         <h2><b>Assign Logs</b></h2>
       <h5><b>Product Name: </b>{{ product.name }}</h5>
      <h5><b>Serial Number: </b>{{ product.serialNo }}</h5>
      <table class="w3-table-all">
      <tr style="background-color: var(--blue)">
        <th class="w3-center cell-v-center">Employee ID</th>
        <th class="w3-center cell-v-center">Assigned Date</th>
        <th class="w3-center cell-v-center">Returned Date</th>
      </tr>

      <tr v-for="(log, id) in state.assignLogs" :key="id">
        <td class="w3-center cell-v-center">{{ log.employeeId }}</td>
        <td class="w3-center cell-v-center">{{ log.assignedDate }}</td>
        <td v-show="log.returnedDate != null" class="w3-center cell-v-center">{{ log.returnedDate }}</td>
        <td v-show="log.returnedDate == null" class="w3-center cell-v-center">
            <button class="w3-button w3-round-large w3-card-4 color-yellow" @click="returnItem(product.id)">Return Now</button>
        </td>

      </tr>
    </table>
    </div>
    </div>
  </div>
</template>

<script>
  import { reactive } from 'vue';
  //import { useRouter } from 'vue-router';
  import { repository } from '@/store/repository.js';

  export default {
    name: 'assignLogs',
    props: ['showLogsModal', 'product'],
    emits: ['closeLogsModal'],
    
    setup(props, context) {
      const state = reactive({
          assignLogs: {},
      });

      //const router = useRouter();
      var date = new Date().toLocaleString();
      const { getAssignLogs, returnProduct } = repository();
      
      async function getLogs(productId){
        state.assignLogs = await getAssignLogs(productId);

        state.assignLogs.forEach(element => {
          if(element['returnedDate']){
              element['returnedDate'] = element['returnedDate'].replace('T', ' ');
          }
          
          if(element['assignedDate']){
              element['assignedDate'] = element['assignedDate'].replace('T', ' ');
          }
        });
      }
      async function returnItem(productId){
          await returnProduct(productId);
      }

      function close() {
      context.emit('closeLogsModal', false);
    }
      return {
        state,
        getAssignLogs,
        date,
        close,
        getLogs,
        returnItem,
      }
    }
  }
</script>

<style scoped>
</style>