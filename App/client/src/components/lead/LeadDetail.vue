<template>
  <div class="">
    <Header/>
    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
          <VButton :data="btnBack"/>
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <VButton :data="btnConvert"/>
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
            <UserInfo :tags="['xxx']"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="description" :title="'Description'"/>
          </div>
          <div class="row mt-3" id="address">
            <BasicInfo :arr-left="dataLeftAddress" :title="'Address'"/>
          </div>
          <div class="row mt-3" id="notes">
            <Note/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VButton from "@/components/common/VButton";
import Header from "@/components/common/Header";
import MenuLeft from "@/components/common/MenuLeft";
import UserInfo from "@/components/common/info/UserInfo";
import BasicInfo from "@/components/common/info/BasicInfo";
import Note from "@/components/common/info/Note";

export default {
  name: "LeadDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    }
  },
  data: function () {
    return {
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#detail', text: 'Detail'},
        {id: '#description', text: 'Description'},
        {id: '#address', text: 'Addresses'},
        {id: '#notes', text: 'Notes'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnConvert: {btnClass: 'btn-white px-3', icon: 'fa-exchange', text: 'Convert To Account'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      dataLeftBaseInfo: [
        {key: 'Name', value: 'Lead'},
        {key: 'Email', value: '<i class="fa fa-envelope-o"></i> <a href="">email@gmail.com</a>'},
        {key: 'Phone', value: '<i class="fa fa-phone"></i> 0987654321'},
        {key: 'Status', value: ''},
        {key: 'Priority', value: ''}
      ],
      dataLeftDetail: [
        {key: 'Name', value: 'Lead'},
        {key: 'Email', value: '<i class="fa fa-envelope-o"></i> <a href="">email@gmail.com</a>'},
        {key: 'Phone', value: '<i class="fa fa-phone"></i> 0987654321'},
        {key: 'Fax', value: ''},
        {key: 'Industry', value: ''},
        {key: 'Website', value: ''},
        {key: 'Annual Revenue', value: ''},
        {key: 'Lead Status', value: ''},
        {key: 'Skype', value: ''},
        {key: 'Priority', value: ''}
      ],
      dataRightDetail: [
        {key: 'No Email', value: ''},
        {key: 'No Call', value: ''},
        {key: 'Lead Source', value: ''},
        {key: 'Company Name', value: ''},
        {key: 'Number Of Employee', value: ''},
        {key: 'Create At', value: ''},
        {key: 'Create By', value: ''},
        {key: 'Modified At', value: ''},
        {key: 'Modified By', value: ''}
      ],
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum',
      dataLeftAddress: [
        {key: 'Country', value: ''},
        {key: 'City', value: ''},
        {key: 'Address Detail', value: ''}
      ]
    }
  },
  components: {Note, BasicInfo, UserInfo, MenuLeft, Header, VButton}
}
</script>

<style scoped>

</style>