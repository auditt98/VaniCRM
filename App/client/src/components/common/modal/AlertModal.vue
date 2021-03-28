<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <header class="modal-header">
          <slot name="header">
            <h2 class="text-center w-100">Forgot Password</h2>
          </slot>
        </header>

        <section class="modal-body text-center">
          <slot name="body">
            <img src="images/icon_check.png" alt="">
            <p>
              Please click on the link sent to your email to continue the
              password reset process.
            </p>
          </slot>
        </section>

        <footer class="modal-footer text-center">
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
      btnOK: {btnClass: 'btn-red px-4', icon: '', text: 'Ok!'}
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
</style>