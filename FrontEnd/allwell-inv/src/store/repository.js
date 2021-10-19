import { ref } from 'vue';

export function repository() {
  const productLites = ref([]);
  const productDetail = ref({});
  const employee = ref({});
  const assignLogs = ref([]);
  const employees = ref({});
  async function getProductLites() {
    const url = 'https://localhost:44364/inventory/productLite';
    
    productLites.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        productLites.value = data;
      })

      return productLites.value;
  }

  async function getProductDetail(productId) {
    const url = 'https://localhost:44364/inventory/product/' + productId;
    
    productDetail.value = {};

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        productDetail.value = data;
      })
      
      return productDetail.value;
  }

  async function login(userName, password) {
    const url = 'https://localhost:44364/api/Employee/' + userName + '/' + password;

    employee.value = {};

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        employee.value = data;
      })

    return employee.value;

  }

  async function getAssignLogs(productId) {
    const url = 'https://localhost:44364/api/AssignLogs/' + productId;
    
    assignLogs.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        assignLogs.value = data;
        console.log(assignLogs.value);
      })
      
      return assignLogs.value;
  }

  /**Return a product that is assigned to an employee
   * by updating the returned date field.
   */
  async function returnProduct(productId) {
    const url = 'https://localhost:44364/api/AssignLogs/returnProduct/' + productId;
    await fetch(url, {method: "PUT"})
      .then(response => response.json())
      .then(function (data) {
        assignLogs.value = data;
      })
      
      return assignLogs.value;
  }

  async function getEmployees() {
    const url = 'https://localhost:44364/api/Employee/';
    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        employees.value = data;
      })
      
      return employees.value;
  }

  async function assignProduct(employeeId, productId) {
    const url = 'https://localhost:44364/api/AssignLogs/assignProduct/' + employeeId + '/' + productId;

    await fetch(url, {method: "POST"})
    .then(response => response.json())
    .then(function (data) {
      assignLogs.value = data;
    })

    return assignLogs;
  }
  return {
    getProductLites,
    getProductDetail,
    login,
    getAssignLogs,
    returnProduct,
    getEmployees,
    assignProduct
  }
}