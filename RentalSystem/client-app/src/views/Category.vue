<template>
  <b-container>
    <h1>{{ category_name }}</h1>
    <b-row>
      <b-col cols="3">
        <b-list-group>
          <b-list-group-item v-for="c in child_categories" :key="c.id"
                             :to="'/category/' + c.id"
                             :active="c.id == $route.params.id">{{ c.description }}
          </b-list-group-item>
        </b-list-group>
      </b-col>
      <b-col cols="9">
        <div v-for="g in goods" :key="g.id" class="my-3">
          <router-link :to="'/goods/' + g.id" target="_blank">{{g.id}} - {{g.name}} </router-link>
          -
          <b-button>租它</b-button>
        </div>
        <b-pagination
            v-model="page"
            :total-rows="total"
            :per-page="limit" @input="changePage"
        ></b-pagination>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import {getCategoryDetail, getChildCategories} from "@/api/category";
import {getGoodByCategoryId} from "@/api/goods";

export default {
  name: "Category",
  data() {
    this.ready();
    return {
      category_name: "",
      child_categories: [],
      goods: [],
      page: 1,
      limit: 10,
      total: 10
    }
  },
  methods: {
    ready() {
      getCategoryDetail(this.$route.params.id)
          .then(res => {
            this.category_name = res.result.description
            let parentId = res.result.parentId;
            if (parentId === 0) parentId = res.result.id;
            getChildCategories(parentId)
                .then(res => this.child_categories = res.result)
          });
      getGoodByCategoryId(this.$route.params.id, 1, 10)
          .then(res => {
            this.goods = res.result.data;
            this.total = res.result.total;
          })
    },
    changePage() {
      console.log(this.page)
      getGoodByCategoryId(this.$route.params.id, this.page, this.limit)
          .then(res => {
            this.goods = res.result.data;
            this.total = res.result.total;
          })
    }
  },
  watch: {
    $route: function () {
      this.ready();
    }
  }

}
</script>

<style scoped>

</style>