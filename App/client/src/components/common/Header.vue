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
    <div class="nav-item mx-auto order-last notificationButton" style="position: relative" v-popover:notificationPanel >
      <popover id="notificationPanel" name="notificationPanel">
        <div class="notificationPanelHeader"><span>Notifications</span></div>
        <div class="notificationPanelContent">
          <div class="notificationGroup">
            <span class="notificationGroupTag">Today</span>
            <div class="notification">Hello</div>
            <div class="notification"></div>
            <div class="notification"></div>
            <div class="notification"></div>
            <div class="notification"></div>
            <div class="notification">Hello</div>
            <div class="notification"></div>
            <div class="notification"></div>
            <div class="notification"></div>
            <div class="notification"></div>
          </div>
          <div class="notificationGroup">
            <span class="notificationGroupTag">Previous</span>

          </div>
          
         

        </div>
      </popover>
      <div class="header-avatar-icon" >
        <i class="fa fa-bell-o notificationIcon" aria-hidden="true"></i>
      </div>
      
    </div>
      

    <div class="nav-item mx-auto order-last dropdown">
      <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown2" role="button" data-toggle="dropdown"
         aria-haspopup="true" aria-expanded="false">
        Admin
      </a>
      <div class="dropdown-menu" aria-labelledby="navbarDropdown">
        <a class="dropdown-item" :href="'/user-page?id=' + currentUser.id" >View profile</a>
        <a class="dropdown-item" @click="logout()" style="cursor: pointer;">Logout</a>

      </div>
    </div>
    <div class="nav-item mx-auto order-last">
      <img class="header-avatar m-auto" v-bind:src="currentUser.avatar">
        <!-- <img v-bind:src="currentUser.avatar"> -->

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
        <!-- <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'MeetingCreate'}" class="nav-link">Meeting</router-link>
        </li> -->
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'CampaignList'}" class="nav-link">Campaigns</router-link>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown"
             aria-haspopup="true" aria-expanded="false">
            Tasks
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <router-link active-class="active" :to="{ name: 'TaskCreate'}" class="dropdown-item">Task</router-link>
            <router-link active-class="active" :to="{ name: 'MeetingCreate'}" class="dropdown-item">Meeting</router-link>
            <router-link active-class="active" :to="{ name: 'CallCreate'}" class="dropdown-item">Call</router-link>
          </div>
        </li>
        <!-- <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'TaskList'}" class="nav-link">Tasks</router-link>
        </li> -->
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
// console.log(authenticationService.currentUserValue)
import { hubConnection } from 'signalr-no-jquery';
// if(authenticationService)

var connection = hubConnection("https://localhost:44375/signalr", { useDefaultPath: false});
  var proxy = connection.createHubProxy('notificationHub');
  proxy.on('pushNotifications', function(notifications){
    console.log(notifications)
  })
  connection.start()
    .done(function(){ 
      console.log('Now connected, connection ID=' + connection.id);  
      proxy.invoke('Join', 1004).done(function () {
        console.log ('Invocation of join succeeded');
    }).fail(function (error) {
        console.log('Error: ' + error);
    });
    })
    .fail(function(){ console.log('Could not connect'); });

export default {
  name: "Header",
  data: function(){
    return {
      currentUser: authenticationService.currentUserValue
    }
  },
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
  /* background: url("../../assets/avatar-header.jpeg"); */
}

.header-avatar {
  width: 44px;
  height: 44px;
  border: 1.5px solid #DFE0EB;
  /* background: url("../../assets/avatar-header.jpeg"); */
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

.notificationIcon:hover{
  color: #D93915;
}

.notificationButton{
  border-radius: 50%;
}

.notificationButton:hover{
  background-color: RGBA(217,57,21,0.2)

}

.notification{
  height:30px;
}

.notificationPanelHeader{
  /* position: sticky;
  top: 0; */
  background-color: white;
  text-align: center;
  height: 40px;
  line-height: 38px;
  vertical-align: middle;

}

.notificationPanelHeader span{
  font-weight: bold;
  vertical-align: middle;
  font-family: "Segoe UI";
  display: inline-block;
  vertical-align: middle;
  line-height: normal;
  font-size: 22px;
}

.notificationGroupTag{
  font-size: 20px;
  line-height: normal;
    font-family: "Segoe UI";
    font-weight: bold;
}

#notificationPanel{
  padding: 0 !important;
  overflow:auto;
  position:absolute !important; 
  top: 60px !important; 
  left: -80px !important;
  height: 80vh !important;
  width: 300px !important;
  background-color: white;
}

</style>