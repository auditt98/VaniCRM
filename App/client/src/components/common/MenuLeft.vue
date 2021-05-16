<template>
  <div>
    <div class="menu-left">
      <ul class="list-group" v-if="elements">
        <li class="list-group-item font-weight-bold" style="padding-bottom: 0 !important; margin-bottom: 10px !important">Jump to</li>
        <li v-for="(e, i) in elements" :key="i" @click="scrollToElement(e.id)" :class="{'text-danger' : (activeMenu && e.id === activeMenu)}" class="list-group-item">{{e.text}}</li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "MenuLeft",
  props: {
    elements: Array
  },
  data() {
    return {
      activeMenu: null
    }
  },
  methods: {
   scrollToElement(element) {
     this.activeMenu = element;
     this.$emit('scroll-to', element);
   }
  },
  created() {
    this.activeMenu = null;
  }
}
</script>

<style scoped>

.menu-left {
  background: #FFFFFF;
  box-shadow: 4px 4px 24px rgba(0, 0, 0, 0.16);
  border-radius: 30px;
  overflow: hidden;
  max-width: 13rem;
  position: fixed;

  width: 100%;
  z-index: 99;
}

.menu-left .list-group .list-group-item {
  padding-top: 0.75rem !important;
  padding-bottom: 0.3rem !important;
}

.menu-left .list-group-item {
  border: none;
  padding-left: 45px;
  margin-bottom: 20px;
  cursor: pointer;
}
.text-danger {
  font-weight: bold;
}
</style>