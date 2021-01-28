<template>
    <div>
    <!-- <VuePerfectScrollbar class="scroll-area" v-once :settings="settings"> -->
        <BaseAppBar/>
        <v-navigation-drawer permanent app>
        <v-list-item two-line>
                <v-list-item-avatar height="2.25rem" width="2.25rem">
                    <v-img src="https://cdn.vuetifyjs.com/images/john.png"></v-img>
                </v-list-item-avatar>
                <v-list-item-content>
                    <!-- <v-list-item-icon><v-icon>mdi-web</v-icon></v-list-item-icon> -->
                    <v-list-item-title class="title">
                        Trịnh Việt Anh         

                    </v-list-item-title>
                    <!-- <v-spacer></v-spacer> -->
                    <v-list-item-subtitle>Quản trị viên
                        <v-btn icon text> 
                            <v-icon >mdi-dots-vertical</v-icon> 
                        </v-btn>
                    </v-list-item-subtitle>

                </v-list-item-content>
                
        </v-list-item>
        <v-list rounded>
            <v-subheader>Demo</v-subheader>
            <v-list-item>
                <v-list-item-icon>
                    <v-icon>mdi-account-plus</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                    <v-list-item-title>Title</v-list-item-title>
                </v-list-item-content>
            </v-list-item>            
            <v-list-item>
                <v-list-item-icon>
                    <v-icon>mdi-account-plus</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                    <v-list-item-title>Title</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-list-item>
                <v-list-item-icon>
                    <v-icon>mdi-account-plus</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                    <v-list-item-title>Title</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list>
        <v-list rounded>
            <v-subheader>Demo</v-subheader>
            <v-list-item>
                <v-list-item-icon>
                    <v-icon>mdi-account-plus</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                    <v-list-item-title>Title</v-list-item-title>
                </v-list-item-content>
            </v-list-item>            
            <v-list-group prepend-icon="mdi-account-circle">
                <template v-slot:activator>
                    <v-list-item-title>Users</v-list-item-title>
                </template>
                <v-list-group no-action sub-group>
                    <template v-slot:activator>
                        <v-list-item-content>
                            <v-list-item-title>Admin</v-list-item-title>
                        </v-list-item-content>
                    </template>
                </v-list-group>
            </v-list-group>
        </v-list>
        </v-navigation-drawer>
        <v-main fill-height style="background-color: rgb(243,244,247)">

        <!-- Nested route -->
            <!-- <router-view></router-view> -->
        </v-main>
        <!-- </VuePerfectScrollbar> -->
    </div>

</template>

<script>
// import VuePerfectScrollbar from 'vue-perfect-scrollbar'
export default {
    components: {
        // VuePerfectScrollbar
    },  
    data() {
        return {
            // settings: {
            //     maxScrollbarLength: 60
            // }
        }
    },
}
</script>

<style>
.scroll-area {
  position: relative;
  margin: auto;
  width: 400px;
  height: 300px;
}
</style>