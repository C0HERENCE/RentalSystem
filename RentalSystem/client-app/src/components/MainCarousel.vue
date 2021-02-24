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
      <b-carousel-slide class="image" :img-src="require('../assets/sample1.jpg')"></b-carousel-slide>
      <b-carousel-slide class="image" :img-src="require('../assets/sample2.jpg')"></b-carousel-slide>
    </b-carousel>
  </div>
</template>

<script>
export default {
  name: 'MainCarousel',
}
</script>