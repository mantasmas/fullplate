<template>
  <form class="login-form" v-on:submit.prevent="handleSubmit">
    <a class="logo"><img src="../assets/fullplage-logo-black.svg" alt="logo"></a>
    <div class="field-holder js-prototype-login-helper">
      <input v-model="usrName" @keyup.enter="usrName" type="text" class="login-username" id="username"
             placeholder="user name" required="">
    </div>
    <div class="field-holder js-prototype-login-helper">
      <input v-model="pass" @keyup.enter="pass" type="password" class="login-password" id="password"
             placeholder="password" required="">
    </div>
    <button type="submit" class="btn-big btn-login js-prototype-login">Login</button>
  </form>
</template>

<script>
  export default {
    name: 'hello',
    data () {
      return {
        usrName: '',
        pass: ''
      };
    },
    methods: {
      handleSubmit () {
        console.log(this.usrName);
        console.log(this.pass);
      }
    }
  };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>

</style>
