<template>
  <div>
    <div v-if="restaurants.length">
      <table class="table-primary menu-items-table">
        <thead>
        <tr class="table-headings">
          <th><i class="icon-filter-arrow-table"></i>Name</th>
          <th style="width: 125px;"></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="restaurant in restaurants">
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
          <td><input type="text" class="restaurant-input" name="guest-name" required="" v-model="newRestaurantName"></td>
          <td>
            <button type="button" class="btn-primary"><i class="icon-plus" @click="handleAdd(newRestaurantName)"></i></button>
            <button type="button" class="btn-secondary" @click="toggleAdd()">
              <i class="icon-minus"></i>
            </button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
    <div v-else>
      <h2>No restaurants registered!</h2>
    </div>
    <button class="btn-primary add-restaurant" v-if="!showAdd" @click="toggleAdd()"><i class="icon-plus"></i>Add Restaurant</button>
  </div>

</template>

<script>
  export default {
    name: 'restaurants-table',
    props: ['restaurantsList'],
    data () {
      return {
        restaurants: this.restaurantsList,
        showAdd: false,
        newRestaurantName: ''
      }
    },
    methods: {
      handleDelete (id) {
        this.$store.commit('showConfirm', {
          title: 'Do you want to delete this restaurant?',
          requestUrl: 'delete'
        })
      },
      handleAdd (restaurantName) {
        console.log(restaurantName)
      },
      toggleAdd () {
        this.showAdd = !this.showAdd
        this.newRestaurantName = ''
      }
    }
  }
</script>
