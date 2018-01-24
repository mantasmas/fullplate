<template>
  <div>
    <h2>Register new user</h2>

    <md-input-container class="login-wide">
      <label>Username</label>
      <md-input v-model="username"></md-input>
    </md-input-container>

    <md-input-container class="login-wide">
      <label>Password</label>
      <md-input type="password" v-model="password"></md-input>
    </md-input-container>

    <md-input-container class="login-wide">
      <label>Email</label>
      <md-input v-model="email"></md-input>
    </md-input-container>

    <md-input-container class="login-wide">
      <label>First Name</label>
      <md-input v-model="firstName"></md-input>
    </md-input-container>

    <md-input-container class="login-wide">
      <label>Last Name</label>
      <md-input v-model="lastName"></md-input>
    </md-input-container>

    <md-button class="md-raised wide-button" @click="onSubmit">Register Employee</md-button>
    <md-button class="md-raised wide-button" @click="onCancel">Cancel Registration</md-button>
  </div>

</template>

<script>
  import {mapGetters} from 'vuex';

  export default {
    name: 'friday-lunches',
    data () {
      return {
        username: '',
        password: '',
        email: '',
        firstName: '',
        lastName: ''
      };
    },
    methods: {
      onLunchAdd () {
        this.$router.push('/friday-lunches/new-lunch');
      },
      handleDisable (userId) {
        this.$store.dispatch('disableUser', userId);
      },
      onSubmit () {
        const requestObject = {
          username: this.username,
          password: this.password,
          email: this.email,
          firstName: this.firstName,
          lastName: this.lastName
        };

        this.$store.dispatch('createUser', requestObject).then((success) => {
          if (success) {
            this.$router.push('/users');
          }
        });
      },
      onCancel () {
        this.$router.push('/users');
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
  .wide-button {
    width: 100%
  }
</style>