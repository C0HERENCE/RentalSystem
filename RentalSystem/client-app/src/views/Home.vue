<template>
  <div>
    <MainCarousel></MainCarousel>
    <b-container>
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
