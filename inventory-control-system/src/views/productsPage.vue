<template>
    <p class="text-h4 text-center font-weight-medium my-4">
      Product Page
    </p>
    <v-row>
      <v-col cols="4" v-for="product in products" :key="product.productId">
        <v-card class="mx-auto my-8" tonal="16" max-width="360" min-height="200">
          <v-card-title>{{ product.productName }}</v-card-title>
          <v-card-text>{{ product.productDescription }}</v-card-text>
          <v-card-text>{{ product.productPrice }}</v-card-text>
          <v-card-text>{{ product.productQuantity }}</v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </template>
  
  <script>
  import axios from 'axios'
  
  export default {
    name: 'productsPage',
    data() {
      return {
        products: [],
      }
    },
    mounted() {
      // Fetch data when the component is mounted
      this.fetchData();
    },
    methods: {
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
    },
  }
  </script>
  