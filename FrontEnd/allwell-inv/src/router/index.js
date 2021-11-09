import { createRouter, createWebHistory } from 'vue-router';
import Inventory from '../components/Inventory.vue';
import Login from '../components/Login.vue';
import ProductAddEdit from '../components/ProductAddEdit.vue';
import Employee from '../components/Employee.vue';
import EmployeeAddEdit from '../components/EmployeeAddEdit.vue';
import Location from '../components/Location.vue';
import LocationAddEdit from '../components/LocationAddEdit.vue';
import Type from '../components/Type.vue';
import TypeAddEdit from '../components/TypeAddEdit.vue';
import AssignLogs from '../components/AssignLogs.vue';

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
    path: '/employee',
    name: 'Employee',
    component: Employee
  },
  {
    path: '/location',
    name: 'Location',
    component: Location
  },
  {
    path: '/type',
    name: 'Type',
    component: Type
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
  },
  {
    path: '/locationAddEdit',
    name: 'LocationAddEdit',
    component: LocationAddEdit
  }, 
  {
    path: '/typeAddEdit',
    name: 'TypeAddEdit',
    component: TypeAddEdit
  },
  {
    path: '/assignLogs',
    name: 'AssignLogs',
    component: AssignLogs
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router