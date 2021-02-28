<template>
  <b-row>
    <h1>密码重置</h1>
    <b-col cols="12">
      <b-card bg-variant="light" class="my-3">
        <b-form-group
            label-cols-lg="2"
            label="基本信息:"
            label-size="lg"
            label-class="font-weight-bold pt-0"
            class="mb-0"
        ></b-form-group>
        <b-form>
          <b-input type="text" autocomplete="username" hidden></b-input>
          <b-form-group label="原密码:" label-cols-sm="3" label-align-sm="right">
            <b-input v-model="pwd" type="password" autocomplete="current-password"></b-input>
          </b-form-group>
          <b-form-group label="新密码:" label-cols-sm="3" label-align-sm="right">
            <b-input v-model="new_pwd" type="password" autocomplete="new-password"></b-input>
          </b-form-group>
          <b-form-group label="重新输入新密码:" label-cols-sm="3" label-align-sm="right">
            <b-input v-model="new_pwd2" type="password" autocomplete="new-password"></b-input>
          </b-form-group>
        </b-form>
      </b-card>
      <div class="mx-auto my-1" style="width:100px">
        <b-button variant="primary" @click="saveChanges" style="width: 100px;" class="mx-auto my-1"
                  :disabled="disabled">
          保存
        </b-button>
      </div>
    </b-col>
  </b-row>
</template>

<script>
import {resetPassword} from "@/api/user";

export default {
  name: "Reset.vue",
  data() {
    return {
      pwd: '',
      new_pwd: '',
      new_pwd2: '',
      disabled: false
    }
  },
  methods: {
    saveChanges() {
      if (this.pwd === "" || this.new_pwd2 === "" || this.new_pwd === "") {
        this.$bvToast.toast("请填写完整");
        return;
      }
      if (this.new_pwd2 !== this.new_pwd) {
        this.$bvToast.toast("两次密码输入不一致");
        return;
      }
      if (this.new_pwd === this.pwd) {
        this.$bvToast.toast("新密码不得与原密码一致");
        return;
      }
      resetPassword(this.pwd, this.new_pwd)
          .then(res => console.log(res))
    }
  },
}
</script>

<style scoped>

</style>