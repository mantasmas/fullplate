import api from '../../utils/apiHelper';

const state = {
  lunches: [],
  availableDishes: [],
  lunch: {}
};

const mutations = {
  updateLunchesList (state, lunchesData) {
    state.lunches = lunchesData;
  },
  removeLunch (state, removedLunchId) {
    const newLunchesList = state.lunches.filter((lunch) => lunch.id !== removedLunchId);

    state.lunches = newLunchesList;
  },
  enableLunch (state, enabledLunchId) {
    const newLunchesList = state.lunches.map((lunch) => {
      if (lunch.id === enabledLunchId) {
        const changedLunch = lunch;
        changedLunch.enabled = true;

        return changedLunch;
      }

      return lunch;
    });

    state.lunches = newLunchesList;
  },
  updateAvailableDishesList (state, availableDishes) {
    console.log(availableDishes);
    state.availableDishes = availableDishes;
  },
  updateLunch (state, lunchData) {
    state.lunch = lunchData;
  }
};

const actions = {
  getAllLunches ({commit}) {
    commit('toggleSpinner');

    return api
      .get(`/api/lunch`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('updateLunchesList', response.data);
        }
      });
  },
  removeLunch ({commit}, lunchId) {
    commit('toggleSpinner');

    return api
      .$delete(`/api/lunch/${lunchId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('removeLunch', lunchId);
        }
      });
  },
  enableLunch ({commit}, lunchId) {
    commit('toggleSpinner');

    return api
      .put(`/api/lunch/${lunchId}/toggle-enable`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('enableLunch', lunchId);
        }
      });
  },
  getAvailableDishes ({commit}) {
    commit('toggleSpinner');

    return api
      .get(`/api/lunch/available-dishes`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('updateAvailableDishesList', response.data);
        }
      });
  },
  saveLunch ({commit}, lunchData) {
    commit('toggleSpinner');

    return api
      .post(`/api/lunch`, lunchData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
          return false;
        } else {
          return true;
        }
      });
  },
  getAvailableLunches ({commit}) {
    commit('toggleSpinner');

    return api
      .get(`/api/lunch/available-lunches`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('updateLunchesList', response.data);
        }
      });
  },
  getOneLunch ({commit}, lunchId) {
    commit('toggleSpinner');

    return api
      .get(`/api/lunch/${lunchId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('updateLunch', response.data);
        }
      });
  },
  saveOrder ({commit}, orderData) {
    commit('toggleSpinner');

    return api
      .post(`/api/orders/lunch/${orderData.lunchId}`, orderData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
          return false;
        } else {
          return true;
        }
      });
  },
  deleteOrder ({commit}, orderData) {
    commit('toggleSpinner');

    return api
      .$delete(`/api/orders/lunch/${orderData.lunchId}/orders/${orderData.orderId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          return false;
        } else {
          return true;
        }
      });
  }
};

const getters = {
  lunchesList: state => state.lunches,
  availableDishesList: state => state.availableDishes,
  oneLunchData: state => state.lunch
};

export default {
  state,
  mutations,
  actions,
  getters
};
