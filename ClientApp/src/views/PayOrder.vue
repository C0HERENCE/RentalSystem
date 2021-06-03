<template>
  <div style="text-align: center">
    <h4 class="my-3">支付订单</h4>
    <el-card v-if="product!==null">
      <el-row>
        <el-col :span="12">
          <el-image style="width: 40%" fit="cover" :src="'/uploads/' + product.cover"></el-image>
        </el-col>
        <el-col :span="6" class="my-3">
          <p>商品名称：{{ product.name }}</p>
          <p>租赁时长：{{ $route.params.days }}天</p>
          <p>租金：{{ product.price * $route.params.days }}元</p>
          <p>押金：{{product.yaMoney}}</p>
        </el-col>
      </el-row>
    </el-card>
    <el-button type="primary" @click="payDialog" class="my-3">确认并支付</el-button>
  </div>
</template>

<script>
import {makeNewOrder} from "@/api/order";
import {getProductById} from "@/api/product";

export default {
  name: "PayOrder",
  mounted() {
    getProductById(this.$route.params.productId).then(res => {
      this.product = res.data.data;
    })
  },
  data() {
    return {
      product: null
    }
  },
  methods: {
    payDialog() {
      this.$alert("<h6>租金将从余额里扣除，请确保余额充足！</h6>", "支付", {
        dangerouslyUseHTMLString: true,
        showCancelButton: true
      }).then(() => {
        makeNewOrder(this.$route.params.productId, this.$route.params.days).then(res => {
          this.$alert(res.data.message)
          console.log(res.data)
          if (res.data.status === 200) {
            this.$router.push("/profile") // 如果成功就跳转到个人中心
          }
        })
      })
    }
  },
}
</script>

<style scoped>

</style>