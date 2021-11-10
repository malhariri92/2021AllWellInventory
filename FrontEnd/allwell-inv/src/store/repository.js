import { ref } from 'vue';

export function repository() {
  const productLites = ref([]);
  const productDetail = ref({});
  const employeeDetail = ref({});
  const locationDetail = ref({});
  const typeDetail = ref({});
  const assignLogs = ref([]);
  const employees = ref([]);
  const types = ref([]);
  const locations = ref([]);

  async function getProductLites(includeDamaged) {
    const url = 'https://localhost:44364/inventory/productLite/' + includeDamaged;
    
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

  async function putEmployeeDetail(id, fName, lName, username, password, isAdmin, inactive) {
    const url = 'https://localhost:44364/api/Employee/' + id + '/' + fName + '/' + lName + '/' + username + 
                                                          '/' + password + '/' + isAdmin + '/' + inactive + '/';
    
    employeeDetail.value = {};

    const options = {
      method: "PUT"
    };
    

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        employeeDetail.value = data;
      })
      
      return employeeDetail.value;
  }

  async function postEmployee(fName, lName, username, password, isAdmin, inactive) {
    const url = 'https://localhost:44364/api/Employee/' + fName + '/' + lName + '/' + username + 
                                                          '/' + password + '/'+ isAdmin + '/' + inactive;
    
    employeeDetail.value = {};

    const options = {
      method: "POST"
    };
    

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        employeeDetail.value = data;
      })
      
      return employeeDetail.value;
  }

  async function getLocations() {
    const url = 'https://localhost:44364/Location'
    
    locations.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        locations.value = data;
      })
      
      return locations.value;
  }

  async function getLocationDetail(locationId) {
    const url = 'https://localhost:44364/Location/' + locationId;
    
    locationDetail.value = {};

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        locationDetail.value = data;
      })
      
      return locationDetail.value;
  }

  async function putLocationDetail(id, name, city, county) {
    const url = 'https://localhost:44364/Location/' + id + '/' + name + '/' + city + '/' + county;
    
    locationDetail.value = {};

    const options = {
      method: "PUT"
    };

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        locationDetail.value = data;
      })
      
      return locationDetail.value;
  }

  async function postLocation(name, city, county) {
    const url = 'https://localhost:44364/location/' + name + '/' + city + '/' + county;
    
    locationDetail.value = {};

    const options = {
      method: "POST"
    };
    
    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        locationDetail.value = data;
      })
      
      return locationDetail.value;
  }

  async function getTypes() {
    const url = 'https://localhost:44364/Type/'
    
    types.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        types.value = data;
      })
      
      return types.value;
  }

  async function getTypeDetail(typeId) {
    const url = 'https://localhost:44364/Type/' + typeId;
    
    typeDetail.value = {};

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        typeDetail.value = data;
      })
      
      return typeDetail.value;
  }

  async function putTypeDetail(id, name) {
    const url = 'https://localhost:44364/Type/' + id + '/' + name;
    
    typeDetail.value = {};

    const options = {
      method: "PUT"
    };

    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        typeDetail.value = data;
      })
      
      return typeDetail.value;
  }

  async function postType(name) {
    const url = 'https://localhost:44364/Type/' + name;
    
    typeDetail.value = {};

    const options = {
      method: "POST"
    };
    
    await fetch(url, options)
      .then(response => response.json())
      .then(function (data) {
        typeDetail.value = data;
      })
      
      return typeDetail.value;
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
        productDetail.value = data;
      })
      
      return productDetail.value;
  }

  async function listAllTypes() {
    const url = 'https://localhost:44364/inventory/type';

    types.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        types.value = data;
      })

    return types.value;
  }

  async function listAllLocations() {
    const url = 'https://localhost:44364/inventory/location';

    locations.value = [];

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        locations.value = data;
      })

    return locations.value;
  }  

  return {
    getProductLites,
    getProductDetail,
    getEmployeeDetail,
    getLocationDetail,
    getTypeDetail,
    putEmployeeDetail,
    putLocationDetail,
    putTypeDetail,
    postEmployee,
    postLocation,
    postType,
    getAssignLogs,
    returnProduct,
    getEmployees,
    assignProduct,
    putProductDetail,
    getTypes,
    getLocations,
    postProduct,
    listAllTypes,
    listAllLocations
  }
}
