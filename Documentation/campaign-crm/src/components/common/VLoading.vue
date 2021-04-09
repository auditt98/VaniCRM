<template>
<div>
  <div class="loading" v-if="loading">
    <div class="lds-facebook"><div></div><div></div><div></div></div>
  </div>
</div>
</template>

<script>
export default {
  name: "VLoading",
  props: {
    loading: Boolean
  }
}
</script>

<style scoped>
.loading {
  position: fixed;
  z-index: 999;
  overflow: visible;
  margin: auto;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background: #ccc;
  opacity: 0.3;

}
.lds-facebook {
  position: relative;
  width: 80px;
  height: 80px;
  margin: 10% auto;
}
.lds-facebook div {
  display: inline-block;
  position: absolute;
  left: 8px;
  width: 16px;
  background: red;
  animation: lds-facebook 1.2s cubic-bezier(0, 0.5, 0.5, 1) infinite;
}
.lds-facebook div:nth-child(1) {
  left: 8px;
  animation-delay: -0.24s;
}
.lds-facebook div:nth-child(2) {
  left: 32px;
  animation-delay: -0.12s;
}
.lds-facebook div:nth-child(3) {
  left: 56px;
  animation-delay: 0;
}
@keyframes lds-facebook {
  0% {
    top: 8px;
    height: 64px;
  }
  50%, 100% {
    top: 24px;
    height: 32px;
  }
}
</style>