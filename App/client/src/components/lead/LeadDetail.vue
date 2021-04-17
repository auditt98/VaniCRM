<template>
  <div class="" >

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'LeadList'}"><VButton :data="btnBack"/></router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <span @click="convertToAccount"><VButton :data="btnConvert"/></span>
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link :to="{name: 'LeadUpdate', query: {id: lead.id}}"><VButton :data="btnEdit"/></router-link>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <UserInfo ref="userInfo" @create-tag="createTag" :tags="lead.tags" :image="lead.avatar" :title="'Lead'" :title-detail="lead.name"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="lead.description" :title="'Description'"/>
          </div>
          <div class="row mt-3" id="address">
            <BasicInfo :arr-left="dataLeftAddress" :title="'Address'"/>
          </div>
          <div class="row mt-3" id="notes" v-if="lead.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="createNote" :notes="lead.notes"/>
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
import {formatDate, getValueInArr, mapValue} from "@/config/config";
import {leadService} from "@/service/lead.service";
import VLoading from "@/components/common/VLoading";

export default {
  name: "LeadDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },

    loadLead() {
      this.loading = true;
      leadService.getById(this.lead.id).then(res => {
        if (res && res.data) {
          this.lead = res.data;
          mapValue(this.dataLeftBaseInfo, [
              this.lead.name,
              `<i class="fa fa-envelope-o"></i> <a href="mailto:${this.lead.email}">${this.lead.email}</a>`,
              `<i class="fa fa-phone"></i> ${this.lead.phone}` ,
              getValueInArr(this.lead.status, 'selected', 'name'),
              getValueInArr(this.lead.priority, 'selected', 'name')
          ]);
          mapValue(this.dataLeftDetail, [
            this.lead.name,
            `<i class="fa fa-envelope-o"></i> <a href="mailto:${this.lead.email}">${this.lead.email}</a>`,
            `<i class="fa fa-phone"></i> ${this.lead.phone}` ,
              this.lead.fax,
            getValueInArr(this.lead.industry, 'selected', 'name'),
            this.lead.website,
            this.lead.annualRevenue,
            getValueInArr(this.lead.status, 'selected', 'name'),
            this.lead.skype,
            getValueInArr(this.lead.priority, 'selected', 'name')

          ]);
          mapValue(this.dataRightDetail, [
              this.lead.noEmail,
              this.lead.noCall,
              getValueInArr(this.lead.leadSource, 'selected', 'name'),
              this.lead.companyName,
              formatDate(this.lead.CreatedAt, 'dd/MM/yyyy HH:mm'),
              this.lead.CreatedBy ? this.lead.CreatedBy.username : '',
              formatDate(this.lead.ModifiedAt, 'dd/MM/yyyy HH:mm'),
              this.lead.ModifiedBy ? this.lead.ModifiedBy.username : '',
          ]);
          mapValue(this.dataLeftAddress, [
             this.lead.country,
             this.lead.city,
             this.lead.addressDetail,
          ]);
        } else {
          alert('Không có dữ liệu');
          this.$router.push('/leads');
        }
      }).catch(err => alert(err))
          .finally(() => this.loading = false);
    },
    removeNote(noteId) {
      console.log(noteId)
      if (!confirm('Xác nhận xóa!')) {
        return ;
      }
      leadService.removeNote(this.lead.id, noteId)
      .then(res => {
        if (res) {
          this.loadLead();
        }
      });
    },
    createNote(event) {
      console.log(event)
      let formData = new FormData();
      formData.append("body", event.text);
      if (event.files && event.files.length > 0) {
        for (let i = 0; i < event.files.length; i++) {
          formData.append("files" + i, event.files[i]);
        }
      }
      leadService.createNote(formData, this.lead.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadLead();
        }
      }).catch(err => alert(err))
    },
    convertToAccount() {
      if (!confirm('Xác nhận chuyển')) {
        return ;
      }
      leadService.convertToAccount(this.lead.id)
          .then(res => {
            if (res && res.status === 'success') {
              alert('Thành công');
              this.$router.push('/leads');
            }
          })
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return ;
      }
      leadService.remove(this.lead.id)
      .then(res => {
        if (res) {
          alert('Thành công');
          this.$router.push('/leads');
        }
      })
    },
    createTag(event) {
      if (!confirm('Xác nhận!')) {
        return ;
      }
      leadService.createTag({name: event}, this.lead.id)
      .then(res => {
        if (res) {
          alert('Thành công!');
          this.$refs.userInfo.closeModal();
          this.loadLead();
        }
      })
    }
  },
  created() {
    if (this.$route.query.id) {
      this.lead.id = this.$route.query.id;
      this.loadLead();
    }
    // else {
    //   this.$router.push('/');
    // }
  },
  data: function () {
    return {
      lead: {},
      loading: false,
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
  components: {VLoading, Note, BasicInfo, UserInfo, MenuLeft, VButton}
}
</script>

<style scoped>

</style>