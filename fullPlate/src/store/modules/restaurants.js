/* eslint no-shadow: ["error", { "allow": ["state"] }] */
const state = {
  visibleModals: {
    confirm: false,
    add: false
  }
}

const mutations = {
  showConfirm (state, payload) {
    state.visibleModals.confirm = true
  },
  hideConfirm (state) {
    state.visibleModals.confirm = false
  }
}

const getters = {
  isConfirmVisible: state => state.visibleModals.confirm
}

export default {
  state,
  mutations,
  getters
}
