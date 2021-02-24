<template>
  <div class="main-container">
    <div class="upper-part">
      <TopNavbar></TopNavbar>
      <router-view></router-view>
    </div>
    <Footer class="footer"></Footer>
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
.main-container
{
  min-height: 100vh;
  position: relative;
}
.upper-part
{
  padding-bottom:4em;
}
.footer
{
  position: absolute;
  bottom: 0;
  height: 4em;
  width:100%
}
</style>