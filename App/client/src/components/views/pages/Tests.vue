<template>
    <div>
    <!-- <VuePerfectScrollbar class="scroll-area" v-once :settings="settings"> -->
        <BaseAppBar/>
        
        <v-main fill-height style="background-color: rgb(243,244,247); padding: 0;">
        <v-file-input chips multiple label="File input w/ chips"></v-file-input>
        <v-file-input small-chips multiple label="File input w/ small chips"></v-file-input>
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

</style>