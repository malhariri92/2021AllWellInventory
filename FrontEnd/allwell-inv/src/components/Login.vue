<template>
  <div>
    <div class="login-container w3-container w3-light-grey w3-round-xxlarge" title="LogIn">
      <h2>Login</h2>
      <div class="input-field">
        <font-awesome-icon icon="user" class="icons" />
        <input type="text" v-model="state.userName" placeholder="Username" />
      </div>

      <div v-if="state.isUserEmpty">
        <p class="font-color-red">Please enter your username</p>
      </div>

      <div class="input-field">
        <font-awesome-icon icon="lock" class="icons" />
        <input type="password" v-model="state.password" placeholder="Password"/>
      </div>

      <div v-if="state.isPasswordEmpty">
        <p class="font-color-red">Please enter your password</p>
      </div>

      <div v-if="!state.isValidUser">
        <p class="font-color-red">Invalid username or password</p>
      </div>
      <button @click="validateUser()" class="w3-button w3-round-xxlarge w3-blue w3-hover-cyan">LOGIN</button>
    </div>
  </div>
</template>

<script>
  import { reactive, inject } from 'vue';
  import { useRouter } from 'vue-router';
  
  export default {
    name: "Login",
  
    setup() {
      const state = reactive({
        userName: "",
        password: "",
        isUserEmpty: false,
        isPasswordEmpty: false,
        employee: {'id':0},
        isValidUser: true,
      });
  
      const router = useRouter();
      const store = inject('store');
  
      /**
       * check if the username or password are empty
       * to display error messages
       */
      function validateUser() {
        if (state.userName === "") {
          state.isUserEmpty = true;
          state.isPasswordEmpty = false;
          state.isValidUser = true;
        } else if (state.password === "") {
          state.isUserEmpty = false;
          state.isPasswordEmpty = true;
          state.isValidUser = true;
        } else {
          state.isUserEmpty = false;
          state.isPasswordEmpty = false;
          doValidateUser();
        }
      }
<<<<<<< Updated upstream
      /**
       * do the actual validation for user.
       */
      async function doValidateUser() {
        state.employee = await store.methods.login(state.userName, state.password);
        
        if (JSON.stringify(state.employee) === "false") {
          state.isValidUser = false;
        } else {
          router.push('/inventory');
        }
=======
    }
    /**
     * do the actual validation for user.
     */
    async function doValidateUser() {
      state.employee = await store.methods.login(state.userName, state.password);
      
      if (state.employee === null) {
        state.isValidUser = false;
      } else {
        router.push('/inventory');
>>>>>>> Stashed changes
      }
  
      return {
        state,
        validateUser,
        doValidateUser,
        store
      };
    },
  };
</script>

<style scoped>
.login-container {
  display: flex;
  flex-direction: column;
  width: 50%;
  margin: auto;
  justify-content: center;
  align-items: center;
}
.login-container h2 {
  font-weight: 600;
  font-size: 2.2rem;
  color: #444;
}
.input-field {
  width: 80%;
  height: 55px;
  background-color: white;
  margin: 10px 0;
  border-radius: 55px;
  display: grid;
  grid-template-columns: 15% 75%;
  padding: 0 0.4rem;
  position: relative;
}

.input-field input {
  border: none;
  outline: none;
  overflow: hidden;
  background-color: none;
  font-weight: 600;
  font-size: 1.1rem;
}
.icons {
  text-align: center;
  line-height: 55px;
  color: #acacac;
  font-size: 2rem;
  margin: auto;
}
button {
  font-weight: bold;
  margin: 15px 0;
  width: 80%;
}
</style>