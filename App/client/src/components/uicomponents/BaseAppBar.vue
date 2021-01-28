<template>
  <div>
    <v-app-bar app color="white" light elevation="0" class="bottom-border">
      <div class="d-flex align-center">
          <router-link to="/" tag="span" style="cursor: pointer">
            <v-img
              alt="VaniCRM Logo"
              class="shrink mr-2"
              contain
              src="../../assets/logo_3.png"
              transition="scale-transition"
              width="60"
              height="40"
            />
          </router-link>

          <v-toolbar-title>
            <router-link to="/" tag="span" style="cursor: pointer">
              <h2>VaniCRM</h2>
            </router-link>
          </v-toolbar-title>
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
            <v-icon>mdi-web</v-icon>
          </v-btn>
        </template>
        <span>Language</span>
      </v-tooltip>
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
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            icon
            text
            v-bind="attrs"
            v-on="on"
            class="mr-6"
          >
            <v-icon>mdi-theme-light-dark</v-icon>
          </v-btn>
        </template>
        <span>Switch theme</span>
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
</style>