<template>
    <div>
    <!-- <VuePerfectScrollbar class="scroll-area" v-once :settings="settings"> -->
        <BaseAppBar/>
        <v-navigation-drawer permanent absolute>
        <v-list-item two-line>
                <v-list-item-avatar height="50px" width="50px" style="border: 0.5px solid rgba(0,0,0,0.25)">
                    <v-img src="../../assets/logo_3.png"></v-img>
                </v-list-item-avatar>
                <v-list-item-content>
                    <!-- <v-list-item-icon><v-icon>mdi-web</v-icon></v-list-item-icon> -->
                    <div>
                        <v-list-item-title class="d-inline mr-4">
                            Trịnh Việt Anh
                        </v-list-item-title>
                        <v-btn icon text> 
                            <v-icon dense>mdi-tune-vertical-variant</v-icon> 
                        </v-btn>
                    </div>

                    <!-- <v-spacer></v-spacer> -->
                    <v-list-item-subtitle class="ml-50">Quản trị viên

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