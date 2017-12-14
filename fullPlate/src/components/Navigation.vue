<template>
  <md-tabs md-centered @change="handleTabChange">
    <md-tab v-for="(route, idx) in routes" :key="idx" :md-label="route.title" :md-active="isCurrentPath(route.link)"></md-tab>
  </md-tabs>
</template>

<script>
  export default {
    name: 'Navigation',
    data () {
      return {
        pageLoaded: false,
        routes: [
          {title: 'Late Requests', link: '/late-requests'},
          {title: 'Friday Orders', link: '/friday-orders'},
          {title: 'Totals', link: '/totals'},
          {title: 'Restaurants', link: '/restaurants'}
        ]
      };
    },
    methods: {
      handleTabChange (tabIndex) {
        if (this.pageLoaded) {
          this.$router.push(this.routes[tabIndex].link);
        }
      },
      isCurrentPath (pathRegexp) {
        const regexp = new RegExp(`^${pathRegexp}.*`, 'i');
        return regexp.test(this.$route.path);
      }
    },
    mounted () {
      this.pageLoaded = true;
    }
  };
</script>
