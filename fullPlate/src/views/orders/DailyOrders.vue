<template>

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
      }
    }
  };
</script>

<style>

</style>
