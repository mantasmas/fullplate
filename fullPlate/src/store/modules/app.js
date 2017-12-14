const state = {
  spinnerVisible: false,
  notificationVisible: false,
  notificationText: ''
};

const mutations = {
  toggleSpinner (state) {
    state.spinnerVisible = !state.spinnerVisible;
  },
  toggleNotification (state, notificationText = '') {
    state.notificationVisible = !state.notificationVisible;
    state.notificationText = notificationText;
  }
};

const getters = {
  spinnerVisible: state => state.spinnerVisible
};

export default {
  state,
  mutations,
  getters
};
