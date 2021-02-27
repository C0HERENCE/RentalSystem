<template>
  <b-container class="py-3">
    <b-row class="my-5">
      <!--    左侧logo和图片-->
      <b-col md="6">
        <div class="justify-content-center py-3">
          <img src="../assets/register_left.png" class="image" alt="注册插图">
        </div>
      </b-col>
      <!--    右侧注册表单-->
      <b-col md="6">
        <b-form class="card border-0 px-4 py-5">
          <div class="row px-3">
            <label class="mb-1 text-sm">邮箱</label>
            <b-form-input class="mb-4" type="text" name="email" v-model="email" autocomplete="off"
                          placeholder="输入电子邮箱"/>
          </div>
          <div class="row px-3">
            <label class="mb-1 text-sm">用户名</label>
            <b-form-input class="mb-4" type="text" name="username" v-model="username" autocomplete="off"
                          placeholder="输入用户名"/>
          </div>
          <div class="row px-3">
            <label class="mb-1 text-sm">密码</label>
            <b-form-input class="mb-4" type="password" name="password" v-model="password" autocomplete="off"
                          placeholder="输入密码"/>
          </div>
          <div class="row px-3">
            <label class="mb-1 text-sm">重复密码</label>
            <b-form-input class="mb-4" type="password" name="password_repeat" v-model="password_repeat"
                          autocomplete="off" placeholder="重复输入密码"/>
          </div>
          <div class="row mb-3 px-3">
            <b-button variant="info" @click="register" :disabled="disabled">注册</b-button>
          </div>
          <div class="row mb-4 px-3">
            <small class="font-weight-bold">已有账户?
              <router-link class="text-info" to="/login">去登录</router-link>
            </small>
          </div>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import {register} from "@/api/user";

export default {
  name: "Register",
  data() {
    return {
      email: '',
      username: '',
      password: '',
      password_repeat: '',
      disabled: false
    }
  },
  methods: {
    register() {
      if (this.password === "" || this.password_repeat === "" || this.email === "" || this.username === "") {
        this.$bvToast.toast('请将注册信息填写完整');
        return;
      }
      if (this.password !== this.password_repeat) {
        this.$bvToast.toast('两次密码输入不一致');
        return;
      }
      let data = {
        username: this.username,
        password: this.password,
        email: this.email,
      };
      this.disabled = true;
      register(data)
          .then(() => {
            setTimeout(() => {
              this.$router.push('/login')
            }, 1500);
          })
          .catch(() => {
            this.$bvToast.toast("注册失败");
          })
          .finally(() => {
            this.disabled = false;
            this.password_repeat = "";
            this.password = "";
          });
    }
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