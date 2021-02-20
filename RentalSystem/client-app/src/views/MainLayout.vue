<template>
  <div>
    <TopNavbar></TopNavbar>
    <router-view></router-view>
    <Footer></Footer>
  </div>
</template>

<script>
import TopNavbar from "@/components/TopNavbar";
import Footer from "@/components/Footer";
export default {
  name: "MainLayout",
  components: {Footer, TopNavbar}
}
</script>

<style scoped>

</style>