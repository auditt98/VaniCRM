<template>
<span class="v-tag ml-3" :style="'background-color: ' + data.bgColor">
  <img :src="require('../../assets/' + data.icon)" alt="" class="mr-1" v-if="data.icon">
  <span>{{data.text}}</span>
</span>
</template>

<script>
export default {
  name: "VTag",
  props: {
    data: Object
  }
}
</script>

<style scoped>

</style>