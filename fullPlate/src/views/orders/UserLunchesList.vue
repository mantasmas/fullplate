<template>
  <div>
    <md-table-card style="width: 100%;">
      <md-toolbar>
        <h1 class="md-title">Upcoming Lunches</h1>
      </md-toolbar>

      <md-table>
        <md-table-header>
          <md-table-row>
            <md-table-head>Date</md-table-head>
            <md-table-head>Last Registration Time</md-table-head>
            <md-table-head>Paid By Company</md-table-head>
            <md-table-head>Actions</md-table-head>
          </md-table-row>
        </md-table-header>

        <md-table-body>
          <md-table-row v-for="(lunch, idx) in lunchesList" :key="idx" :md-item="lunch">
            <md-table-cell>
              {{formatDate(lunch.date)}}
            </md-table-cell>
            <md-table-cell>
              {{formatDateTime(lunch.lastRegistrationTime)}}
            </md-table-cell>
            <md-table-cell>
              {{lunch.paidByCompany ? 'Yes' : 'No'}}
            </md-table-cell>

            <md-table-cell>
              <md-menu md-size="4">
                <md-button class="md-icon-button" md-menu-trigger>
                  <md-icon>more_vert</md-icon>
                </md-button>

                <md-menu-content>
                  <md-menu-item v-if="!lunch.userOrderId" @selected="onOrderAdd(lunch.id)">
                    <md-icon>add</md-icon>
                    <span>Add order</span>
                  </md-menu-item>

                  <md-menu-item v-if="lunch.userOrderId" @selected="onOrderEdit(lunch.id, lunch.orderId)">
                    <md-icon>edit</md-icon>
                    <span>Save order</span>
                  </md-menu-item>

                  <md-menu-item v-if="lunch.userOrderId" @selected="onOrderRemove(lunch.id, lunch.orderId)">
                    <md-icon>remove</md-icon>
                    <span>Remove order</span>
                  </md-menu-item>
                </md-menu-content>
              </md-menu>
            </md-table-cell>
          </md-table-row>
        </md-table-body>
        <div v-if="!lunchesList.length" class="flexible-container">
          <h2>No upcomming lunches are available!</h2>
        </div>
      </md-table>
    </md-table-card>
  </div>
</template>

<script>
  import myDatepicker from 'vue-datepicker/vue-datepicker-es6.vue';
  import {mapGetters} from 'vuex';
  import { format } from 'date-fns'; // eslint-disable-line

  export default {
    components: {
      'date-picker': myDatepicker
    },
    name: 'user-lunches',
    data () {
      return {};
    },
    methods: {
      formatDate (date) {
        return format(date, 'YYYY-MM-DD');
      },
      formatDateTime (date) {
        return format(date, 'YYYY-MM-DD HH:mm');
      },
      handleDelete (lunchId) {
        this.$store.dispatch('removeLunch', lunchId);
      },
      handleEnable (lunchId) {
        this.$store.dispatch('enableLunch', lunchId);
      },
      onOrderAdd (lunchId) {
        this.$router.push(`/available-lunches/${lunchId}/order`);
      }
    },
    created () {
      this.$store.dispatch('getAvailableLunches');
    },
    computed: {
      ...mapGetters(['lunchesList'])
    }
  };
</script>

<style>
  .flexible-container {
    display: flex;
    justify-content: center;
  }
</style>