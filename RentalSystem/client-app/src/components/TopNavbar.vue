<template>
  <div>
<!--    顶部导航栏-->
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-container>
<!--        网站名称-->
        <b-navbar-brand to="/">租赁平台</b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav>
<!--          右侧按钮-->
          <b-navbar-nav class="ml-auto">
            <b-nav-item to="/contact">联系我们</b-nav-item>
            <b-nav-item-dropdown right>
              <template #button-content>
                <em>749976734@qq.com</em>
              </template>
              <b-dropdown-item to="/profile">个人中心</b-dropdown-item>
              <b-dropdown-item href="#">登出</b-dropdown-item>
            </b-nav-item-dropdown>
          </b-navbar-nav>
        </b-collapse>
      </b-container>
    </b-navbar>
<!--    中间-->
    <b-container class="mt-3 mb-3">
      <b-row align-v="center">
<!--        logo-->
        <router-link to="/">
          <img alt="logo" src="../assets/logo.png" style="max-height: 45px" class="mr-3">
        </router-link>
<!--        搜索框-->
        <b-row class="col mr-3" align-v="center">
          <b-form-input size="sm" class="mr-2 col" placeholder="你想找什么？"></b-form-input>
          <b-button size="sm" class="my-2 my-sm-0 col-auto" type="submit">搜索</b-button>
        </b-row>
<!--        购物车等按钮-->
        <b-button size="sm" class="mr-2 my-sm-0">发布</b-button>
        <b-button size="sm" class="mr-2 my-sm-0">购物车</b-button>
      </b-row>
    </b-container>
<!--    分类导航-->
    <b-navbar type="dark" variant="dark">
      <b-container>
        <b-navbar-nav>
          <b-nav-item to="/">首页</b-nav-item>
          <b-nav-item v-for="category in categories" :key="category.id" :to="'/category/' + category.id">{{category.text}}</b-nav-item>
        </b-navbar-nav>
      </b-container>
    </b-navbar>
  </div>
</template>

<script>
export default {
  name: 'TopNavbar',
  data() {
    return {
      categories: [
        {
          id: 1,
          text: '数码',
        },
        {
          id: 2,
          text: '运动',
        },
        {
          id: 3,
          text: '服饰',
        },
        {
          id: 4,
          text: '图书',
        },
      ]
    }
  },
}
</script>

<style scoped>

</style>
