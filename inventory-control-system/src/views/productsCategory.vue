<template>
    <p class="text-h4 text-center font-weight-medium my-4">
      Product Category Page
    </p>
    <v-row>
      <v-col cols="4" v-for="category in categories" :key="category.categoryId">
        <v-card class="mx-auto my-8" tonal="16" max-width="360" min-height="200">
          <v-card-title>{{ category.categoryName }}</v-card-title>
          <v-card-text>{{ category.categoryDescription }}</v-card-text>
          <v-card-actions>
            <v-btn color="orange-lighten-2" variant="text" :to="'/productsPage/' + category.categoryId"	>View Products</v-btn>
            <v-btn @click="openEditModal(category)">Edit</v-btn>
            <v-btn @click="openDeleteModal(category)">Delete</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
    <v-dialog v-model="isDeleteCategoryModalOpen" width="auto">
      <v-card max-width="400" title="Delete Category">
        <v-card-text>Are you sure you want to delete {{ selectedCategory.categoryName }} category?</v-card-text>
        <v-card-actions class="mx-2">
          <v-btn @click="isDeleteCategoryModalOpen = false">Cancel</v-btn>
          <v-btn @click="DeleteCategory(selectedCategory)">confirm</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="isEditCategoryModalOpen" width="auto">
      <v-card width="300">
        <v-form @submit.prevent>
          <v-text-field v-model="categoryName" label="categoryName"></v-text-field>
          <v-text-field v-model="categoryDescription" label="categoryDescription"></v-text-field>
          <v-btn class="mt-2" type="submit" @click="EditCategory(selectedCategory, categoryName, categoryDescription)">Submit</v-btn>
          <v-btn @click="isEditCategoryModalOpen = false">Cancel</v-btn>
        </v-form>
      </v-card>
    </v-dialog>
  </template>
  <script>
  import axios from 'axios';
  import { RouterLink, RouterView } from 'vue-router'
  
  export default {
    name: 'App',
    data() {
      return {
        categories: [],
        selectedCategory: null,
        isDeleteCategoryModalOpen: false,
        isEditCategoryModalOpen: false,
        categoryName: '',
        categoryDescription	: '',
        newCategory: {
          categoryName: '',
          categoryDescription: '',
        },
      };
    },
    mounted() {
      this.fetchData();
    },
    methods: {
      openDeleteModal(category) {
        // Set the selectedCategory for deletion when opening the modal
        this.selectedCategory = category;
        this.isDeleteCategoryModalOpen = true;
      },
      openEditModal(category) {
        // Set the selectedCategory for deletion when opening the modal
        this.selectedCategory = category;
        this.isEditCategoryModalOpen = true;
      },
      fetchData() {
        axios.get('http://localhost:5027/api/category/categoryList')
          .then(response => {
            this.categories = response.data;
          })
          .catch(error => {
            console.error('Error fetching categories:', error);
          });
      },
      DeleteCategory(category) {
  
        axios.delete('http://localhost:5027/api/category/delete/' + category.categoryId)
          .then(() => {
            this.fetchData()
            this.isDeleteCategoryModalOpen = false;
            this.selectedCategory = null;
          }
  
          )
          .catch(error => {
            console.error('Error fetching categories:', error);
          });
      },
      EditCategory(category, categoryName, categoryDescription) {
        const payload = {
          categoryId: category.categoryId,
          categoryName: categoryName,
          categoryDescription: categoryDescription
        }
        axios.put('http://localhost:5027/api/category/update', payload)
          .then(() => {
            this.fetchData()
            this.isEditCategoryModalOpen = false;
            this.selectedCategory = null;
          }
          )
          .catch(error => {
            console.error('Error fetching categories:', error);
          });
      }
    }
  }
  
  </script>
  
  <style scoped></style>
  