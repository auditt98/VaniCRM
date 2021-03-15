<template>
    <div>
    <!-- <VuePerfectScrollbar class="scroll-area" v-once :settings="settings"> -->
        <BaseAppBar/>
        
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