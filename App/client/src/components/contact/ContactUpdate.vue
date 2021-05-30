<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link :to="{name : 'ContactList'}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3">
        <VLoading :loading="loading"/>
        <input style="display: none" id="avatarId" type="file" v-on:change="fileChange($event)">
        <div class="row mt-3">
          <div class="basic-edit" v-if="contact.id">
            <h4 class="ml-2"><strong>User Avatar</strong></h4>
            <div class="row">
              <div class="user-info-avatar">
                <div class="user-info-avatar-image">
                  <img v-if="contact.avatar" class="w-100" :src="contact.avatar" alt="">
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
          </div>
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Contact Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr>
                    <td>Contact Owner</td>
                    <td style="width: 80%">
                      <vc-select id="cOwner" label="username" :filterable="false" :options="owners" @search="onSearch"
                                v-model="contact.owner">
                      </vc-select>
                    </td>
                  </tr>
                  <tr :class="{ 'form-group--error': $v.contact.name.$error }">
                    <td class="required">Name</td>
                    <td><input class="form-control " type="text" v-model="contact.name"></td>
                  </tr>
                  <tr :class="{ 'form-group--error': $v.contact.email.$error }">
                    <td class="required">Email</td>
                    <td>
                      <input class="form-control" type="text" v-model.trim="$v.contact.email.$model">
                      <small class="error text-danger" v-if="!$v.contact.email.isValidEmail">Email is not correct.</small>
                    </td>
                  </tr>
                  <tr>
                    <td>Phone</td>
                    <td><input class="form-control " type="text" v-model="contact.phone"></td>
                  </tr>
                  <tr>
                    <td>Mobile</td>
                    <td><input class="form-control " type="text" v-model="contact.mobile"></td>
                  </tr>
                  <tr>
                    <td>Department Name</td>
                    <td><input class="form-control " type="text" v-model="contact.departmentName"></td>
                  </tr>
                  <tr>
                    <td>Birthday</td>
                    <td><input class="form-control " type="date" v-model="contact.birthday"></td>
                  </tr>
                  <tr>
                    <td><label for="Collaborator">Collaborator</label></td>
                    <td style="width: 80%">
                      <vc-select id="Collaborator" label="username" :filterable="false" :options="owners" @search="onSearch"
                                v-model="contact.collaborator">
                      </vc-select>
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
                    <td>No Email</td>
                    <td><input class="form-control" type="checkbox" v-model="contact.noEmail"></td>
                  </tr>
                  <tr>
                    <td>No Call</td>
                    <td><input class="form-control " type="checkbox" v-model="contact.noCall"></td>
                  </tr>
                  <tr>
                    <td>Skype</td>
                    <td><input class="form-control " type="text" v-model="contact.skype"></td>
                  </tr>
                  <tr>
                    <td>Assistant Name</td>
                    <td><input class="form-control " type="text" v-model="contact.assistantName"></td>
                  </tr>
                  <tr>
                    <td>Assistant Phone</td>
                    <td><input class="form-control " type="text" v-model="contact.assistantPhone"></td>
                  </tr>
                  <tr>
                    <td><label for="Priority">Priority</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Priority" v-if="priorities"
                              v-model.trim="contact.priority">
                        <option v-for="(i, index) in priorities" :key="index" :value="i.id">{{ i.name }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Account">Account</label></td>
                    <td style="width: 80%">
                      <vc-select id="Account" label="name" :filterable="false" :options="accounts" :class="{'select-disabled': accountId !== null}"
                                @search="onSearchAccount"
                                v-model="contact.account">
                      </vc-select>
                    </td>
                  </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
          <!--          address-->
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Address Information</strong></h4>
            <div class="row">
              <div class="col-sm-12">
                <table class="ml-4">
                  <tbody>
                  <tr class="row">
                    <td class="col-sm-2">Country</td>
                    <td class="col-sm-3"><input class="form-control " type="text" v-model="contact.country"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">City</td>
                    <td class="col-sm-3"><input class="form-control " type="text" v-model="contact.city"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">Address Detail</td>
                    <td class="col-sm-9"><input class="form-control " type="text" v-model="contact.addressDetail"></td>
                  </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script>

import VButton from "@/components/common/VButton";
import {contactService} from "@/service/contact.service";
import {userService} from "@/service/user.service";
import {required} from "vuelidate/lib/validators";
import {formatDate, getValueInArr} from "@/config/config";
import {accountService} from "@/service/account.service";
import VLoading from "@/components/common/VLoading";

export default {
  name: "LeadUpdate",
  components: {VLoading, VButton, },
  validations: {
    contact: {
      name: {
        required
      },
      email: {
        required,
        isValidEmail(value) {
          if (value === '') return true;
          return this.validateEmail(value);
        }
      }
    }
  },
  methods: {
    async save() {
      this.$v.$touch()
      if (this.$v.$invalid) {
        alert('Failed')
        return;
      }
      this.loading = true;
      this.contact.owner = this.contact.owner ? this.contact.owner.id : null;
      this.contact.collaborator = this.contact.collaborator ? this.contact.collaborator.id : null;
      this.contact.account = this.contact.account ? this.contact.account.id : null;
      if (this.contact.id) {
        if (this.files) {
          await this.upload();
        }
        contactService.update(this.contact, this.contact.id)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/contacts/detail?id=' + this.contact.id);
              }
            })
            .catch(err => alert(err))
            .finally(() => {
              this.loading = false;
            })
      } else {
        contactService.create(this.contact)
            .then(res => {
              if (res) {
                alert(res.message);
                if (this.accountId) {
                  this.$router.push('/accounts/detail?id=' + this.accountId);
                } else if (this.campaignId) {
                  this.$router.push('/campaigns/detail?id=' + this.campaignId);
                } else {
                  this.$router.push('/contacts');
                }
              }
            })
            .catch(err => alert(err))
            .finally(() => {
              this.loading = false;
            })
      }
    },
    loadContact() {
      this.loading = true;
      contactService.getById(this.contact.id)
          .then(res => {
            if (res && res.data) {
              this.contact = res.data;
              this.contact.birthday = formatDate(this.contact.birthday, 'yyyy-MM-dd') ;
              this.contact.priority = getValueInArr(res.data.priorities, 'selected', 'id');
            } else {
              alert('No data found');
              this.$router.push('/');
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    loadSelectList() {
      this.loading = true;
      contactService.loadAllObject()
          .then(res => {
            if (res && res.data && res.status === 'success') {
              this.priorities = res.data.priority;
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    fileChange(event) {
      this.files = event.target.files[0];
      this.contact.avatar = URL.createObjectURL(this.files);
    },
    upload() {
      let formData = new FormData();
      formData.append("file", this.files);
      contactService.changeAvatar(formData, this.contact.id).then().catch(err => alert(err))
    },
    onSearch(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search) {
        query['query'] = search;
      }
      userService.getAll(query).then(res => {
        if (res) {
          this.owners = res.data.users;
        }
      })
    },
    onSearchAccount(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search && search.trim()) {
        query['query'] = search;
      }
      accountService.getAll(query).then(res => {
        if (res && res.data) {
          this.accounts = res.data.accounts;
        }
      })
    },
    validateEmail(email) {
      return /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(email);
    },
    getAccountById() {
      accountService.getById(this.accountId)
      .then(res => {
        if (res && res.data) {
          this.contact.account = res.data;
        }
      })
    },
  },
  created() {
    if (this.$route.path.indexOf('contacts/update') > -1) {
      if (!this.$route.query.id) {
        this.$router.push('/');
        return;
      }
      this.contact.id = this.$route.query.id;
      this.loadContact();
    } else {
      if (this.$route.query.accountId) {
        this.accountId = Number(this.$route.query.accountId);
        this.getAccountById();
      } else if (this.$route.query.campaignId) {
        this.campaignId = Number(this.$route.query.campaignId);
        // this.getAccountById();
      }
    }
    this.loadSelectList();
    this.onSearch(null);
    this.onSearchAccount(null);
  },
  data: function () {
    return {
      contact: {
        owner: null,
        account: null,
        collaborator: null,
        name: null,
        email: null,
        phone: null,
        mobile: null,
        departmentName: null,
        birthday: null,
        priority: 0,
        noEmail: true,
        noCall: true,
        skype: null,
        assistantName: null,
        assistantPhone: null,
        country: null,
        city: null,
        addressDetail: null
      },
      accountId: null,
      campaignId: null,
      owners: [],
      priorities: [],
      accounts: [],
      loading: false,
      files: null,
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},
    }
  }
}
</script>

<style scoped>
/deep/ .vs__search {
  width: unset;
  border: none;
  margin-top: 0;
}

/deep/ .vs--open .vs--single .vs__selected {
  position: unset;
  margin-top: 0;
}

/deep/ .vs__dropdown-toggle {
  height: 36px;
}

.background-main {
  position: relative;
  min-height: 900px;
}
.select-disabled {
  pointer-events: none;
  color: #bfcbd9;
  cursor: not-allowed;
  background-image: none;
  background-color: #eef1f6;
  border-color: #d1dbe5;
}
</style>