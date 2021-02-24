<template>
  <b-container>
      <h1>发布出租商品</h1>
    <b-form>
        <label class="mr-sm-2">商品名称</label>
        <b-form-input v-model="text" placeholder="输入商品名称"></b-form-input>

        <label class="mr-sm-2">选择图片</label>
        <b-form-file v-model="file1" :state="Boolean(file1)"
            placeholder="选择图片或拖拽图片到这里"
            drop-placeholder="拖拽到这里"
            browse-text="浏览"></b-form-file>
        <div class="mt-3">已选择的图片: {{ file1 ? file1.name : "" }}</div>

        <label class="mr-sm-2">商品售价</label>
        <b-form-input v-model="text" placeholder="想卖多少钱" type="number"></b-form-input>
        元
        <br>
        <label class="mr-sm-2">出租时间</label>
        <b-form-input v-model="text" placeholder="天数" type="number"></b-form-input>
        天
        <b-form-input v-model="text" placeholder="小时" type="number"></b-form-input>
        小时
        <br>
        <label class="mr-sm-2">商品介绍</label>
        <b-form-textarea
            id="textarea"
            v-model="text"
            placeholder="输入商品介绍(200字内)"
            rows="3"
            max-rows="6"
        ></b-form-textarea>
      <b-button variant="primary" @click="publish">发布</b-button>
    </b-form>
  </b-container>
</template>

<script>
export default {
  name: "Publish",
  data() {
    return {
      file1: null,
    };
  },
  methods: {
      publish() {
        this.$bvModal.msgBoxOk('发布成功，即将跳转到商品详情页');
      }
  }
};
</script>

<style>
</style>