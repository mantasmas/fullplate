<template>
  <div>
    <md-table-card style="width: 100%;">
      <md-toolbar>
        <h1 class="md-title">Lunches</h1>
        <md-button id="btn-new-dish" @click="onLunchAdd">New Friday Lunch</md-button>
      </md-toolbar>

      <md-table>
        <md-table-header>
          <md-table-row>
            <md-table-head>Date</md-table-head>
            <md-table-head>Last Registration Time</md-table-head>
            <md-table-head>Paid By Company</md-table-head>
            <md-table-head>Dishes Count</md-table-head>
            <md-table-head>Is Enabled</md-table-head>
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
              {{lunch.dishes ? lunch.dishes.length : '0'}}
            </md-table-cell>
            <md-table-cell>
            {{lunch.enabled ? 'Yes' : 'No'}}
            </md-table-cell>

            <md-table-cell>
              <md-menu md-size="4">
                <md-button class="md-icon-button" md-menu-trigger>
                  <md-icon>more_vert</md-icon>
                </md-button>

                <md-menu-content>
                  <md-menu-item v-if="!lunch.enabled" @selected="handleEnable(lunch.id)">
                    <md-icon>add</md-icon>
                    <span>Enable lunch</span>
                  </md-menu-item>

                  <md-menu-item @selected="handleDelete(lunch.id)">
                    <md-icon>remove</md-icon>
                    <span>Remove lunch</span>
                  </md-menu-item>
                </md-menu-content>
              </md-menu>
            </md-table-cell>
          </md-table-row>
        </md-table-body>
        <div v-if="!lunchesList.length" class="flexible-container">
          <h2>No upcomming lunches are registered in the system!</h2>
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
    name: 'friday-lunches',
    data () {
      return {};
    },
    methods: {
      onLunchAdd () {
        this.$router.push('/friday-lunches/new-lunch');
      },
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
      }
    },
    created () {
      this.$store.dispatch('getAllLunches');
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