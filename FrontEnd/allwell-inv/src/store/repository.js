import { ref } from 'vue';

export function repository() {
  const productLites = ref([]);
  const productDetail = ref({});
  const employee = ref({});
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

  async function login(userName, password) {
    const url = 'https://localhost:44364/api/Employee/' + userName + '/' + password;

    employee.value = {};

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        console.log(data);
        employee.value = data;
      })

    return employee.value;
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
    login,
    listAllTypes,
    listAllLocations
  }
}