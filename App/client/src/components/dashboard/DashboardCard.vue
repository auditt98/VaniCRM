<template>
  <div>
    <div class="card dashboard-card mb-3">
      <div class="card-body">
        <h5 class="card-title">{{data.title}}</h5>
        <p class="card-text">{{data.accountName}}</p>
        <p class="card-text">{{data.ownerUsername}}</p>
        <span v-if="data.tags" class="d-flex flex-wrap">
          <a v-for="tag in data.tags" :key="tag.tagID" class="col" href="javascript:void(0)" >{{tag.tagName}}</a>
        </span>
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
  color: white;
  /*max-width: 70px;*/
  font-size: 11px;
  padding: 4px 12px;
  margin-right: 3px;
  margin-top: 3px;
  text-align: center;
}


</style>