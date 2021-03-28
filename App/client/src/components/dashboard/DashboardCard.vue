<template>
  <div>
    <div class="card dashboard-card mb-3">
      <div class="card-body">
        <h5 class="card-title">{{data.title}}</h5>
        <p class="card-text">{{data.email}}</p>
        <p class="card-text">{{data.cost}} đ</p>
        <a href="" class="px-3 py-1">TAG</a>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DashboardCard",
  props: {
    data: Object
  }
}
</script>

<style scoped>
.dashboard-card {
  background: #FFFFFF;
  box-shadow: 6px 0px 18px rgba(0, 0, 0, 0.06);
  border: none;
}
.dashboard-card:hover {
  cursor: pointer;
  background: #ccc;
}
.dashboard-card a {
  background: #F12B2C;
  border-radius: 100px;
  width: 54px;
  height: 54px;
  color: white;
  font-size: 13px;
}


</style>