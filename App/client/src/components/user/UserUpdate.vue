<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link :to="{name : 'Users'}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span @click="save()" class="ml-4"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3 row">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <input style="display: none" id="avatarId" type="file" v-on:change="fileChange($event)">
        <div class="col-sm-10">
          <div class=" mt-3">
            <div class="basic-edit" v-if="user.id">
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
            </div>
            <div id="basicInfo" class="basic-edit">
              <h4 class="ml-2"><strong>Basic Info</strong></h4>
              <div class="row">
                <div class="col-sm-5">
                  <table class="ml-4">
                    <tbody v-if="user">
                    <tr>
                      <td >First Name</td>
                      <td><input class="form-control" type="text" v-model.trim="user.FirstName"></td>
                    </tr>
                    <tr>
                      <td >Last Name</td>
                      <td><input class="form-control" type="text" v-model.trim="user.LastName"></td>
                    </tr>
                    <tr :class="{ 'form-group--error': $v.user.Username.$error }">
                      <td class="required">Username</td>
                      <td><input class="form-control" type="text" v-model.trim="$v.user.Username.$model"></td>
                    </tr>
                    <tr>
                      <td >Phone</td>
                      <td><input class="form-control" type="text" v-model.trim="user.Phone"></td>
                    </tr>
                    <tr>
                      <td >Skype Address</td>
                      <td><input class="form-control" type="text" v-model.trim="user.Skype"></td>
                    </tr>
                    </tbody>
                  </table>
                </div>
                <div class="col-sm-1"></div>

              </div>
            </div>
            <!--<div class="row mt-3" id="tasks">
              <TableInDetail :header-columns="columns" :tags="tags" :title="'Tasks'"
                             :page-config="{page: currentPageTask, pageSize: pageSizeTask, totalItems: totalItemTask, totalPage: totalPageTask}"
                             @page-size-change="onPageSizeChange($event, 'TASK')"
                             @go-to-page="goToPage($event, 'TASK')">
                <template slot="body">
                  <tbody v-if="taskLst && taskLst.length > 0">
                  <tr v-for="(t, i) in taskLst" :key="i">
                    <td>{{t.title}}</td>
                    <td>{{t.type}}</td>
                    <td>{{t.status}}</td>
                    <td>{{t.startDate | formatDate}}</td>
                    <td>{{t.endDate | formatDate}}</td>
                    <td class="pl-0">
                      <VTag :data="{text: t.priotity, bgColor: 'rgb(222, 0, 80)'}"/>
                    </td>
                    <td>
                      <span class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
                      <span ><img src="images/delete-bin-2-line.png" alt=""></span>
                    </td>
                  </tr>
                  </tbody>
                  <tbody v-else>
                  <tr><td colspan="9" class="text-center">Không có dữ liệu</td></tr>
                  </tbody>
                </template>
              </TableInDetail>
            </div>
            <div class="row mt-3" id="leadOwn">
              <TableInDetail :header-columns="leadOwnColumns" :data="[]" :title="'Lead Own'">
                <template slot="body">
                  <tbody v-if="leadOwnLst && leadOwnLst.length > 0">
                  <tr v-for="(t, i) in leadOwnLst" :key="i">
                    <td>{{t.name}}</td>
                    <td><a :href="'mailto:'+t.email">{{t.email}}</a></td>
                    <td>{{t.phone}}</td>
                    <td>{{t.website}}</td>
                    <td>{{t.skype}}</td>
                  </tr>
                  </tbody>
                  <tbody v-else>
                  <tr><td colspan="6" class="text-center">Không có dữ liệu</td></tr>
                  </tbody>
                </template>
              </TableInDetail>
            </div>
            <div class="row mt-3" id="allAccounts">
              <TableInDetail :header-columns="accountColumns" :data="accountLst" :title="'All Accounts'">
                <template slot="body">
                  <tbody v-if="accountLst && accountLst.length > 0">
                  <tr v-for="(t, i) in accountLst" :key="i">
                    <td>{{t.name}}</td>
                    <td><a :href="'mailto:'+t.email">{{t.email}}</a></td>
                    <td>{{t.phone}}</td>
                    <td>{{t.website}}</td>
                    <td>{{t.taxCode}}</td>
                    <td><input type="checkbox" name="isOwn" :checked="t.isOwner"></td>
                    <td><input type="checkbox" name="isCollaborator" :checked="t.isCollaborator"></td>
                  </tr>
                  </tbody>
                  <tbody v-else>
                  <tr><td colspan="7" class="text-center">Không có dữ liệu</td></tr>
                  </tbody>
                </template>
              </TableInDetail>
            </div>
            <div class="row mt-3" id="allContacts">
              <TableInDetail :header-columns="contractColumns" :data="contractLst" :title="'All Contracts'">
                <template slot="body">
                  <tbody v-if="contractLst && contractLst.length > 0">
                  <tr v-for="(t, i) in contractLst" :key="i">
                    <td>{{t.name}}</td>
                    <td><a :href="'mailto:'+t.email">{{t.email}}</a></td>
                    <td>{{t.phone}}</td>
                    <td>{{t.mobile}}</td>
                    <td><a href="#">{{t.skype}}</a></td>
                    <td><input type="checkbox" name="isOwn" :checked="t.isOwner"></td>
                    <td><input type="checkbox" name="isCollaborator" :checked="t.isCollaborator"></td>
                  </tr>
                  </tbody>
                  <tbody v-else>
                  <tr><td colspan="7" class="text-center">Không có dữ liệu</td></tr>
                  </tbody>
                </template>
              </TableInDetail>
            </div>-->
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script>

