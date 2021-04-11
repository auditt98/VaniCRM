<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <header class="modal-header">
          <slot name="header">
            <h2 class="text-center w-100">Create New Tag</h2>
          </slot>
        </header>

        <section class="modal-body">
          <slot name="body">
            <input :class="{'input-error-red': error !== ''}" class="form-control w-75 mx-auto" type="text" v-model="tagName" placeholder="Enter Tag Name">
          </slot>
        </section>

        <footer class="modal-footer text-center">
          <slot name="footer">
            <span @click="close"><VButton :data="btnCancel"/></span>
            <span @click="save()"><VButton :data="btnSend"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
export default {
  name: "CreateTagModal",
  components: {VButton},
  data () {
    return {
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-times', text: 'Cancel'},
      btnSend: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Save'},
      tagName: '',
      error: ''
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
    save() {
      this.error = '';
      if (!this.tagName) {
        this.error = 'error';
        return ;
      }
      this.$emit('create-tag', this.tagName);
    },
    clear() {
      this.tagName ='';
      this.error = '';
    }
  },
  created() {
    this.clear();
  }
}
</script>

<style scoped>
.modal-footer {
  display: unset;
}
</style>