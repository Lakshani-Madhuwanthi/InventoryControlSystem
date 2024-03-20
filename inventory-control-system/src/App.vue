<template>
  <div class="background">
    <RouterView />
  </div>
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

<style scoped>
.background{
  background-color: rgb(228, 228, 228);
  min-height: 100vh;
}
</style>
