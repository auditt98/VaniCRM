<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <VButton :data="btnBack"/>
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link :to="{name: 'CallUpdate', query: {id: call.id}}"><VButton :data="btnEdit"/></router-link>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <UserInfo ref="userTags" :tags="call.tags" :title="'Tags'" @create-tag="createTag" />
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="call.description" :title="'Description'"/>
          </div>
          <div class="row mt-3" id="notes" v-if="call.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="submitNote" :notes="call.notes"/>
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
import {DATE_TIME_FORMAT, formatDate, getValueInArr, mapValue} from "@/config/config";
import {callService} from "@/service/call.service";
import VLoading from "@/components/common/VLoading";
import {RRule} from "rrule";

export default {
  name: "CallDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },
    loadCall() {
      this.loading = true;
      callService.getById(this.call.id).then(res => {
        if (res && res.data) {
          this.call = res.data;
          mapValue(this.dataLeftBaseInfo, [
            `<span class="text-danger">${getValueInArr(this.call.types, 'selected', 'name')}</span>`,
            getValueInArr(this.call.purposes, 'selected', 'name'),
            `<span class="text-success">${getValueInArr(this.call.statuses, 'selected', 'name')}</span>`,
            this.call.contact ? this.call.contact.name : '',
            this.call.owner ? this.call.owner.username : '',
          ]);
          mapValue(this.dataLeftDetail, [
            this.call.owner ? this.call.owner.username : '',
            this.call.title,
            this.call.contact ? this.call.contact.name : '',
            this.call.lead ? this.call.lead.name : '',
            `<span class="text-success">${getValueInArr(this.call.statuses, 'selected', 'name')}</span>`,
            `<span class="text-danger">${getValueInArr(this.call.types, 'selected', 'name')}</span>`,
            getValueInArr(this.call.purposes, 'selected', 'name'),
            getValueInArr(this.call.results, 'selected', 'name'),
            formatDate(this.call.startTime, DATE_TIME_FORMAT),
            getValueInArr(this.call.priorities, 'selected', 'name')

          ]);
          mapValue(this.dataRightDetail, [
            this.call.relatedDeal ? this.call.relatedDeal.name : '',
            this.call.relatedAccount ? this.call.relatedAccount.name : '',
            this.call.relatedCampaign ? this.call.relatedCampaign.name : '',
            this.call.duration,
            this.call.createdBy ? this.call.createdBy.username : '',
            formatDate(this.call.createdAt, DATE_TIME_FORMAT),
            this.call.modifiedBy ? this.call.modifiedBy.username : '',
            formatDate(this.call.modifiedAt, DATE_TIME_FORMAT),
            RRule.fromString(this.call.rrule).toText(),
          ]);
        } else {
          this.$router.push('/');
        }
      }).finally(() => this.loading = false);
    },
    removeNote(noteId) {
      console.log(noteId)
      if (!confirm('Xác nhận xóa!')) {
        return;
      }
      callService.removeNote(this.call.id, noteId)
          .then(res => {
            if (res) {
              this.loadCall();
            }
          });
    },
    submitNote(event) {
      let formData = new FormData();
      formData.append("body", event.text);
      if (event.files && event.files.length > 0) {
        for (let i = 0; i < event.files.length; i++) {
          formData.append("files" + i, event.files[i]);
        }
      }
      callService.createNote(formData, this.call.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadCall();
        }
      }).catch(err => alert(err))
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return;
      }
      callService.remove(this.call.id)
          .then(res => {
            if (res) {
              alert('Thành công');
              this.$router.push('/')
            }
          })
    },
    createTag(event) {
      if (!confirm('Xác nhận!')) {
        return ;
      }
      callService.createTag({name: event}, this.call.id)
          .then(res => {
            if (res) {
              alert('Thành công!');
              this.$refs['userTags'].closeModal();
              this.loadCall();
            }
          })
    },
  },
  created() {
    if (this.$route.query.id) {
      this.call.id = this.$route.query.id;
      this.loadCall();
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      call: {},
      loading: false,
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#detail', text: 'Detail'},
        {id: '#description', text: 'Description'},
        {id: '#notes', text: 'Notes'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      dataLeftBaseInfo: [
        {key: 'Call Type', value: 'Lead'},
        {key: 'Call Purpose', value: '<i class="fa fa-envelope-o"></i> <a href="">email@gmail.com</a>'},
        {key: 'Status', value: '<i class="fa fa-phone"></i> 0987654321'},
        {key: 'Contact Name', value: ''},
        {key: 'Call Owner', value: ''}
      ],
      dataLeftDetail: [
        {key: 'Call Owner', value: 'Lead'},
        {key: 'Title', value: '<i class="fa fa-envelope-o"></i> <a href="">email@gmail.com</a>'},
        {key: 'Contact Name', value: '<i class="fa fa-phone"></i> 0987654321'},
        {key: 'Lead Name', value: ''},
        {key: 'Status', value: ''},
        {key: 'Call Type', value: ''},
        {key: 'Call Purpose', value: ''},
        {key: 'Call Result', value: ''},
        {key: 'Start Time', value: ''},
        {key: 'Priority', value: ''},
      ],
      dataRightDetail: [
        {key: 'Related Deal', value: ''},
        {key: 'Related Account', value: ''},
        {key: 'Related Campaign', value: ''},
        {key: 'Call Duration', value: ''},
        {key: 'Create By', value: ''},
        {key: 'Create At', value: ''},
        {key: 'Modified By', value: ''},
        {key: 'Modified At', value: ''},
        {key: 'Repeat', value: ''},
      ],
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum',

    }
  },
  components: {VLoading, Note, BasicInfo, UserInfo, MenuLeft, VButton}
}
</script>

<style scoped>

</style>