<template>

  <el-menu :default-active="currentIndex" mode="horizontal" @select="handleSelect"
           background-color="#F9F7E8"
           text-color="#1b1d1c"
           active-text-color="#e88565">
    <el-menu-item style="width: 450px">
      <router-link to="/">
        <img src="../assets/小羊租赁.png" alt="" style="width: 45%;">
      </router-link>
    </el-menu-item>
    <el-menu-item index="UserManagement">用户管理</el-menu-item>
    <el-menu-item index="StudentManagement">学生信息管理</el-menu-item>
    <el-menu-item index="CategoryManagement">商品分类管理</el-menu-item>
    <el-menu-item index="ProductManagement">商品管理</el-menu-item>
    <el-menu-item index="DemandManagement">需求管理</el-menu-item>
    <el-menu-item index="OrderManagement">订单管理</el-menu-item>
  </el-menu>

</template>

<script>
export default {
  name: "AdminNavBar",
  data() {
    return {
      currentIndex: "1"
    }
  },
  methods: {
    handleSelect(index) {
      if (this.currentIndex !== index) {
        this.$router.push({name: index})
        this.currentIndex = index
      }
    }
  },
}
</script>

<style scoped>

</style>