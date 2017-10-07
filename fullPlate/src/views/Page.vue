<template>
  <div class="page">
    <page-header></page-header>
    <navigation></navigation>
    <div class="page-frame">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
  import PageHeader from '../components/PageHeader.vue'
  import Navigation from '../components/Navigation.vue'

  export default {
    name: 'page',
    components: {
      'page-header': PageHeader,
      'navigation': Navigation
    }
  }
</script>

<style>

</style>
