<template>
  <div class="row header justify-content-between mx-0">
    <div class="col-sm-8 px-0">
      <nav class="navbar navbar-expand-lg">
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item active">
              <a class="nav-link" href="#">Dashboard</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Leads</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Contacts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Accounts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Deals</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Campaigns</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Tasks</a>
            </li>
            <li class="nav-item">
              <button type="button" class="btn btn-danger" @click="logout()">Logout</button>
            </li>
          </ul>
        </div>
      </nav>
    </div>
    <div class="col-sm-3 row justify-content-end px-0">
      <div class="col-sm-5 m-auto text-right header-avatar-icon">
        <i class="fa fa-calendar-o" aria-hidden="true"></i>
        <i class="fa fa-bell-o" aria-hidden="true"></i>
        <a href="">Admin</a>
      </div>
      <div class="header-avatar col-sm-4 m-auto p-0">
        <div></div>
      </div>
    </div>
  </div>
</template>

<script>
import {authenticationService} from "@/service/authentication.service";

export default {
  name: "Header",
  methods: {
    logout() {
      authenticationService.logout();
    }
  }
}
</script>

<style scoped>
.header {
  height: 80px;
  box-shadow: 0px -8px 10px rgba(255, 255, 255, 0.5), 0px 16px 24px rgba(55, 71, 79, 0.2);
  overflow: hidden;
  margin-bottom: 2px;
  background: white;
}
.header .nav-link {
  color: #000000;
  font-weight: normal;
  font-size: 24px;
  line-height: 29px;
}
.nav-item.active a {
  color: #D93915;
}
.header-avatar {
  text-align: center;
  text-align: -webkit-center;
}
.header-avatar div {
  width: 44px;
  height: 44px;
  border: 1.5px solid #DFE0EB;
  background: url("../../assets/avatar-header.jpeg");
}
.header-avatar-icon {
  font-weight: 600;
  font-size: 18px;
  line-height: 20px;
  letter-spacing: 0.2px;
}
i {
  color: black;
}
</style>