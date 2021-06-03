<template>
  <!--      设置只有在product被加载出来的时候才显示当前页面，否则报红-->
  <div v-if="product.category!==undefined">
    <el-card class="my-3">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item :to="{ path: '/' }"><i class="el-icon-s-home"></i>首页</el-breadcrumb-item>
        <el-breadcrumb-item>{{ product.category.label }}</el-breadcrumb-item>
        <el-breadcrumb-item>{{ product.name }}</el-breadcrumb-item>
      </el-breadcrumb>
    </el-card>
    <el-card>
      <el-row style="height: 400px" :gutter="32">
        <el-col :span="8">
          <el-image style="width: 100%; height: 400px" fit="cover" :src="'/uploads/' + product.cover"></el-image>
        </el-col>
        <el-col :span="16">
          <el-form size="mini" label-width="80px">
            <el-form-item label="名称">
              <h5>{{ product.name }}</h5>
            </el-form-item>
            <el-form-item label="租金">
              <h5 style="color: #ff0036">{{ product.price }} 元 / 天</h5>
            </el-form-item>
            <el-form-item label="押金">
              <h5 style="color: #ff0036">{{ product.yaMoney }}</h5>
            </el-form-item>
            <el-form-item label="简介">
              <p>{{ product.description }}</p>
            </el-form-item>
            <el-form-item label="时长">
              <el-col :span="7">
                <el-input-number :precision="0" :min="1" :max="1000" size="mini" v-model="days"></el-input-number>
                <span class="ml-1">天</span>
              </el-col>
              <el-col :span="17">
                <el-form-item label="总金额">
                  <span class="h5" style="color: #ff0036">{{ product.price * days }} 元</span>
                  <span class="small text-muted ml-2">(不含押金)</span>
                </el-form-item>
              </el-col>
            </el-form-item>
            <el-form-item label="">

              <el-button @click="contact()">联系卖家</el-button>
              <el-button @click="newOrder()" :disabled="product.enabled !== 1">立即下单</el-button>
              <el-button @click="dislikeProduct" v-if="product.favourited">取消收藏</el-button>
              <el-button @click="likeProduct" v-if="!product.favourited">收藏</el-button>
            </el-form-item>
          </el-form>
        </el-col>
      </el-row>
    </el-card>
    <el-row class="my-3">
      <el-card>
        <h5>发布者信息</h5>
        <el-form label-width="100px" size="mini">
          <el-form-item label="姓名">
            <el-col :span="2">
              <span>{{ product.publisher.studentName }}</span>
            </el-col>
            <el-col :span="8">
              <el-form-item label="班级">
                <span>{{ product.publisher.class }}</span>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="联系方式">
                <span>{{ product.publisher.tel }}</span>
              </el-form-item>
            </el-col>
          </el-form-item>
          <el-form-item label="出租者介绍">
            <span>{{ product.publisher.description }}</span>
          </el-form-item>
        </el-form>

        <h5>商品信息</h5>
        <el-form label-width="100px">
          <el-form-item label="">
            <p v-html="product.details"></p>
          </el-form-item>
        </el-form>
      </el-card>
    </el-row>

  </div>
</template>

<script>
import {getProductById, likeProduct, dislikeProduct} from "@/api/product"

export default {
  name: "Detail.vue",
  data() {
    return {
      product: {},
      days: 1
    }
  },
  methods: {
    contact() {
      if (!this.$store.state.isLogin) {
        this.$alert("请登录后再操作")
        return;
      }
      if (this.$store.state.currentStudentName === this.product.publisher.studentName)
      {
        this.$alert("不能和自己沟通哦")
        return;
      }
      if (this.$connection.connection.connectionState !== "Connected") {
        this.$connection.start().then(() => {
          this.send()
        })
      } else this.send()
    },
    send() {
      let data = {
        tid: this.product.publisher.id,
        tname: this.product.publisher.studentName,
        message: "我想咨询一下" + this.product.name,
        dir: 1,
      }
      this.$store.dispatch("SendMessage", data).then(() => {
        this.$connection.invoke("SendMessage", data.tid.toString(), this.$store.state.currentStudentName, data.message)
      })
      this.$parent.showChatDrawer = true
    },
    newOrder() {
      if (!this.$store.state.isLogin) {
        this.$alert("请登录后再操作")
        return;
      }
      if (this.$store.state.currentStudentName === this.product.publisher.studentName)
      {
        this.$alert("不能购买自己的商品")
        return;
      }
      this.$router.push({name: 'PayOrder', params: {productId: this.$route.params.id, days: this.days.toString()}})
    },
    // 收藏 按钮点击
    likeProduct() {
      if (this.$store.state.isLogin === false) {
        this.$alert("请登录后再操作");
        return;
      }
      likeProduct(this.product.id).then(res => {
        this.$alert(res.data.message)
        if (res.data.status === 200) {
          this.product.favourited = true
        }
      })
    },
    // 取消收藏按钮点击
    dislikeProduct() {
      if (this.$store.state.isLogin === false) {
        this.$alert("请登录后再操作");
        return;
      }
      dislikeProduct(this.product.id).then(res => {
        this.$alert(res.data.message)
        if (res.data.status === 200) {
          this.product.favourited = false
        }
      })
    }
  },
  mounted() {
    getProductById(this.$route.params.id).then(res => {
      if (res.data.status === 200)
        this.product = res.data.data;
      else
        this.$alert(res.data.message).then(() => this.$router.push("/"))
    })
  }
}
</script>

<style scoped>

</style>