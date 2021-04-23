<template>
  <div class="mb-5">

    <VLoading :loading="loading"/>
    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'Users'}"><VButton :data="btnBack"/></router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <span @click="isModalVisible = true"><VButton :data="btnChangePass"/></span>
          <span @click="deleteUser"><VButton :data="btnDelete"/></span>
          <router-link :to="{name: 'UserUpdate', query: {id: user.id}}"><VButton :data="btnEdit"/></router-link>
        </div>
      </div>
      <div class="row mt-3" v-if="user">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <UserInfo :image="user.avatar" :title="'User'" :title-detail="user.username"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="tasks">
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
            <TableInDetail :header-columns="leadOwnColumns" :data="[]" :title="'Lead Own'"
                           :page-config="{page: currentPageLead, pageSize: pageSizeLead, totalItems: totalItemLead, totalPage: totalPageLead}"
                           @page-size-change="onPageSizeChange($event, 'LEAD')"
                           @go-to-page="goToPage($event, 'LEAD')">
              <template slot="body">
                <tbody v-if="leadOwnLst && leadOwnLst.length > 0">
                <tr v-for="(t, i) in leadOwnLst" :key="i">
                  <td>{{t.name}}</td>
                  <td><a :href="'mailto:'+t.email">{{t.email}}</a></td>
                  <td>{{t.phone}}</td>
                  <td>{{t.website}}</td>
                  <td>{{t.companyName}}</td>
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
            <TableInDetail :header-columns="accountColumns" :data="accountLst" :title="'All Accounts'"
                           :page-config="{page: currentPageAccount, pageSize: pageSizeAccount, totalItems: totalItemAccount, totalPage: totalPageAccount}"
                           @page-size-change="onPageSizeChange($event, 'ACCOUNT')"
                           @go-to-page="goToPage($event, 'ACCOUNT')">
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
            <TableInDetail :header-columns="contractColumns" :data="contractLst" :title="'All Contacts'"
                           :page-config="{page: currentPageContact, pageSize: pageSizeContact, totalItems: totalItemContact, totalPage: totalPageContact}"
                           @page-size-change="onPageSizeChange($event, 'CONTACT')"
                           @go-to-page="goToPage($event, 'CONTACT')">
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
          </div>

        </div>
      </div>
    </div>
    <ChangePassModal
        v-show="isModalVisible"
        @close="closeModal"
    />
  </div>

</template>

<script>

import VButton from "@/components/common/VButton";
import MenuLeft from "@/components/common/MenuLeft";
import UserInfo from "@/components/common/info/UserInfo";
import BasicInfo from "@/components/common/info/BasicInfo";
import TableInDetail from "@/components/common/table/TableInDetail";

import ChangePassModal from "@/components/common/modal/ChangePassModal";
import {userService} from "@/service/user.service";
import VLoading from "@/components/common/VLoading";
import {formatDate, mapValue} from "@/config/config";
import VTag from "@/components/common/VTag";

