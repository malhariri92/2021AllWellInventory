import { ref } from 'vue';

export function repository() {
  const productLites = ref([]);
  const productDetail = ref({});
  const employee = ref({});
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

  async function login(userName, password) {
    const url = 'https://localhost:44364/api/Employee/' + userName + '/' + password;

    employee.value = {};

    await fetch(url)
      .then(response => response.json())
      .then(function (data) {
        employee.value = data;
        console.log(data);
      })

    return employee.value;

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
    login,
    putProductDetail,
    getTypes,
    getLocations,
    postProduct
  }
}