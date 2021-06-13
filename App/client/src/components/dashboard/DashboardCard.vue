<template>
  <div>
    <div class="card dashboard-card mb-3">
      <div class="card-body">
        <h5 class="card-title">
          <router-link :to="{name: data.routeName, query: {id: data.id}}">
          {{data.title}}
          </router-link>
        </h5>
        <p class="card-text">{{data.title1}}</p>
        <p class="card-text">{{data.title2}}</p>
        <p v-if="data.expectedRevenue >=0" class="card-text">{{data.expectedRevenue}}đ</p>
        <p v-if="data.companyName" class="card-text">{{data.companyName}}</p>
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
.dashboard-card span a {
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
 a{
   color: black;
 }

</style>