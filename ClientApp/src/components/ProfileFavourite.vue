<template>
  <div>
    <el-row>
      <el-col :lg="5" :sm="12" :md="6" v-for="p in favourites" :key="p.id" class="my-2">
        <router-link :to="'/Detail/'+ p.productId">
          <el-card :body-style="{ padding: '0px' }">
            <el-image :src="'/uploads/'+p.cover" fit="fit" style="height: 240px"/>
            <div style="padding: 14px;">
              <span class="font-weight-bold">{{ p.productName }}</span>
              <span class="mx-3">收藏于<br> {{ p.createTime  | fTime }}</span>
            </div>
          </el-card>
        </router-link>
      </el-col>

    </el-row>
  </div>
</template>

<script>
import {getMyFavourite} from "@/api/user";

export default {
  name: "ProfileFavourite",
  mounted() {
    getMyFavourite().then(res => {
      this.favourites = res.data.data
    })
  },
  data() {
    return {
      favourites: []
    }
  },
}
</script>

<style scoped>

</style>