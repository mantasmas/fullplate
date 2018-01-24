import api from '../../utils/apiHelper';

const state = {
  usersList: []
};

const mutations = {
  updateUsersData (state, usersData) {
    state.usersList = usersData;
  },
  removeUser (state, userId) {
    const newUsersList = state.users.filter((user) => user.id !== userId);

    state.usersList = newUsersList;
  }
};

const actions = {
  getAllUsers ({commit}) {
    commit('toggleSpinner');

    return api
      .get(`/api/users/`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('updateUsersData', response.data);
        }
      });
  },
  disableUser ({commit}, userId) {
    commit('toggleSpinner');

    return api
      .$delete(`/api/users/${userId}`)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
        } else {
          commit('removeUser', userId);
        }
      });
  },
  createUser ({commit}, userData) {
    commit('toggleSpinner');

    return api
      .post(`/api/user/register`, userData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
          return false;
        } else {
          return true;
        }
      });
  }
};

const getters = {
  usersList: state => state.usersList
};

export default {
  state,
  mutations,
  actions,
  getters
};
