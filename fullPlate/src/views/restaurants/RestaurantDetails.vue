<template>
  <div class="clearfix">
    <div v-if="editRestaurantName" class="field-holder restaurant_details_name">
      <input type="text" id="restaurant-name" name="restaurant-name" v-model="restaurantName">
      <button type="button" class="btn-primary" @click="toggleNameEdit()"><i class="icon-white-tick"></i></button>
    </div>

    <div v-else="" class="field-holder restaurant_details_name">
      <h2 class="restaurant_visible_label">{{restaurantName}}</h2>
      <button type="button" class="btn-primary" @click="toggleNameEdit()"><i class="icon-pencil"></i></button>
    </div>


    <h1>Menu items</h1>
    <h2>Soups</h2>
    <h2>Main Dishes</h2>
  </div>
</template>

<script>
  export default {
    name: 'restaurant-delete',
    props: ['restaurantData'],
    data () {
      return {
        restaurantName: 'Talluti',
        restaurantOffers: [],
        editRestaurantName: false
      }
    },
    methods: {
      toggleNameEdit () {
        this.editRestaurantName = !this.editRestaurantName
      }
    }
  }
</script>

<style>

</style>
