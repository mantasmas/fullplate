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
  removableRestaurantId: null
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
  updateRestaurant (state, restaurantData) {
    const newList = [...state.restaurantsList];
    const index = newList.findIndex(restaurant => restaurant.id === restaurantData.id);
    newList[index] = restaurantData;

    state.restaurantsList = newList;
  },
  removeRestaurant (state, removedRestaurantId) {
    state.restaurantsList = _.filter(state.restaurantsList,
      (restaurant) => restaurant.id !== removedRestaurantId);
  },
  sortRestaurantsTable (state, sortObj) {
    state.restaurantsList = _.orderBy(state.restaurantsList, sortObj.name, sortObj.type);
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
          console.log(response.data);
          commit('updateRestaurantsList', response.data);

          return true;
        }
      });
  },
  addNewRestaurant ({commit}, restaurantData) {
    console.log(restaurantData);
    commit('toggleSpinner');
    return api
      .post('/api/restaurants', restaurantData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh when adding');

          return false;
        } else {
          console.log(response.data);
          commit('addNewRestaurant', response.data);

          return true;
        }
      });
  },
  updateRestaurant ({commit}, restaurantData) {
    commit('toggleSpinner');
    return api
      .put(`/api/restaurants/${restaurantData.id}`, restaurantData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log('blueh when updating');

          return false;
        } else {
          commit('updateRestaurant', response.data);

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
  }
};

const getters = {
  isModalVisible: state => state.showDeleteRestaurantModal,
  restaurantsList: state => JSON.parse(JSON.stringify(state.restaurantsList)),
  restaurantsLoaded: state => state.restaurantsLoaded,
  restaurantDeleteId: state => state.removableRestaurantId
};

export default {
  state,
  mutations,
  getters,
  actions
};
