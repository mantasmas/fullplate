<template>
  <div class="page">
    <page-header></page-header>
    <navigation></navigation>

    <div class="page-frame">
      <router-view></router-view>
    </div>

    <div class="loader" v-if="spinnerVisible"></div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  import PageHeader from '../components/PageHeader.vue';
  import Navigation from '../components/Navigation.vue';

  export default {
    name: 'page',
    data () {
      return {};
    },
    created () {
//      const a =  document.cookie.match(/^(.*;)?\s*MyCookie\s*=\s*[^;]+(.*)?$/);
    },
    components: {
      'page-header': PageHeader,
      'navigation': Navigation
    },
    computed: {
      ...mapGetters(['spinnerVisible'])
    }
  };
</script>

<style>

</style>
