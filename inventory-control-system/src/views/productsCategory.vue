<template>
  <p class="text-h3 py-6 text-center font-weight-bold text-cyan-darken-2">
    Product Category Page
  </p>
  <v-row>
    <v-col cols="8"></v-col>
    <v-col cols="4" class="d-flex justify-end align-end pr-10">
      <v-btn @click="isAddCategoryModalOpen = true" variant="tonal" color="primary" size="large"
        style="text-transform:unset;">Add Category</v-btn>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="4" v-for="category in categories" :key="category.categoryId">
      <v-card class="mx-auto" tonal="16" max-width="440" min-height="200">
        <v-card-title class="text-h5 text-center">{{ category.categoryName }}</v-card-title>
        <v-card-text class="text-subtitle-2">{{ category.categoryDescription }}</v-card-text>
        <v-card-actions class="d-flex align-end px-4">
          <v-btn style="text-transform:unset;" @click="openDeleteModal(category.categoryName, category.categoryId)" variant="tonal"
            color="error">Delete</v-btn>
          <v-btn style="text-transform:unset;" @click="openEditModal(category)" variant="tonal">Edit</v-btn>
          <v-btn style="text-transform:unset;" class="ml-auto" color="cyan-lighten-2" variant="tonal"
            :to="'/productsPage/' + category.categoryId">View Products</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
  <v-dialog v-model="isDeleteCategoryModalOpen" width="auto">
    <v-card max-width="400" title="Delete Product">
      <v-card-text>Are you sure you want to delete {{ DeleteCategoryName }} category?</v-card-text>
      <v-card-actions class="mx-2">
        <v-btn @click="isDeleteCategoryModalOpen = false">Cancel</v-btn>
        <v-btn @click="DeleteCategory(DeleteCategoryId)">confirm</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <v-dialog v-model="isEditCategoryModalOpen" width="auto">
    <v-card width="500" class="ma-4">
      <v-card-title class="text-center">Edit Category</v-card-title>
      <v-form @submit.prevent class="mx-4">
        <v-text-field variant="underlined" v-model="categoryName" label="Category Name"></v-text-field>
        <v-text-field variant="underlined" v-model="categoryDescription" label="Category Description"></v-text-field>
      </v-form>
      <div class="d-flex justify-end">
        <v-btn class="my-4" @click="isEditCategoryModalOpen = false">Cancel</v-btn>
        <v-btn class="ma-4" @click="EditCategory(selectedCategory, categoryName, categoryDescription)">Submit</v-btn>
      </div>
    </v-card>
  </v-dialog>
  <v-dialog v-model="isAddCategoryModalOpen" width="auto">
    <v-card width="500" class="ma-4">
      <v-card-title class="text-center">Add Category</v-card-title>
      <v-form @submit.prevent class="mx-4">
        <v-text-field variant="underlined" v-model="newCategory.categoryName" label="Category Name"></v-text-field>
        <v-text-field variant="underlined" v-model="newCategory.categoryDescription"
          label="Category Description"></v-text-field>
      </v-form>
      <div class="d-flex justify-end">
        <v-btn class="my-4" @click="isAddCategoryModalOpen = false">Cancel</v-btn>
        <v-btn class="ma-4" type="submit" @click="AddCategory(newCategory)">Submit</v-btn>
      </div>
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
      DeleteCategoryName: '',
      DeleteCategoryId: null,
      isDeleteCategoryModalOpen: false,
      isEditCategoryModalOpen: false,
      isAddCategoryModalOpen: false,
      categoryName: '',
      categoryDescription: '',
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
    openDeleteModal(categoryName, categoryId) {
      // Set the selectedCategory for deletion when opening the modal
      this.DeleteCategoryName = categoryName;
      this.DeleteCategoryId = categoryId;
      this.isDeleteCategoryModalOpen = true;
    },
    openEditModal(category) {
      // Set the selectedCategory for deletion when opening the modal
      this.selectedCategory = category;
      this.categoryName = category.categoryName;
      this.categoryDescription = category.categoryDescription;
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
    DeleteCategory(categoryId) {

      axios.delete('http://localhost:5027/api/category/delete/' + categoryId)
        .then(() => {
          this.fetchData()
          this.isDeleteCategoryModalOpen = false;
          console.log(this.isDeleteCategoryModalOpen);
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
    },
    AddCategory(newCategory) {
      axios.post('http://localhost:5027/api/category/createCategory', newCategory)
        .then(() => {
          this.fetchData()
        })
        .catch(error => {
          console.error('Error fetching categories:', error);
        })
        .finally(() => {
          this.isAddCategoryModalOpen = false;
        })
        this.newCategory = {
          categoryName: '',
          categoryDescription: '',
        }
    }
  }
}

</script>

<style scoped></style>