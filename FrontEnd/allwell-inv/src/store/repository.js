import { ref } from 'vue';

export function repository() {
  const productLites = ref([]);
  const productDetail = ref({});
  const employee = ref({});

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
        console.log(data);
        employee.value = data;
      })

    return employee.value;

  }

  async function putProductDetail(product) {
    const url = 'https://localhost:44364/inventory/product/';
    
    productDetail.value = {};

    const options = {
      method: "PUT",
      body: product
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
    putProductDetail
  }
}