<template>

  <nav class="navbar navbar-light navbar-expand-lg header sticky-top">
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
            aria-controls="#navbarNav" aria-expanded="false" aria-label="Toggle navigation" style="color: black;">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="nav-item mx-auto order-last">
      <div class="header-avatar-icon">
        <router-link active-class="active" :to="{ name: 'Calendar'}" class="nav-link"> <i class="fa fa-calendar-o" aria-hidden="true"></i> </router-link>
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
<!--    -->
    <div class="nav-item mx-auto order-last dropdown"  v-if="[3].indexOf(currentUser.group) > -1">
      <a class="nav-link dropdown-toggle ml-3 mr-0" href="#" id="navbarDropdown9" role="button" data-toggle="dropdown"
         aria-haspopup="true" aria-expanded="false">
        <img src="../../assets/icon-settings.png" alt="">
      </a>
      <div class="dropdown-menu" aria-labelledby="navbarDropdown9">
        <router-link :to="{ name: 'UserList'}" class="nav-link">User</router-link>
        <router-link :to="{ name: 'GroupList'}" class="nav-link">Group</router-link>
      </div>
    </div>
<!--    -->
    <div class="nav-item mx-auto order-last dropdown">
      <a v-if="currentUser" class="nav-link dropdown-toggle mx-0" href="#" id="navbarDropdown2" role="button" data-toggle="dropdown"
         aria-haspopup="true" aria-expanded="false">
        {{currentUser.username}}
      </a>
      <div class="dropdown-menu" aria-labelledby="navbarDropdown" v-if="currentUser">
        <router-link :to="{ name: 'UserPage', query: {id: currentUser.id}}" class="dropdown-item">View Profile</router-link>
        <a class="dropdown-item" @click="logout()" style="cursor: pointer;">Logout</a>

      </div>
    </div>
    <div class="nav-item mx-auto order-last" v-if="currentUser">
      <img class="header-avatar m-auto" v-bind:src="currentUser.avatar">


    </div>
    <div class="collapse navbar-collapse" id="navbarNav">
      <div> <img style="width:80px; height:80px" src="../../assets/CRM-01.png" alt=""></div>
      <ul class="navbar-nav">
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="javascript:void(0)" :class="{ 'active': isActive }" id="navbarDropdown1" role="button" data-toggle="dropdown"
             aria-haspopup="true" aria-expanded="false">
            Dashboard
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown1" v-if="currentUser">
            <router-link v-if="[3, 2].indexOf(currentUser.group) > -1" :to="{ name: 'DashboardSale'}" class="nav-link">Dashboard Sale</router-link>
            <router-link v-if="[3, 1].indexOf(currentUser.group) > -1" :to="{ name: 'DashboardMarketing'}" class="nav-link">Dashboard Marketing</router-link>
          </div>
        </li>
        <li v-if="userRoleId !== 2"  class="nav-item">
          <router-link active-class="active" :to="{ name: 'LeadList'}" class="nav-link">Leads</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'ContactList'}" class="nav-link">Contacts</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'AccountList'}" class="nav-link">Accounts</router-link>
        </li>
        <li v-if="userRoleId !== 1" class="nav-item">
          <router-link active-class="active" :to="{ name: 'DealList'}" class="nav-link">Deals</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'CampaignList'}" class="nav-link">Campaign</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'TaskList'}" class="nav-link">Tasks</router-link>
        </li>
        <li class="nav-item">
          <router-link active-class="active" :to="{ name: 'Report'}" class="nav-link">Reports</router-link>
        </li>
      </ul>
    </div>
  </nav>

</template>

<script>
import {authenticationService} from "@/service/authentication.service";
import {config} from "@/config/config";
import { hubConnection } from 'signalr-no-jquery';
import axios from 'axios';
import Sugar from 'sugar'
Sugar.extend();

var connection = hubConnection(`${config.apiUrl + 'signalr'}`, { useDefaultPath: false});
var proxy = connection.createHubProxy('notificationHub');
import notiSfx from "@/assets/notification.mp3"


const api = `${config.apiUrl + 'notifications'}`;

export default {
  name: "Header",
  data() {
    return {
      currentUser: null,
      notifications: [],
      page: 1,
      pageSize: 10,
      unreadCount: 0,
      isDashboard: false,
      sound: notiSfx,
    }
  },
  methods: {
    playNotiSound(){
      let s = new Audio(this.sound)
      s.addEventListener("canplaythrough", () =>{
        s.play();
      })
    },
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
        this.$router.push({ path: notification.module + '/detail', query: { id: notification.moduleObjectId } }).catch(() =>{});
      }
    },
    logout() {
          proxy.invoke('Leave', authenticationService.currentUserValue.id).done()
          .fail(function (error) {
            console.log('Error: ' + error);
          });
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
  created() {
    this.currentUser = authenticationService.currentUserValue;
      proxy.on('reloadDashboardSale', () =>{
        if(this.$route.name === 'DashboardSale'){
          this.$root.$refs.Dashboard.loadListDashboardSale();
        }
      })
      proxy.on('getUnreadCount', (count) =>{
        this.unreadCount = count;
      });
      proxy.on('pushNotification', (notification) =>{
          this.playNotiSound()
          this.$notify({
            group: 'custom-template',
            title: notification.title,
            text:  notification.content,
            type:  notification.type,
            });
          this.notifications.unshift(notification);
          this.unreadCount++;
          // if(notification.module == "deals"){
          //   this.$root.$refs.Dashboard.loadListDashboardSale();
          // }
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
  },
  computed: {
    isActive() {
      return this.$route.name === 'DashboardSale' || this.$route.name === 'DashboardMarketing';
    },
    userRoleId() {
      return JSON.parse(window.localStorage.getItem('currentUser')).group;
    }
  },


}
</script>

<style scoped>

.nav-link{
  padding-left: 5px !important;
  
}

.header {
  height: 80px;
  backdrop-filter: blur(10px);
  background-color: rgba(255, 255, 255,0.87);
  box-shadow: 0px -8px 10px rgba(255, 255, 255, 0.5), 0px 7px 24px rgba(55, 71, 79, 0.08);
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

.active i {
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
  /* background: url("../../assets/default_avatar.png"); */
}

.header-avatar {
  width: 44px;
  height: 44px;
  border: 1.5px solid #DFE0EB;
  /* background: url("../../assets/default_avatar.png"); */
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
  margin-top: 10px;
  margin-bottom: 10px;
  /* background: transparent; */

}

.notification:hover{
  background-color: #FBFAFA;
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
.dropdown-menu a {
  font-size: 15px !important;
  margin: 0 !important;
}
#navbarDropdown9::after {
  display: none;
}
</style>
