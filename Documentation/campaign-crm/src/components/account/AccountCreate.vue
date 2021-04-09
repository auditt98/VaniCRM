<template>
  <div class="">
    <Header/>
    <div class="px-5 pt-3 m-0 background-main">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <router-link :to="{name : 'LeadList'}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3">

        <input style="display: none" id="avatarId" type="file" v-on:change="fileChange($event)">
        <div class="row mt-3">
          <div class="basic-edit" v-if="account.id">
            <h4 class="ml-2"><strong>User Avatar</strong></h4>
            <div class="row">
              <div class="user-info-avatar">
                <div class="user-info-avatar-image">
                  <img v-if="account.avatar" class="w-100" :src="account.avatar" alt="">
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
            <h4 class="ml-2"><strong>Account Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr>
                    <td><label for="Owner1">Account Owner</label></td>
                    <td style="width: 525px">
                      <v-select id="Owner1" label="username" :filterable="false" :options="owners" @search="onSearch"
                                v-model="account.owner"
                                :reduce="i => i.id">
                        <template slot="no-options">
                          Type for searching...
                        </template>
                        <template slot="option" slot-scope="option">
                          <div class="d-center">
                            {{ `${option.username} - ${option.firstName}` }}
                          </div>
                        </template>
                        <template slot="selected-option" slot-scope="option">
                          <div class="selected d-center">
                            {{ `${option.username} - ${option.firstName}` }}
                          </div>
                        </template>
                      </v-select>
                    </td>
                  </tr>
                  <tr :class="{ 'form-group--error': $v.account.name.$error }">
                    <td class="required">Account Name</td>
                    <td>
                      <input class="form-control" type="text" v-model.trim="$v.account.name.$model">
                    </td>
                  </tr>
                  <tr :class="{ 'form-group--error': $v.account.email.$error }">
                    <td class="required">Email</td>
                    <td>
                      <input class="form-control" type="text" v-model.trim="$v.account.email.$model">
                      <small class="error text-danger" v-if="!$v.account.email.isValidEmail">Email is not correct.</small>
                    </td>
                  </tr>
                  <tr>
                    <td>Phone</td>
                    <td><input class="form-control " type="text" v-model.trim="account.phone"></td>
                  </tr>
                  <tr>
                    <td>Fax</td>
                    <td><input class="form-control " type="text" v-model.trim="account.fax"></td>
                  </tr>

                  </tbody>
                </table>
              </div>
              <div class="col-sm-1"></div>
              <div class="col-sm-5">
                <table>
                  <tbody>
                  <tr>
                    <td>Tax Code</td>
                    <td><input class="form-control" type="text" v-model.trim="account.taxCode"></td>
                  </tr>
                  <tr>
                    <td>Number Of Employees</td>
                    <td><input class="form-control " type="text" v-model.trim="account.numberOfEmployees"></td>
                  </tr>
                  <tr>
                    <td>Annual Revenue</td>
                    <td><input class="form-control " type="text" v-model.trim="account.annualRevenue"></td>
                  </tr>
                  <tr>
                    <td>Website</td>
                    <td><input class="form-control " type="text" v-model.trim="account.website"></td>
                  </tr>
                  <tr>
                    <td><label for="Collaborator">Account Collaborator</label></td>
                    <td style="width: 525px">
                      <v-select id="Collaborator" label="username" :filterable="false" :options="collaborators" @search="onSearchCo"
                                v-model="account.collaborator"
                                :reduce="i => i.id">
                        <template slot="no-options">
                          Type for searching...
                        </template>
                        <template slot="option" slot-scope="option">
                          <div class="d-center">
                            {{ `${option.username} - ${option.firstName}` }}
                          </div>
                        </template>
                        <template slot="selected-option" slot-scope="option">
                          <div class="selected d-center">
                            {{ `${option.username} - ${option.firstName}` }}
                          </div>
                        </template>
                      </v-select>
                    </td>
                  </tr>


                  </tbody>
                </table>
              </div>
            </div>
          </div>
          <!--          address-->
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Bank Information</strong></h4>
            <div class="row">
              <div class="col-sm-12">
                <table class="ml-4">
                  <tbody>
                  <tr class="row">
                    <td class="col-sm-2">Bank Name</td>
                    <td class="col-sm-3"><input class="form-control " type="text" v-model.trim="account.bankName"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">Bank Account Name</td>
                    <td class="col-sm-3"><input class="form-control " type="text"
                                                v-model.trim="account.bankAccountName"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">Bank Account</td>
                    <td class="col-sm-9"><input class="form-control " type="text" v-model.trim="account.bankAccount">
                    </td>
                  </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Address Information</strong></h4>
            <div class="row">
              <div class="col-sm-12">
                <table class="ml-4">
                  <tbody>
                  <tr class="row">
                    <td class="col-sm-2">Country</td>
                    <td class="col-sm-3"><input class="form-control " type="text" v-model.trim="account.country"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">City</td>
                    <td class="col-sm-3"><input class="form-control " type="text" v-model.trim="account.city"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">Address Detail</td>
                    <td class="col-sm-9"><input class="form-control " type="text" v-model.trim="account.addressDetail">
                    </td>
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
import Header from "@/components/common/Header";
import VButton from "@/components/common/VButton";
import VLoading from "@/components/common/VLoading";
import {required} from 'vuelidate/lib/validators'
import {userService} from "@/service/user.service";
import {accountService} from "@/service/account.service";


