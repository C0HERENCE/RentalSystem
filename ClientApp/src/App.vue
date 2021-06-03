<template>
  <div id="app">
    <div id="content">
      <AdminNavBar v-if="$route.fullPath.startsWith('/admin')"></AdminNavBar>
      <MyNavBar v-else @showChat="showChatDrawer=true"></MyNavBar>

      <!--      聊天列表-->
      <b-sidebar v-model="showChatDrawer" title="消息中心" ref="chatSidebar" backdrop-variant="light" backdrop>
        <div style="height: calc(100vh - 100px)">
          <b-list-group style="overflow-y: scroll;height: 100% ">
            <b-list-group-item v-for="i in $store.state.messages" :key="i.id">
              <p class="font-weight-bold">
                <el-button type="text"
                           @click="showChatDialog(i.id)">
                  {{ i.name }}
                </el-button>
              </p>
              <span>{{ i.data[i.data.length - 1].time | fTime }}</span>
            </b-list-group-item>
          </b-list-group>
        </div>
      </b-sidebar>
      <!--      聊天窗口-->
      <ChatWindow ref="chatWindow" @sendMessage="Send"></ChatWindow>


      <b-container>
        <router-view/>
      </b-container>
    </div>
    <div id="footer" class="text-center small">
      <div class="copyright">
        <div class="id">
          <div class="site-record-code">
            <a href="#" style="color: #3a745f;text-decoration: none;">小羊租赁</a>
          </div>
        </div>
        <div class="info">
          <span>Powered By</span>
          <a href="#" class="site" style="color: #3a745f;text-decoration: none;">&nbsp; pongpongpong</a>
          <span class="block">&nbsp; © 2021</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MyNavBar from "./components/MyNavBar";
import AdminNavBar from "./components/AdminNavBar";
import ChatWindow from "@/components/ChatWindow";

export default {
  data() {
    return {
      showChatDrawer: false
    };
  },
  components: {AdminNavBar, MyNavBar, ChatWindow},
  methods: {
    showChatDialog(tid) {
      this.$refs.chatWindow.visible = true;
      this.$refs.chatWindow.userId = tid
    },
    Send(targetId, message) {
      if (!this.$store.state.isLogin) {
        this.$alert("请登录后再操作")
        return;
      }
      if (this.$connection.connection.connectionState !== "Connected") {
        this.$connection.start().then(() => {
          let data = {
            tid: targetId,
            message: message,
            dir: 1,
          }
          this.$store.dispatch("SendMessage", data).then(() => {
            this.$connection.invoke("SendMessage", data.tid.toString(), this.$store.state.currentStudentName, data.message)
          })
        })
      } else {
        let data = {
          tid: targetId,
          message: message,
          dir: 1,
        }
        this.$store.dispatch("SendMessage", data).then(() => {
          this.$connection.invoke("SendMessage", data.tid.toString(), this.$store.state.currentStudentName, data.message)
        })
      }
    }
  },
  watch: {
    "$store.state.count": function () {
      this.$refs.chatWindow.$forceUpdate()
      this.$forceUpdate()
    }
  },
}
</script>

<style>
#app {
  min-height: 100vh;
  position: relative;
}

#footer {
  position: absolute;
  bottom: 0;
  width: 100%;
  height: 2.5rem;
}

#content {
  padding-bottom: 2.5rem;
}
</style>
