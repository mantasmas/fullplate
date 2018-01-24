<template>
  <div>
    <md-tabs v-if="role === roles.ADMIN" md-centered @change="handleTabChange">
      <md-tab v-for="(route, idx) in routes.adminTabs" :key="idx" :md-label="route.title" :md-active="isCurrentPath(route.link)"></md-tab>
    </md-tabs>

    <md-tabs v-if="role === roles.EMPLOYEE" md-centered @change="handleTabChange">
      <md-tab v-for="(route, idx) in routes.employeeTabs" :key="idx" :md-label="route.title" :md-active="isCurrentPath(route.link)"></md-tab>
    </md-tabs>
  </div>
</template>

<script>
  import {mapGetters} from 'vuex';
  import userRoles from '../utils/enums/userRoles';

  export default {
    name: 'Navigation',
    data () {
      return {
        pageLoaded: false,
        roles: userRoles,
        routes: {
          adminTabs: [
            {title: 'Home', link: '/'},
            {title: 'Friday Lunches', link: '/friday-lunches'},
            {title: 'Restaurants', link: '/restaurants'},
            {title: 'Users', link: '/users'}
          ],
          employeeTabs: [
            {title: 'Home', link: '/'},
            {title: 'Available Lunches', link: '/available-lunches'}
//            {title: 'My orders', link: '/old-orders'}
          ]
        }
      };
    },
    methods: {
      handleTabChange (tabIndex) {
        if (this.pageLoaded) {
          if (this.role === userRoles.ADMIN) {
            this.$router.push(this.routes.adminTabs[tabIndex].link);
          } else {
            this.$router.push(this.routes.employeeTabs[tabIndex].link);
          }
        }
      },
      isCurrentPath (pathRegexp) {
        const regexp = new RegExp(`^${pathRegexp}.*`, 'i');
        return regexp.test(this.$route.path);
      }
    },
    mounted () {
      this.pageLoaded = true;
    },
    computed: {
      ...mapGetters(['role'])
    }
  };
</script>
