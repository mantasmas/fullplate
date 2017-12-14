<template>
  <div>
    <md-table-card style="width: 100%;" v-if="restaurantsLoaded">
      <md-toolbar>
        <h1 class="md-title">Restaurants</h1>
        <md-button id="btn-new-restaurant" @click="onNewRestaurantModalOpen('new-restaurant-modal')">Add</md-button>
      </md-toolbar>

      <md-table @sort="onSort">
        <md-table-header>
          <md-table-row>
            <md-table-head md-sort-by="name">Restaurant name</md-table-head>
            <md-table-head>Restaurant Address</md-table-head>
            <md-table-head>Restaurant Telephone No.</md-table-head>
            <md-table-head>Dishes Count</md-table-head>
            <md-table-head>Actions</md-table-head>
          </md-table-row>
        </md-table-header>

        <md-table-body>
          <md-table-row v-for="(restaurant, idx) in restaurantsList" :key="idx" :md-item="restaurant">
            <md-table-cell>
              {{restaurant.name}}
            </md-table-cell>
            <md-table-cell>
              {{restaurant.address || '---'}}
            </md-table-cell>
            <md-table-cell>
              {{restaurant.telephoneNumber || '---'}}
            </md-table-cell>
            <md-table-cell>
              {{restaurant.dishes ? restaurant.dishes.length : '---'}}
            </md-table-cell>
            <md-table-cell>
              <md-menu md-size="4">
                <md-button class="md-icon-button" md-menu-trigger>
                  <md-icon>more_vert</md-icon>
                </md-button>

                <md-menu-content>
                  <md-menu-item @selected="onManageDishes(restaurant.id, restaurant.name)">
                    <md-icon>add</md-icon>
                    <span>Manage dishes</span>
                  </md-menu-item>

                  <md-menu-item @selected="onRestaurantEdit('new-restaurant-modal', restaurant)">
                    <md-icon>edit</md-icon>
                    <span>Edit info</span>
                  </md-menu-item>

                  <md-menu-item @selected="handleDelete(restaurant.id)">
                    <md-icon>remove</md-icon>
                    <span>Remove</span>
                  </md-menu-item>
                </md-menu-content>
              </md-menu>
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
    </md-table-card>

    <md-dialog-confirm
        :md-title="confirm.title"
        :md-content-html="confirm.contentHtml"
        :md-ok-text="confirm.ok"
        :md-cancel-text="confirm.cancel"
        @close="onConfirmClose"
        ref="confirm-restaurant-delete">
    </md-dialog-confirm>

    <restaurant-data-modal
        :edit-mode="editRestaurantModal"
        :restaurant-data="editedRestaurant"
        ref="new-restaurant-modal"/>
  </div>
</template>

<script>
  import {mapGetters} from 'vuex';
  import RestaurantDataModal from './RestaurantDataModal.vue';

  export default {
    components: {RestaurantDataModal},
    name: 'restaurants-table',
    data () {
      return {
        removedRestaurantId: 0,
        confirm: {
          title: 'Delete this restaurant?',
          contentHtml: 'Are you sure you want to delete this restaurant?',
          ok: 'Yes',
          cancel: 'No'
        },
        editRestaurantModal: false,
        editedRestaurant: {
          id: 0,
          name: '',
          address: '',
          telephoneNumber: ''
        }
      };
    },
    methods: {
      handleDelete (restaurantId) {
        this.removedRestaurantId = restaurantId;
        this.$refs['confirm-restaurant-delete'].open();
      },
      closeDialog (ref) {
        this.$refs[ref].close();
      },
      onConfirmClose (type) {
        if (type === 'ok') {
          this.$store.dispatch('removeRestaurant', this.removedRestaurantId);
        }
      },
      onNewRestaurantModalOpen (ref) {
        this.editRestaurantModal = false;
        this.editedRestaurant = {
          name: '',
          address: '',
          telephoneNumber: ''
        };
        this.$refs[ref].$refs['new-restaurant-dialog'].open();
      },
      onRestaurantEdit (ref, restaurantData) {
        this.editRestaurantModal = true;
        this.editedRestaurant = JSON.parse(JSON.stringify(restaurantData));
        this.$refs[ref].$refs['new-restaurant-dialog'].open();
      },
      onSort (sortObj) {
        this.$store.commit('sortRestaurantsTable', sortObj);
      },
      onManageDishes (restaurantId) {
        this.$router.push({path: `restaurants/${restaurantId}/dishes`});
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


<style>
  .table-actions {
    width: 125px;
  }
</style>