export default {
  name: "AccountCreate",
  components: {VLoading, VButton, Header},
  validations: {
    account: {
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
        alert('loi')
        return;
      }
      if (!this.account.id) {
        accountService.create(this.account)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/accounts')
              }
            })
      } else {
        if (this.files) {
          await this.upload();
        }
        accountService.update(this.account, this.account.id)
            .then(res => {
              if (res) {
                alert(res.message);
                // this.$router.push('/lead-detail?id=' + this.account.id);
              }
            })
      }
    },
    loadAccount() {
      accountService.getById(this.account.id)
          .then(res => {
            if (res && res.data) {
              this.account = res.data;
              this.account.owner = this.account.owner.id;
              this.account.collaborator = this.account.collaborator.id;
            }
          })
    },
    fileChange(event) {
      this.files = event.target.files[0];
      this.account.avatar = URL.createObjectURL(this.files);
    },
    upload() {
      let formData = new FormData();
      formData.append("file", this.files);
      accountService.changeAvatar(formData, this.account.id).then(res => {
        console.log(res)
      }).catch(err => alert(err))
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
    onSearchCo(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search) {
        query['query'] = search;
      }
      userService.getAll(query).then(res => {
        if (res) {
          this.collaborators = res.data.users;
        }
      })
    },
    validateEmail(email) {
      return /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(email);
    }
  },
  created() {
    if (this.$route.path.indexOf('account-update') > -1) {
      if (!this.$route.query.id) {
        this.$router.push('/');
        return;
      }
      this.account.id = this.$route.query.id;
      this.loadAccount();
    }
    this.onSearch(null);
    this.onSearchCo(null);
  },
  data: function () {
    return {
      account: {
        owner: 0,
        collaborator: 0,
        name: null,
        email: null,
        phone: null,
        fax: null,
        taxCode: null,
        numberOfEmployees: 0,
        annualRevenue: 0,
        website: null,
        bankName: null,
        bankAccountName: null,
        bankAccount: null,
        country: null,
        city: null,
        addressDetail: null
      },
      loading: false,
      files: null,
      owners: [],
      collaborators: [],
      industry: [],
      status: [],
      priority: [],
      leadSource: [],
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
}

/deep/ .vs--open .vs--single .vs__selected {
  position: unset;
}
/deep/ .vs__dropdown-toggle {
  height: 36px;
}
</style>