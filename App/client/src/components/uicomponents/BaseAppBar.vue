<template>
  <div>
    <v-app-bar app color="white" light class="bottom-border shadow">
      <div class="d-flex align-center">
          <router-link to="/dashboard" tag="span" style="cursor: pointer" class="app-bar-text ml-6 mr-8">
            <h4 class="app-bar-text">Dashboard</h4>
          </router-link>
          <router-link to="/leads" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Leads</h4>
          </router-link>
          <router-link to="/contacts" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Contacts</h4>
          </router-link>
          <router-link to="/accounts" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Accounts</h4>
          </router-link>
          <router-link to="/deals" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Deals</h4>
          </router-link>
          <router-link to="/campaigns" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Campaigns</h4>
          </router-link>
          <router-link to="/tasks" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Tasks</h4>
          </router-link>
          <router-link to="/reports" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Reports</h4>
          </router-link>
          <router-link to="/tests" tag="span" style="cursor: pointer" class="app-bar-text ma-6">
            <h4 class="app-bar-text">Dev Tests</h4>
          </router-link>
      </div>
      <v-spacer></v-spacer>

      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            icon
            text
            v-bind="attrs"
            v-on="on"
            class="mr-2"
          >
            <v-icon>mdi-bell-outline</v-icon>
          </v-btn>
        </template>
        <span>Notifications</span>
      </v-tooltip>

      <!-- <v-btn href="https://github.com/vuetifyjs/vuetify/releases/latest" target="_blank" text>
        <span class="mr-2">Latest Release</span>
        <v-icon>mdi-open-in-new</v-icon>
      </v-btn> -->
    </v-app-bar>
    <v-divider></v-divider>
    </div>
</template>

<script>
export default {
    
}
</script>

<style>
  .bottom-border{
    border-bottom: 0.5px solid gray !important;
  }
  .shadow{
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
  }
  
</style>