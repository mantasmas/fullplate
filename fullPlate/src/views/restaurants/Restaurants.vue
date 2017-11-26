<template>
  <div>
    <restaurants-table></restaurants-table>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  import RestaurantsTable from './RestautantsTable';

  export default {
    name: 'Restaurants',
    data () {
      return {};
    },
    components: {
      'restaurants-table': RestaurantsTable
    },
    computed: {
      ...mapGetters(['isModalVisible'])
    }
  };
</script>
