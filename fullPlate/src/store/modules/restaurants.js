/* eslint no-shadow: ["error", { "allow": ["state"] }] */
import _ from 'lodash';
import api from '../../utils/apiHelper';

const state = {
  restaurantsLoaded: false,
  showDeleteRestaurantModal: false,
  newRestaurant: {
    title: ''
  },
  restaurantsList: [],
  removableRestaurantId: null,
  editedRestaurant: {}
};

const mutations = {
  toggleConfirm (state, restaurantId) {
    state.showDeleteRestaurantModal = !state.showDeleteRestaurantModal;
    state.removableRestaurantId = restaurantId;
  },
  updateRestaurantsList (state, newRestaurantList) {
    state.restaurantsList = newRestaurantList;
    state.restaurantsLoaded = true;
  },
  addNewRestaurant (state, newRestaurant) {
    state.restaurantsList = [...state.restaurantsList, newRestaurant];
  },
  removeRestaurant (state, removedRestaurantId) {
    state.restaurantsList = _.filter(state.restaurantsList,
      (restaurant) => restaurant.id !== removedRestaurantId);
  },
  getOneRestaurant (state, restaurantData) {
    state.editedRestaurant = restaurantData;
  },
  addNewDish (state, dishData) {
    state.editedRestaurant.dishes = state.editedRestaurant.dishes.concat(dishData);
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
  },
  getOneRestaurant ({commit}, restaurantId) {
    commit('toggleSpinner');
    return api
      .get(`/api/restaurants/${restaurantId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh when getting');

          return false;
        } else {
          commit('toggleConfirm');
          commit('getOneRestaurant', response.data);

          return true;
        }
      });
  },
  saveRestaurantChanges ({commit}, restaurantId) {
    commit('toggleSpinner');
    return api
      .get(`/api/restaurants/${restaurantId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh when getting');

          return false;
        } else {
          commit('toggleConfirm');
          commit('getOneRestaurant', response.data);

          return true;
        }
      });
  },
  addNewDish ({commit}, {restaurantId, dishData}) {
    commit('toggleSpinner');
    return api
      .post(`/api/restaurants/${restaurantId}/dishes`, dishData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh when adding new dish');

          return false;
        } else {
          commit('toggleConfirm');
          commit('addNewDish', response.data);

          return true;
        }
      });
  }
};

const getters = {
  isModalVisible: state => state.showDeleteRestaurantModal,
  restaurantsList: state => state.restaurantsList,
  restaurantsLoaded: state => state.restaurantsLoaded,
  restaurantDeleteId: state => state.removableRestaurantId,
  editedRestaurant: state => state.editedRestaurant
};

export default {
  state,
  mutations,
  getters,
  actions
};
