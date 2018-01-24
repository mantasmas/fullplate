<template>
  <div>
    <h3>Welcome back, {{fullName}}!</h3>
  </div>
</template>

<script>
  import {mapGetters} from 'vuex';

  export default {
    name: 'Index',
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
