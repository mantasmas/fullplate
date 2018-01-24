import api from '../../utils/apiHelper';

const state = {
  username: '',
  fullName: '',
  email: '',
  role: ''
};

const mutations = {
  updateUserData (state, userData) {
    state.username = userData.username;
    state.fullName = `${userData.firstName} ${userData.lastName}`;
    state.email = userData.email;
    state.role = userData.role;
  },
  clearUserData (state, userData) {
    state.username = '';
    state.fullName = '';
    state.email = '';
    state.role = '';
  }
};

const actions = {
  login ({commit}, userData) {
    commit('toggleSpinner');

    return api
      .post(`/api/user/login`, userData)
      .then((response) => {
        commit('toggleSpinner');

        if (!response.ok) {
          console.log(response.data);
          return false;
        } else {
          commit('updateUserData', response.data);
          return true;
        }
      });
  },
  logout ({commit}) {
    return api
      .get(`/api/user/logout`)
      .then((response) => {
        if (!response.ok) {
          console.log(response.data);
          return false;
        } else {
          commit('clearUserData');
          return true;
        }
      });
  }
};

const getters = {
  fullName: state => state.fullName,
  role: state => state.role
};

export default {
  state,
  mutations,
  actions,
  getters
};
