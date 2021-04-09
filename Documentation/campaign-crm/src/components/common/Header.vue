<template>

  <nav class="navbar navbar-light navbar-expand-lg header sticky-top">
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
            aria-controls="#navbarNav" aria-expanded="false" aria-label="Toggle navigation" style="color: black;">
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
    <div class="nav-item mx-auto order-last dropdown">
      <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown2" role="button" data-toggle="dropdown"
         aria-haspopup="true" aria-expanded="false">
        Admin
      </a>
      <div class="dropdown-menu" aria-labelledby="navbarDropdown">
        <a class="dropdown-item" href="#">View profile</a>
        <a class="dropdown-item" @click="logout()" style="cursor: pointer;">Logout</a>

      </div>
    </div>
    <div class="nav-item mx-auto order-last">
      <div class="header-avatar m-auto">
        <div></div>
      </div>
    </div>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav">
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown1" role="button" data-toggle="dropdown"
             aria-haspopup="true" aria-expanded="false">
            Dashboard
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown1">
            <router-link active-class="active" :to="{ name: 'Dashboard'}" class="dropdown-item">Dashboard Sale</router-link>
            <router-link active-class="active" :to="{ name: 'Dashboard'}" class="dropdown-item">Dashboard Marketing</router-link>
          </div>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'LeadList'}" class="nav-link">Leads</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'ContactList'}" class="nav-link">Contacts</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'AccountList'}" class="nav-link">Accounts</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'MeetingCreate'}" class="nav-link">Meeting</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'CampaignList'}" class="nav-link">Campaigns</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'TaskList'}" class="nav-link">Tasks</router-link>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown"
             aria-haspopup="true" aria-expanded="false">
            Configs
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <router-link active-class="active" :to="{ name: 'Users'}" class="dropdown-item">Users</router-link>
            <router-link active-class="active" :to="{ name: 'GroupList'}" class="dropdown-item">Groups</router-link>
            <router-link active-class="active" :to="{ name: 'CallCreate'}" class="dropdown-item">Call</router-link>
          </div>
        </li>
      </ul>
    </div>
  </nav>

</template>

<script>
import {authenticationService} from "@/service/authentication.service";

export default {
  name: "Header",
  methods: {
    logout() {
      authenticationService.logout(null);
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

.nav-item a.active {
  color: #D93915 !important;
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
  display: inline-block;
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