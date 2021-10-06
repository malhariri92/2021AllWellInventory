import { createRouter, createWebHistory } from 'vue-router';
//import Home from '../components/Home.vue';
import Inventory from '../components/Inventory.vue';
import Login from '../components/Login.vue';
import ProductAddEdit from '../components/ProductAddEdit.vue';
import EmployeeAddEdit from '../components/EmployeeAddEdit.vue';

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: '/inventory',
    name: 'Inventory',
    component: Inventory
  },
  {
    path: '/productAddEdit',
    name: 'ProductAddEdit',
    component: ProductAddEdit
  },
  {
    path: '/employeeAddEdit',
    name: 'EmployeeAddEdit',
    component: EmployeeAddEdit
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router