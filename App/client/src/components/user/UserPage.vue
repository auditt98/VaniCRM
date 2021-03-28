<template>
  <div class="mb-5">
    <Header/>
    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
          <VButton :data="btnBack"/>
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <span @click="isModalVisible = true"><VButton :data="btnChangePass"/></span>
          <VButton :data="btnDelete"/>
          <VButton :data="btnEdit"/>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <UserInfo :image="'x'" :title="'User'" :title-detail="'Username'"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="tasks">
            <TableInDetail :header-columns="columns" :data="taskLst" :tags="tags" :title="'Tasks'"/>
          </div>
          <div class="row mt-3" id="leadOwn">
            <TableInDetail :header-columns="leadOwnColumns" :data="[]" :title="'Lead Own'"/>
          </div>
          <div class="row mt-3" id="allAccounts">
            <TableInDetail :header-columns="accountColumns" :data="accountLst" :title="'All Accounts'"/>
          </div>
          <div class="row mt-3" id="allContacts">
            <TableInDetail :header-columns="contractColumns" :data="contractLst" :title="'All Contracts'"/>
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
import Header from "@/components/common/Header";
import ChangePassModal from "@/components/common/modal/ChangePassModal";
import {userService} from "@/service/user.service";

export default {
  name: "UserPage",
  methods: {
    closeModal() {
      this.isModalVisible = false;
    },
    scrollTo(element) {
      this.$scrollTo(element);
    }
  },
  created() {
    if (this.$route.query.id) {
      this.user.id = this.$route.query.id;
      userService.getById(this.$route.query.id).then(res => {
        console.log(res)
      }).catch(err => alert(err));
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      user: {},
      isModalVisible: false,
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
         'Name', 'Email', 'Phone', 'Mobile', 'Facebook', 'Skype'
      ],
      columns: [
          'Title',  'Type',  'Status',  'Start Date',  'End Date',  'Priority',  'Owner',  'Call Duration',  'Action'
      ],
      taskLst: [
      ],
      accountColumns: [
        'Account Name', 'Email', 'Phone', 'Website', 'Tax Code', 'Is Owner', 'Is Collaborator'
      ],
      accountLst: [

      ],
      contractColumns: [
        'Contract Name', 'Email', 'Phone', 'Mobile', 'Facebook', 'Skype',
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
  components: {ChangePassModal, Header, TableInDetail, BasicInfo, UserInfo, MenuLeft, VButton}
}
</script>

<style scoped>

</style>