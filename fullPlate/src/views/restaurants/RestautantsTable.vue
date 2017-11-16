<template>
  <div v-if="restaurantsLoaded">
    <div v-if="restaurantsList.length">
      <table class="table-primary menu-items-table">
        <thead>
        <tr class="table-headings">
          <th><i class="icon-filter-arrow-table"></i>Name</th>
          <th style="width: 125px;"></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="restaurant in restaurantsList">
          <td>{{restaurant.name}}</td>
          <td>
            <router-link :to="'/restaurants/' + restaurant.id">
              <button type="button" class="btn-primary">
                <i class="icon-pencil"></i>
              </button>
            </router-link>
            <button type="button" class="btn-secondary js-confirm-deletion" @click="handleDelete(restaurant.id)">
              <i class="icon-minus"></i>
            </button>
          </td>
        </tr>
        <tr v-if="showAdd">
          <td>
            <form id="new-restaurant" v-on:submit.prevent="handleAdd(newRestaurantName)">
              <input type="text" class="restaurant-input" name="guest-name" required="" v-model="newRestaurantName"/>
            </form>
          </td>
          <td>
            <button class="btn-primary" type="submit" form="new-restaurant">
              <i class="icon-plus"/>
            </button>
            <button class="btn-secondary" type="button" @click="toggleAdd()">
              <i class="icon-minus"/>
            </button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
    <div v-else-if="restaurantsList.length === 0">
      <h2>No restaurants registered!</h2>
    </div>
    <button class="btn-primary add-restaurant" v-if="!showAdd" @click="toggleAdd()"><i class="icon-plus"></i>Add Restaurant</button>
  </div>

</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    name: 'restaurants-table',
    data () {
      return {
        showAdd: false,
        newRestaurantName: ''
      };
    },
    methods: {
      handleDelete (restaurantId) {
        this.$store.commit('toggleConfirm', restaurantId);
      },
      handleAdd (restaurantName) {
        this.$store.dispatch('addNewRestaurant', restaurantName)
          .then((success) => {
            if (success) {
              this.showAdd = false;
              this.newRestaurantName = '';
            }
          });
      },
      toggleAdd () {
        this.showAdd = !this.showAdd;
        this.newRestaurantName = '';
      }
    },
    created () {
      this.$store.dispatch('getAllRestaurants');
    },
    computed: {
      ...mapGetters(['restaurantsList', 'restaurantsLoaded'])
    }
  };
</script>
