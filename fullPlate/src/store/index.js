import Vue from 'vue';
import Vuex from 'vuex';
import app from './modules/app';
import restaurants from './modules/restaurants';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    app,
    restaurants
  },
  strict: true
});
