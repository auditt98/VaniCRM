<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal" style="position: relative">
        <VLoading :loading="loading"/>
        <header class="modal-header">
          <slot name="header">
            <h5 class="text-center w-100">Forgot Password</h5>
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
            <span @click="openConfirm()"><VButton :data="btnSend"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
import {authenticationService} from "@/service/authentication.service";
import VLoading from "@/components/common/VLoading";
export default {
  name: "ForgotPassModal",
  components: {VLoading, VButton},
  data () {
    return {
      loading: false,
      btnCancel: {btnClass: 'btn-white px-3 mr-4 btn-modal', icon: '', text: 'Cancel'},
      btnSend: {btnClass: 'btn-red px-4 btn-modal', icon: '', text: 'Send'},
      email: '',
      error: ''
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
    openConfirm() {
      this.error = '';
      if (!this.email) {
        this.error = 'error';
        return ;
      }
      this.$emit('open-confirm');
    },
    sendRequest() {
      this.loading = true;
      authenticationService.requestResetPass(this.email)
          .then(res => {
            console.log(res);
            this.$emit('reset-success');
            this.email ='';
            this.error = '';
          }).catch(err => alert(err))
      .finally(() => {
        this.loading = false;
      });
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
.modal-body {
  flex: unset;
}
.modal-header {
  margin-top: 20px;
}
.modal {
  width: 440px;
  height: 276px;
  position: absolute !important;
  top: 15%;
  left: 38%;
  z-index: 10040;
  overflow: auto;
  overflow-y: auto;
}
</style>