import VButton from "@/components/common/VButton";
import VLoading from "@/components/common/VLoading";
import { required } from 'vuelidate/lib/validators'
import {userService} from "@/service/user.service";
import MenuLeft from "@/components/common/MenuLeft";


export default {
  name: "UserUpdate",
  components: {MenuLeft, VLoading, VButton},
  validations: {
    user: {
      Username: {
        required
      }
    }
  },
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },
    save() {
      this.$v.$touch()
      if (this.$v.$invalid) {
        alert('Failed')
        return;
      }
      if (this.user.id) {
        this.loading = true;
        if (this.files) {
          this.upload();
        }
        userService.update(this.user, this.user.id)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/users/page?id=' + this.user.id);
              }
            }).finally(() => {
              this.loading = false;
        })
      }
    },
    loadUser() {
      userService.getById(this.user.id)
          .then(res => {
            if (res && res.data) {
              this.user.FirstName = res.data.firstName;
              this.user.LastName = res.data.lastName;
              this.user.Username = res.data.username;
              this.user.Phone = res.data.phone;
              this.user.Skype = res.data.skype;
              this.user.Avatar = res.data.avatar;
/*              this.loadAccounts();
              this.loadContacts();
              this.loadLeads();
              this.loadTasks();*/
            }
          })
    },
    fileChange(event) {
      this.files = event.target.files[0];
      this.user.Avatar = URL.createObjectURL(this.files);
    },
    async upload() {
      let formData = new FormData();
      formData.append("file", this.files);
      await userService.changeAvatar(formData, this.user.id).then().catch(err => alert(err))
    },
    loadTasks() {
      this.loading = true;
      this.taskLst = [];
      let query = {
        currentPage: this.currentPageTask,
        pageSize: this.pageSizeTask
      };
      userService.getAllTasks(this.user.id, query).then(res => {
        if (res && res.data) {
          this.taskLst = res.data.tasks;
          this.totalPageTask = Number(res.data.pageInfo.TotalPages);
          this.totalItemTask = Number(res.data.pageInfo.TotalItems);
        }
      }).finally(() => {
        this.loading = false;
      })
    },
    loadAccounts() {
      this.loading = true;
      this.accountLst = [];
      let query = {
        currentPage: this.currentPageAccount,
        pageSize: this.pageSizeAccount
      };
      userService.getAllAccounts(this.user.id, query).then(res => {
        if (res && res.data) {
          this.accountLst = res.data.accounts;
          this.totalPage = Number(res.data.pageInfo.totalPage);
        }
      }).finally(() => {
        this.loading = false;
      })
    },
    loadContacts() {
      this.loading = true;
      this.contractLst = [];
      let query = {
        currentPage: this.currentPageContact,
        pageSize: this.pageSizeContact
      };
      userService.getAllContacts(this.user.id, query).then(res => {
        if (res && res.data) {
          this.contractLst = res.data.contacts;
          this.totalPage = Number(res.data.pageInfo.totalPage);
        }
      }).finally(() => {
        this.loading = false;
      })
    },
    loadLeads() {
      this.loading = true;
      this.leadOwnLst = [];
      let query = {
        currentPage: this.currentPageLead,
        pageSize: this.pageSizeLead
      };
      userService.getAllLeads(this.user.id, query).then(res => {
        if (res && res.data) {
          this.leadOwnLst = res.data.leads;
          this.totalPage = Number(res.data.pageInfo.totalPage);
        }
      }).finally(() => {
        this.loading = false;
      })
    },
    onPageSizeChange(event, type) {
      if (type === 'TASK') {
        this.pageSizeTask = Number(event);
        this.loadTasks();
      }
    },
    goToPage(event, type) {
      if (type === 'TASK') {
        this.currentPageTask = Number(event);
        // this.loadTasks();
      }
    }
  },
  created() {
    if (this.$route.query.id) {
      this.user.id = this.$route.query.id;
      this.loadUser();
    } else {

      alert('No data found');
    }
  },
  data: function () {
    return {
      user: {
        FirstName: null,
        Avatar: null,
        LastName: null,
        Username: null,
        Phone: null,
        Skype: null,
      },
      loading: false,
      files: null,
      currentPageTask: 1,
      pageSizeTask: 5,
      totalItemTask: 0,
      totalPageTask: 1,
      currentPageLead: 1,
      pageSizeLead: 5,
      currentPageAccount: 1,
      pageSizeAccount: 5,
      currentPageContact: 1,
      pageSizeContact: 5,
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},
      tags: [ ],
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#tasks', text: 'Tasks'},
        {id: '#leadOwn', text: 'Lead Own'},
        {id: '#allAccounts', text: 'All Accounts'},
        {id: '#allContacts', text: 'All Contacts'},
      ],
      leadOwnColumns: [
        'Name', 'Email', 'Phone', 'Website', 'Skype'
      ],
      columns: [
        'Title',  'Type',  'Status',  'Start Date',  'End Date',  'Priority',  'Action'
      ],
      taskLst: [ ],
      accountColumns: [
        'Account Name', 'Email', 'Phone', 'Website', 'Tax Code', 'Is Owner', 'Is Collaborator'
      ],
      accountLst: [ ],
      contractColumns: [
        'Contract Name', 'Email', 'Phone', 'Mobile', 'Skype',
        'Is Owner', 'Is Collaborator'
      ],
      contractLst: [ ],
      leadOwnLst: [ ],
    }
  }
}
</script>

<style scoped>

</style>