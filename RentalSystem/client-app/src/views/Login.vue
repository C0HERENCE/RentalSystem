<template>
  <div class="container py-3">
    <b-row class="my-5">
      <!--    左侧logo和图片-->
      <b-col md="6">
        <div class="justify-content-center py-3">
          <img src="../assets/login_left.png" class="image" alt="登录插图">
        </div>
      </b-col>
      <!--    右侧登录表单-->
      <b-col md="6">
        <b-form class="card border-0 px-4 py-5" id="login_form">
          <div class="row px-3">
            <label class="mb-1 text-sm">用户名</label>
            <b-form-input class="mb-4" type="text" name="email" v-model="username" autocomplete="off"
                          placeholder="请输入用户名"/>
          </div>
          <div class="row px-3">
            <label class="mb-1 text-sm">密码</label>
            <b-form-input type="password" name="password" v-model="password" autocomplete="off" placeholder="输入密码"/>
          </div>
          <div class="row px-3 my-4">
            <div class="custom-control custom-checkbox custom-control-inline">
              <input id="chk1" type="checkbox" name="chk" class="custom-control-input">
              <label for="chk1" class="custom-control-label text-sm">保持登录</label>
            </div>
            <a href="#" class="ml-auto mb-0 text-sm text-info">忘记密码?</a>
          </div>
          <div class="row mb-3 px-3">
            <b-button variant="info" @click="login" :disabled="disabled">登录</b-button>
          </div>
          <div class="row mb-4 px-3">
            <small class="font-weight-bold">还没有账户?
              <router-link class="text-info" to='/register'>去注册</router-link>
            </small>
          </div>
        </b-form>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "Login",
  data() {
    return {
      username: "",
      password: "",
      disabled: false
    }
  },
  methods: {
    login() {
      if (this.username === "" || this.password === "") {
        this.$bvToast.toast(`用户名或密码不能为空`)
        return;
      }
      this.disabled = true;
      this.$http.post('/user/login', {
        "username": this.username,
        "password": this.password
      }).then(response => {
        this.$bvToast.toast(response.data.result)
        if (response.data.status === 1) {
          this.$store.commit('SET_TOKEN', 'f') // TODO: TOKEN
          this.$store.commit('SET_USER', this.username)
          setTimeout(() => {
            window.location.href = '/'
          }, 1500);
        }
      }).catch(()=>{
        this.$bvToast.toast("注册失败")
      }).finally(() => {
        this.password = "";
        this.disabled = false;
        }
      )
    },
  },
  mounted() {
    if (this.$store.getters.IS_LOGIN)
      this.$router.push('/profile')
  }
}
</script>

<style scoped>
.image {
  width: 100%;
}

.text-sm {
  font-size: 14px
}
</style>