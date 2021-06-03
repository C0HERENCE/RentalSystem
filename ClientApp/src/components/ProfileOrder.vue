<template>
  <el-row>
    <el-col :span="3">
      <el-image style="height: 130px" :src="order.cover"></el-image>
    </el-col>
    <el-col :span="12" class="small text-muted">
      <p class="mx-3">订单编号： {{ order.id }}</p>
      <p class="mx-3">商品名称： {{ order.productName }}</p>
      <p class="mx-3">订单创建时间: {{ order.createTime  | fTime }}</p>
      <p class="mx-3">租赁时长: {{ order.days }} 天</p>
    </el-col>
    <el-col :span="9" class="text-right">
      <span style="font-size: 2rem;color: #ff0036">{{ order.totalPrice }}</span>
      <span class="small">元</span>
      <br>
      <p class="small text-muted">{{ status[order.status] }}</p>
      <br>
      <el-button size="mini" @click="dialogVisible2 = true" v-if="this.order.status === 2 && showRate">订单评价</el-button>
      <el-button size="mini" @click="dialogVisible = true">订单详情</el-button>
      <span v-if="isPublisher">
        <span v-if="this.order.status === 0">
              <el-button size="mini" @click="confirmOrder" style="margin-left: 12px">确认订单</el-button>
              <el-button size="mini" @click="rejectOrder">拒绝订单</el-button>
        </span>
      <el-button size="mini" v-if="this.order.status === 1" @click="backOrder">确认归还</el-button>
      </span>
    </el-col>
    <el-dialog title="订单评价" :visible.sync="dialogVisible2" width="60%">
      <div class="block">
        <span class="demonstration">此次租赁体验在你心中的评分是</span>
        <el-rate v-model="value"></el-rate>
      </div>
      <span slot="footer" class="dialog-footer">
           <el-button type="primary" @click="rateOrder">确 定</el-button>
      </span>
    </el-dialog>
    <el-dialog title="订单详情" :visible.sync="dialogVisible" width="60%">
      <el-row>
        <el-col :span="18">
          <span class="mx-3">订单编号 {{ this.order.id }}</span><br>
          <span class="mx-3">商品名称：{{ this.order.productName }}</span><br>
          <span class="mx-3">订单创建时间：{{ this.order.createTime | fTime }}</span><br>
          <span class="mx-3">租赁时长： {{ this.order.days }}天</span><br>
          <span class="mx-3">租金： {{ this.order.zuMoney }}元</span><br>
          <span class="mx-3">押金： {{ this.order.yaMoney }}元</span><br>
          <span class="mx-3">扣除押金： {{ this.order.extraMoney }}元</span><br>
          <span class="mx-3">总价：{{ this.order.totalPrice }}元</span><br>
          <span class="mx-3">订单状态：{{ this.status[this.order.status] }}</span><br>
          <span class="mx-3" v-if="this.order.status === 2">订单评价：
            <el-rate :value="this.order.rate" disabled></el-rate>
          </span>
        </el-col>
        <el-col :span="6">
          <img width="100%" :src="this.order.cover" alt="cover">
        </el-col>
      </el-row>
      <span slot="footer" class="dialog-footer">
           <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
      </span>
    </el-dialog>
  </el-row>
</template>

<script>
import {confirmOrder, backOrder} from "../api/order";

export default {
  name: "ProfileOrder",
  data() {
    return {
      status: ["待当面交易", "使用中", "成功结束", "卖家拒绝"],
      dialogVisible: false,
      dialogVisible2: false,
      value: null,
      colors: ['#99A9BF', '#F7BA2A', '#FF9900']  // 等同于 { 2: '#99A9BF', 4: { value: '#F7BA2A', excluded: true }, 5: '#FF9900' }
    }
  },
  props: {
    showRate: {
      default: false
    },
    order: {
      default: {}
    },
    isPublisher: {
      default: false
    }
  },
  methods: {
    rateOrder(){
      this.$emit("rateOrder", this.value)
      this.dialogVisible2 = false
    },
    confirmOrder() {
      confirmOrder(this.order.id, "yes").then(res => {
        this.$alert(res.data.message)
        if (res.data.status === 200) {
          this.$emit("StatusChange", "1", this.order.days * this.order.price + this.order.yaMoney, 0)
        }
      })
    },
    rejectOrder() {
      confirmOrder(this.order.id, "no").then(res => {
        this.$alert(res.data.message)
        if (res.data.status === 200) {
          this.$emit("StatusChange", "3", this.order.days * this.order.price + this.order.yaMoney, 0)
        }
      })
    },
    backOrder() {
      this.$prompt('请输入押金扣除金额（元）', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
      }).then(({value}) => {
        if (value >= 0)
          backOrder(this.order.id, value).then(res => {
            this.$alert(res.data.message)
            if (res.data.status === 200) {
              this.$emit("StatusChange", "2", this.order.days * this.order.price + parseInt(value), parseInt(value))
            }
          })
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '你取消了归还确认'
        });
      });
    }
  },
}
</script>

<style scoped>

</style>