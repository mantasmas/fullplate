const state = {
  app: {
    spinnerVisible: false
  }
};

const mutations = {
  toggleSpinner (state) {
    state.app.spinnerVisible = !state.app.spinnerVisible;
  }
};

const getters = {
  spinnerVisible: state => state.app.spinnerVisible
};

export default {
  state,
  mutations,
  getters
};