export default {
  name: "UserPage",
  methods: {
    closeModal() {
      this.isModalVisible = false;
    },
    scrollTo(element) {
      this.$scrollTo(element);
    },
    loadUser() {
      this.loading = true;
      userService.getById(this.user.id).then(res => {
        if (res && res.data) {
          this.user = res.data;
          this.loadAccounts();
          this.loadContacts();
          this.loadLeads();
          this.loadTasks();
          mapValue(this.dataLeftDetail, [this.user.firstName, this.user.lastName, this.user.username, `<a href="mailto:${this.user.email}">${this.user.email}</a>`]);
          mapValue(this.dataRightDetail, [this.user.skype, this.user.phone, formatDate(this.user.createdAt, 'dd/MM/yyyy HH:mm'), this.user.createdByName]);
        } else {
          alert('Không có dữ liệu');
          this.$router.push('/users');
        }
      }).catch(err => alert(err))
      .finally(() => this.loading = false);
    },
    deleteUser() {
      if (!confirm("Xác nhận xóa!")) {
        return ;
      }
      this.loading = true;
      userService.remove(this.user.id).then(res => {
        if(res) {
          alert('Xóa thành công!');
          this.$router.push('/users');
        }
      }).finally(() => {
        this.loading = false;
      });
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
          this.totalPageAccount = Number(res.data.pageInfo.TotalPages);
          this.totalItemAccount = Number(res.data.pageInfo.TotalItems);
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
          this.totalPageContact= Number(res.data.pageInfo.TotalPages);
          this.totalItemContact = Number(res.data.pageInfo.TotalItems);
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
          this.totalPageLead = Number(res.data.pageInfo.TotalPages);
          this.totalItemLead = Number(res.data.pageInfo.TotalItems);
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
      if (type === 'LEAD') {
        this.pageSizeLead = Number(event);
        this.loadLeads();
      }
      if (type === 'ACCOUNT') {
        this.pageSizeAccount = Number(event);
        this.loadAccounts();
      }
      if (type === 'CONTACT') {
        this.pageSizeContact = Number(event);
        this.loadContacts();
      }
    },
    goToPage(event, type) {
      if (type === 'TASK') {
        this.currentPageTask = Number(event);
        this.loadTasks();
      }
      if (type === 'LEAD') {
        this.currentPageLead = Number(event);
        this.loadLeads();
      }
      if (type === 'ACCOUNT') {
        this.currentPageAccount = Number(event);
        this.loadAccounts();
      }
      if (type === 'CONTACT') {
        this.currentPageContact = Number(event);
        this.loadContacts();
      }
    }

  },
  created() {
    if (this.$route.query.id) {
      this.user.id = this.$route.query.id;
      this.loadUser();
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      user: {},
      loading: false,
      isModalVisible: false,

      currentPageTask: 1,
      pageSizeTask: 5,
      totalItemTask: 0,
      totalPageTask: 0,

      currentPageLead: 1,
      pageSizeLead: 1,
      totalItemLead: 0,
      totalPageLead:0,

      currentPageAccount: 1,
      pageSizeAccount: 5,
      totalItemAccount: 0,
      totalPageAccount:0,

      currentPageContact: 1,
      pageSizeContact: 5,
      totalItemContact: 0,
      totalPageContact:0,

      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnChangePass: {btnClass: 'btn-white px-3', icon: 'fa-refresh', text: 'Change Password'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      tags: [
        {bgColor: '#28D194', text: 'Finished', icon: 'tick.png'},
        {bgColor: '#DE0050', text: 'Cancelled', icon: 'close.png'},
        {bgColor: '#3BBCF3', text: 'Scheduled', icon: 'calendar_today.png'},
        {bgColor: '#F47500', text: 'Late', icon: 'alarm_off.png'}
      ],
      dataLeftDetail: [
        {key: 'First Name', value: 'Trịnh'},
        {key: 'Last Name', value: 'Việt Anh'},
        {key: 'Username', value: 'xinchaoVietAnh'},
        {key: 'Email', value: '<a href="">email@gmail.com</a>'}

      ],
      dataRightDetail: [
        {key: 'Skype Address', value: ''},
        {key: 'Phone Number', value: ''},
        {key: 'Created At', value: ''},
        {key: 'Created By', value: ''}
      ],
      leadOwnColumns: [
         'Name', 'Email', 'Phone', 'Website', 'Company Name', 'Skype'
      ],
      columns: [
          'Title',  'Type',  'Status',  'Start Date',  'End Date',  'Priority',  'Action'
      ],
      taskLst: [
      ],
      accountColumns: [
        'Account Name', 'Email', 'Phone', 'Website', 'Tax Code', 'Is Owner', 'Is Collaborator'
      ],
      accountLst: [

      ],
      contractColumns: [
        'Contract Name', 'Email', 'Phone', 'Mobile', 'Skype',
        'Is Owner', 'Is Collaborator'
      ],
      contractLst: [

      ],
      leadOwnLst: [

      ],
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#tasks', text: 'Tasks'},
        {id: '#leadOwn', text: 'Lead Own'},
        {id: '#allAccounts', text: 'All Accounts'},
        {id: '#allContacts', text: 'All Contacts'},
      ]

    }
  },
  components: {VTag, VLoading, ChangePassModal, TableInDetail, BasicInfo, UserInfo, MenuLeft, VButton}
}
</script>

<style scoped>

</style>