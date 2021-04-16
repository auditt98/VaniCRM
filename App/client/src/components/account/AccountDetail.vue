<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'AccountList'}">
            <VButton :data="btnBack"/>
          </router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link class="ml-4" :to="{name: 'AccountUpdate', query: {id: account.id}}">
            <VButton :data="btnEdit"/>
          </router-link>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <UserInfo ref="userInfo" @create-tag="createTag" :tags="account.tags" :image="account.avatar"
                      :title="'Account Name'" :title-detail="account.name"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="bankingInformation">
            <BasicInfo :arr-left="dataLeftBanking" :arr-right="[]" :title="'Banking Information'"/>
          </div>
          <div class="row mt-3" id="address">
            <BasicInfo :arr-left="dataLeftAddress" :arr-right="[]" :title="'Address'"/>
          </div>
          <div class="row mt-3" id="notes" v-if="account.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="createNote" :notes="account.notes"/>
          </div>
          <div class="row mt-3" id="allContacts">
            <TableInDetail :header-columns="contractColumns" :data="contactLst" :title="'All Contracts'"
                           :page-config="{page: currentPageContact, pageSize: pageSizeContact, totalItems: totalItemContact, totalPage: totalPageContact}"
                           @page-size-change="onPageSizeChange($event)"
                           @go-to-page="goToPage($event)">
              <template slot="button">
                <router-link class="mr-2" :to="{name: 'ContactCreate', query: {accountId: account.id}}">
                  <VButton :data="btnCreateContact"/>
                </router-link>
              </template>
              <template slot="body">
                <tbody v-if="contactLst && contactLst.length > 0">
                <tr v-for="(t, i) in contactLst" :key="i">
                  <td>{{ t.contactName }}</td>
                  <td><a :href="'mailto:'+t.email">{{ t.email }}</a></td>
                  <td>{{ t.phone }}</td>
                  <td>{{ t.mobile }}</td>
                  <td><a href="#">{{ t.mobile }}</a></td>
                  <td><a href="#">{{ t.mobile }}</a></td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr>
                  <td class="text-center" colspan="6">Không có dữ liệu</td>
                </tr>
                </tbody>
              </template>
            </TableInDetail>
          </div>

        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VButton from "@/components/common/VButton";

import MenuLeft from "@/components/common/MenuLeft";
import UserInfo from "@/components/common/info/UserInfo";
import BasicInfo from "@/components/common/info/BasicInfo";
import Note from "@/components/common/info/Note";
import {DATE_TIME_FORMAT, formatDate, mapValue} from "@/config/config";
import {accountService} from "@/service/account.service";
import VLoading from "@/components/common/VLoading";
import TableInDetail from "@/components/common/table/TableInDetail";

