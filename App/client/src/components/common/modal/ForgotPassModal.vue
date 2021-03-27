<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <header class="modal-header">
          <slot name="header">
            <h2 class="text-center w-100">Forgot Password</h2>
          </slot>
        </header>

        <section class="modal-body">
          <slot name="body">
            <input :class="{'input-error-red': error !== ''}" class="form-control w-75 mx-auto" type="text" v-model="email" placeholder="Enter Email">
          </slot>
        </section>

        <footer class="modal-footer text-center">
          <slot name="footer">
            <span @click="close"><VButton :data="btnCancel"/></span>
            <span @click="sendRequest()"><VButton :data="btnSend"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
import {authenticationService} from "@/service/authentication.service";
export default {
  name: "ForgotPassModal",
  components: {VButton},
  data () {
    return {
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-times', text: 'Cancel'},
      btnSend: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Send'},
      email: '',
      error: ''
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
    sendRequest() {
      this.error = '';
      if (!this.email) {
        this.error = 'error';
        return ;
      }
      authenticationService.requestResetPass(this.email)
          .then(res => {
            console.log(res);
            alert('Thanh cong');
            this.$emit('reset-success');
            this.email ='';
            this.error = '';
          }).catch(err => alert(err));
    },
  },
  created() {
    this.email ='';
    this.error = '';
  }
}
</script>

<style scoped>
.modal-footer {
  display: unset;
}
</style>