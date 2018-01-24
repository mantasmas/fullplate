<template>
  <div id="login-card-wrapper">
    <md-card>
      <md-card-media>
        <div class="logo" id="login-image-wrapper"><img src="../assets/fullplage-logo-black.svg" alt="logo"></div>
      </md-card-media>

      <md-card-header>
        <div class="md-title">Login</div>
        <div class="md-subhead">Login to Full Plate with your credentials</div>
      </md-card-header>

      <md-card-content>
        <form v-on:submit.prevent="handleSubmit"  id="login-card-content">
          <md-input-container class="login-wide">
            <label>Username</label>
            <md-input v-model="username"></md-input>
          </md-input-container>

          <md-input-container class="login-wide">
            <label>Password</label>
            <md-input type="password" v-model="password"></md-input>
          </md-input-container>

          <md-button type="submit" class="md-raised md-primary">Login</md-button>
        </form>
      </md-card-content>
    </md-card>

    <md-snackbar md-position="bottom center" ref="snackbar" md-duration="4000">
      <span>Login not unsuccessful. Check your username and/or password</span>
      <md-button class="md-accent" @click="$refs.snackbar.close()">Close</md-button>
    </md-snackbar>
  </div>
</template>

<script>
  export default {
    name: 'login',
    data () {
      return {
        username: '',
        password: ''
      };
    },
    methods: {
      handleSubmit () {
        this.$store.dispatch('login', {username: this.username, password: this.password}).then((loginSuccess) => {
          if (loginSuccess) {
            console.log('hello');
            this.$router.push({path: '/'});
          } else {
            this.$refs.snackbar.open();
          }
        });
      }
    }
  };
</script>

<style>
  #login-card-wrapper{
    display: flex;
    justify-content: center;
    margin-top: 200px;
  }

  #login-image-wrapper{
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }

  #login-card-content{
    display: flex;
    justify-content: center;
    margin-top: 20px;
    flex-direction: column;
  }

  .login-wide{
    width: 100%;
  }
</style>
