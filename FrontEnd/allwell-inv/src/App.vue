<template>
  <div>
    <div id="nav">
      <div class="w3-bar">
        <img class="w3-left w3-padding-small" src="./assets/images/logo.svg" width="250">
        <button class="w3-bar-item w3-button w3-round-large" @click="doRoute('employeeAddEdit')">Add/Edit Employee</button>
      </div>
    </div>

    <router-view/>
  </div>
</template>

<script>
import { useRouter } from 'vue-router';

export default {
  name: 'App',

  setup() {
    const router = useRouter();

    doRoute('login');

    function doRoute(whereTo) {
      if (whereTo === 'login') {
        router.push('/');
      } else if (whereTo === 'inventory') {
        router.push('/inventory')
      } else if (whereTo === 'employeeAddEdit') {
        router.push('/employeeAddEdit')
      }
    }

    return {
      doRoute
    }
  }
}
</script>

<style>
@import './assets/styles/w3.css';

:root {
  --red: #ff3e3e;
  --light-red: #f0bcbc;
  --orange: #d19830;
  --light-orange: #ffd8ad;
  --yellow: #ffd83c;
  --light-yellow: #ffefad;
  --green: #32ff95;
  --light-green: #adffd5;
  --blue: #42bdff;
  --light-blue: #ade2ff;    
  --purple: #ff3cf5;
  --light-purple: #ffadfb;    
  --gray: #c5c5c2;
  --light-gray: #e6e6e6;
}

/* As is garbage (just leave it) */
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

/* General */
th.cell-v-center,
td.cell-v-center {
  vertical-align: middle;
  display: table-cell;
}

td.cell-h-right {
  text-align: right;
  display: table-cell;
}

td.cell-h-left {
  text-align: left;
  display: table-cell;
}

td.cell-h-center {
  text-align: center;
  display: table-cell;
}

.align-h-center {
  display: flex;
  justify-content: center;
  flex-direction: row;
}

.align-h-left {
  display: flex;
  justify-content: left;
  flex-direction: row;
}

.align-v-center {
  vertical-align: middle;
}
  
/* Color classes */
.color-red {
  background-color: var(--red);
}
  
.color-light-red {
  background-color: var(--light-red);
}
  
.color-orange {
  background-color: var(--orange);
}
.color-light-orange {
  background-color: var(--light-orange);
}
    
.color-yellow {
  background-color: var(--yellow);
}
.color-light-yellow {
  background-color: var(--light-yellow);
}
      
.color-green {
  background-color: var(--green);
}
.color-light-green {
  background-color: var(--light-green);
}
        
.color-blue {
  background-color: var(--blue);
}
.color-light-blue {
  background-color: var(--light-blue);
}
          
.color-purple {
  background-color: var(--purple);
}
.color-light-purple {
  background-color: var(--light-purple);
}
            
.color-gray {
  background-color: var(--gray);
}

.color-light-gray {
  background-color: var(--light-gray);
}

.font-color-red {
  color: var(--red);
}

/* Margin classes */
.margin-all-0 {
  margin: 0px;
}

.margin-all-5 {
  margin: 5px;
}

.margin-all-10 {
  margin: 10px;
}

.margin-all-15 {
  margin: 15px;
}
  
.margin-top-0 {
  margin-top: 0px;
}

.margin-top-5 {
  margin-top: 5px;
}

.margin-top-10 {
  margin-top: 10px;
}

.margin-top-15 {
  margin-top: 15px;
}
    
.margin-right-0 {
  margin-right: 0px;
}

.margin-right-5 {
  margin-right: 5px;
}

.margin-right-10 {
  margin-right: 10px;
}

.margin-right-15 {
  margin-right: 15px;
}

.margin-top-15 {
  margin-top: 15px;
}
    
.margin-bottom-0 {
  margin-bottom: 0px;
}

.margin-bottom-5 {
  margin-bottom: 5px;
}

.margin-bottom-10 {
  margin-bottom: 10px;
}

.margin-bottom-15 {
  margin-bottom: 15px;
}
      
.margin-left-0 {
  margin-left: 0px;
}

.margin-left-5 {
  margin-left: 5px;
}

.margin-left-10 {
  margin-left: 10px;
}

.margin-left-15 {
  margin-left: 15px;
}

/* Padding classes */
.padding-all-0 {
  padding: 0px;
}

.padding-all-5 {
  padding: 5px;
}

.padding-all-10 {
  padding: 10px;
}

.padding-all-15 {
  padding: 15px;
}
  
.padding-top-0 {
  padding-top: 0px;
}

.padding-top-5 {
  padding-top: 5px;
}

.padding-top-10 {
  padding-top: 10px;
}

.padding-top-15 {
  padding-top: 15px;
}
    
.padding-right-0 {
  padding-right: 0px;
}

.padding-right-5 {
  padding-right: 5px;
}

.padding-right-10 {
  padding-right: 10px;
}

.padding-right-15 {
  padding-right: 15px;
}
      
.padding-bottom-0 {
  padding-bottom: 0px;
}

.padding-bottom-5 {
  padding-bottom: 5px;
}

.padding-bottom-10 {
  padding-bottom: 10px;
}

.padding-bottom-15 {
  padding-bottom: 15px;
}
        
.padding-left-0 {
  padding-left: 0px;
}

.padding-left-5 {
  padding-left: 5px;
}

.padding-left-10 {
  padding-left: 10px;
}

.padding-left-15 {
  padding-left: 15px;

}

/* Font Sizes */
.font-size-tiny {
  font-size: 10px;
}

.font-size-small {
  font-size: 12px;
}

.font-size-medium {
  font-size: 14px;
}

.font-size-large {
  font-size: 16px;
}

.font-size-xlarge {
  font-size: 20px;
}

/* Buttons */
button.button-close {
  background: url("assets/images/closeIcon.png") 0 0 no-repeat;
  background-position: center center;
  background-size: 100% 100%;
  height: 2.5em;
  width: 2.5em;
  background-color: #000000;
}
</style>