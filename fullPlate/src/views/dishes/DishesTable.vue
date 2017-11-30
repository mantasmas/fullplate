<template>
  <div>
    <md-table-card style="width: 100%;">
      <md-toolbar>
        <h1 class="md-title">{{restaurantName}} dishes</h1>
        <md-button id="btn-new-dish" @click="onNewDishAdd">New Dish</md-button>
      </md-toolbar>

      <md-table md-sort="dishType" md-sort-type="asc" @sort="onSort">
        <md-table-header>
          <md-table-row>
            <md-table-head md-sort-by="name">Dish Name</md-table-head>
            <md-table-head>Dish Price</md-table-head>
            <md-table-head md-sort-by="dishType">Dish Type</md-table-head>
            <md-table-head>Is Vegetarian</md-table-head>
          </md-table-row>
        </md-table-header>

        <md-table-body>
          <md-table-row v-for="(dish, idx) in dishesList" :key="idx" :md-item="dish">
            <md-table-cell>
              {{dish.name}}
            </md-table-cell>
            <md-table-cell>
              {{dish.price}}
            </md-table-cell>
            <md-table-cell>
              {{dish.dishType === dishTypesEnum.SOUP ? 'Soup' : 'Main'}}
            </md-table-cell>
            <md-table-cell>
              {{dish.isVegetarian ? 'Yes' : 'No'}}
            </md-table-cell>
            <md-table-cell>
              <md-menu md-size="3">
                <md-button class="md-icon-button" md-menu-trigger>
                  <md-icon>more_vert</md-icon>
                </md-button>
                <md-menu-content>
                  <md-menu-item @selected="onDishEdit(dish)">
                    <md-icon>edit</md-icon>
                    <span>Edit info</span>
                  </md-menu-item>

                  <md-menu-item @selected="onDishDelete(dish.id)">
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

    <dish-data-modal
        :edit-mode="modalEditMode"
        :dish-data="editedDish"
        ref="dish-data-modal"
    />
  </div>
</template>

<script>
  import {mapGetters} from 'vuex';
  import DishDataModal from './DishDataModal.vue';
  import DishTypes from '../../utils/enums/dishTypes';

  export default {
    components: {DishDataModal},
    name: 'dish-table',
    data () {
      return {
        modalEditMode: false,
        editedDish: {
          name: '',
          price: '',
          dishType: 0,
          isVegetarian: false
        },
        dishTypesEnum: DishTypes
      };
    },
    methods: {
      onNewDishAdd () {
        this.editedDish = {
          name: '',
          price: '',
          dishType: 0,
          isVegetarian: false
        };
        this.$refs['dish-data-modal'].$refs['new-dish-dialog'].open();
      },
      onDishEdit (dishData) {
        this.modalEditMode = true;
        this.editedDish = JSON.parse(JSON.stringify(dishData));
        this.$refs['dish-data-modal'].$refs['new-dish-dialog'].open();
      },
      onDishDelete (dishId) {
        this.$store.dispatch('deleteDish', {restaurantId: this.$route.params.id, dishId});
      },
      onSort (sortObj) {
        this.$store.commit('sortDishesTable', sortObj);
      }
    },
    created () {
      this.$store.dispatch('getAllDishes', this.$route.params.id);
    },
    computed: {
      ...mapGetters(['dishesList', 'restaurantName'])
    }
  };
</script>