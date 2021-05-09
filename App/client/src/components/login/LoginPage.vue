<template>
  <div class="w-100 login-page">
    <div class="row mx-0">
      <VLoading :loading="loading"/>
      <div class="col-sm-6 login-form">
        <div class="col-sm-7 m-auto">
          <div class="mb-5">
            <p class="text-center font-weight-bold login-text">LOGIN</p>
            <p class="under-line mx-auto mb-5"></p>
            <p class="text-center login-note">Welcome back! Please login to your account.</p>
          </div>
          <form class="mt-2" @submit.prevent="onSubmit">
            <div class="form-group">
              <input :class="{'input-error-red': error && error.includes('EMAIL')}" type="email" class="form-control"
                     id="exampleInputEmail1" placeholder="Email" v-model="email">
            </div>
            <div class="form-group">
              <div class="input-group input-password">
                <input :class="{'input-error-red': error && error.includes('PASSWORD')}" :type="passwordType"
                       class="form-control" id="exampleInputPassword1" placeholder="Password" v-model="password">
                <button @click="showHidePass()" class="btn btn-default" type="button">
                  <i class="fa fa-eye"></i>
                </button>
              </div>
            </div>
            <div class="row mx-0 my-4">
              <div class="col-12 p-0 text-center">
                <a href="javascript:void(0)" @click="showModal" class="text-dark">Forgot Password?</a>
              </div>
            </div>

            <button type="submit" class="btn btn-login w-100 mt-2">Login</button>
          </form>
        </div>
      </div>
      <div class="col-sm-6 login-right">
        <div class="login-image mx-auto">
          <div class="login-title">
            <span>CRM</span>
          </div>
          <div class="icon icon-green">
            <span><img src="../../assets/like.png" alt=""></span>
          </div>
          <div class="icon icon-purple">
            <span><img src="../../assets/purple.png" alt=""></span>
          </div>
          <div class="icon icon-pink">
            <span><img src="../../assets/pink.png" alt=""></span>
          </div>
          <div class="icon icon-blue">
            <span><img src="../../assets/x1.png" alt=""></span>
          </div>
        </div>
      </div>
    </div>
    <ForgotPassModal
        v-show="isModalVisible"
        ref="forgot"
        @close="closeModal"
        @open-confirm="openConfirmModal()"
        @reset-success="showAlertModal()"
    />
    <AlertModal
        v-show="isAlertModalVisible"
        @close="closeAlertModal"/>
    <ConfirmModal
        v-show="isConfirmModalVisible"
        @confirm="confirmSendMail"
        @close="closeConfirmModal"/>

  </div>
</template>

<script>
import ForgotPassModal from "@/components/common/modal/ForgotPassModal";
import AlertModal from "@/components/common/modal/AlertModal";
import {authenticationService} from "@/service/authentication.service";
import router from "@/router";
import VLoading from "@/components/common/VLoading";
import ConfirmModal from "@/components/common/modal/ConfirmModal";

export default {
  name: "LoginPage",
  components: {ConfirmModal, VLoading, AlertModal, ForgotPassModal},
  data() {
    return {
      email: '',
      password: '',
      passwordType: 'password',
      returnUrl: '/',
      isModalVisible: false,
      isAlertModalVisible: false,
      isConfirmModalVisible: false,
      error: Array(String),
      loading: false
    };
  },
  methods: {
    onSubmit() {
      this.error = [];
      if (!this.email || this.email.trim() === '') {
        this.error.push('EMAIL');
      }
      if (!this.password || this.password.trim() === '') {
        this.error.push('PASSWORD');
      }
      if (this.error.length > 0) {
        return;
      }
      this.loading = true;
      authenticationService.login(this.email, this.password)
          .then(
              (res) => {
                if (res) {
                  router.push(this.returnUrl)
                }
              },
              error => {
                console.log(error);
                alert(error);
              }
          ).finally(() => {
        this.loading = false;
      });
    },
    confirmSendMail() {
      this.closeConfirmModal();
      this.$refs['forgot'].sendRequest();
    },
    showModal() {
      this.isModalVisible = true;
    },
    closeModal() {
      this.isModalVisible = false;
    },
    closeConfirmModal() {
      this.isConfirmModalVisible = false;
    },
    openConfirmModal() {
      this.isConfirmModalVisible = true;
    },
    showAlertModal() {
      this.closeModal();
      this.isAlertModalVisible = true;
    },
    closeAlertModal() {
      this.isAlertModalVisible = false;
    },
    showHidePass() {
      this.passwordType = this.passwordType === 'text' ? 'password' : 'text';
    }
  },
  created() {
    if (authenticationService.currentUserValue) {
      return router.push('/');
    }
    this.returnUrl = this.$route.query.returnUrl || '/';
  }
}
</script>

<style scoped>
.login-page {
  background: rgba(229, 229, 229, 0.41);
}

.login-form {
  background: white;
}

.login-image {
  height: 780px;
  width: 65%;
  background: url('../../assets/Messages-bro.png');
  background-repeat: no-repeat;
  border-radius: 10px;
}

.btn-login {
  height: 50px;
  background: #D93915;
  border-radius: 5px;
  font-weight: bold;
  font-size: 18px;
  line-height: 22px;
  color: white;
}

.login-text {
  font-size: 36px;
  line-height: 43px;
  color: #3E3A39;
}

.input-password {
  position: relative;
}

.input-password button {
  position: absolute;
  top: 0;
  right: 0;
  z-index: 3;
  background: none;
}

i {
  color: black;
}
.login-right, .login-form  {
  padding-top: 150px;
}
.login-form > div {
  padding-top: 100px;
}
.login-note {
  font-size: 18px;
}
.under-line {
  width: 40%;
}
.login-title {
  color: #D93915;
  font-weight: bold;
  font-size: 36px;
  position: absolute;
  left: 26%;
  top: 15%;
}
</style>