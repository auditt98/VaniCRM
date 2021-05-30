<template>
  <div class="w-100 mb-5" style="position: relative">
    <VLoading :loading="loading"/>
    <div class="note p-3">
      <h4 class="ml-2"><strong>Notes</strong></h4>
      <div class="row ml-3">
        <div class="note-input">
          <label for="files"><img src="../../../assets/ghim.png" alt="" class="note-input-ghim cursor-pointer"></label>
          <input v-model="text" type="text" placeholder="Type something here...">
          <img @click="submit" src="../../../assets/send.png" alt="" class="note-input-send cursor-pointer">
          <input @change="upload" type="file" name="filefield" multiple="multiple" style="display: none" ref="files" id="files">

        </div>

      </div>
      <div class="row">
        <div class="list-file-upload mt-3 ml-2">
          <ul v-if="files && files.length > 0">
            <li v-for="(f, i) in files" @click="download(f)" class="cursor-pointer" title="Click to download" :key="i">{{f.name}}</li>
          </ul>
        </div>
      </div>
      <div class="row">
        <div class="note-comment mt-2 w-100">
          <ul >
<!--            file-->
            <li class="row mt-3" v-for="(note, i) in notes" :key="i">
              <div class="col-sm-1 ">
                <div class="note-comment-image">
                  <img v-if="note.avatar" class="w-100 h-100" :src="note.avatar" alt="">
                  <img v-else class="w-100 h-100" src="../../../assets/image2.png" alt="">
                </div>
              </div>
              <div class="col-sm-5">
                <p class="note-comment-text mb-0">{{note.body}}</p>
                <div class="note-comment-file-list row mx-0 px-0" v-if="note.files && note.files.length > 0">
                  <div class="note-comment-file-item col-3 pl-0" v-for="(f, index) in note.files" :key="index">
                      <div class="file-image">
                          <div class="file-image-header text-right">
                            <img @click="showExpandable(f.fileName)" src="../../../assets/eye-line.png" style="cursor: pointer;" alt="">

                            <a :href="f.url"><img width="37" height="37" src="../../../assets/icon-download.png" alt="" style="margin-top: -7px; margin-right: -5px"></a>
                          </div>
                          <div v-if="imgExt.includes(f.fileName.split('.').pop())">
                            <expandable-image :id="f.fileName" style="width: 100%; height: 13vh; background-color: rgba(255, 255, 255, 0.9)" :close-on-background-click='true' class="w-100" :src="'https://localhost:44375/avatar?fileName=' + f.fileName" alt="" />
                            <!-- <img style="width: 100%; height: 13vh;" :src="'https://localhost:44375/avatar?fileName=' + f.fileName" alt=""> -->
                          </div>
                          <div v-else-if="docExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/doc.png" alt="">
                          </div>
                          <div v-else-if="xmlExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/xml.png" alt="">
                          </div>
                          <div v-else-if="pdfExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/pdf.png" alt="">
                          </div>
                          <div v-else-if="xlsExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/xls.png" alt="">
                          </div>
                          <div v-else-if="pptExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/ppt.png" alt="">
                          </div>
                          <div v-else-if="sqlExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/sql.png" alt="">
                          </div>
                          <div v-else-if="txtExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/txt.png" alt="">
                          </div>
                          <div v-else-if="jsExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/js.png" alt="">
                          </div>
                          <div v-else-if="zipExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/zip.png" alt="">
                          </div>
                          <div v-else-if="htmlExt.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/html.png" alt="">
                          </div>
                          <div v-else-if="mp4Ext.includes(f.fileName.split('.').pop())">
                            <img style="width: 100%; height: 13vh;" src="../../../assets/mp4.png" alt="">
                          </div>
                          <div v-else>
                            <img style="width: 100%; height: 13vh;" src="../../../assets/file.png" alt="">
                          </div>

                          <!-- <img style="width: 100%; height: 75px;" :src="'https://localhost:44375/avatar?fileName=' + f.fileName" alt=""> -->
                      </div>
                      <div class="text-center ttip">
                        {{ trimText(f.fileName) }}
                        <span class="ttip-text">{{f.fileName}}</span>
                      </div>
                  </div>
                </div>
                <p class="note-comment-time">{{note.createdAt | formatDate}} - {{note.createdBy.username}}</p>
              </div>
              <div class="col-sm-2 text-center icon-delete" @click="removeNote(note.id)">
                <i class="fa fa-trash text-danger"></i>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VLoading from "@/components/common/VLoading";
