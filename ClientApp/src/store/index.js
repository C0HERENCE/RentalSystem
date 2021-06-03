import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)
export default new Vuex.Store({
    state: {
        isLogin: sessionStorage.getItem("isLogin") === 'true',
        currentStudentName: sessionStorage.getItem("currentStudentName"),
        messages: {}, // 8 所有人，所有消息
        count: 0,
        isAdmin: sessionStorage.getItem("isAdmin") === 'true',
    },
    mutations: {
        SetLogin(state, b) {
            state.isLogin = b;
            sessionStorage.setItem("isLogin", b)
        },
        SetIsAdmin(state, b) {
            state.isAdmin = b;
            sessionStorage.setItem("isAdmin", b)
        },

        SetStudentName(state, xxx) {
            state.currentStudentName = xxx;
            sessionStorage.setItem("currentStudentName", xxx)
        },

        AddUserMessage(state, data) { // 添加一条消息
            if (!state.messages[data.tid])
                state.messages[data.tid] = {
                    id: data.tid,
                    name: data.tname,
                    data: []
                }
            state.messages[data.tid].data.push({
                tid: data.tid,
                message: data.message,
                dir: data.dir,
                time: new Date()
            })
            state.count++;
        }
    },
    actions: {
        SendMessage(context, data) {
            context.commit("AddUserMessage", data)
        }
    }
})
