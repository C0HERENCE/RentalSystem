<template>
<b-container>
  <h1>{{category_name}}</h1>
  <b-row>
    <b-col cols="3">
      <b-list-group>
        <b-list-group-item v-for="c in child_categories" :key="c.id">{{c.description}}</b-list-group-item>
      </b-list-group>
    </b-col>
    <b-col cols="9">
      <router-view></router-view>
    </b-col>
  </b-row>
</b-container>
</template>

<script>
import {getCategoryDetail, getChildCategories} from "@/api/category";

export default {
  name: "Category",
  data() {
    this.ready();
    return {
      category_name: "",
      child_categories: []
    }
  },
  methods: {
    ready() {
      getCategoryDetail(this.$route.params.id)
          .then(res => {
            this.category_name = res.result.description
            getChildCategories(this.$route.params.id)
            .then(res => this.child_categories = res.result)
          })
    }
  },
  watch: {
    $route: function() {
      this.ready();
    }
  }

}
</script>

<style scoped>

</style>