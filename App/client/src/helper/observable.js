import Vue from 'vue';

const state = Vue.observable({ // this is the magic
    isRefreshing: false,
});

export const getters = {
    isRefreshing: () => { return state.isRefreshing},
};

export const mutations =  {
    setIsRefreshing: (val) => state.isRefreshing = val,
}
