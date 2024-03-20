<template>
    <p class="text-h4 text-center py-6 font-weight-medium text-cyan-darken-2">
      Products Page
    </p>
    <v-row>
      <v-col cols="8" class="pl-10">
        <v-btn to="/" variant="tonal" color="purple-darken-2" style="text-transform:unset;">View Categories</v-btn>
      </v-col>
      <v-col cols="4" class="d-flex justify-end align-end pr-10">
        <v-btn @click="isAddProductModalOpen = true" variant="tonal" color="primary" size="large" style="text-transform:unset;">Add Product</v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="4" v-for="product in products" :key="product.productId">
        <v-card class="mx-auto" tonal="16" max-width="440" min-height="200">
          <v-card-title class="text-h5 text-center">{{ product.productName }}</v-card-title>
          <v-card-text>{{ product.productDescription }}</v-card-text>
          <div class="d-flex justify-space-between align-center">
          <div class="d-flex flex-column">
          <v-card-text>Product Price</v-card-text>
          <v-card-text class="py-0">{{ product.productPrice }}</v-card-text>
          </div>
          <div class="d-flex flex-column pr-2">
          <v-card-text>Product Quantity</v-card-text> 
          <v-card-text class="py-0">{{ product.productQuantity }}</v-card-text>
          </div>
          </div>
          <v-card-actions class="d-flex align-end px-4 my-4">
            <v-btn style="text-transform:unset;" @click="openDeleteModal(product)" variant="tonal" color="error">Delete</v-btn>
            <v-btn style="text-transform:unset;" @click="openEditModal(product)" variant="tonal" >Edit</v-btn> 
          </v-card-actions>
        </v-card>
      </v-col>
      <!-- <v-col cols="4" class="d-flex justify-center align-center">
        <v-btn @click="isAddProductModalOpen = true" icon="mdi-plus" variant="tonal" color="primary" size="x-large"></v-btn>
      </v-col> -->
    </v-row>
    <!-- <v-btn class="mt-2" @click="isAddProductModalOpen = true">Add Product</v-btn> -->
    <v-dialog v-model="isDeleteProductModalOpen" width="auto">
      <v-card max-width="400" title="Delete Product">
        <v-card-text>Are you sure you want to delete {{ deleteProductName }} Product?</v-card-text>
        <v-card-actions class="mx-2">
          <v-btn @click="isDeleteProductModalOpen = false">Cancel</v-btn>
          <v-btn @click="DeleteProduct(selectedProduct)">confirm</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="isEditProductModalOpen" width="auto">
      <v-card width="500" class="ma-4">
        <v-card-title class="text-center">Edit Product</v-card-title>
        <v-form @submit.prevent class="mx-4">
          <v-text-field variant="underlined" v-model="ProductName" label="ProductName"></v-text-field>
          <v-text-field variant="underlined" v-model="ProductDescription" label="ProductDescription"></v-text-field>
          <v-text-field variant="underlined" v-model="productPrice" label="productPrice"></v-text-field>
          <v-text-field variant="underlined" v-model="productQuantity" label="productQuantity"></v-text-field>
          <div class="d-flex justify-end">
            <v-btn class="my-4" type="submit" @click="EditProduct(selectedProduct, ProductName, ProductDescription, productPrice, productQuantity)">Submit</v-btn>
          <v-btn class="ma-4" @click="isEditProductModalOpen = false">Cancel</v-btn>
          </div>
        </v-form>
      </v-card>
    </v-dialog>
    <v-dialog v-model="isAddProductModalOpen" width="auto">
      <v-card width="500" class="ma-4">
        <v-card-title class="text-center">Add Product</v-card-title>
        <v-form @submit.prevent class="mx-4">
          <v-text-field variant="underlined" v-model="newProduct.productName" label="Product Name"></v-text-field>
          <v-text-field variant="underlined" v-model="newProduct.productDescription" label="Product Description"></v-text-field>
          <v-text-field variant="underlined" v-model="newProduct.productPrice" label="product Price"></v-text-field>
          <v-text-field variant="underlined" v-model="newProduct.productQuantity" label="product Quantity"></v-text-field>
          <div class="d-flex justify-end">
            <v-btn class="my-4" @click="isAddProductModalOpen = false">Cancel</v-btn>
          <v-btn class="ma-4" type="submit" @click="AddProduct(newProduct)">Submit</v-btn>
          </div>
        </v-form>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  import axios from 'axios'
  
  export default {
    name: 'productsPage',
    data() {
      return {
        products: [],
        isAddProductModalOpen: false,
        deleteProductName: '',
        newProduct: {
          productName: '',
          productDescription: '',
          productPrice: '',
          productQuantity: '',
        },
        ProductName: '',
        ProductDescription: '',
        productPrice: '',
        productQuantity: '',
        selectedProduct: null,
        isDeleteProductModalOpen: false,
        isEditProductModalOpen: false,
      }
    },
    mounted() {
      // Fetch data when the component is mounted
      this.fetchData();
    },
    methods: {
      openDeleteModal(Product) {
        // Set the selectedProduct for deletion when opening the modal
        this.selectedProduct = Product;
        this.deleteProductName = Product.productName;
        this.isDeleteProductModalOpen = true;
      },
      openEditModal(Product) {
        // Set the selectedProduct for deletion when opening the modal
        this.selectedProduct = Product;
        this.ProductName = Product.productName;
        this.ProductDescription = Product.productDescription;
        this.productPrice = Product.productPrice;
        this.productQuantity = Product.productQuantity;
        this.isEditProductModalOpen = true;
      },
      fetchData() {
        
        const categoryId = this.$route.params.categoryId;
  
        axios.get(`http://localhost:5027/api/product/${categoryId}`)
          .then(response => {
            this.products = response.data;
          })
          .catch(error => {
            console.error('Error fetching products:', error);
          });
      },
      DeleteProduct(product) {
  
  axios.delete('http://localhost:5027/api/Product/delete/' + product.productId)
    .then(() => {
      this.fetchData()
      this.isDeleteProductModalOpen = false;
      console.log(this.isDeleteProductModalOpen);
      this.selectedProduct = null;
    }

    )
    .catch(error => {
      console.error('Error fetching categories:', error);
    });
},
EditProduct(Product, ProductName, ProductDescription, productPrice, productQuantity) {
  const payload = {
    ProductId: Product.productId,
    categoryId: Product.categoryId,
    ProductName: ProductName,
    ProductDescription: ProductDescription,
    productPrice : productPrice,
    productQuantity : productQuantity
  }
  console.log(payload);
  axios.put('http://localhost:5027/api/Product/update', payload)
    .then(() => {
      this.fetchData()
      this.isEditProductModalOpen = false;
      this.selectedProduct = null;
    }
    )
    .catch(error => {
      console.error('Error fetching categories:', error);
    });
},
      AddProduct(newProduct) {
        const categoryId = this.$route.params.categoryId;
        console.log(newProduct);
        const payload = {
          categoryId: categoryId,
          ProductName: newProduct.productName,
          ProductDescription: newProduct.productDescription,
          productPrice: newProduct.productPrice,
          productQuantity: newProduct.productQuantity
        }
        axios.post(`http://localhost:5027/api/product/createProduct`, payload)
          .then(() => {
            this.fetchData();
          })
          .catch(error => {
            console.error('Error adding product:', error);
          })
          .finally(() => {
            this.isAddProductModalOpen = false;
          });
        this.newProduct = {
          productName: '',
          productDescription: '',
          productPrice: '',
          productQuantity: '',
        }
      }
    },
  }
  </script>
  