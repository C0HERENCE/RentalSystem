<template>
  <div>
    <h1>发布的商品</h1>
    <div v-if="goods.length === 0">好像没发布过商品...</div>
    <div v-else v-for="g in goods" :key="g.id" class="my-3">
      <router-link :to="'/goods/' + g.id" target="_blank">{{ g.id }} - {{ g.name }}</router-link>
      -
      <b-button>管理</b-button>
    </div>
  </div>
</template>

<script>
import {getUserGoods} from "@/api/goods";

export default {
  name: "PersonalGoods",
  data() {
    getUserGoods()
        .then(res => this.goods = res.result);
    return {
      goods: []
    }
  },
}
</script>

<style scoped>

</style>