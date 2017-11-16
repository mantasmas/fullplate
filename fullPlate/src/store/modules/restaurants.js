/* eslint no-shadow: ["error", { "allow": ["state"] }] */
import _ from 'lodash';
import api from '../../utils/apiHelper';

const state = {
  restaurants: {
    loaded: false,
    showDeleteModal: false,
    newRestaurant: {
      title: ''
    },
    restaurantsList: [],
    removableRestaurantId: null
  }
};

const mutations = {
  toggleConfirm (state, restaurantId) {
    state.restaurants.showDeleteModal = !state.restaurants.showDeleteModal;
    state.restaurants.removableRestaurantId = restaurantId;
  },
  updateRestaurantsList (state, newRestaurantList) {
    state.restaurants.restaurantsList = newRestaurantList;
    state.restaurants.loaded = true;
  },
  addNewRestaurant (state, newRestaurant) {
    state.restaurants.restaurantsList = [...state.restaurants.restaurantsList, newRestaurant];
  },
  removeRestaurant (state, removedRestaurantId) {
    state.restaurants.restaurantsList = _.filter(state.restaurants.restaurantsList,
      (restaurant) => restaurant.id !== removedRestaurantId);
  }
};

const actions = {
  getAllRestaurants ({commit}) {
    commit('toggleSpinner');

    return api
      .get('/api/restaurants')
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh');

          return false;
        } else {
          commit('updateRestaurantsList', response.data);

          return true;
        }
      });
  },
  addNewRestaurant ({commit}, restaurantName) {
    commit('toggleSpinner');
    return api
      .post('/api/restaurants', {Title: restaurantName})
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh when adding');

          return false;
        } else {
          commit('addNewRestaurant', response.data);

          return true;
        }
      });
  },
  removeRestaurant ({commit}, restaurantId) {
    commit('toggleSpinner');
    return api
      .$delete(`/api/restaurants/${restaurantId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh when removing');

          return false;
        } else {
          commit('toggleConfirm');
          commit('removeRestaurant', restaurantId);

          return true;
        }
      });
  }
};

const getters = {
  isModalVisible: state => state.restaurants.showDeleteModal,
  restaurantsList: state => state.restaurants.restaurantsList,
  restaurantsLoaded: state => state.restaurants.loaded,
  restaurantDeleteId: state => state.restaurants.removableRestaurantId
};

export default {
  state,
  mutations,
  getters,
  actions
};