export default {
  name: "AccountDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },
    async loadContactByAccount() {
      this.loading = true;
      this.accountLst = [];
      let query = {
        currentPage: this.currentPageContact,
        pageSize: this.pageSizeContact
      };
      await accountService.loadContacts(query, this.account.id)
      .then(res => {
        if (res && res.data) {
          this.contactLst = res.data.contacts;
        }
      })
    },
    loadAccount() {
      this.loading = true;
      accountService.getById(this.account.id).then(res => {
        if (res && res.data) {
          this.account = res.data;
          this.loadContactByAccount();
          mapValue(this.dataLeftBaseInfo, [
            this.account.owner ? this.account.owner.username : '',
            this.account.name,
            `<i class="fa fa-envelope-o"></i> <a href="mailto:${this.account.email}">${this.account.email}</a>`,
            `<i class="fa fa-phone"></i> ${this.account.phone}`
          ]);
          mapValue(this.dataLeftDetail, [
            this.account.owner ? this.account.owner.username : '',
            this.account.name,
            this.account.email,
            this.account.phone,
            this.account.fax,
            this.account.website,
            this.account.taxCode,
            this.account.convertedFrom ? this.account.convertedFrom : 'Empty',
          ]);
          mapValue(this.dataRightDetail, [
            this.account.numberOfEmployees,
            this.account.annualRevenue,
            this.account.collaborator ? this.account.collaborator.username : '',
            this.account.CreatedBy ? this.account.CreatedBy.username : '',
            formatDate(this.account.CreatedAt, DATE_TIME_FORMAT),
            this.account.ModifiedBy ? this.account.ModifiedBy.username : '',
            formatDate(this.account.ModifiedAt, DATE_TIME_FORMAT),
          ]);
          mapValue(this.dataLeftBanking, [
            this.account.bankName,
            this.account.bankAccountName,
            this.account.bankAccount,
          ]);
          mapValue(this.dataLeftAddress, [
            this.account.country,
            this.account.city,
            this.account.addressDetail,
          ]);

        } else {
          this.$router.push('/');
        }
      }).catch(err => alert(err))
          .finally(() => this.loading = false);
    },
    removeNote(noteId) {
      if (!confirm('Xác nhận xóa!')) {
        return;
      }
      accountService.removeNote(this.account.id, noteId)
          .then(res => {
            if (res) {
              this.loadAccount();
            }
          });
    },
    createNote(event) {
      let formData = new FormData();
      formData.append("body", event.text);
      if (event.files && event.files.length > 0) {
        for (let i = 0; i < event.files.length; i++) {
          formData.append("files" + i, event.files[i]);
        }
      }
      accountService.createNote(formData, this.account.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadAccount();
        }
      }).catch(err => alert(err))
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return;
      }
      accountService.remove(this.account.id)
          .then(res => {
            if (res) {
              alert('Thành công');
              this.$router.push('/accounts')
            }
          })
    },
    createTag(event) {
      if (!confirm('Xác nhận!')) {
        return;
      }
      accountService.createTag({name: event}, this.account.id)
          .then(res => {
            if (res) {
              alert('Thành công!');
              this.$refs.userInfo.closeModal();
              this.loadAccount();
            }
          })
    },
    onPageSizeChange(event) {
      this.pageSizeContact = Number(event);
      this.loadContactByAccount();
    },
    goToPage(event) {
      this.currentPageContact = Number(event);
      this.loadContactByAccount();
    }
  },
  created() {
    if (this.$route.query.id) {
      this.account.id = this.$route.query.id;
      this.loadAccount();
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      account: {},
      loading: false,
      contactLst: [],
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#detail', text: 'Detail'},
        {id: '#bankingInformation', text: 'Banking Information'},
        {id: '#address', text: 'Address'},
        {id: '#notes', text: 'Notes'},
        {id: '#contact', text: 'Contact'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      btnCreateContact: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Contact'},
      dataLeftBaseInfo: [
        {key: 'Account Owner', value: 'Lead'},
        {key: 'Account Name', value: 's'},
        {key: 'Email', value: 's'},
        {key: 'Phone', value: 's'}
      ],
      dataLeftDetail: [
        {key: 'Account Owner', value: 'Lead'},
        {key: 'Account Name', value: 'Lead'},
        {key: 'Email', value: 's'},
        {key: 'Phone', value: 's'},
        {key: 'Fax', value: 's'},
        {key: 'Website', value: 's'},
        {key: 'Tax Code', value: 's'},
        {key: 'Converted From', value: 's'},
      ],
      dataRightDetail: [
        {key: 'Number Of Employees', value: ''},
        {key: 'Annual Revenue', value: ''},
        {key: 'Account Collaborator', value: ''},
        {key: 'Created By', value: ''},
        {key: 'Create At', value: ''},
        {key: 'Modified By', value: ''},
        {key: 'Modified At', value: ''}
      ],
      dataLeftBanking: [
        {key: 'Bank Name', value: ''},
        {key: 'Bank Account Name', value: ''},
        {key: 'Bank Account', value: ''}
      ],
      dataLeftAddress: [
        {key: 'Country', value: ''},
        {key: 'City', value: ''},
        {key: 'Address Detail', value: ''}
      ],
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum',
      contractColumns: ['Contract Name', 'Email', 'Phone', 'Mobile', 'Facebook', 'Skype'],
      currentPageContact: 1,
      pageSizeContact: 5,
      totalItemContact: 0,
      totalPageContact:0,
    }
  },
  components: {TableInDetail, VLoading, Note, BasicInfo, UserInfo, MenuLeft, VButton}
}
</script>

<style scoped>

</style>