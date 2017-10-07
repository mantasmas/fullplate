<template>
  <div>
    <restaurants-table :restaurantsList="restaurants"></restaurants-table>
    <confirm-modal v-if="isConfirmVisible"
                   :title="'Do you want to remove this category?'"></confirm-modal>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'

  import RestaurantsTable from '../../components/restaurants/RestautantsTable'
  import ConfirmModal from '../../components/ConfirmModal'
  import restaurantsFixture from '../../fixtures/restaurants'

  export default {
    name: 'Restaurants',
    data () {
      return {
        restaurants: restaurantsFixture
      }
    },
    components: {
      RestaurantsTable,
      'restaurants-table': RestaurantsTable,
      'confirm-modal': ConfirmModal
    },
    computed: {
      ...mapGetters(['isConfirmVisible'])
    }
  }
</script>
