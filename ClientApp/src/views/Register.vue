<template>
  <div>
    <el-row type="flex" justify="center">
      <el-col :span="12">
        <el-card class="m-3">
          <h1 class="text-center">注册</h1>
          <el-divider>注册前须知</el-divider>
          <div style="text-align: center;font-size: 16px">请在注册前阅读<router-link to="/Guide">租赁指南</router-link></div>
          <el-divider>身份验证</el-divider>
          <el-form :model='form' label-width="80px" size="mini" :rules="rules" ref="ruleForm" >
            <el-form-item label="班级" prop="classId">
              <el-select v-model="form.classId" placeholder="请选择"  style="width: 100%">
                <el-option v-for="item in classOptions" :key="item.id" :label="item.grade + ' ' + item.major" :value="item.id">
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="学生姓名" prop="studentName">
              <el-input v-model="form.studentName"></el-input>
            </el-form-item>
            <el-form-item label="学号" prop="schoolNum">
              <el-input v-model="form.schoolNum"></el-input>
            </el-form-item>
            <el-form-item label="身份证号" prop="IdCard">
              <el-input v-model="form.IdCard"></el-input>
            </el-form-item>
            <el-divider>个人信息</el-divider>
            <el-form-item label="密码" prop="password">
              <el-input type="password" v-model="form.password"></el-input>
            </el-form-item>
            <el-form-item label="确认密码" prop="password2">
              <el-input type="password" v-model="form.password2"></el-input>
            </el-form-item>
            <el-form-item label="邮箱" prop="email">
              <el-input v-model="form.email"></el-input>
            </el-form-item>
            <el-form-item label="验证码" prop="captcha">
              <el-col :span="10"><el-input v-model="form.captcha"></el-input></el-col>
              <el-col :span="10" class="mx-2"><el-button @click="sendCaptcha">发送验证码</el-button></el-col>
            </el-form-item>
          </el-form>
          <div class="text-center">
            <el-button type="primary" @click="register">注册</el-button>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import {register,captcha} from "@/api/user";
import {getAllClasses} from "@/api/class";

export default {
  name: "Register.vue",
  data() {
    return {
      form: {
        classId: '',
        studentName: '',
        schoolNum: '',
        IdCard: '',
        password: '',
        password2: '',
        captcha: '',
        email: ''
      },
      classOptions: [],
      rules: {
        classId: [
          { required: true, message: '请选择班级', trigger: 'blur' },
        ],
        studentName: [
          { required: true, message: '请输入你的姓名', trigger: 'blur' },
        ],
        schoolNum: [
          { required: true, message: '请输入学号', trigger: 'blur' },
        ],
        IdCard: [
          { required: true, message: '请输入身份证号', trigger: 'blur' },
          { len: 18, message: '身份证错误', trigger: 'blur' }
        ],
        password: [
          { required: true, message: '请输入密码', trigger: 'blur' },
          { min: 6, max: 18, message: '长度在 6 到 18 个字符', trigger: 'blur' }
        ],
        password2: [
          { required: true, message: '请重复输入密码', trigger: 'blur' },
          { min: 6, max: 18, message: '长度在 6 到 18 个字符', trigger: 'blur' }
        ],
        email: [
          { required: true, message: '请输入邮箱地址', trigger: 'blur' },
          { type: 'email', message: '请输入正确的邮箱地址', trigger: ['blur', 'change'] }
        ],
        captcha: [
          { required: true, message: '请输入验证码', trigger: 'blur' },
          { len: 6, message: '请输入6位验证码', trigger: 'blur' }
        ]
      }
    }
  },
  methods: {
    register() {
      this.$refs['ruleForm'].validate((valid) => {
        if (valid) {
          if (this.form.password2!== this.form.password) {
            this.$alert("两次密码输入不一致")
            return;
          }
          register(this.form).then(res => {
            this.$alert(res.data.message)
            if (res.data.status === 200) {
              this.$router.push('/login')
            }
          })
        } else {
          return false;
        }
      });
    },
    sendCaptcha() {
      this.$refs['ruleForm'].validateField( 'email' ,result => {
        if (result === '') {
          captcha(this.form.email).then(res => this.$alert(res.data.message));
        }
      })
    }
  },
  mounted() {
    getAllClasses().then(res => {
      this.classOptions = res.data.data;
    })
  }
}
</script>

<style scoped>

</style>