export default {
  name: "Note",
  components: {VLoading},
  props: {
    notes: Array
  },
  data() {
    return {
      isShowExpandable: false,
      files: [],
      docExt: ['doc', 'docm', 'docx'],
      pptExt: ['ppt', 'pptm', 'pptx'],
      pdfExt: ['pdf'],
      xmlExt: ['xml'],
      csvExt: ['csv'],
      sqlExt: ['sql'],
      xlsExt: ['xls', 'xlsx'],
      txtExt: ['txt'],
      jsExt: ['js'],
      zipExt: ['rar', '7z', 'zip', 'tar', 'iso', 'ISO'],
      imgExt: ['png', 'jpg', 'jpeg', 'tiff', 'tif'],
      htmlExt: ['html', 'htm'],
      mp4Ext: ['mp4'],
      text: '',
      loading: false
    }
  },
  methods : {
    trimText(fileName){
      if(fileName.length > 15){
        return fileName.slice(0, 12) + "..."
      }
    },
    showExpandable(fileName){
      var element = document.getElementById(fileName);
      if(element){
        element.click();
      }
      // console.log(element);
    },
    removeNote(id) {
      console.log(id)
      this.$emit('remove-note', id);
    },
    upload(event) {
      this.files = [...this.files, ...event.target.files];
    },
    download(file) {
      let link = document.createElement('a')
      link.href = window.URL.createObjectURL(file)
      link.download = file.name;
      link.click();
      link.remove();
    },
    submit() {
      if (!this.text || !this.text.trim()) {
        return;
      }
      this.loading = true;
      this.$emit('submit', {text: this.text, files: this.files});
    },
    clear() {
      this.text = '';
      this.files = [];
      this.$refs.files.value=null;
      this.loading = false;
    }
  }
}
</script>

<style scoped>

.ttip .ttip-text{
  visibility: hidden;
  width: 500px;
  height: fit-content;
  background-color: #333;
  /* color: #ff9c9c; */
  color: white;
  text-align: center;
  border-radius: 6px;
  padding: 5px 0;
  box-shadow: 0 14px 28px rgba(0,0,0,0.25), 0 10px 10px rgba(0,0,0,0.22);
  top: 100%;
  left: 50%;
  margin-left: -250px; /* Use half of the width (120/2 = 60), to center the tooltip */
  /* Position the tooltip */
  font-family: "Segoe UI";
  position: absolute;
  z-index: 1;
}

.ttip:hover .ttip-text {
  visibility: visible;
}

.note {
  background: #FFFFFF;
  box-shadow: 0px -8px 16px rgba(255, 255, 255, 0.4), 0px 16px 24px rgba(55, 71, 79, 0.16);
  border-radius: 30px;
}
.note-input {
  position: relative;
  width: 60%;
}
.note-input-ghim {
  position: absolute;
  left: 15px;
  top: 25%;
}
.note-input-send {
  position: absolute;
  right: 2%;
  top: 25%;
}
.note-input input {
  border: 1px solid rgba(0, 0, 0, 0.87);
  box-sizing: border-box;
  border-radius: 8px;
  padding: 15px 10px 15px 40px;
  width: 100%;
}
.note-comment li:hover .icon-delete {
  display: block;
}
.note-comment-image {
  border-radius: 50%;
  overflow: hidden;
  width: 61px;
  height: 60px;
  border: solid 1px #ccc;
  margin-top: 5px;
}

.expandable-image.expanded{
  height: 100% !important;
  background: rgba( 0, 0, 0, 0.93 ) !important;
}

.expandable-image img{
  height: 13vh !important;
}

.note-comment-text {
  color: #2D3436;
  font-size: 21px;
  line-height: 32px;
}
.note-comment-time {
  font-size: 19px;
  line-height: 32px;
  color: rgba(45, 52, 54, 0.7);
}
i {
  font-size: 20px;
  color: #C2CFE0;
  cursor: pointer;
  margin-top: 5px;
}
.note-comment-file-list .note-comment-file-item p {

  word-break: break-all;
}
.note-comment-file-list .file-image {
  background: #fafafa;
  border: 1px solid #000000;
  box-sizing: border-box;
  /* height: 100px; */
  height: fit-content;
}
.note-comment-file-list .file-image img {
  vertical-align: top;
}
.note-comment-file-list .file-image-header {
  background: #E8E0E0;
  height: 25px;
}
.icon-delete {
  display: none;
}
.cursor-pointer:hover {
  color: black;
  text-decoration: underline;
}
</style>