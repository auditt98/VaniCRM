<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <header class="modal-header mt-4">
          <slot name="header">
            <h5 class="text-center w-100 color-red">Forgot Password</h5>
          </slot>
        </header>

        <section class="modal-body text-center py-0 mb-5 mt-4">
          <slot name="body">
            <p class="m-0 p-0 mt-3 color-red">
              Please click on the link sent to your email to continue the
              password reset process.
            </p>
          </slot>
        </section>

        <footer class="modal-footer text-center pt-0 mt-4">
          <slot name="footer">
            <p class="d-flex justify-content-between w-75 mx-auto">
              <span @click="save"><VButton :data="btnOK"/></span>
              <span @click="close"><VButton :data="btnClose"/></span>
            </p>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
export default {
  name: "ConfirmModal",
  components: {VButton},
  data () {
    return {
      btnOK: {btnClass: 'btn-white px-4 btn-modal', icon: '', text: 'Send Again'},
      btnClose: {btnClass: 'btn-red px-4 btn-modal', icon: '', text: 'Close'},
    }
  },
  methods: {
    save() {
      this.$emit('confirm');
    },
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
.color-red {
  color: #D93915;
  font-weight: bold;
}
</style>