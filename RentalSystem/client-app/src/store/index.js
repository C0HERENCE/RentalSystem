import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    user: window.sessionStorage.getItem('user'),
    token: window.sessionStorage.getItem('token'),

    autoLogin: window.localStorage.getItem('autoLogin'),
    local_user: window.localStorage.getItem('user'),
    local_token: window.localStorage.getItem('token'),
  },
  mutations: {
    SET_TOKEN: (state, data) => {
      window.sessionStorage.setItem('token', data);
    },
    SET_USER: (state, data) => {
      window.sessionStorage.setItem('user', data)
    },
    LOG_OUT: () => {
      window.sessionStorage.removeItem('user');
      window.sessionStorage.removeItem('token');
    }
  },
  getters: {
    IS_LOGIN: (state) => {
      return state.token !== null;
    }
  },
  actions: {

  }
  ,
  modules: {

  }
})
