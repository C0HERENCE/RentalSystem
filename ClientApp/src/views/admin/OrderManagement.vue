<template>
  <div style="width: 100%" class="my-3">
    {{ tableData[0] }}
    <el-table :data="tableData">
      <el-table-column prop="id" label="编号"></el-table-column>
      <el-table-column prop="product.name" label="商品名称"></el-table-column>
      <el-table-column label="创建时间">
        <template slot-scope="xxx">
          {{ xxx.row.createTime | fTime }}
        </template>
      </el-table-column>
      <el-table-column prop="days" label="租赁时长"></el-table-column>
      <el-table-column label="总价">
        <template slot-scope="xxx">
          {{ xxx.row.product.price * xxx.row.days + xxx.row.extraMoney}}
        </template>
      </el-table-column>
      <el-table-column label="状态">
        <template slot-scope="xxx">
          {{ statusChinese[xxx.row.status] }}
        </template>
      </el-table-column>
      <el-table-column label="详情编辑">
        <template slot-scope="x">
          <el-button @click="current = x.row" size="small"><i class="el-icon-edit"></i></el-button>
        </template>
      </el-table-column>
    </el-table>

    <!--    编辑框部分-->
    <el-dialog title="订单详情" :visible="current !== null" width="30%" v-if="current" @close="current = null">
      <el-form @submit.native.prevent>
        <!--        当一个 form 元素中只有一个输入框时，在该输入框中按下回车应提交该表单。如果希望阻止这一默认行为，可以在 <el-form> 标签上添加 @submit.native.prevent。-->
        <el-form-item label="订单状态">
          <el-tag type="success" v-if="current.status === 0">待当面交易</el-tag>
          <el-tag type="danger" v-else-if="current.status === 1">使用中</el-tag>
          <el-tag type="info" v-else-if="current.status === 2">成功结束</el-tag>
          <el-tag type="warning" v-else>卖家拒绝</el-tag>
          <div class="text-right">
            <el-button v-if="current.status !== 0" @click="onChangeStatus(current,0)" size="mini" type="success">
              设为待当面交易
            </el-button>
            <el-button v-if="current.status !== 1" @click="onChangeStatus(current,1)" size="mini" type="danger">
              设为使用中
            </el-button>
            <el-button v-if="current.status !== 2" @click="onChangeStatus(current,2)" size="mini" type="info">
              设为成功结束
            </el-button>
            <el-button v-if="current.status !== 3" @click="onChangeStatus(current,3)" size="mini" type="warning">
              设为卖家拒绝
            </el-button>
          </div>
        </el-form-item>
      </el-form>
    </el-dialog>
    
  </div>
</template>

<script>
import {GetOrders,UpdateOrderById} from "@/api/admin";

export default {
  name: "OrderManagement",
  mounted() {
    GetOrders().then(res => this.tableData = res.data.data)
  },
  data() {
    return {
      tableData: [],
      statusChinese: ["待当面交易", "使用中", "成功结束", "卖家拒绝"],
      current: null,
    }
  },
  methods: {
    onChangeStatus(xx, status) {
      UpdateOrderById(xx.id, status).then(res => {
        this.$alert(res.data.message)
        if (res.data.status === 200)
          xx.status = status;
      })
    }
  }
}
</script>

<style scoped>

</style>