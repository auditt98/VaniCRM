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
        <div class="notificationPanelHeader"><h2>Notifications</h2></div>
        <div class="notificationPanelContent">
            <div class="notification" v-for="notification in this.notifications" :key="notification.id">
              <div v-on:click="goToNotification(notification)">
                <h6 style="color: rgba(0,0,0,0.7); font-weight:bold;">{{notification.title}}</h6>
                {{notification.content}}
                <div class="circle" v-if="notification.isRead == false"></div>
                <div class="notificationTimestamp" style="margin-top:5px;"><span v-bind:class="{ newNotification: notification.isRead == false,  }">{{Date.create(notification.createdAt).relative('en', () => {}) }}</span></div>
              </div>
            </div>
            <infinite-loading @infinite="infiniteHandler"></infinite-loading>
          <!-- <div class="notificationGroup">
            <h4 class="notificationGroupTag">All</h4>
          </div> -->
        </div>
      </popover>
      <div class="header-avatar-icon" >
        <i class="fa fa-bell-o notificationIcon has-badge" aria-hidden="true"></i><span class="badge badge-main">{{unreadCount}}</span>
      </div>
    </div>
    <div class="nav-item mx-auto order-last dropdown">
      <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown2" role="button" data-toggle="dropdown"
         aria-haspopup="true" aria-expanded="false">
        {{currentUser.username}}
      </a>
      <div class="dropdown-menu" aria-labelledby="navbarDropdown">
        <a class="dropdown-item" href="#">View profile</a>
        <a class="dropdown-item" @click="logout()" style="cursor: pointer;">Logout</a>

      </div>
    </div>
    <div class="nav-item mx-auto order-last">
      <img class="header-avatar m-auto" v-bind:src="currentUser.avatar">


    </div>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav">
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle"  :class="{'active': isDashboard}" href="#" id="navbarDropdown1" role="button" data-toggle="dropdown"
             aria-haspopup="true" aria-expanded="false">
            Dashboard
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown1">
            <a href="/dashboard-sale" class="dropdown-item">Dashboard Sale</a>
            <a href="/dashboard-marketing" class="dropdown-item">Dashboard Marketing</a>
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
import { hubConnection } from 'signalr-no-jquery';
import axios from 'axios';
import Sugar from 'sugar'
Sugar.extend();

var connection = hubConnection("https://localhost:44375/signalr", { useDefaultPath: false});
var proxy = connection.createHubProxy('notificationHub');

const api = "https://localhost:44375/notifications";

export default {
  name: "Header",
  data() {
    return {
      currentUser: authenticationService.currentUserValue,
      notifications: [],
      page: 1,
      pageSize: 10,
      unreadCount: 0,
      isDashboard: false,
    }
  },
  methods: {
    goToNotification(notification){
      //mark as read
      if(notification.isRead == false){
        axios.get(api + "/read", {
          params:{
            notificationId: notification.id,
            userId: authenticationService.currentUserValue.id,
          }
        }).then(({data}) =>{
          if(data.status=="success"){
            var noti = this.notifications.find(x => x.id === notification.id)
            noti.isRead = true;
          }
        })
      }
      //go to module if exist
      if(notification.module != null){
        this.$router.push({ path: notification.module + '-detail', query: { id: notification.moduleObjectId } }).catch(() =>{});
      }
      // console.log(notification);
    },
    logout() {
      authenticationService.logout(null);
    },
    infiniteHandler($state){
      axios.get(api, {
        params: {
          page: this.page,
          userId: authenticationService.currentUserValue.id,
        },
      }).then(({ data }) => {
        if(data.data.notifications.length > 0){
          this.unreadCount = data.data.unreadCount;
          this.page += 1;
          this.notifications.push(...data.data.notifications);
          $state.loaded();
        } else{
          $state.complete();
        }
      })
    },
  },
  mounted(){
      proxy.on('getUnreadCount', (count) =>{  
        this.unreadCount = count;
      });
      proxy.on('pushNotification', (notification) =>{
          this.$notify({
            group: 'custom-template',
            title: notification.title,
            text:  notification.content,
            type:  notification.type,
            });
      })
      connection.start()
        .done(() =>{ 
          console.log('Now connected, connection ID=' + connection.id);  
          proxy.invoke('Join', authenticationService.currentUserValue.id).done(() => {
            console.log("joined")
          }).fail(function (error) {
              console.log('Error: ' + error);
            });
        })
        .fail(function(){ console.log('Could not connect'); });
  },created() {
    if (window.location.pathname.indexOf('dashboard-marketing') > -1 || window.location.pathname.indexOf('dashboard-sale') > -1) {
      this.isDashboard = true;
    } else {
      this.isDashboard = false;
    }
  }
}
</script>

<style scoped>
.header {
  /* height: 80px; */
  box-shadow: 0px -8px 10px rgba(255, 255, 255, 0.5), 0px 7px 24px rgba(55, 71, 79, 0.2);
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

.nav-link{
  color:black !important;
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

/* .notificationIcon:hover{
  color: #D93915;
} */

.notificationButton{
  border-radius: 50%;
}

.notificationIcon:hover{
  color: RGBA(217,57,21,1)
}

.notification{
  /* height:100px; */
  height: fit-content;
  padding: 10px;
  margin-left:5px;
  background-color: white;
  border-radius: 10px;
  cursor: default;


}

.notification:hover{
  background-color: rgba(0,0,0,0.03);
}

.notificationPanelHeader{
  /* position: sticky;
  top: 0; */
  background-color: white;
  height: 40px;
  line-height: 38px;
  vertical-align: middle;
  margin-left: 8px;
  margin-right: 8px;
  margin-bottom: 20px;
}

.notificationPanelHeader h2{
  font-family: "Segoe UI";
  display: inline-block;
  vertical-align: middle;
  line-height: normal;
}

.notificationGroupTag{
  line-height: normal;
  font-family: "Segoe UI";
  margin-left: 8px;
  margin-right: 8px;
}

.notificationLink{
  text-decoration: none !important;
  cursor: default;
  color: black;
}

.circle {
  position:absolute;
  left:90%;
  background: #D93915;
  width: 12px;
  height: 12px;
  border-radius: 50%;
  display: inline-block;
}

.newNotification{
  color: #D93915;
  font-weight: bold;
}

.oldNotification{
  font-weight: bold;
}

.badge-main{
  position:absolute;
  top: 10%;
  font-family: "Segoe UI";
  color: white;
  background-color: #D93915;
  cursor: default;
  border-radius: 20px;
  width: fit-content;
}

#notificationPanel{
  padding: 0 !important;
  overflow:auto;
  position:absolute !important; 
  top: 60px !important; 
  left: -80px !important;
  height: 80vh !important;
  width: 300px !important;
  /* background-color: #FAFAFC; */
}
</style>
