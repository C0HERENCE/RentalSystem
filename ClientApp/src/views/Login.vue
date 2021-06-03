<template>
<div>
  <el-row type="flex" justify="center">
    <el-col :span="10">
      <el-card class="m-3 text-center">
        <h1 class="text-center">登录</h1>
        <el-form :model='form' label-width="80px" size="big" :rules="rules" ref="ruleForm" >
          <el-form-item label="学号" prop="schoolNum">
            <el-input v-model="form.schoolNum"></el-input>
          </el-form-item>
          <el-form-item label="密码" prop="password">
            <el-input type="password" v-model="form.password"></el-input>
          </el-form-item>
        </el-form>
        <div class="text-center">
          <el-button type="primary" @click="login">登录</el-button>
        </div>
      </el-card>
    </el-col>
  </el-row>

</div>
</template>

<script>
import {login} from "../api/user";
export default {
  name: "Login",
  data() {
    return {
      form: {
        schoolNum: '',
        password: ''
      },
      rules: {
        schoolNum: [
          { required: true, message: '请输入学号', trigger: 'blur' },
        ],
        password: [
          { required: true, message: '请输入密码', trigger: 'blur' },
        ],
      }
    }
  },

  methods: {
    login() {
      this.$refs['ruleForm'].validate((valid) => {
        if (valid) {
          login(this.form.schoolNum, this.form.password).then(res => {
            this.$alert(res.data.message)
            if (res.data.status === 200) {
              this.$notify({
                title: '欢迎你，' + res.data.data.name + '同学！',
                duration: 3000
              });
              this.$store.commit('SetLogin', true)
              this.$store.commit('SetStudentName', res.data.data.name)
              this.$store.commit('SetIsAdmin', res.data.data.isAdmin)
              this.$connection.start() // 6 登陆的时候 connection start
              if (res.data.data.isAdmin === true)
                this.$router.push("/admin")
              else
                this.$router.push('/')
            }
          })
        } else {
          return false;
        }
      });
    }
  },
}
</script>

<style scoped>

</style>