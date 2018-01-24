<template>
  <header class="page-header">
    <div class="page-frame">
      <p class="user-greeting">
        <span>Hello</span> <span class="user-name">{{fullName}}</span><span>, <a @click="handleLogout()">logout</a></span>
      </p>

      <router-link to="/" class="logo"><img src="../assets/fullplage-logo-white.svg" alt="logo"></router-link>
    </div>
  </header>
</template>

<script>
  import {mapGetters} from 'vuex';

  export default {
    name: 'page',
    data () {
      return {};
    },
    methods: {
      handleLogout () {
        this.$store.dispatch('logout').then(() => {
          this.$router.push('/login');
        });
      }
    },
    computed: {
      ...mapGetters(['fullName'])
    }
  };
</script>

<style>

</style>
