<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'TaskList'}"><VButton :data="btnBack"/></router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <a :href="task.link" target="_blank"><VButton :data="btnOpen"/></a>
          <span class="ml-5" @click="remove"><VButton :data="btnDelete"/></span>
          <router-link class="ml-5" :to="{name: 'TaskUpdate', query: {id: task.id}}"><VButton :data="btnEdit"/></router-link>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <UserInfo ref="userTags" :tags="task.tags" :title="task.title" @create-tag="createTag" />
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="task.description" :title="'Description'"/>
          </div>
          <div class="row mt-3" id="notes" v-if="task.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="submitNote" :notes="task.notes"/>
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
import {taskService} from "@/service/task.service";
import VLoading from "@/components/common/VLoading";
import {RRule} from "rrule";

export default {
  name: "TaskDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },
    loadTask() {
      this.loading = true;
      taskService.getById(this.task.id).then(res => {
        if (res && res.data) {
          this.task = res.data;
          mapValue(this.dataLeftBaseInfo, [
            `<span class="text-danger">${getValueInArr(this.task.priorities, 'selected', 'name')}</span>`,
            formatDate(this.task.startDate, DATE_TIME_FORMAT),
            formatDate(this.task.dueDate, DATE_TIME_FORMAT),
            `<span class="text-success">${getValueInArr(this.task.statuses, 'selected', 'name')}</span>`,
            this.task.contact ? this.task.contact.name : '',
            this.task.owner ? this.task.owner.username : '',
          ]);
          mapValue(this.dataLeftDetail, [
            this.task.owner ? this.task.owner.username : '',
            this.task.title,
            formatDate(this.task.startDate, DATE_TIME_FORMAT),
            formatDate(this.task.dueDate, DATE_TIME_FORMAT),
            this.task.contact ? this.task.contact.name : '',
            this.task.relatedAccount ? this.task.relatedAccount.name : '',
            `<span class="text-success">${getValueInArr(this.task.statuses, 'selected', 'name')}</span>`,
            getValueInArr(this.task.priorities, 'selected', 'name'),

          ]);
          mapValue(this.dataRightDetail, [
            this.task.isRepeat,
            RRule.fromString(this.task.rrule).toText(),
            // '',
            formatDate(this.task.createdAt, DATE_TIME_FORMAT),
            this.task.createdBy ? this.task.createdBy.username : '',
            this.task.modifiedBy ? this.task.modifiedBy.username : '',
            formatDate(this.task.modifiedAt, DATE_TIME_FORMAT),
          ]);
        } else {
          alert('Không có dữ liệu');
          this.$router.push('/tasks');
        }
      }).finally(() => this.loading = false);
    },
    removeNote(noteId) {
      console.log(noteId)
      if (!confirm('Xác nhận xóa!')) {
        return;
      }
      taskService.removeNote(this.task.id, noteId)
          .then(res => {
            if (res) {
              this.loadTask();
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
      taskService.createNote(formData, this.task.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadTask();
        }
      }).catch(err => alert(err))
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return;
      }
      console.log(this.task.id, "tasks")
      taskService.remove(this.task.id, "tasks")
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
      taskService.createTag({name: event}, this.task.id)
          .then(res => {
            if (res) {
              alert('Thành công!');
              this.$refs['userTags'].closeModal();
              this.loadTask();
            }
          })
    },
  },
  created() {
    if (this.$route.query.id) {
      this.task.id = this.$route.query.id;
      this.loadTask();
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      task: {},
      loading: false,
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#detail', text: 'Detail'},
        {id: '#description', text: 'Description'},
        {id: '#notes', text: 'Notes'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnOpen: {btnClass: 'btn-white px-3', icon: 'fa-google', text: 'Open in Calendar'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      dataLeftBaseInfo: [
        {key: 'Priority', value: 'Lead'},
        {key: 'Start Date', value: ''},
        {key: 'Due Date', value: ''},
        {key: 'Status', value: ''},
        {key: 'Contact Name', value: ''},
        {key: 'Task Owner', value: ''}
      ],
      dataLeftDetail: [
        {key: 'Task Owner', value: 'Lead'},
        {key: 'Title', value: ''},
        {key: 'Start Date', value: ''},
        {key: 'Due Date', value: ''},
        {key: 'Contact ', value: ''},
        {key: 'Account', value: ''},
        {key: 'Status', value: ''},
        {key: 'Priority', value: ''},
      ],
      dataRightDetail: [
        {key: 'Reminder', value: ''},
        {key: 'Repeat', value: ''},
        // {key: 'End', value: ''},
        {key: 'Created At', value: ''},
        {key: 'Created By', value: ''},
        {key: 'Modified By', value: ''},
        {key: 'Modified At', value: ''},
      ],
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum',

    }
  },
  components: {VLoading, Note, BasicInfo, UserInfo, MenuLeft, VButton}
}
</script>

<style scoped>

</style>