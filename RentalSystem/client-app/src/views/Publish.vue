<template>
  <b-container>
    <h1>发布出租商品</h1>
    <b-card bg-variant="light" class="my-3">
      <b-form-group
          label-cols-lg="2"
          label="基本信息:"
          label-size="lg"
          label-class="font-weight-bold pt-0"
          class="mb-0"
      >
        <b-form-group
            label="名称:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-input v-model="publish_data.name" placeholder="输入商品名称"></b-form-input>
        </b-form-group>

        <b-form-group
            label="分类:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-select v-model="parent_category" :options="categories"></b-form-select>
          <b-form-select v-model="publish_data.category_id" :options="child_categories"></b-form-select>
        </b-form-group>

        <b-form-group
            label="缩略图:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-file v-model="pic" :state="Boolean(pic)"
                       placeholder="选择图片或拖拽图片到这里"
                       drop-placeholder="拖拽到这里"
                       browse-text="浏览"></b-form-file>
          <div class="mt-3">已选择的图片: {{ pic ? pic.name : "" }}</div>
        </b-form-group>

        <b-form-group
            label="售价:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-input v-model="publish_data.price" placeholder="想卖多少钱" type="number"></b-form-input>
        </b-form-group>
      </b-form-group>
    </b-card>
    <b-card bg-variant="light" class="my-3">
      <b-form-group
          label-cols-lg="2"
          label="商品详情:"
          label-size="lg"
          label-class="font-weight-bold pt-0"
          class="mb-0"
      >
        <b-form-group
            label="介绍:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-textarea
              id="textarea"
              v-model="publish_data.description"
              placeholder="输入商品介绍(200字内)"
              rows="3"
              max-rows="6"
          ></b-form-textarea>
        </b-form-group>

        <b-form-group
            label="详情页:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-textarea
              id="textarea"
              v-model="publish_data.detail_html"
              placeholder="编辑商品详情页"
              rows="3"
              max-rows="6"
          ></b-form-textarea>
        </b-form-group>
        <b-form-group
            label="库存:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-input type="number" v-model="publish_data.stock"></b-form-input>
        </b-form-group>

        <b-form-group
            label="品牌:"
            label-cols-sm="3"
            label-align-sm="right"
        >
          <b-form-select v-model="publish_data.brand_id" :options="brands"></b-form-select>
        </b-form-group>
      </b-form-group>
    </b-card>
    <div class="mx-auto my-1" style="width:100px">
      <b-button variant="primary" @click="publish" style="width: 100px;" class="mx-auto my-1" :disabled="disabled">
        发布
      </b-button>
    </div>
  </b-container>
</template>

<script>
import {publishGoods} from '@/api/goods'
import {getBrands} from "@/api/brand";
import {getCategories, getChildCategories} from "@/api/category";

export default {
  name: "Publish",
  data() {
    getBrands().then(res => {
      res.result.forEach(b => this.brands.push({text: b.name, value: b.id}))
    });
    getCategories().then(res => {
      res.result.forEach(c => this.categories.push({text: c.description, value: c.id}))
    })
    return {
      publish_data: {
        brand_id: '',
        category_id: '',
        name: '',
        pic: '',
        price: '',
        original_price: '',
        description: '',
        detail_html: '',
        stock: ''
      },
      pic: null,
      parent_category: 0,
      brands: [],
      categories: [],
      child_categories: [],
      disabled: false,
    };
  },
  watch: {
    parent_category(newValue) {
      getChildCategories(newValue).then(res => {
        this.child_categories = [];
        res.result.forEach(c => this.child_categories.push({text: c.description, value: c.id}))
      });
    },
    pic(newValue) {
      this.publish_data.pic = newValue.name;
    }
  },
  methods: {
    publish() {
      this.disabled = true;
      publishGoods(this.publish_data)
          .then((res)=>this.$router.push("/goods/" + res.result))
          .finally(() => this.disabled=false)
    }
  }
}
</script>

<style>
</style>