<template>
  <div>
    <el-row class="my-3" style="height: 50px">
      <el-col :span="10" :offset="6" style="margin-top: 10px">
        <el-input v-model="input" placeholder="请输入想要搜索的内容"></el-input>
      </el-col>
      <el-col :span="3" style="margin-top: 10px">
        <el-button type="primary" icon="el-icon-search"
                   @click="gotoSearch"></el-button>
      </el-col>
    </el-row>
    <el-row :gutter="24" style="margin-top: 0px">
      <el-col :span="20">
        <el-tabs tab-position="top">
          <el-tab-pane v-for="c in category" :key="c.id" :label="c.label">
            <el-row :gutter="24">
              <el-col :lg="6" :sm="12" :md="8" v-for="p in c.products" :key="p.id" class="my-2">
                <router-link :to="'/Detail/'+ p.id">
                  <el-card :body-style="{ padding: '0px' }">
                    <el-image :src="'/uploads/'+p.cover" fit="fit" style="height: 240px"/>
                    <div style="padding: 14px;">
                      <span class="font-weight-bold">{{ p.name }}</span>
                      <div class="bottom clearfix">
                        <div class="float-right text-danger">{{ p.price }}</div>
                      </div>
                    </div>
                  </el-card>
                </router-link>
              </el-col>
            </el-row>
          </el-tab-pane>
        </el-tabs>
      </el-col>
      <el-col :span="4">
        <h4 style="color: #3a745f;text-align: center">
          <i class="el-icon-s-opportunity"></i>最新需求</h4>
        <b-list-group>
          <b-list-group-item v-for="d in demands" :key="d.id">
            <a href="javascript:;" type="link" @click="showDemand(d)"
               style="color: #3a745f;text-decoration: none;">{{ d.name }}</a>
          </b-list-group-item>
        </b-list-group>
        <h4 style="color: #3a745f;text-align: center;margin-top: 40px">
          <i class="el-icon-star-on"></i>猜你喜欢</h4>
        <b-list-group v-if="guessYouLike.length > 0">
          <b-list-group-item v-for="g in guessYouLike" :key="g.id">
            <router-link :to="'/Detail/'+ g.id" style="color: #3a745f;text-decoration: none;">{{ g.name }}</router-link>
          </b-list-group-item>
        </b-list-group>
        <h6 v-else style="color: #3a745f;" class="my-3">啊哦，小羊表示，你的喜好有点难以捉摸...
        </h6>
      </el-col>
    </el-row>
    <el-drawer :visible.sync="openDemand" :title="currentDemand.name" direction="ltr">
      <div v-html="currentDemand.content" class="mx-3"></div>
    </el-drawer>
  </div>
</template>

<script>
import {getAllProduct, guess} from "@/api/product";
import {getAllDemands} from "@/api/demand";

export default {
  name: "Home",
  components: {},
  data() {
    return {
      input: "",
      category: [],
      demands: [],
      guessYouLike: [],
      openDemand: false,
      currentDemand: {},
      currentGuess: {}
    };
  },
  methods: {
    showDemand(row) {
      this.openDemand = true;
      this.currentDemand = row
    },
    showGuess(row) {
      this.openGuess = true;
      this.currentGuess = row
    },
    gotoSearch() {
      if (this.input === "") {
        this.$alert("请先输入要搜索的内容！")
        return;
      }
      this.$router.push(
          {
            name: 'Search',
            params: {
              keywords: this.input
            }
          })
    }
  },
  mounted() {
    getAllProduct().then(res => {
      this.category = res.data.data
    })
    getAllDemands().then(res => {
      this.demands = res.data.data
    })
    guess().then(res => {
      this.guessYouLike = res.data.data
    })
  },
};
</script>
<style>
.el-tabs__item {
  font-size: 17px;
}
</style>