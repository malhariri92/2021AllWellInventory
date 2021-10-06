import { ref } from 'vue';

export function repository() {
<<<<<<< HEAD
  const productLites = ref([]);
  const productDetail = ref({});

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

  return {
    getProductLites,
    getProductDetail
  }
}
=======
    const employee = ref({});

    async function login(userName, password) {
        const url = 'https://localhost:5001/api/Employee/' + userName + '/' + password;

        employee.value = {};

        await fetch(url)
            .then(response => response.json())
            .then(function (data) {
                employee.value = data;
            })

        return employee.value;
    }

    return {
        login
    }
}
>>>>>>> Mutasem-branch
