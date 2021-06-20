<template>
  <div class="w-100 ">
    <div class="user-info d-flex p-3">
      <div class="user-info-avatar" v-if="isShowAvatar && !image">
        <expandable-image style="background-color: rgba(0, 0, 0, 0.9); " :close-on-background-click='true' class="w-100" :src="'https://localhost:44375/avatar?fileName=default.png'" alt="" />
      </div>
      <div class="user-info-avatar" v-if="image">
        <expandable-image style="background-color: rgba(0, 0, 0, 0.9); " :close-on-background-click='true' class="w-100" :src="image" alt="" />
      </div>
      <!-- <div class="user-info-avatar" v-if="isShowAvatar && !image">
        <img class="w-100" src="../../../assets/default_avatar.png" alt="">
        <img class="w-100" :src="'https://localhost:44375/avatar?fileName=default.png'" alt="">
        https://localhost:44375/avatar?fileName=default.png
      </div> -->
      <div class="user-info-detail ml-3">
        <h4><strong v-if="title">{{title}}</strong> <small v-if="titleDetail"> - </small> <span v-if="titleDetail">{{titleDetail}}</span></h4>
        <p class="ml-3" v-if="tags">
          <img src="../../../assets/tag.png" alt="">
<!--          <small class="user-tag ml-4">LOVE</small>-->
          <small class="user-tag ml-3" v-for="(t, i) in tags" :key="i">{{t.name}}</small>
          <img class="cursor-pointer" @click="openModalCreateTag" src="../../../assets/icon-plus.png" alt="">
        </p>
      </div>
    </div>
    <CreateTagModal
        ref="tagmodal"
        @create-tag="createTag"
        v-show="isShowModal"
        @close="closeModal"/>
  </div>
</template>

<script>
import CreateTagModal from "@/components/common/modal/CreateTagModal";
export default {
  name: "UserInfo",
  components: {CreateTagModal},
  props: {
    image: String,
    title: String,
    titleDetail: String,
    tags: Array,
    isShowAvatar: Boolean
  },
  data() {
    return{
      isShowModal: false,
    }
  },
  methods: {
    openModalCreateTag() {
      this.$refs.tagmodal.clear();
      this.isShowModal = true;
    },
    createTag(event) {
      this.$emit('create-tag', event)
    },
    closeModal() {
      this.isShowModal = false;
    }
  }
}
</script>

<style scoped>
.user-info {
  background: #FFFFFF;
  box-shadow: 0px -8px 10px rgba(255, 255, 255, 0.5), 0px 16px 24px rgba(55, 71, 79, 0.2);
  border-radius: 30px;
}

.user-info-avatar {
  width: 100px;
  height: 100px;
  overflow: hidden;
  border-radius: 50%;
}

.user-tag {
  border: 1px solid #F39800;
  box-sizing: border-box;
  border-radius: 30px;
  padding: 2px 15px;
  font-weight: bold;
  font-size: 10px;
}

i {
  color: #5C5C5C;
  font-size: 25px;
  line-height: 25px;
}

.tag-none {
  border: 1px solid #F39800;
  padding: 2px 80px;
}
</style>