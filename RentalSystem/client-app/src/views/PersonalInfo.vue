<template>
  <b-row>
    <h1>个人资料</h1>
    <b-col cols="12">
      <b-card bg-variant="light" class="my-3">
        <b-form-group
            label-cols-lg="2"
            label="基本信息:"
            label-size="lg"
            label-class="font-weight-bold pt-0"
            class="mb-0"
        ></b-form-group>
        <b-form-group label="用户名:" label-cols-sm="3" label-align-sm="right">
          <b-input v-model="user.username" disabled></b-input>
        </b-form-group>
        <b-form-group label="个人介绍:" label-cols-sm="3" label-align-sm="right">
          <b-textarea v-model="user.description"></b-textarea>
        </b-form-group>
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
import {getUserInfo, setUserInfo} from "@/api/user";

export default {
  name: "PersonalInfo",
  data() {
    getUserInfo(null)
        .then(res => this.user = res.result)
    return {
      user: {
        description: ''
      },
      disabled: false
    }
  },
  methods: {
    saveChanges() {
      this.disabled = true;
      setUserInfo(this.user.description)
          .then(res => console.log(res))
          .finally(() => this.disabled = false);
    }
  },
}
</script>

<style scoped>

</style>