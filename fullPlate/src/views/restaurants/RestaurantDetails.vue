<template>
  <div class="clearfix">
    <div v-if="editRestaurantName" class="field-holder restaurant_details_name">
      <input type="text" id="restaurant-name" name="restaurant-name" v-model="editedRestaurant.name">
      <button type="button" class="btn-primary" @click="toggleNameEdit()"><i class="icon-white-tick"></i></button>
    </div>

    <div v-else="" class="field-holder restaurant_details_name">
      <h2 class="restaurant_visible_label">{{editedRestaurant.name}}</h2>
      <button type="button" class="btn-primary" @click="toggleNameEdit()"><i class="icon-pencil"></i></button>
    </div>

    <h1>Menu items</h1>
    <button class="btn-primary add-restaurant" @click="toggleNewDishModal()"><i class="icon-plus"></i>Add Restaurant</button>

    <h2>Soups</h2>
    <table class="table-primary menu-items-table" v-if="editedRestaurant.dishes">
      <thead>
      <tr class="table-headings">
        <th><i class="icon-filter-arrow-table"></i>Name</th>
        <th class="table-actions"></th>
      </tr>
      </thead>
      <tbody>
        <tr v-for="dish in editedRestaurant.dishes" v-if="dish.type === dishEnums.SOUP">
          <td>{{dish.name}}</td>
          <td class="table-actions">
            <button type="button" class="btn-secondary js-confirm-deletion" @click="handleDelete(restaurant.id)">
              <i class="icon-minus"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <h2>Main Dishes</h2>
    <table class="table-primary menu-items-table" v-if="editedRestaurant.dishes">
      <thead>
      <tr class="table-headings">
        <th><i class="icon-filter-arrow-table"></i>Name</th>
        <th class="table-actions"></th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="dish in editedRestaurant.dishes" v-if="dish.type === dishEnums.MAIN">
        <td>{{dish.name}}</td>
        <td class="table-actions">
          <!--<router-link :to="'/restaurants/' + restaurant.id">-->
          <!--<button type="button" class="btn-primary">-->
          <!--<i class="icon-pencil"></i>-->
          <!--</button>-->
          <!--</router-link>-->
          <button type="button" class="btn-secondary js-confirm-deletion" @click="handleDelete(restaurant.id)">
            <i class="icon-minus"></i>
          </button>
        </td>
      </tr>
      </tbody>
    </table>

    <dish-modal
            v-if="showNewDishModal"
            :toggle-modal="toggleNewDishModal"
            :restaurant-id="restaurantId">
    </dish-modal>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  import DishModal from '@/components/restaurants/DishModal.vue';
  import dishTypes from '@/utils/enums/dishTypes';

  export default {
    name: 'restaurant-details',
    components: {
      'dish-modal': DishModal
    },
    data () {
      return {
        restaurantId: this.$route.params.id,
        editRestaurantName: false,
        showNewDishModal: false,
        dishEnums: dishTypes
      };
    },
    methods: {
      toggleNameEdit () {
        this.editRestaurantName = !this.editRestaurantName;
      },
      toggleNewDishModal () {
        this.showNewDishModal = !this.showNewDishModal;
      },
      saveChanges () {
        this.$store.dispatch('saveRestaurantChanges', this.restaurantId);
      }
    },
    created () {
      this.$store.dispatch('getOneRestaurant', this.$route.params.id);
    },
    computed: {
      ...mapGetters(['editedRestaurant'])
    }
  };
</script>

<style>

</style>
