<template>
  <div class="w-100 mb-5">
    <div class="note p-3">
      <h4 class="ml-2"><strong>Notes</strong></h4>
      <div class="row ml-3">
        <div class="note-input">
          <img src="../../../assets/ghim.png" alt="" class="note-input-ghim">
          <input type="text" placeholder="Type something here...">
          <img src="../../../assets/send.png" alt="" class="note-input-send">
        </div>
      </div>
      <div class="row">
        <div class="note-comment mt-2 w-100">
          <ul>
            <li class="row mt-3">
              <div class="col-sm-1 ">
                <div class="note-comment-image">
                  <img class="w-100 h-100" src="../../../assets/image2.png" alt="">
                </div>
              </div>
              <div class="col-sm-5">
                <p class="note-comment-text mb-0">Demo Note with only text</p>
                <p class="note-comment-time">January 21st at 19:03 - Xù</p>
              </div>
              <div class="col-sm-2 text-center">
                <i class="fa fa-trash"></i>
              </div>
            </li>
<!--            file-->
            <li class="row mt-3">
              <div class="col-sm-1 ">
                <div class="note-comment-image">
                  <img class="w-100 h-100" src="../../../assets/image2.png" alt="">
                </div>
              </div>
              <div class="col-sm-5">
                <p class="note-comment-text mb-0">Demo Note with text and attachments</p>
                <div class="note-comment-file-list row mx-0 px-0">
                  <div class="note-comment-file-item col-3 pl-0" v-for="(i, index) in 2" :key="index">
                      <div class="file-image">
                          <div class="file-image-header text-right">
                            <img src="../../../assets/eye-line.png" alt="">
                            <img width="37" height="37" src="../../../assets/icon-download.png" alt="" style="margin-top: -7px; margin-right: -5px">
                          </div>
                      </div>
                      <p class="text-center">0001.jpg</p>
                  </div>
                </div>
                <p class="note-comment-time">January 21st at 19:03 - Xù</p>
              </div>
              <div class="col-sm-2 text-center">
                <i class="fa fa-trash"></i>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Note"
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
.note-comment {

}
.note-comment-image {
  border-radius: 50%;
  overflow: hidden;
  width: 61px;
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
.note-comment-file-list .note-comment-file-item {
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
</style>