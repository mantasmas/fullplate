<template>
  <header class="page-header">
    <div class="page-frame">
      <p class="user-greeting">
        <span>Hello</span> <span class="user-name">{{name}}</span><span>, <router-link to="/login">logout</router-link></span>
      </p>

      <router-link to="/" class="logo"><img src="../assets/fullplage-logo-white.svg" alt="logo"></router-link>
    </div>
  </header>
</template>

<script>
  export default {
    name: 'page',
    data () {
      return {
        name: 'John'
      }
    }
  }
</script>

<style>

</style>
