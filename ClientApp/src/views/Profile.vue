<template>
  <div>
    <el-card>
      <h2 style="text-align: center">
        <i class="el-icon-user-solid"></i>
        {{ profile.personalInfo.studentName }}的个人中心
      </h2>
    </el-card>
    <el-card>

      <el-tabs tab-position="top">
        <el-tab-pane label="个人资料">
          <el-form :model='profile.personalInfo' label-width="80px" size="mini" :rules="rules">
            <el-form-item label="班级">
              <el-col :span="8">
                <p v-text="profile.personalInfo.class"></p>
              </el-col>
              <el-col :span="8">
                <el-form-item label="学生姓名">
                  <p v-text="profile.personalInfo.studentName"></p>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="学号">
                  <p v-text="profile.personalInfo.schoolNum"></p>
                </el-form-item>
              </el-col>
            </el-form-item>
            <el-form-item label="身份证号">
              <el-col :span="8">
                <p v-text="profile.personalInfo.idCard"></p>
              </el-col>
              <el-col :span="8">
                <el-form-item label="余额">
                  <p v-text="profile.personalInfo.balance"></p>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label=" ">
                  <el-button type="success" @click="addMoney">立即充值</el-button>
                  <el-button type="success" @click="$refs.lineChart.visible=true">账户资金变化</el-button>
                </el-form-item>
              </el-col>
            </el-form-item>
            <el-form-item label="邮箱">
              <el-col :span="8">
                <p v-text="profile.personalInfo.email"></p>
              </el-col>
              <el-col :span="8">
                <el-form-item label="联系电话" prop="tel">
                  <el-input v-model="profile.personalInfo.tel"></el-input>
                </el-form-item>
              </el-col>
            </el-form-item>
            <el-form-item label="个人介绍" prop="description">
              <el-input type="textarea" v-model="profile.personalInfo.description"></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="submitForm()">修改</el-button>
            </el-form-item>
          </el-form>
        </el-tab-pane>
        <el-tab-pane label="我发布的商品">
          <h5>截止目前，我已发布{{ profile.published.length }}个商品。</h5>
          <el-card v-for="p in profile.published" :key="p.id">
           <ProfilePublished :product="p"></ProfilePublished>
          </el-card>
        </el-tab-pane>
        <el-tab-pane label="我租到的">
          <h5>截止目前，我已租到商品产生{{ profile.buyOrders.length }}个订单。</h5>
          <el-card v-for="p in profile.buyOrders" :key="p.id">
            <ProfileOrder :order="p" :is-publisher="false" @rateOrder="(value) => rateOrder(p, value)" :show-rate="true"></ProfileOrder>
          </el-card>
        </el-tab-pane>
        <el-tab-pane label="我出租的">
          <h5>截止目前，我已出租产生{{ profile.sellOrders.length }}个订单。</h5>
          <el-card v-for="p in profile.sellOrders" :key="p.id">
            <ProfileOrder :order="p" :is-publisher="true" @StatusChange="(v,x, w) => {p.status = v; p.totalPrice = x; p.extraMoney = w}"></ProfileOrder>
          </el-card>
        </el-tab-pane>
        <el-tab-pane label="修改密码">
          <el-form :model='modifyPasswordForm' label-width="120px" size="mini" :rules="modifyPasswordRule"
                   ref="ruleForm">
            <el-form-item label="旧密码" prop="oldPassword">
              <el-col :span="8">
                <el-input type="password" v-model="modifyPasswordForm.oldPassword"></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="新密码" prop="newPassword">
              <el-col :span="8">
                <el-input type="password" v-model="modifyPasswordForm.newPassword"></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="确认新密码" prop="newPassword2">
              <el-col :span="8">
                <el-input type="password" v-model="modifyPasswordForm.newPassword2"></el-input>
              </el-col>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="modify">修改密码</el-button>
            </el-form-item>
          </el-form>
        </el-tab-pane>
        <el-tab-pane label="我的收藏">
          <ProfileFavourite></ProfileFavourite>
        </el-tab-pane>

        <ProfileLineChart ref="lineChart"></ProfileLineChart>
      </el-tabs>
    </el-card>
  </div>
</template>

<script>
import {getUserProfileById, addMoney, modifyInfo, modify} from "@/api/user";
import {rateOrder} from "@/api/order";
import ProfilePublished from "@/components/ProfilePublished";
import ProfileOrder from "@/components/ProfileOrder";
import ProfileFavourite from "@/components/ProfileFavourite";
import ProfileLineChart from "@/components/ProfileLineChart";

export default {
  components: {ProfilePublished, ProfileOrder,ProfileFavourite, ProfileLineChart},
  data() {
    return {
      profile: {
        personalInfo: {}
      },
      modifyPasswordForm: {
        oldPassword: '',
        newPassword: '',
        newPassword2: ''
      },
      rules: {
        tel: [
          {type: 'string', pattern: /^[1][0-9]{10}$/, message: '请输入正确的手机号码', trigger: ['blur']}
        ]
      },
      modifyPasswordRule: {
        oldPassword: [
          {required: true, message: '请输入旧密码', trigger: 'blur'},
          {min: 6, max: 18, message: '长度在 6 到 18 个字符', trigger: 'blur'}
        ],
        newPassword: [
          {required: true, message: '请输入新密码', trigger: 'blur'},
          {min: 6, max: 18, message: '长度在 6 到 18 个字符', trigger: 'blur'}
        ],
        newPassword2: [
          {required: true, message: '请重复输入新密码', trigger: 'blur'},
          {min: 6, max: 18, message: '长度在 6 到 18 个字符', trigger: 'blur'}
        ],
      },
    };
  },
  methods: {
    addMoney() {
      this.$prompt('请输入要充值的金额', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        inputValidator: (x) => {
          return 0 < x && x < 10000;
        },
        inputErrorMessage: '请输入充值10000元以内的整数金额'
      }).then(({value}) => {
        addMoney(value).then(res => {
          this.$alert(res.data.message)
          this.profile.personalInfo.balance = res.data.data + "元";
        })
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '充值取消'
        });
      });
    },
    rateOrder(order, value){
      rateOrder(order.id, value).then(res => {
        this.$alert(res.data.message)
        if (res.data.status === 200){
          order.rate = value
        }
      })
    },
    submitForm() {
      this.$confirm('是否修改个人信息?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'success'
      }).then(() => {
        modifyInfo({tel: this.profile.personalInfo.tel, description: this.profile.personalInfo.description})
            .then(res => this.$alert(res.data.message))
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消修改'
        });
      });
    },
    modify() {
      this.$refs['ruleForm'].validate((valid) => {
        if (valid) {
          if (this.modifyPasswordForm.newPassword2 !== this.modifyPasswordForm.newPassword) {
            this.$alert("两次密码输入不一致")
            return;
          }
          modify(this.modifyPasswordForm).then(res => {
            this.$alert(res.data.message)
          })
        } else {
          return false;
        }
      })
    },
  },
  mounted() {
    getUserProfileById().then(res => {
      if (res.data.status !== 200)
        this.$alert("获取个人信息失败")
      else {
        this.profile = res.data.data;
      }
    });
  }
}
</script>

<style scoped>

</style>