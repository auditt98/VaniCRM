<template>

<nav class="navbar navbar-light navbar-expand-lg header sticky-top">
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="#navbarNav" aria-expanded="false" aria-label="Toggle navigation" style="color: black;">
    <span class="navbar-toggler-icon"></span>
    </button>
    <div class="nav-item mx-auto order-last" @click="alert('123');">
      <div class="header-avatar-icon">
        <i class="fa fa-calendar-o" aria-hidden="true"></i>
      </div>
    </div>
    <div class="nav-item mx-auto order-last">
      <div class="header-avatar-icon">
        <i class="fa fa-bell-o" aria-hidden="true"></i>
      </div>
    </div>
    <div class="nav-item dropdown mx-auto order-last">
      <div class="dropdown-menu" aria-labelledby="navbarDropdown2">
        <a class="dropdown-item" href="#">View profile</a>
        <a class="dropdown-item" href="#">Logout</a>
      </div>
    </div>
    <div class="nav-item mx-auto order-last dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown2" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Admin
              </a>
              <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                <a class="dropdown-item" href="#">View profile</a>
                <a class="dropdown-item" @click="logout();" style="cursor: pointer;">Logout</a>
                <!-- <div class="dropdown-divider"></div> -->

              </div>
    </div>
    <div class="nav-item mx-auto order-last" >
      <img class="m-auto header-avatar" src="https://localhost:44375/avatar?fileName=Mojave_Day-ce5b52f8d97a4817a4e26510f7a3548f.jpg">
    </div>
  <div class="collapse navbar-collapse" id="navbarNav">
    <ul class="navbar-nav">
            <li class="nav-item active">
              <a class="nav-link" href="/dashboard">Dashboard</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/lead-list">Leads</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/contact-list">Contacts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/account-list">Accounts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/deal-list">Deals</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/campaign-list">Campaigns</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/task-list">Tasks</a>
            </li>
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Configs
              </a>
              <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                <a class="dropdown-item" href="/user-list">Users</a>
                <a class="dropdown-item" href="/group-list">Groups</a>
              </div>
            </li>
          </ul>
  </div>
</nav>
  <!-- <div class="row header justify-content-between mx-0">
    <div class="col-sm-8 px-0">
      <nav class="navbar navbar-expand-lg">
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item active">
              <a class="nav-link" href="/dashboard">Dashboard</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/leads">Leads</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/contacts">Contacts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/accounts">Accounts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/deals">Deals</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/campaigns">Campaigns</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/tasks">Tasks</a>
            </li>
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Dropdown
              </a>
              <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                <a class="dropdown-item" href="#">Action</a>
                <a class="dropdown-item" href="#">Another action</a>
                <div class="dropdown-divider"></div>
                <a class="dropdown-item" href="#">Something else here</a>
              </div>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/users">Users</a>
            </li>
            <li class="nav-item">
              <button type="button" class="btn btn-danger" @click="logout()">Logout</button>
            </li>
          </ul>
        </div>
    
      </nav>
    </div>
      <div class="col-sm-3 justify-content-end px-0">
      <div class="col-sm-5 m-auto text-right header-avatar-icon">
        <i class="fa fa-calendar-o" aria-hidden="true"></i>
        <i class="fa fa-bell-o" aria-hidden="true"></i>
        <a href="">Admin</a>
      </div>
      <div class="header-avatar col-sm-4 m-auto p-0">
        <div></div>
      </div>
    </div>
  </div> -->

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
  /* height: 80px; */
  box-shadow: 0px -8px 10px rgba(255, 255, 255, 0.5), 0px 16px 24px rgba(55, 71, 79, 0.2);
  /* overflow: hidden; */
  margin-bottom: 2px;
  background: white;
}
.header .nav-link {
  color: #000000;
  font-weight: normal;
  font-size: 20px;
  line-height: 29px;
  margin-left: 20px;
  margin-right: 20px;
}
.nav-item.active a {
  color: #D93915;
}
.header-avatar {
  text-align: center;
  text-align: -webkit-center;
}
/* .header-avatar div {
  width: 44px;
  height: 44px;
  border: 1.5px solid #DFE0EB;
  background: url("../../assets/avatar-header.jpeg");
} */

.header-avatar {
  width: 44px;
  height: 44px;
  border: 1.5px solid #DFE0EB;
}

.header-avatar-icon {
display:inline-block;
    font-size: 18px;
    line-height: 50px;
    width: 50px;
    height: 50px;
    text-align: center;
    vertical-align: center;
    color: black;
}
i {
  color: black;
}
</style>