<template>
    <div>
    
        <BaseAppBar/>
    
    
    
        <!-- <VuePerfectScrollbar class="scroll-area" v-once :settings="settings"> -->
    
        <v-main fill-height  style="background-color: rgb(243,244,247); padding: 0;">
          

        </v-main>
    
    
    
    </div>
</template>

<style>

</style>
<script>
/* eslint-disable vue/no-unused-components */
import axios from 'axios'
axios.post('https://localhost:44375/login', 
	{ 
		email: 'admin@gmail.com', 
		password: 'admin'
	}
).then((res) => {
  console.log(res);
});
export default {
    components: {
    },
    data() {
        return {
            files: []
        }
    },
    created(){
    },

    methods:{
      
    }
}
</script>