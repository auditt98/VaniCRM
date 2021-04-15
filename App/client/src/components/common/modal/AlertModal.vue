<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <header class="modal-header">
          <slot name="header">
            <h5 class="text-center w-100">Forgot Password</h5>
          </slot>
        </header>

        <section class="modal-body text-center py-0">
          <slot name="body">
            <img src="images/icon_check.png" alt="">
            <p class="m-0 p-0 mt-3">
              Please click on the link sent to your email to continue the
              password reset process.
            </p>
          </slot>
        </section>

        <footer class="modal-footer text-center pt-0">
          <slot name="footer">
            <span @click="close"><VButton :data="btnOK"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
export default {
  name: "AlertModal",
  components: {VButton},
  data () {
    return {
      btnOK: {btnClass: 'btn-red px-4 btn-modal', icon: '', text: 'OK'}
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
  }
}
</script>

<style scoped>
.modal-footer {
  display: unset;
}
.modal {
  width: 25%;
  height: 320px;
  position: absolute !important;
  top: 15%;
  left: 38%;
  z-index: 10040;
  overflow: auto;
  overflow-y: auto;
}
</style>