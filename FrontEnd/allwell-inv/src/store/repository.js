import { ref } from 'vue';

export function repository() {
  const productLites = ref([]);
  const productDetail = ref({});
  const employeeDetail = ref({});
  const assignLogs = ref([]);
  const employees = ref([]);
  const types = ref([]);
  const locations = ref([]);

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

  async function getEmployeeDetail(employeeId) {
    const url = 'https://localhost:44364/api/Employee/' + employeeId;
    
    employeeDetail.value = {};

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        employeeDetail.value = data;
      })
      
      return employeeDetail.value;
  }

  async function putEmployeeDetail(id, fName, lName, username, password, isAdmin) {
    const url = 'https://localhost:44364/api/Employee/' + id + '/' + fName + '/' + lName + '/' + username + 
                                                          '/' + password + '/' + isAdmin;
    
    employeeDetail.value = {};

    const options = {
      method: "PUT"
    };
    

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        console.log(data);
        employeeDetail.value = data;
      })
      
      return employeeDetail.value;
  }

  async function postEmployee(fName, lName, username, password, isAdmin) {
    const url = 'https://localhost:44364/api/Employee/' + fName + '/' + lName + '/' + username + 
                                                          '/' + password + '/'+ isAdmin;
    
    employeeDetail.value = {};

    const options = {
      method: "POST"
    };
    

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        console.log(data);
        employeeDetail.value = data;
      })
      
      return employeeDetail.value;
  }

  
  async function getTypes() {
    const url = 'https://localhost:44364/inventory/type/'
    
    types.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        types.value = data;
      })
      
      return types.value;
  }

  async function getLocations() {
    const url = 'https://localhost:44364/Inventory/location'
    
    locations.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        locations.value = data;
      })
      
      return locations.value;
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
  async function putProductDetail(id, name, typeId, cost, locationId, condition, damaged, serialNo) {
    const url = 'https://localhost:44364/inventory/product/' + id + '/' + name + '/' + typeId + '/' + cost + 
                                                          '/' + locationId + '/' + condition + 
                                                          '/' + damaged + '/' + serialNo;
    
    productDetail.value = {};

    const options = {
      method: "PUT"
    };
    

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        console.log(data);
        productDetail.value = data;
      })
      
      return productDetail.value;
  }

  async function postProduct(name, typeId, cost, locationId, condition, damaged, serialNo) {
    const url = 'https://localhost:44364/Inventory/product/' + name + '/' + typeId + '/' + cost + 
                                                          '/' + locationId + '/' + condition + 
                                                           '/' + damaged +  '/'+ serialNo;
    
    productDetail.value = {};

    const options = {
      method: "POST"
    };
    

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        console.log(data);
        productDetail.value = data;
      })
      
      return productDetail.value;
  }
  return {
    getProductLites,
    getProductDetail,
    getEmployeeDetail,
    putEmployeeDetail,
    postEmployee,
    getAssignLogs,
    returnProduct,
    getEmployees,
    assignProduct,
    putProductDetail,
    getTypes,
    getLocations,
    postProduct
  }
}
