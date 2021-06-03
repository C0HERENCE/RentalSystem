<template>
  <el-row>
    <el-col :span="3">
      <el-image style="height: 130px" :src="product.cover"></el-image>
    </el-col>
    <el-col :span="12" class="small text-muted">
      <p class="mx-3">商品编号： {{ product.id }}</p>
      <p class="mx-3">商品名称： {{ product.name }}</p>
      <p class="mx-3">商品介绍: {{ product.description }}</p>
    </el-col>
    <el-col :span="9" class="text-right">
      <span style="font-size: 2rem;color: #ff0036">{{ product.price }}</span>
      <span class="small">元 / 天</span>
      <br>
      <p class="small text-muted">{{ checked[product.checked] }}</p>
      <br>
      <router-link :to="'/Detail/'+product.id" class="mx-3">
        <el-button size="mini">商品详情</el-button>
      </router-link>
      <el-button size="mini" type="danger" @click="downProduct(product)" v-if="product.enabled">下架</el-button>
      <span v-else>已下架</span>
    </el-col>
  </el-row>
</template>

<script>
import {deleteProductById} from "@/api/product";

export default {
  name: "ProfilePublished",
  data() {
    return {
      checked: ["待审核", "审核已通过", "审核未通过"]
    }
  },
  props: {
    product: {
      default: {}
    },
  },
  methods: {
    downProduct(row) {
      this.$confirm("产品下架后将无法上架！").then(() => {
        deleteProductById(row.id).then(res => {
          this.$alert(res.data.message)
          if (res.data.status === 200) {
            row.enabled = !row.enabled
          }
        })
      })
    }
  },
}
</script>

<style scoped>

</style>