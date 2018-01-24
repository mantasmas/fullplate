<template>
  <div>
    <md-table-card style="width: 100%;">
      <md-toolbar>
        <h1 class="md-title">Users</h1>
        <md-button id="btn-new-dish" @click="onUserAdd">Add new user</md-button>
      </md-toolbar>

      <md-table md-sort="dishType" md-sort-type="asc">
        <md-table-header>
          <md-table-row>
            <md-table-head>Username</md-table-head>
            <md-table-head>Email</md-table-head>
            <md-table-head>First Name</md-table-head>
            <md-table-head>Last Name</md-table-head>
          </md-table-row>
        </md-table-header>

        <md-table-body>
          <md-table-row v-for="(user, idx) in usersList" :key="idx" :md-item="user">
            <md-table-cell>
              {{user.username}}
            </md-table-cell>
            <md-table-cell>
              {{user.email}}
            </md-table-cell>
            <md-table-cell>
              {{user.firstName}}
            </md-table-cell>
            <md-table-cell>
              {{user.lastName}}
            </md-table-cell>

            <md-table-cell>
              <md-menu md-size="4">
                <md-button class="md-icon-button" md-menu-trigger>
                  <md-icon>more_vert</md-icon>
                </md-button>

                <md-menu-content>
                  <md-menu-item @selected="handleDisable(user.id)">
                    <md-icon>add</md-icon>
                    <span>Disable User</span>
                  </md-menu-item>
                </md-menu-content>
              </md-menu>
            </md-table-cell>
          </md-table-row>
        </md-table-body>
      </md-table>
      <div v-if="!usersList.length" class="flexible-container">
        <h2>No users are registered!</h2>
      </div>
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
      onUserAdd () {
        this.$router.push('/register-user');
      },
      handleDisable (userId) {
        this.$store.dispatch('disableUser', userId);
      }
    },
    created () {
      this.$store.dispatch('getAllUsers');
    },
    computed: {
      ...mapGetters(['usersList'])
    }
  };
</script>

<style>
  .flexible-container {
    display: flex;
    justify-content: center;
  }
</style>