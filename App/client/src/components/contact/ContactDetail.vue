<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'ContactList'}">
            <VButton :data="btnBack"/>
          </router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link class="ml-4" :to="{name: 'ContactUpdate', query: {id: contact.id}}">
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
            <UserInfo ref="userInfo" @create-tag="createTag" :tags="contact.tags" :image="contact.avatar" :is-show-avatar="true"
                      :title="'Contact Name'" :title-detail="contact.name"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="address">
            <BasicInfo :arr-left="dataLeftAddress" :arr-right="[]" :title="'Address'"/>
          </div>
          <div class="row mt-3" id="notes" v-if="contact.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="createNote" :notes="contact.notes"/>
          </div>
          <div class="row mt-3 mb-5" id="task">
            <TableInDetail :header-columns="taskColumns" :data="taskLst" :title="'Tasks'"
                           :page-config="{page: currentPageTask, pageSize: pageSizeTask, totalItems: totalItemTask, totalPage: totalPageTask}"
                           @page-size-change="onPageSizeChange($event)"
                           @go-to-page="goToPage($event)">
              <template slot="button">
                <router-link class="mr-2" :to="{name: 'TaskCreate', query: {contactId: contact.id}}">
                  <VButton :data="btnCreateTask"/>
                </router-link>
                <router-link :to="{name: 'CallCreate', query: {contactId: contact.id}}">
                  <VButton :data="btnCreateCall"/>
                </router-link>
              </template>
              <template slot="body">
                <tbody v-if="taskLst && taskLst.length > 0">
                <tr v-for="(t, i) in taskLst" :key="i">
                  <td>{{t.title}}</td>
                  <td>{{ t.type }}</td>
                  <td>{{ t.status }}</td>
                  <td>{{ t.startDate | formatDate }}</td>
                  <td>{{ t.endDate | formatDate }}</td>
                  <td>{{ t.priority }}</td>
                  <td>{{ t.owner ? t.owner.username : '' }}</td>
                  <td>
                    <span class="action">
                        <span @click="editTask(t.id)" class="mr-1"><img src="../../../public/images/newspaper-line.png" alt=""></span>
                        <span @click="deleteTask(t.id)"><img src="../../../public/images/delete-bin-2-line.png" alt=""></span>
                    </span>
                  </td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr>
                  <td class="text-center" colspan="8">Không có dữ liệu</td>
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
import {DATE_TIME_FORMAT, formatDate, getValueInArr, mapValue} from "@/config/config";
import {contactService} from "@/service/contact.service";
import VLoading from "@/components/common/VLoading";
import TableInDetail from "@/components/common/table/TableInDetail";
import {taskService} from "@/service/task.service";

