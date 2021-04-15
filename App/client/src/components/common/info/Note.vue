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
            <li v-for="(f, i) in files" :key="i">{{f.name}}</li>
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
                            <img src="../../../assets/eye-line.png" alt="">

                            <a :href="f.url"><img width="37" height="37" src="../../../assets/icon-download.png" alt="" style="margin-top: -7px; margin-right: -5px"></a>
                          </div>
                      </div>
                      <p class="text-center">{{f.fileName}}</p>
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
      files: [],
      text: '',
      loading: false
    }
  },
  methods : {
    removeNote(id) {
      console.log(id)
      this.$emit('remove-note', id);
    },
    upload(event) {
      this.files = [...this.files, ...event.target.files];
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
  background: #FFFFFF;
  border: 1px solid #000000;
  box-sizing: border-box;
  height: 100px;
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
</style>