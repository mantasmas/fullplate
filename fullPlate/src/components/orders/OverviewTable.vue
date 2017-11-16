<template>
  <div>
    <h3>{{ type }}</h3>

    <table class="table-primary orders-table">
      <thead>
      <tr class="table-headings">
        <th><i class="icon-filter-arrow-table" @click="sortColumn('title')"></i>Type</th>
        <th style="width: 476px;"><i class="icon-filter-arrow-table" @click="sortColumn('count')"></i>Count</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="dish in dishes">
        <td>{{dish.title}}</td>
        <td>{{dish.count}}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
  import _ from 'lodash';

  export default {
    name: 'OverviewTable',
    props: ['dishTypeObj'],
    data () {
      return {
        type: this.dishTypeObj.type,
        dishes: this.dishTypeObj.dishes,
        sortDescending: true
      };
    },
    methods: {
      sortColumn (sortProperty) {
        const sortOrder = this.sortDescending ? 'desc' : 'asc';
        this.dishes = _.orderBy(this.dishes, sortProperty, sortOrder);
        this.sortDescending = !this.sortDescending;
      }
    }
  };
</script>

<style>
  #dishType {
    width: 100px
  }
</style>
