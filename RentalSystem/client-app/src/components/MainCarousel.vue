<template>
  <div>
    <b-carousel
        :interval="3000"
        controls
        indicators
        background="#ababab"
        img-width="1024"
        img-height="480"
    >
      <b-carousel-slide class="image" img-src="https://picsum.photos/1024/480/?image=51"></b-carousel-slide>
      <b-carousel-slide class="image" img-src="https://picsum.photos/1024/480/?image=52"></b-carousel-slide>
    </b-carousel>
  </div>
</template>

<script>
export default {
  name: 'MainCarousel',
}
</script>

<style scoped>
.image{
  height: 480px;
}
</style>