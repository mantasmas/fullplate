<template>
  <div>
    <md-table-card style="width: 100%;" v-if="restaurantsLoaded">
      <md-toolbar>
        <h1 class="md-title">Restaurants</h1>
        <md-button id="btn-new-restaurant" @click="onNewRestaurantModalOpen('new-restaurant-modal')">Add</md-button>
      </md-toolbar>

      <md-table md-sort="name">
        <md-table-header>
          <md-table-row>
            <md-table-head md-sort-by="name">Restaurant name</md-table-head>
            <md-table-head md-sort-by="address">Restaurant Address</md-table-head>
            <md-table-head md-sort-by="number">Restaurant Telephone No.</md-table-head>
            <md-table-head md-sort-by="count">Dishes Count</md-table-head>
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
              <md-menu md-size="3">
                <md-button class="md-icon-button" md-menu-trigger>
                  <md-icon>more_vert</md-icon>
                </md-button>

                <md-menu-content>
                  <md-menu-item>
                    <md-icon>add</md-icon>
                    <span>Add dishes</span>
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
        :edit-mode="this.editRestaurant"
        ref="new-restaurant-modal"/>
  </div>
</template>

<script>
  import {mapGetters} from 'vuex';
  import RestaurantDataModal from '../../components/restaurants/RestaurantDataModal.vue';

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
        editRestaurant: false
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
        this.editRestaurant = false;
        const newData = {
          id: 0,
          name: '',
          address: '',
          telephoneNumber: ''
        };
        this.$store.commit('setEditedRestaurant', newData);
        this.$refs[ref].$refs['new-restaurant-dialog'].open();
      },
      onRestaurantEdit (ref, editableRestaurantData) {
        this.editRestaurant = true;
        this.$store.commit('setEditedRestaurant', JSON.parse(JSON.stringify(editableRestaurantData)));
        this.$refs[ref].$refs['new-restaurant-dialog'].open();
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
