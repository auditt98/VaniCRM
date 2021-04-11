<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <header class="modal-header">
          <slot name="header">
            <h2 class="text-center w-100">Change Password</h2>
          </slot>
        </header>

        <section class="modal-body">
          <slot name="body">
            <table class="w-100">
              <tbody>
              <tr>
                <td class="required">Old Password</td>
                <td>
                  <input :class="{'is-invalid': !oldPassword && isCheck}" class="form-control" type="text" v-model="oldPassword">
                </td>
              </tr>
              <tr>
                <td class="required">New Password</td>
                <td>
                  <input :class="{'is-invalid': !newPassword && isCheck}" class="form-control" type="text"  v-model="newPassword">
                </td>
              </tr>
              <tr>
                <td class="required">Confirm</td>
                <td>
                  <input :class="{'is-invalid': (!confirmNewPassword || (confirmNewPassword !== newPassword)) && isCheck}" class="form-control" type="text" v-model="confirmNewPassword"
                  >
                  <div class="invalid-feedback" v-show="(confirmNewPassword !== newPassword) && isCheck">
                    {{ 'Mật khẩu không khớp!' }}
                  </div>
                </td>
              </tr>
              </tbody>
            </table>
          </slot>
        </section>

        <footer class="modal-footer d-flex justify-content-end">
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
import {userService} from "@/service/user.service";

export default {
  name: "ChangePassModal",
  components: {VButton},
  data() {
    return {
      oldPassword: null,
      newPassword: null,
      confirmNewPassword: null,
      isCheck: false,
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: '', text: 'Cancel'},
      btnSend: {btnClass: 'btn-red px-4', icon: '', text: 'Save Changes'}
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
    save() {
        this.isCheck = true;
        if (!this.oldPassword || !this.newPassword || !this.confirmNewPassword) {
          return ;
        }
        if (this.newPassword !== this.confirmNewPassword) {
          return ;
        }
        this.isCheck = false;
        userService.changePassword(10, this.oldPassword, this.newPassword)
      .then(res=> {
        if (res) {
          alert(res.message);
        }
      }).catch(err => alert(err));
    }
  }
}
</script>

<style scoped>
tr td {
  padding-bottom: 10px;
}

.modal-footer {
  display: unset;
}
</style>