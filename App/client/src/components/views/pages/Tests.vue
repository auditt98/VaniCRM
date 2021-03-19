<template>
    <div>
        <BaseAppBar/>
        <!-- <VuePerfectScrollbar class="scroll-area" v-once :settings="settings"> --> 
        <v-main fill-height style="background-color: rgb(243,244,247); padding: 0;">
            <!-- <label>
                Hello
                <input type="file" id="file" name="file" @change="selectedFile($event)"/>
            </label>
            <button @click="submitForm()">Upload</button> -->
            <img :src="img.data" style="height: 100px; width: 100px; border-radius: 50%;">
        </v-main>
    </div>
</template>

<style>

</style>
<script>
/* eslint-disable vue/no-unused-components */
import axios from 'axios'
// axios.post('https://localhost:44375/login', {
//     email: 'admin@gmail.com',
//     password: 'admin'
// }).then((res) => {
//     console.log(res);
// });
export default {
    components: {},
    data() {
        return {
            img: null
        }
    },
    created() {
        axios.get('https://localhost:44375/test/1004').then(res =>{
            this.img = res.data
            console.log(this.img)
        })
    },
    methods: {
        // selectedFile(event){
        //     console.log(event)
        //     this.file = event.target.files[0]
        // },
        // submitForm(){
        //     let formData = new FormData();
        //     formData.append('file', this.file)
        //     console.log(formData)
        //     axios.post('https://localhost:44375/test', formData, {headers: {
        //         "Content-Type": "multipart/form-data"
        //     }})
        //     .then( res => {
        //         console.log(res);
        //     })
        // }
    }
}
</script>