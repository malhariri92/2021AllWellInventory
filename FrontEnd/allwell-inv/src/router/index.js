import { createRouter, createWebHistory } from 'vue-router';
import Home from '../components/Home.vue';
<<<<<<< HEAD
import Inventory from '../components/Inventory.vue';
=======
import Login from '../components/Login.vue';
>>>>>>> Mutasem-branch

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
<<<<<<< HEAD
    path: '/inventory',
    name: 'Inventory',
    component: Inventory
=======
    path: '/Login',
    name: 'Login',
    component: Login,
>>>>>>> Mutasem-branch
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router