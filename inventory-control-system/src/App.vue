<template>
  <RouterView />
</template>

<script>
import productCategory from './views/productsCategory.vue';

export default {
  name: 'App',
  components: {
    productCategory
  },
  
}

</script>

<style scoped></style>
