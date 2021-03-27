<template>
  <button class="btn " :class="data.btnClass">
    <i v-if="data.icon" class="fa mr-2" :class="data.icon"></i>
    {{data.text}}
  </button>
</template>

<script>
export default {
  name: "VButton",
  props: {
    data: Object
  }
}
</script>

<style scoped>

</style>