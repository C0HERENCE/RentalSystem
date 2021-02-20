<template>
  <div class="container py-3">
    <b-row class="my-5">
      <!--    左侧logo和图片-->
      <b-col md="6">
        <div class="justify-content-center py-3">
          <img src="../assets/login_left.png" class="image">
        </div>
      </b-col>
      <!--    右侧登录表单-->
      <b-col md="6">
        <div class="card border-0 px-4 py-5">
          <div class="row px-3">
            <label class="mb-1 text-sm">用户名</label>
            <b-form-input class="mb-4" type="text" name="email" autocomplete="off" placeholder="输入电子邮箱"/>
          </div>
          <div class="row px-3">
            <label class="mb-1 text-sm">密码</label>
            <b-form-input type="password" name="password" autocomplete="off" placeholder="输入密码"/>
          </div>
          <div class="row px-3 my-4">
            <div class="custom-control custom-checkbox custom-control-inline">
              <input id="chk1" type="checkbox" name="chk" class="custom-control-input">
              <label for="chk1" class="custom-control-label text-sm">保持登录</label>
            </div>
            <a href="#" class="ml-auto mb-0 text-sm text-info">忘记密码?</a>
          </div>
          <div class="row mb-3 px-3">
            <b-button type="submit" variant="info">登录</b-button>
          </div>
          <div class="row mb-4 px-3">
            <small class="font-weight-bold">还没有账户?
              <a class="text-info">去注册</a>
            </small>
          </div>
        </div>
      </b-col>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "Login"
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