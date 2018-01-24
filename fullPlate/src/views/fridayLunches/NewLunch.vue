<template>
  <div class="new-lunch">
    <h2>
      New Lunch
    </h2>

    <md-input-container id="container-lunch">
      <date-picker class="date" :date="startTime" :option="timeoption" :limit="limit"></date-picker>
    </md-input-container>

    <md-input-container id="container-lunch">
      <date-picker class="date" :date="startTime2" :option="timeoption2" :limit="limit"></date-picker>
    </md-input-container>

    <div>
      <md-checkbox v-model="paidByCompany" class="md-primary">Paid by company</md-checkbox>
    </div>

    <div>
      <md-checkbox v-model="enabled" class="md-primary">Enabled</md-checkbox>
    </div>

    <md-table-card style="width: 100%;">
      <md-toolbar>
        <h1 class="md-title">Available dishes</h1>
      </md-toolbar>

      <md-table md-sort="dishType" md-sort-type="asc" @select="onSelect">
        <md-table-header>
          <md-table-row>
            <md-table-head md-sort-by="name">Dish Name</md-table-head>
            <md-table-head>Dish Price</md-table-head>
            <md-table-head md-sort-by="dishType">Dish Type</md-table-head>
            <md-table-head>Is Vegetarian</md-table-head>
            <md-table-head>Supplier Restaurant</md-table-head>
          </md-table-row>
        </md-table-header>

        <md-table-body>
          <md-table-row v-for="(dish, idx) in availableDishesList" :md-item="dish" :key="dish.id" md-selection>
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
              {{dish.restaurantName}}
            </md-table-cell>
          </md-table-row>
        </md-table-body>

      </md-table>
    </md-table-card>

    <md-button class="md-raised wide-button" @click="onSubmit">Create Lunch</md-button>
    <md-button class="md-raised wide-button" @click="onCancel">Cancel Creation</md-button>
  </div>
</template>

<script>
  import myDatepicker from 'vue-datepicker/vue-datepicker-es6.vue';
  import {mapGetters} from 'vuex';
  import DishTypes from '../../utils/enums/dishTypes';
  import moment from 'moment';

  export default {
    components: {
      'date-picker': myDatepicker
    },
    name: 'friday-lunches',
    data () {
      return {
        dishTypesEnum: DishTypes,
        enabled: false,
        paidByCompany: false,
        startTime: {
          time: ''
        },
        startTime2: {
          time: ''
        },
        timeoption: {
          type: 'day',
          week: ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'],
          month: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
          format: 'YYYY-MM-DD',
          placeholder: 'Enter lunch date',
          limit: [{
            type: 'fromto',
            from: moment(new Date()).format('YYYY-MM-DD')
          }]
        },
        timeoption2: {
          type: 'min',
          week: ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'],
          month: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
          format: 'YYYY-MM-DD HH:mm',
          placeholder: 'Enter last registration date'
        },
        limit: [{
          type: 'fromto',
          from: moment(new Date()).format('YYYY-MM-DD')
        }],
        selectedDishes: []
      };
    },
    methods: {
      onLunchAdd () {
        this.$router.push('/new-lunch');
      },
      formatDate (date) {
        return moment().format(date, 'YYYY-MM-DD HH:mm');
      },
      handleDelete (lunchId) {
        this.$store.dispatch('removeLunch', lunchId);
      },
      onSelect (e) {
        this.selectedDishes = e;
      },
      onSubmit () {
        const requestObject = {
          lunchDate: moment(this.startTime.time, 'YYYY-MM-DD HH:mm').toISOString(),
          lastRegisterDate: moment(this.startTime2.time, 'YYYY-MM-DD HH:mm').toISOString(),
          paidByCompany: this.paidByCompany,
          enabled: this.enabled,
          dishIds: this.selectedDishes.map((dish) => dish.id)
        };

        if (requestObject.lunchDate && requestObject.lastRegisterDate && requestObject.dishIds.length) {
          this.$store.dispatch('saveLunch', requestObject).then((success) => {
            if (success) {
              this.$router.push('/friday-lunches');
            }
          });
        }
      },
      onCancel () {
        this.$router.push('/friday-lunches');
      }
    },
    created () {
      this.$store.dispatch('getAvailableDishes');
    },
    computed: {
      ...mapGetters(['availableDishesList'])
    }
  };
</script>

<style>
  .date{
    width: 100%;
    font-size: 16px;
  }

  .md-input-container input, .md-input-container textarea {
    font-size: 16px;
  }

  .wide-button {
    width: 100%
  }
</style>