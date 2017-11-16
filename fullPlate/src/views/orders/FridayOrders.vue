<template>
  <div class="orders">
    <div class="table-information">
      <h1>Friday Lunch Orders</h1>
      <p class="current-date"><strong>Friday,</strong> {{overviewDate}}</p>
    </div>

    <h2><strong>Orders overview</strong></h2>

    <div v-for="overview in overviewInfo">
      <overview-table :dishTypeObj="overview"></overview-table>
    </div>

    <h2><strong>Individual orders:</strong></h2>

    <individual-orders :orders="this.individualOrders"></individual-orders>
  </div>
</template>

<script>
  import moment from 'moment';

  import fixture from '../../fixtures/fridayOrders';
  import OverviewTable from '../../components/orders/OverviewTable.vue';
  import IndividualOrders from '../../components/orders/IndividualOrders.vue';

  export default {
    name: 'FridayOrders',
    data () {
      return {
        overviewDate: moment(new Date(fixture.overviewDate)).format('MMMM DD'),
        overviewInfo: fixture.overviewInfo,
        individualOrders: fixture.individualOrders
      };
    },
    methods: {

    },
    components: {
      IndividualOrders,
      'overview-table': OverviewTable,
      'individual-orders': IndividualOrders
    }
  };
</script>

<style>
  #dishType {
    width: 100px
  }
</style>
