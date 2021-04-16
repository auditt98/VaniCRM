<template>
  <div>
    <div class="menu-left">
      <ul class="list-group" v-if="elements">
        <li class="list-group-item font-weight-bold">Jump to</li>
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
  max-width: 270px;
  position: fixed;
  top: 17%;
  width: 100%;
}

.menu-left .list-group {

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