<template>
  <div>
    <restaurants-table></restaurants-table>
    <confirm-modal v-if="isModalVisible"
                   :title="'Do you want to remove this restaurant?'"></confirm-modal>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';

  import RestaurantsTable from './RestautantsTable';
  import ConfirmModal from '../../components/ConfirmModal';

  export default {
    name: 'Restaurants',
    data () {
      return {};
    },
    components: {
      RestaurantsTable,
      'restaurants-table': RestaurantsTable,
      'confirm-modal': ConfirmModal
    },
    computed: {
      ...mapGetters(['isModalVisible'])
    }
  };
</script>
