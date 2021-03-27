<template>
    <div>
        <BaseAppBar/>
        <!-- <VuePerfectScrollbar class="scroll-area" v-once :settings="settings"> --> 
        <v-main fill-height style="background-color: rgb(243,244,247); padding: 0; margin-top: 10px;">

            <v-text-field label="First name" id="content" ref="content" v-model="content" @change="setContent();" required></v-text-field>
            <label>
                File Upload
                <input type="file" id="files" ref="files" multiple @change="handleFileUpload()"/>

                <button class="myButton" @click="submitFile()">Submit</button>
            </label>

            <button class="myButton" @click="downloadRequest()">Download</button>
        </v-main>
    </div>
</template>

<style>
.myButton {
	box-shadow: -1px 4px 26px -8px #575757;
	background-color:#d93915;
	border-radius:8px;
	cursor:pointer;
	color:#ffffff;
	font-size:16px;
	padding:13px 21px;
	text-decoration:none;
}
.myButton:hover {
	background-color:#bd2300;
}
.myButton:active {
	position:relative;
	top:1px;
}

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
            files: [],
            content: ''
        }
    },
    // created() {
    //     axios.get('https://localhost:44375/test/1004').then(res =>{
    //         this.img = res.data
    //         console.log(this.img)
    //     })
    // },
    methods: {
        handleFileUpload(){
            this.files = this.$refs.files.files;
        },
        setContent(){
            // this.content = this.$refs["content"].value;
            console.log(this.content)
        },
        downloadRequest(){
            let url = "https://localhost:44375/test/file_download"; 
            window.location.href  = url;
            axios({
                method:'get',
                url: 'https://localhost:44375/test/file_download',
                response_type: "blob"
            }).then(res => {
                var content = res.data;
                var blob = new Blob([content]);
                var fileName = "test.docx";
                if ("download" in document.createElement("a")) {
                     // non-IE download
                    var elink = document.createElement("a");
                    elink.download = fileName;
                    elink.style.display = "none";
                    elink.href = URL.createObjectURL(blob);
                    document.body.appendChild(elink);
                    elink.click();
                                URL.revokeObjectURL(elink.href); // Release the URL object
                    document.body.removeChild(elink);
                    } else {
                                // IE10+ download
                    navigator.msSaveBlob(blob, fileName);
                    }
                    console.log(res);
            })
        },
        submitFile(){
            let formData = new FormData();
            formData.append("content", this.content);
            for(var i = 0; i < this.files.length; i++){
                let file = this.files[i];
                formData.append('files[' + i + ']', file);
            }
            // formData.append('file', this.file)
            // console.log(formData)
            axios.post('https://localhost:44375/test/multiple_files_upload', formData, {headers: {
                "Content-Type": "multipart/form-data"
            }})
            .then( res => {
                console.log(res);
            })
        }
    }
}
</script>