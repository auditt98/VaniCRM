<template>
  <div class="" >

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'LeadList'}"><VButton :data="btnBack"/></router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link :to="{name: 'MeetingUpdate', query: {id: meeting.id}}"><VButton :data="btnEdit"/></router-link>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <div class="basic-info d-flex p-3 w-100">
              <div class="user-info-detail ml-3">
                <h4>
                  <span><i class="fa fa-calendar-o"></i></span>
                  <strong>Meeting</strong> <small> - </small> <span>{{meeting.title}}</span>
                </h4>
                <h5 class="ml-4 pl-2" v-if="meeting.host">
                  Host: {{meeting.host.username}}
                </h5>
                <p class="ml-3" v-if="meeting.tags">
                  <img src="../../assets/tag.png" alt="">
                  <small class="user-tag ml-4" v-for="(t, i) in meeting.tags" :key="i">{{t.name}}</small>
                  <img class="cursor-pointer" @click="openModalCreateTag" src="../../assets/icon-plus.png" alt="">
                </p>
              </div>
            </div>
            <CreateTagModal
                v-show="isShowModal"
                @create-tag="createTag"
                @close="closeModal"
                ref="tagModal"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="otherInformation">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="[]" :title="'Other Information'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="meeting.description" :title="'Description'"/>
          </div>
          <div class="row mt-3" id="participants">
              <div class="w-100">
                <div class="basic-info p-3">
                  <div class="d-flex justify-content-between">
                    <h4 class="ml-2"><strong>Participants</strong></h4>
                    <span @click="openModalParticipant"><VButton :data="btnAdd"/></span>
                  </div>
                  <div class="row ml-2" v-if="meeting.participants">
                    <ul v-if="meeting.participants.users">
                      <li v-for="(u, i) in meeting.participants.users" :key="i">
                          {{u.username}} - {{u.email}}
                      </li>
                    </ul>
                    <ul v-if="meeting.participants.leads">
                      <li v-for="(u, i) in meeting.participants.leads" :key="i">
                        {{u.name}} - {{u.email}}
                      </li>
                    </ul>
                    <ul v-if="meeting.participants.contacts">
                      <li v-for="(u, i) in meeting.participants.contacts" :key="i">
                        {{u.name}} - {{u.email}}
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
          </div>
          <div class="row mt-3" id="notes" v-if="meeting.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="createNote" :notes="meeting.notes"/>
          </div>

        </div>
      </div>
    </div>
    <div v-if="meeting.id">
      <AddParticipantModal
          ref="modal"
          :meeting-id="Number(meeting.id)"
          v-show="isOpenModalParticipant"
          @close="closeModalParticipant"/>
    </div>
  </div>
</template>

<script>
import VButton from "@/components/common/VButton";

import MenuLeft from "@/components/common/MenuLeft";
import BasicInfo from "@/components/common/info/BasicInfo";
import Note from "@/components/common/info/Note";
import {DATE_TIME_FORMAT, formatDate, getValueInArr, mapValue} from "@/config/config";
import VLoading from "@/components/common/VLoading";
import CreateTagModal from "@/components/common/modal/CreateTagModal";
import {meetingService} from "@/service/meeting.service";
import {RRule} from 'rrule'
import AddParticipantModal from "@/components/common/modal/AddParticipantModal";

export default {
  name: "MeetingDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },

    loadMeeting() {
      this.loading = true;
      meetingService.getById(this.meeting.id).then(res => {
        if (res && res.data) {
          this.meeting = res.data;
          mapValue(this.dataLeftBaseInfo, [
            this.meeting.fromDate + ' - ' + this.meeting.toDate,
            this.meeting.location,
            this.meeting.isAllDay ? 'All Day' : 'No' ,
            RRule.fromString(this.meeting.rrule).toText(),
            getValueInArr(this.meeting.priorities, 'selected', 'name')
          ]);
          mapValue(this.dataLeftDetail, [
            this.meeting.createdBy ? this.meeting.createdBy.username : '',
            formatDate(this.meeting.createdAt, DATE_TIME_FORMAT),
            this.meeting.modifiedBy ? this.meeting.modifiedBy.username : '',
            formatDate(this.meeting.modifiedAt, DATE_TIME_FORMAT),
          ]);
          
        } else {
          //this.$router.push('/');
        }
      }).catch(err => alert(err))
          .finally(() => this.loading = false);
    },
    removeNote(noteId) {
      if (!confirm('Xác nhận xóa!')) {
        return ;
      }
      meetingService.removeNote(this.meeting.id, noteId)
          .then(res => {
            if (res) {
              this.loadMeeting();
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
      meetingService.createNote(formData, this.meeting.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadMeeting();
        }
      }).catch(err => alert(err))
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return ;
      }
      meetingService.remove(this.meeting.id)
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
      meetingService.createTag({name: event}, this.meeting.id)
          .then(res => {
            if (res) {
              alert('Thành công!');
              this.closeModal();
              this.loadMeeting();
            }
          })
    },
    openModalCreateTag() {
      this.isShowModal = true;
    },
    closeModal() {
      this.isShowModal = false;
    },
    async openModalParticipant() {
      await this.$refs['modal'].loadMeeting();
      this.$refs['modal'].loadItems();
      this.isOpenModalParticipant = true;
    },
    closeModalParticipant() {
      this.loadMeeting();
      this.isOpenModalParticipant = false;
    }
  },
  created() {
    if (this.$route.query.id) {
      this.meeting.id = this.$route.query.id;
      this.loadMeeting();
    }
    // else {
    //   this.$router.push('/');
    // }
  },
  data: function () {
    return {
      meeting: {},
      loading: false,
      isShowModal: false,
      isOpenModalParticipant: false,
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#otherInformation', text: 'Other Information'},
        {id: '#description', text: 'Description'},
        {id: '#participants', text: 'Participants'},
        {id: '#notes', text: 'Notes'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      btnAdd: {btnClass: 'btn-red px-4 color-white', icon: 'fa-plus-circle', text: 'Add'},
      dataLeftBaseInfo: [
        {key: '<i class="fa fa-clock-o" aria-hidden="true"></i>', value: 'Lead'},
        {key: '<i class="fa fa-map-marker" aria-hidden="true"></i>', value: ''},
        {key: '<i class="fa fa-calendar-o" aria-hidden="true"></i>', value: ''},
        {key: '<i class="fa fa-mars-stroke-v" aria-hidden="true"></i>', value: ''},
        {key: 'Priority', value: ''}
      ],
      dataLeftDetail: [
        {key: 'Created By', value: 'Lead'},
        {key: 'Created At', value: ''},
        {key: 'Modified By', value: ''},
        {key: 'Modified At', value: ''}
      ],
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum',

    }
  },
  components: {AddParticipantModal, CreateTagModal, VLoading, Note, BasicInfo, MenuLeft, VButton}
}
</script>

<style scoped>
.user-tag {
  border: 1px solid #F39800;
  box-sizing: border-box;
  border-radius: 30px;
  padding: 2px 15px;
  font-weight: bold;
  font-size: 10px;
}
</style>