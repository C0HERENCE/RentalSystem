<template>
  <div>
    <b-navbar toggleable="lg">
      <b-container>
        <b-navbar-brand to="/" style="font-size: 30px;color: #006699">
          <img src="../assets/小羊租赁.png" alt="" style="width: 30%">
        </b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav>
          <b-navbar-nav>
          </b-navbar-nav>
          <b-navbar-nav class="ml-auto">
              <router-link to="/Guide">
                <el-button class="mx-3" type="info">
                  <i class="el-icon-s-help">租赁指南</i>
                </el-button>
              </router-link>
            <el-col :span="10">
              <router-link to="/Publish">
                <el-button type="info" icon="el-icon-thumb" class="mx-3">点击此处开始发布</el-button>
              </router-link>
            </el-col>
            <b-nav-item-dropdown right v-if="$store.state.isLogin === true" class="mx-3">
              <template #button-content>
                <em style="font-weight: bold;font-size: 20px"><i class="el-icon-user-solid"></i>{{ $store.state.currentStudentName }}</em>
              </template>
              <b-dropdown-item to="/profile">个人中心</b-dropdown-item>
              <b-dropdown-item @click="$emit('showChat')">消息中心</b-dropdown-item>
              <b-dropdown-item v-if="$store.state.isAdmin === true" @click="$router.push('/admin')">管理面板</b-dropdown-item>
              <b-dropdown-item href="javascript:;" @click.native="logout">退出登录</b-dropdown-item>
            </b-nav-item-dropdown>
            <b-button v-if="$store.state.isLogin === false" to="/login" size="sm" class="mx-3" style="width: 80px;background-color: #167C80;line-height: 28px;">登录</b-button>
            <b-button v-if="$store.state.isLogin === false" to="/register" size="sm" class="mx-3" style="width: 80px;background-color: #167C80;line-height: 28px;">注册</b-button>
          </b-navbar-nav>
        </b-collapse>
      </b-container>
    </b-navbar>
    <el-divider class="my-auto"></el-divider>
  </div>
</template>

<script>
export default {
  name: "MyNavBar",
  methods: {
    logout() {
      this.$store.commit('SetLogin', false)
      this.$store.commit('SetStudentName', '')
      this.$connection.stop() // 7，退出登录时，关闭接收聊天消息。
      if (this.$route.name !== "Home")
        this.$router.push("/") // 退出登录后，跳转到首页
      this.$cookies.remove(".AspNetCore.Session")
    }
  },
}
</script>

<style scoped>

</style>