<template>
  <div class="">

    <VLoading :loading="loading"/>
    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link to="/users"><VButton :data="btnCancel"/></router-link>

          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class=" mt-3">

        <input style="display: none" id="avatarId" type="file" v-on:change="fileChange($event)">
        <div class="row mt-3">
          <!--<div class="basic-edit">
            <h4 class="ml-2"><strong>User Avatar</strong></h4>
            <div class="row">
              <div class="user-info-avatar">
                <div class="user-info-avatar-image">
                  <img v-if="user.Avatar" class="w-100" :src="user.Avatar" alt="">
                  <img v-else class="w-100" src="../../assets/avatar-header.jpeg" alt="">
                </div>
                <div class="image-select-background">
                </div>
                <div>
                  <label for="avatarId"><img src="../../assets/image.png" class="image-select" alt=""></label>
                  <img src="../../assets/done.png" class="image-done" alt="">
                </div>
              </div>
            </div>
          </div>-->
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr>
                    <td class="required">First Name</td>
                    <td><input class="form-control " :class="{'is-invalid': !checkValid(user.FirstName)}" type="text" v-model="user.FirstName">
                      <div class="invalid-feedback" v-show="!checkValid(user.FirstName)">
                        {{ requireError }}
                      </div>
                    </td>
                  </tr>
                  <tr>
                    <td class="required">Last Name</td>
                    <td><input class="form-control " type="text" v-model="user.LastName" :class="{'is-invalid': !checkValid(user.LastName)}">
                      <div class="invalid-feedback" v-show="!checkValid(user.LastName)">
                        {{ requireError }}
                      </div>
                    </td>
                  </tr>
                  <tr>
                    <td class="required">Username</td>
                    <td><input class="form-control " type="text" v-model="user.Username" :class="{'is-invalid': !checkValid(user.Username)}">
                      <div class="invalid-feedback" v-show="!checkValid(user.Username)">
                        {{ requireError }}
                      </div>
                    </td>
                  </tr>
                  <tr>
                    <td class="required">Password</td>
                    <td><input class="form-control " type="text" v-model="user.Password" :class="{'is-invalid': !checkValid(user.Password) || passwordError}">
                      <div class="invalid-feedback" v-show="!checkValid(user.Password)">
                        {{ requireError }}
                        {{ passwordError }}
                      </div>
                      <div class="invalid-feedback" v-show="checkValid(user.Password)">
                        {{ passwordError }}
                      </div>
                    </td>
                  </tr>
                  </tbody>
                </table>
              </div>
              <div class="col-sm-1"></div>
              <div class="col-sm-5">
                <table>
                  <tbody>
                  <tr>
                    <td>Phone</td>
                    <td><input class="form-control" type="text" v-model="user.Phone"></td>
                  </tr>
                  <tr>
                    <td class="required">Email</td>
                    <td>
                      <input class="form-control " type="text" v-model="user.Email" :class="{'is-invalid': !checkValid(user.Email) || emailError}">
                      <div class="invalid-feedback" v-show="!checkValid(user.Email)">
                        {{ requireError }}
                      </div>
                      <div class="invalid-feedback" v-show="checkValid(user.Email)">
                        {{ emailError }}
                      </div>
                    </td>
                  </tr>
                  <tr>
                    <td>Skype</td>
                    <td><input class="form-control " type="text" v-model="user.Skype"></td>
                  </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Information</strong></h4>
            <div class="row ml-4">
                <label for="exampleFormControlSelect1" class="col-sm-1">Group</label>
                <select class="form-control py-0 col-sm-3" id="exampleFormControlSelect1" v-model="user.GROUP_ID">
                  <option v-for="(g, i) in groups" :value="g.id" :key="i">{{g.groupName}}</option>
                </select>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VButton from "@/components/common/VButton";
import {userService} from "@/service/user.service";
import router from "@/router";

import {groupService} from "@/service/group.service";
import VLoading from "@/components/common/VLoading";

export default {
  name: "UserUpdate",
  components: {VLoading, VButton},
  methods: {
/*    fileChange(event) {
      this.files = event.target.files[0];
      this.user.Avatar = URL.createObjectURL(this.files);
    },
    upload() {
      let formData = new FormData();
      formData.append("file", this.files);
      userService.updateAvatar(formData, 1).then(res => {
      }).catch(err => alert(err))
    },*/
    save() {
      this.isValidate = true;
      this.passwordError= '';
      this.emailError= '';
      if (!this.user.FirstName || !this.user.LastName || !this.user.Username || !this.user.Password || !this.user.Email) {
        return ;
      }
      if (!this.user.FirstName.trim() || !this.user.LastName.trim() || !this.user.Username.trim() || !this.user.Password.trim() || !this.user.Email.trim()) {
        return ;
      }

      if (this.user.Password.length < 6 || !this.validateEmail()) {
        if (this.user.Password.length < 6) {
          this.passwordError= 'Password length gretter than 6.';
        }
        if (!this.validateEmail()) {
          this.emailError= 'Email invalid format.';
        }
        return ;
      }
      const userCreate = {...this.user};
      if (!this.user.id) {
        this.loading = true;
        userService.create(userCreate).then(res => {
          if(res) {
            alert('Succesfully');
            router.push('/users');
          } else {
            alert('Failed');
          }
        }).catch(err=> {
          alert(err);
        }).finally(() => {
          this.loading = false;
        })
      } else {
        alert("User existed");
      }
    },
    loadGroups() {
      this.loading = true;
      this.groups = [];
      groupService.getAll({})
      .then(res => {
        if (res && res.data && res.data.groups) {
          this.groups = res.data.groups;
        }
      }).catch(() => {
        alert('LOAD_GROUP: Error occured');
      }).finally(() => {
        this.loading = false;
      })
    },
    checkValid(item) {
      if (!this.isValidate) {
        return true;
      }
      if (!item) {
        return false;
      }
      if (!item.trim()) {
        return false;
      }
      return true;
    },
    validateEmail() {
      return /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(this.user.Email);
    }
  },
  created() {
    this.loadGroups();
    /*if (this.$route.query.id) {
      this.user.id = this.$route.query.id;
      userService.getById(this.$route.query.id).then(res => {
        if (res && res.data) {
          const userTemp = res.data;
          this.user.id = userTemp.id;
          this.user.Username = userTemp.username;
          this.user.FirstName = userTemp.firstName;
          this.user.LastName = userTemp.lastName;
          this.user.Email = userTemp.email;
          this.user.Skype = userTemp.skype;
          this.user.Phone = userTemp.phone;
          this.user.Avatar = userTemp.avatar;
          userTemp.groups.forEach(g => {
            if (g.selected) {
              this.user.GROUP_ID = g.id;
            }
          });
          this.groups = userTemp.groups;
        }
      }).catch(err => alert(err));
    }*/
  },
  data: function () {
    return {
      user: {
        id: null,
        Username: null,
        FirstName: null,
        LastName: null,
        Email: null,
        Skype: null,
        Phone: null,
        Avatar: null,
        Password: null,
        GROUP_ID: 1
      },
      loading: false,
      groups: [],
      files: new FormData(),
      isValidate: false,
      requireError: 'Không được bỏ trống.',
      passwordError: '',
      emailError: '',
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'}

    }
  }
}
</script>

<style scoped>

</style>