export default {
  name: "ContactDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },
    editTask(id) {
      this.$router.push({path: '/tasks/update', query : { id: id}});
    },
    deleteTask(id) {
      if (!confirm("Xác nhận xóa!")) {
        return ;
      }
      this.loading = true;
      taskService.remove(id).then(res => {
        if(res) {
          alert('Xóa thành công!');
          this.loadTaskByContact();
        }
      }).finally(() => {
        this.loading = false;
      });
    },
    async loadTaskByContact() {
      this.taskLst = [];
      let query = {
        currentPage: this.currentPageTask,
        pageSize: this.pageSizeTask
      };
      await contactService.loadTasks(query, this.contact.id)
          .then(res => {
            if (res && res.data) {
              this.taskLst = res.data.tasks;
              this.totalPageTask= Number(res.data.pageInfo.TotalPages);
              this.totalItemTask = Number(res.data.pageInfo.TotalItems);
            }
          })
    },
    loadContact() {
      this.loading = true;
      contactService.getById(this.contact.id).then(res => {
        if (res && res.data) {
          this.contact = res.data;
          this.loadTaskByContact();
          mapValue(this.dataLeftBaseInfo, [
            this.contact.owner ? this.contact.owner.username : '',
            this.contact.account ? this.contact.account.name : '',
            `<i class="fa fa-envelope-o"></i> <a href="mailto:${this.contact.email}">${this.contact.email}</a>`,
            `<i class="fa fa-phone"></i> ${this.contact.phone}`
          ]);
          mapValue(this.dataLeftDetail, [
            this.contact.owner ? this.contact.owner.username : '',
            this.contact.account ? this.contact.account.username : '',
            this.contact.email,
            this.contact.phone,
            this.contact.mobile,
            this.contact.departmentName,
            formatDate(this.contact.birthday, DATE_TIME_FORMAT),
            getValueInArr(this.contact.priorities, 'selected', 'name'),
            this.contact.skype
          ]);
          mapValue(this.dataRightDetail, [
            this.contact.noEmail,
            this.contact.noCall,
            this.contact.assistantName,
            this.contact.assistantPhone,
            this.contact.CreatedBy ? this.contact.CreatedBy.username : '',
            formatDate(this.contact.CreatedAt, DATE_TIME_FORMAT),
            this.contact.ModifiedBy ? this.contact.ModifiedBy.username : '',
            formatDate(this.contact.ModifiedAt, DATE_TIME_FORMAT),
            this.contact.collaborator ? this.contact.collaborator.username : '',
          ]);
          mapValue(this.dataLeftAddress, [
            this.contact.country,
            this.contact.city,
            this.contact.addressDetail,
          ]);

        } else {
          alert('Không có dữ liệu');
          this.$router.push('/contacts');
        }
      }).catch(err => alert(err))
          .finally(() => this.loading = false);
    },
    removeNote(noteId) {
      if (!confirm('Xác nhận xóa!')) {
        return;
      }
      contactService.removeNote(this.contact.id, noteId)
          .then(res => {
            if (res) {
              this.loadContact();
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
      contactService.createNote(formData, this.contact.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadContact();
        }
      }).catch(err => alert(err))
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return;
      }
      contactService.remove(this.contact.id)
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
      contactService.createTag({name: event}, this.contact.id)
          .then(res => {
            if (res) {
              alert('Thành công!');
              this.$refs.userInfo.closeModal();
              this.loadContact();
            }
          })
    },
    onPageSizeChange(event) {
      this.pageSizeTask = Number(event);
      this.loadTaskByContact();
    },
    goToPage(event) {
      this.currentPageTask = Number(event);
      this.loadTaskByContact();
    }
  },
  created() {
    if (this.$route.query.id) {
      this.contact.id = this.$route.query.id;
      this.loadContact();
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      contact: {},
      loading: false,
      taskLst: [],
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#detail', text: 'Detail'},
        {id: '#address', text: 'Address'},
        {id: '#notes', text: 'Notes'},
        {id: '#task', text: 'Task'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      btnCreateTask: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Task'},
      btnCreateCall: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Call'},
      dataLeftBaseInfo: [
        {key: 'Contact Owner', value: 'Lead'},
        {key: 'Account', value: 's'},
        {key: 'Email', value: 's'},
        {key: 'Phone', value: 's'}
      ],
      dataLeftDetail: [
        {key: 'Contact Owner', value: 'Lead'},
        {key: 'Account', value: 's'},
        {key: 'Email', value: 's'},
        {key: 'Phone', value: 's'},
        {key: 'Mobile', value: 's'},
        {key: 'Department Name', value: 's'},
        {key: 'Birthday', value: 's'},
        {key: 'Priority', value: 's'},
        {key: 'Skype', value: 's'},
      ],
      dataRightDetail: [
        {key: 'No Email', value: ''},
        {key: 'No Call', value: ''},
        {key: 'Assistant Name', value: ''},
        {key: 'Assistant Phone', value: ''},
        {key: 'Created By', value: ''},
        {key: 'Create At', value: ''},
        {key: 'Modified By', value: ''},
        {key: 'Modified At', value: ''},
        {key: 'Contact Collaborator', value: ''}
      ],
      dataLeftAddress: [
        {key: 'Country', value: ''},
        {key: 'City', value: ''},
        {key: 'Address Detail', value: ''}
      ],
      taskColumns: ['Title', 'Type', 'Status', 'Start Date', 'End Date', 'Priority', 'Owner', 'Action'],
      currentPageTask: 1,
      pageSizeTask: 5,
      totalItemTask: 0,
      totalPageTask: 0,

    }
  },
  components: {TableInDetail, VLoading, Note, BasicInfo, UserInfo, MenuLeft, VButton}
}
</script>

<style scoped>

</style>