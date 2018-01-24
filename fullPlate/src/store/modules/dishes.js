import _ from 'lodash';
import api from '../../utils/apiHelper';

const state = {
  restaurantName: '',
  dishes: []
};

const mutations = {
  updateDishesList (state, dishesData) {
    state.restaurantName = dishesData.restaurantName;
    state.dishes = dishesData.dishes;
  },
  addNewDish (state, dishData) {
    state.dishes = state.dishes.concat(dishData);
  },
  updateDish (state, dishData) {
    const newDishes = [...state.dishes];
    const index = newDishes.findIndex((dish) => dish.id === dishData.id);
    newDishes[index] = dishData;

    state.dishes = newDishes;
  },
  deleteDish (state, dishId) {
    state.dishes = state.dishes.filter((dish) => dish.id !== dishId);
  },
  sortDishesTable (state, sortObj) {
    state.dishes = _.orderBy(state.dishes, sortObj.name, sortObj.type);
  }
};

const actions = {
  getAllDishes ({commit}, restaurantId) {
    commit('toggleSpinner');

    return api
      .get(`/api/restaurants/${restaurantId}/dishes`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('updateDishesList', response.data);
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
          return false;
        } else {
          commit('toggleConfirm');
          commit('addNewDish', response.data);

          return true;
        }
      });
  },
  updateDish ({commit}, {restaurantId, dishData}) {
    commit('toggleSpinner');
    return api
      .put(`/api/restaurants/${restaurantId}/dishes/${dishData.id}`, dishData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          return false;
        } else {
          commit('toggleConfirm');
          commit('updateDish', response.data);

          return true;
        }
      });
  },
  deleteDish ({commit}, {restaurantId, dishId}) {
    commit('toggleSpinner');
    return api
      .$delete(`/api/restaurants/${restaurantId}/dishes/${dishId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          return false;
        } else {
          commit('toggleConfirm');
          commit('deleteDish', dishId);

          return true;
        }
      });
  }
};

const getters = {
  dishesList: state => state.dishes,
  restaurantName: state => state.restaurantName,
  editableDish: state => JSON.parse(JSON.stringify(state.editableDish))
};

export default {
  state,
  mutations,
  actions,
  getters
};
