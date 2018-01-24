import Vue from 'vue';
import Vuex from 'vuex';
import app from './modules/app';
import restaurants from './modules/restaurants';
import dishes from './modules/dishes';
import user from './modules/user';
import lunches from './modules/lunches';
import users from './modules/users';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    app,
    restaurants,
    dishes,
    user,
    lunches,
    users
  },
  strict: true
});
