<template xmlns:el-col="http://www.w3.org/1999/html">
  <div>
    <el-row class="my-3">
      <el-col :span="10" :offset="6" class="my-3">
        <el-input @keyup.enter="searchClick" v-model="input" placeholder="请输入想要搜索的内容"></el-input>
      </el-col>
      <el-col :span="3" class="my-3">
        <router-link to="/Search">
          <el-button type="primary" icon="el-icon-search" @click="searchClick"></el-button>
        </router-link>
      </el-col>
    </el-row>
    <div v-if="this.productData.length > 0" style="width: 70%;margin: auto">
    <el-row v-for="s in this.productData" :key="s.id">
      <el-divider></el-divider>
      <el-col :span="5">
        <el-image style="width: 75%" :src="'/uploads/' + s.cover"></el-image>
      </el-col>
      <el-col :span="12" class="small text-muted">
        <h4 class="m-3">{{s.name}}</h4>
        <p class="m-3">{{s.description}}</p>
      </el-col>
      <el-col :span="7" class="text-right">
        <span style="font-size: 2rem;color: #ff0036">{{s.price}}</span>
        <span class="small">元/天</span>
        <br>
        <el-button size="middle" @click="dialogVisible = true">
          <router-link :to="'/Detail/' + s.id" style="color: #3a745f;text-decoration: none;">查看详情</router-link>
        </el-button>
      </el-col>
    </el-row>
    </div>
    <div v-else>
      <h4 style="text-align: center" class="my-5">抱歉！没有找到与”{{this.lastInput}}“相关的结果。别担心，我们换个关键词试试吧~</h4>
    </div>
  </div>
</template>

<script>
import {search} from "@/api/product";

export default {
  name: "Search",
  mounted() {
    if(this.$route.params.keywords) {
      this.input = this.$route.params.keywords
      this.searchClick()
    }
  },
  data() {
    return {
      input: '',
      productData: [],
      lastInput: ''
    }
  },
  methods: {
    searchClick() {
      if (this.input === ''){
        this.$alert("请先输入你要搜索的内容！")
      }
      search(this.input).then(res => {
        this.productData = res.data.data
        this.lastInput = this.input
      })
    }
  },
}
</script>

<style scoped>

</style>