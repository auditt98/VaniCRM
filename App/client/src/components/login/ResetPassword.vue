<template>
  <div class="w-100 login-page">
    <VLoading :loading="loading"/>
    <div class="row mx-0">
      <div class="col-sm-6 mx-auto pt-5">
        <div class="col-sm-12 m-auto">
          <div class="mb-5">
            <p class="text-center font-weight-bold login-text">Reset Password</p>
            <p class="under-line w-50 mx-auto mb-5"></p>
            <p class="text-center">Enter your new password.</p>
          </div>
          <form class="rơmt-2" @submit.prevent="onSubmit">
            <div class="form-group row">
              <label for="inputPassword" class="col-sm-4 col-form-label">New password</label>
              <div class="col-sm-8">
                <input :class="{'input-error-red': error && error.includes('NEW_PASS')}" type="password" class="form-control" id="inputPassword" placeholder="" v-model="password">
              </div>
            </div>
            <div class="form-group row">
              <label for="password" class="col-sm-4 col-form-label">Confirm new password</label>
              <div class="col-sm-8">
                <input :class="{'input-error-red': error && error.includes('NEW_PASS_CONFIRM')}" type="password" class="form-control" id="password" placeholder="" v-model="passwordConfirm">
              </div>
            </div>

            <button type="submit" class="btn btn-reset w-100 mt-2">Reset password</button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {authenticationService} from "@/service/authentication.service";
import router from "@/router";
import VLoading from "@/components/common/VLoading";

export default {
  name: "ResetPassword",
  components: {VLoading},
  data() {
    return {
      loading: false,
      password: '',
      passwordConfirm: '',
      key: '',
      error: Array(String)
    }
  },
  methods: {
    onSubmit() {
      this.error = [];
      if (!this.password || this.password.trim() === '') {
        this.error.push('NEW_PASS');
      }
      if (!this.passwordConfirm || this.passwordConfirm.trim() === '') {
        this.error.push('NEW_PASS_CONFIRM');
      }
      if (this.error.length > 0) {
        return;
      }
      if (this.password !== this.passwordConfirm) {
        alert('Ko trung')
        return ;
      }
      this.loading = true;
      authenticationService.updateNewPass(this.password, this.key)
          .then(
              (res) => {
                if (res) {
                  alert('Updated successfully!');
                  router.push('/login')
                }
              },
              error => {
                console.log(error);
                alert(error);
              }
          ).finally(() => {
        this.loading = true;
      });
    }
  },
  created() {
    if (authenticationService.currentUserValue) {
      return router.push('/');
    }
    this.key = this.$route.query.key || null;
    if(!this.key) {
      router.push('/');
    }
  }
}
</script>

<style scoped>
.btn-reset {
  background: #D93915;
  color: white;
}
</style>