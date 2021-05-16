<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link :to="{name: 'LeadDetail', query: {id: lead.id}}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3">

        <input style="display: none" id="avatarId" type="file" v-on:change="fileChange($event)">
        <div class="row mt-3">
          <div class="basic-edit" v-if="lead.id">
            <h4 class="ml-2"><strong>User Avatar</strong></h4>
            <div class="row">
              <div class="user-info-avatar">
                <div class="user-info-avatar-image">
                  <img v-if="lead.avatar" class="w-100" :src="lead.avatar" alt="">
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
            <h4 class="ml-2"><strong>Lead Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr :class="{ 'form-group--error': $v.lead.name.$error }">
                    <td class="required">Name</td>
                    <td><input class="form-control" type="text" v-model.trim="$v.lead.name.$model"></td>
                  </tr>
                  <tr :class="{ 'form-group--error': $v.lead.email.$error }">
                    <td class="required">Email</td>
                    <td>
                      <input class="form-control" type="text" v-model.trim="$v.lead.email.$model">
                      <small class="error text-danger" v-if="!$v.lead.email.isValidEmail">Email is not correct.</small>
                    </td>
                  </tr>
                  <tr>
                    <td>Phone</td>
                    <td><input class="form-control " type="text" v-model.trim="lead.phone"></td>
                  </tr>
                  <tr>
                    <td>Fax</td>
                    <td><input class="form-control " type="text" v-model.trim="lead.fax"></td>
                  </tr>
                  <tr>
                    <td><label for="industry">Industry</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="industry" v-if="industry" v-model.trim="lead.industry">
                        <option v-for="(i, index) in industry" :key="index" :value="i.id">{{i.name}}</option>
                      </select>
                    </td>

                  </tr>
                  <tr>
                    <td>Website</td>
                    <td><input class="form-control " type="text" v-model.trim="lead.website"></td>
                  </tr>
                  <tr>
                    <td>Annual Revenue</td>
                    <td><input class="form-control " type="text" v-model.trim="lead.annualRevenue"></td>
                  </tr>
                  <tr>
                    <td><label for="Priority">Priority</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Priority" v-if="priority" v-model.trim="lead.priority">
                        <option v-for="(i, index) in priority" :key="index" :value="i.id">{{i.name}}</option>
                      </select>
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
                    <td><input class="form-control" type="checkbox" v-model="lead.noEmail"></td>
                  </tr>
                  <tr>
                    <td>No Call</td>
                    <td><input class="form-control " type="checkbox" v-model="lead.noCall"></td>
                  </tr>
                  <tr>
                    <td><label for="Source">Lead Source</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Source" v-if="leadSource" v-model.trim="lead.leadSource">
                        <option v-for="(i, index) in leadSource" :key="index" :value="i.id">{{i.name}}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td>Company Name</td>
                    <td><input class="form-control " type="text" v-model.trim="lead.companyName"></td>
                  </tr>
                  <tr>
                    <td><label for="Status">Lead Status</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Status" v-if="status" v-model.trim="lead.leadStatus">
                        <option v-for="(i, index) in status" :key="index" :value="i.id">{{i.name}}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td>Skype</td>
                    <td><input class="form-control " type="text" v-model.trim="lead.skype"></td>
                  </tr>
                  <tr>
                    <td><label for="Owner">Owner</label></td>
                    <td style="width: 80%">
                      <vc-select id="Owner" label="username" :filterable="false" :options="owners" @search="onSearch"
                                v-model="lead.owner">

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
                    <td class="col-sm-3"><input class="form-control " type="text" v-model.trim="lead.country"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">City</td>
                    <td class="col-sm-3"><input class="form-control " type="text" v-model.trim="lead.city"></td>
                  </tr>
                  <tr class="row">
                    <td class="col-sm-2">Address Detail</td>
                    <td class="col-sm-9"><input class="form-control " type="text" v-model.trim="lead.addressDetail"></td>
                  </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="basic-edit" style="height: fit-content;">
            <h4 class="ml-2"><strong>Description</strong></h4>
            <div class="row ml-2">
              <!-- <vue-editor v-model.trim="lead.description" name="" id="" cols="25" rows="5"></vue-editor> -->
              <textarea v-model.trim="lead.description" name="" class="form-control" id="" cols="25" rows="5"></textarea>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

import VButton from "@/components/common/VButton";
import {leadService} from "@/service/lead.service";
import VLoading from "@/components/common/VLoading";
import { required } from 'vuelidate/lib/validators'
import {userService} from "@/service/user.service";
import {getValueInArr} from "@/config/config";


export default {
  name: "LeadUpdate",
  components: {VLoading, VButton, 
  },
  validations: {
    lead: {
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
      if (!this.lead.id) {
        this.lead.owner = this.lead.owner ? this.lead.owner.id : null;
        leadService.create(this.lead)
            .then(res => {
              if (res) {
                alert(res.message);
                if (this.campaignId) {
                  this.$router.push('/campaigns/detail?id=' + this.campaignId);
                } else {
                  this.$router.push('/leads');
                }
              }
            })
      } else {
        if (this.files) {
          await this.upload();
        }
        this.lead.owner = this.lead.owner ? this.lead.owner.id : null;
        leadService.update(this.lead, this.lead.id)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/leads/detail?id=' + this.lead.id);
              }
            })
      }
    },
    loadLead() {
      leadService.getById(this.lead.id)
      .then(res => {
        if (res && res.data) {
          this.lead = res.data;
          this.lead.industry = getValueInArr(res.data.industry, 'selected', 'id');
          this.lead.priority = getValueInArr(res.data.priority, 'selected', 'id');
          this.lead.leadSource = getValueInArr(res.data.leadSource, 'selected', 'id');
          this.lead.leadStatus = getValueInArr(res.data.status, 'selected', 'id');
        }
      })
    },
    fileChange(event) {
      this.files = event.target.files[0];
      this.lead.avatar = URL.createObjectURL(this.files);
    },
    upload() {
      let formData = new FormData();
      formData.append("file", this.files);
      leadService.changeAvatar(formData, this.lead.id).then(res => {
        console.log(res)
      }).catch(err => alert(err))
    },
    loadSelectList() {
      this.loading = true;
        leadService.loadAllObject()
      .then(res => {
        if (res && res.data &&res.status === 'success') {
          this.industry = res.data.industry;
          this.status = res.data.status;
          this.priority = res.data.priority;
          this.leadSource = res.data.leadSource;
        }
      }).finally(() => {
        this.loading = false;
        })
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
    validateEmail(email) {
      return /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(email);
    }
  },
  created() {
    if (this.$route.query.id) {
      this.lead.id = this.$route.query.id;
      this.loadLead();
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
  },
  data: function () {
    return {
      lead: {
        name: null,
        email: null,
        phone: null,
        fax: null,
        industry: null,
        website: null,
        annualRevenue: null,
        priority: null,
        noEmail: true,
        noCall: true,
        leadSource: null,
        companyName: null,
        leadStatus: null,
        skype: null,
        country: null,
        city: null,
        addressDetail: null,
        description: null,
        owner: null
      },
      loading: false,
      campaignId: null,
      files: null,
      owners: [],
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
/deep/.vs__search {
  width: unset;
  border: none;
}
/deep/ .vs--open .vs--single .vs__selected {
  position: unset;
}
</style>