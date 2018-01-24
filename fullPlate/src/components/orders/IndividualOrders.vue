<template>
  <table class="table-primary orders-table">
    <thead>
    <tr class="table-headings">
      <th><i class="icon-filter-arrow-table" @click="sortColumn('username')"></i>User Name</th>
      <th style="width: 476px;">Courses</th>
    </tr>
    </thead>
    <tbody>
    <tr v-for="order in individualOrders">
      <td>{{order.username}}</td>
      <td class="data-item">
        <span class="first-item">Soup: {{order.courses.soup}}</span>
        <span class="second-item">Main dish: {{order.courses.mainCourse}}</span>
      </td>
    </tr>
    </tbody>
  </table>
</template>

<script>
  import _ from 'lodash';

  export default{
    name: 'IndividualOrders',
    props: ['orders'],
    data () {
      return {
        individualOrders: this.orders
      };
    },
    methods: {
      sortColumn (sortProperty) {
        const sortOrder = this.sortDescending ? 'desc' : 'asc';
        this.individualOrders = _.orderBy(this.individualOrders, sortProperty, sortOrder);
        this.sortDescending = !this.sortDescending;
      }
    }
  };
</script>
