<template>
  <div>
    <b-container>
      <MainCarousel></MainCarousel>
      <h1>首页</h1>
    </b-container>
  </div>
</template>

<script>
import MainCarousel from "@/components/MainCarousel";
export default {
  name: 'Home',
  components: {
    MainCarousel
  }
}
</script>
