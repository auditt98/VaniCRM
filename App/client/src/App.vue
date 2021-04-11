<template>
  <div id="app">
    <Header v-if="!['ResetPassword', 'LoginPage', 'About'].includes($route.name)" />
    <router-view/>
  </div>
</template>

<style>
#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
<script>
import Header from "@/components/common/Header";
export default {
  components: {Header}
}
</script>