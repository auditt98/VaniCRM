<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'DealList'}">
            <VButton :data="btnBack"/>
          </router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link class="ml-4" :to="{name: 'DealUpdate', query: {id: deal.id}}">
            <VButton :data="btnEdit"/>
          </router-link>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft @scroll-to="scrollTo" :elements="dataMenuLeft"/>
        </div>
        <div class="col-sm-10">
          <div class="row" id="tags">
            <UserInfo ref="userInfo" @create-tag="createTag" :tags="deal.tags"
                      :title="'Tags'"/>
          </div>
          <div class="row mt-3" id="timeline">
            <VTimeLine :timeline-status="stages"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="deal.description" :title="'Description'"/>
          </div>
          <div class="row mt-3" id="notes" v-if="deal.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="createNote" :notes="deal.notes"/>
          </div>
          <div class="row mt-3" id="competitor">
            <TableInDetail :header-columns="competitorColumns" :title="'Competitor'"
                           :page-config="{page: currentPageCompetitor, pageSize: pageSizeCompetitor, totalItems: totalItemCompetitor, totalPage: totalPageCompetitor}"
                           @page-size-change="onPageSizeChange($event, 'COMPETITOR')"
                           @go-to-page="goToPage($event, 'COMPETITOR')">
              <template slot="button">
                <span @click="openCompetitorModal"><VButton :data="btnCreateCompetitor"/></span>
              </template>
              <template slot="body">
                <tbody v-if="competitorLst && competitorLst.length > 0">
                <tr v-for="(t, i) in competitorLst" :key="i">
                  <td>{{ t.name }}</td>
                  <td>{{ t.website }}</td>
                  <td>{{ t.strengths }}</td>
                  <td>{{ t.weaknesses }}</td>
                  <td>{{ t.threat }}</td>
                  <td>{{ t.suggestions }}</td>
                  <td>
                    <span class="action">
                      <span @click="editCompetitor(t.id)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
                      <span @click="deleteCompetitor(t.id)"><img src="images/delete-bin-2-line.png" alt=""></span>
                    </span>
                  </td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr>
                  <td class="text-center" colspan="7">Không có dữ liệu</td>
                </tr>
                </tbody>
              </template>
            </TableInDetail>
          </div>
          <div class="row mt-3" id="history">
            <TableInDetail :header-columns="historyColumns" :title="'History'"
                           :page-config="{page: currentPageHistory, pageSize: pageSizeHistory, totalItems: totalItemHistory, totalPage: totalPageHistory}"
                           @page-size-change="onPageSizeChange($event, 'HISTORY')"
                           @go-to-page="goToPage($event, 'HISTORY')">
              <template slot="body">
                <tbody v-if="historyLst && historyLst.length > 0">

                </tbody>
                <tbody v-else>
                <tr>
                  <td class="text-center" colspan="5">Không có dữ liệu</td>
                </tr>
                </tbody>
              </template>
            </TableInDetail>
          </div>
          <div class="row mt-3 mb-5" id="task">
            <TableInDetail :header-columns="taskColumns" :data="taskLst" :title="'Tasks'"
                           :page-config="{page: currentPageTask, pageSize: pageSizeTask, totalItems: totalItemTask, totalPage: totalPageTask}"
                           @page-size-change="onPageSizeChange($event, 'TASK')"
                           @go-to-page="goToPage($event, 'TASK')">
              <template slot="button">
                <router-link class="mr-2" :to="{name: 'TaskCreate', query: {dealId: deal.id}}">
                  <VButton :data="btnCreateTask"/>
                </router-link>
                <router-link :to="{name: 'CallCreate', query: {dealId: deal.id}}">
                  <VButton :data="btnCreateCall"/>
                </router-link>
              </template>
              <template slot="body">
                <tbody v-if="taskLst && taskLst.length > 0">
                <tr v-for="(t, i) in taskLst" :key="i">
                  <td></td>
                  <td>{{ t.type }}</td>
                  <td>{{ t.status }}</td>
                  <td>{{ t.startDate | formatDate }}</td>
                  <td>{{ t.endDate | formatDate }}</td>
                  <td>{{ t.priority }}</td>
                  <td>{{ t.owner ? t.owner.username : '' }}</td>
                  <td>
                    <span class="action">
                        <span @click="editTask(t.id)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
                        <span @click="deleteTask(t.id)"><img src="images/delete-bin-2-line.png" alt=""></span>
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
          <AddCompetitorModal @close="closeCompetitorModal" @create-success="onCreateCompetitor" ref="cModal" v-show="openModalCompetitor"/>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VButton from "@/components/common/VButton";

import {DATE_TIME_FORMAT, formatDate, getValueInArr, mapValue} from "@/config/config";
import {dealService} from "@/service/deal.service";
import VLoading from "@/components/common/VLoading";
import {taskService} from "@/service/task.service";
import UserInfo from "@/components/common/info/UserInfo";
import MenuLeft from "@/components/common/MenuLeft";
import VTimeLine from "@/components/common/VTimeLine";
import Note from "@/components/common/info/Note";
import BasicInfo from "@/components/common/info/BasicInfo";
import TableInDetail from "@/components/common/table/TableInDetail";
import AddCompetitorModal from "@/components/common/modal/AddCompetitorModal";

export default {
  name: "DealDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },
    editCompetitor() {

    },
    deleteCompetitor() {

    },
    openCompetitorModal() {
      this.$refs['cModal'].refresh();
      this.$refs['cModal'].dealId = this.deal.id;
      this.openModalCompetitor = true;
    },
    onCreateCompetitor() {
      this.loadCompetitorByDeal();
      this.$refs['cModal'].close();
    },
    closeCompetitorModal() {
      this.openModalCompetitor = false;
    },
    editTask(id) {
      this.$router.push({path: '/task-update', query: {id: id}});
    },
    deleteTask(id) {
      if (!confirm("Xác nhận xóa!")) {
        return;
      }
      this.loading = true;
      taskService.remove(id).then(res => {
        if (res) {
          alert('Xóa thành công!');
          this.loadTaskByDeal();
        }
      }).finally(() => {
        this.loading = false;
      });
    },
    async loadTaskByDeal() {
      let query = {
        currentPage: this.currentPageTask,
        pageSize: this.pageSizeTask
      };
      await dealService.loadTasks(query, this.deal.id)
          .then(res => {
            if (res && res.data) {
              this.taskLst = res.data.tasks;
            }
          })
    },
    async loadCompetitorByDeal() {
      let query = {
        currentPage: this.currentPageCompetitor,
        pageSize: this.pageSizeCompetitor
      };
      await dealService.loadCompetitors(query, this.deal.id)
          .then(res => {
            if (res && res.data) {
              this.competitorLst = res.data.competitors;
            }
          })
    },
    loadDeal() {
      this.loading = true;
      dealService.getById(this.deal.id).then(res => {
        if (res && res.data) {
          this.deal = res.data;
          this.loadTaskByDeal();
          this.loadCompetitorByDeal();
          mapValue(this.dataLeftBaseInfo, [
            this.deal.name,
            this.deal.owner ? this.deal.owner.username : '',
            formatDate(this.deal.closingDate, DATE_TIME_FORMAT),
          ]);
          mapValue(this.dataLeftDetail, [
            this.deal.name,
            this.deal.owner ? this.deal.owner.username : '',
            formatDate(this.deal.closingDate, DATE_TIME_FORMAT),
            this.deal.account ? this.deal.account.name : '',
            this.deal.contact ? this.deal.contact.name : '',
            this.deal.campaign ? this.deal.campaign.name : '',
            getValueInArr(this.deal.lostReason, 'selected', 'name'),
            getValueInArr(this.deal.priorities, 'selected', 'name'),

          ]);
          mapValue(this.dataRightDetail, [
            this.deal.amount,
            this.deal.probability,
            Math.ceil(Number(this.deal.amount ? this.deal.amount : 0) * Number(this.deal.probability ? this.deal.probability : 0) / 100),
            getValueInArr(this.deal.stages, 'passed', 'name'),
            formatDate(this.deal.CreatedAt, DATE_TIME_FORMAT),
            this.deal.CreatedBy ? this.deal.CreatedBy.username : '',
            formatDate(this.deal.ModifiedAt, DATE_TIME_FORMAT),
            this.deal.ModifiedBy ? this.deal.ModifiedBy.username : '',
          ]);

        } else {
          alert('Không có dữ liệu');
          this.$router.push('/deals');
        }
      }).catch(err => alert(err))
          .finally(() => this.loading = false);
    },
    removeNote(noteId) {
      if (!confirm('Xác nhận xóa!')) {
        return;
      }
      dealService.removeNote(this.deal.id, noteId)
          .then(res => {
            if (res) {
              this.loadDeal();
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
      dealService.createNote(formData, this.deal.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadDeal();
        }
      }).catch(err => alert(err))
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return;
      }
      dealService.remove(this.deal.id)
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
      dealService.createTag({name: event}, this.deal.id)
          .then(res => {
            if (res) {
              alert('Thành công!');
              this.$refs.userInfo.closeModal();
              this.loadDeal();
            }
          })
    },
    loadStages() {
      this.loading = true;
      dealService.loadAllObject()
          .then(res => {
            if (res && res.data && res.status === 'success') {
              this.stages = res.data.stages;
              console.log(this.stages)
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    onPageSizeChange(event, type) {
      if (type === 'TASK') {
        this.pageSizeTask = Number(event);
        this.loadTaskByDeal();
      }
      if (type === 'COMPETITOR') {
        this.pageSizeLead = Number(event);
        this.loadCompetitorByDeal();
      }
      if (type === 'HISTORY') {
        this.pageSizeContact = Number(event);
        this.loadContacts();
      }
    },
    goToPage(event, type) {
      if (type === 'TASK') {
        this.currentPageTask = Number(event);
        this.loadTaskByDeal();
      }
      if (type === 'COMPETITOR') {
        this.currentPageLead = Number(event);
        this.loadCompetitorByDeal();
      }
      if (type === 'HISTORY') {
        this.currentPageAccount = Number(event);
        // this.loadAccounts();
      }
    }
  },
  created() {
    if (this.$route.query.id) {
      this.deal.id = this.$route.query.id;
      this.loadDeal();
      this.loadStages();
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      deal: {},
      loading: false,
      openModalCompetitor: false,
      taskLst: [],
      historyLst: [],
      competitorLst: [],
      stages: [],
      dataMenuLeft: [
        {id: '#tags', text: 'Tags'},
        {id: '#timeline', text: 'Timeline'},
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#details', text: 'Details'},
        {id: '#description', text: 'Description'},
        {id: '#notes', text: 'Notes'},
        {id: '#competitors', text: 'Competitors'},
        {id: '#history', text: 'History'},
        {id: '#task', text: 'Tasks'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      btnCreateTask: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Task'},
      btnCreateCall: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Call'},
      btnCreateCompetitor: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Add competitor'},
      dataLeftBaseInfo: [
        {key: 'Deal Name', value: 'Lead'},
        {key: 'Deal Owner', value: 's'},
        {key: 'Closing Date', value: 's'}
      ],
      dataLeftDetail: [
        {key: 'Deal Name', value: 'Lead'},
        {key: 'Deal Owner', value: 's'},
        {key: 'Closing Date', value: 's'},
        {key: 'Account', value: 's'},
        {key: 'Contact', value: 's'},
        {key: 'Campaign', value: 's'},
        {key: 'Lost Reason', value: 's'},
        {key: 'Priority', value: 's'},
      ],
      dataRightDetail: [
        {key: 'Amount', value: ''},
        {key: 'Probability', value: ''},
        {key: 'Expected Revenue', value: ''},
        {key: 'Stage', value: ''},
        {key: 'Create At', value: ''},
        {key: 'Created By', value: ''},
        {key: 'Modified At', value: ''},
        {key: 'Modified By', value: ''}
      ],
      competitorColumns: ['Name', 'Website', 'Strengths', 'Weaknesses', 'Threat', 'Suggestion', 'Action'],
      historyColumns: ['Stage', 'Probability', 'Expected Revenue', 'Modified At', 'Modified By'],
      taskColumns: ['Title', 'Type', 'Status', 'Start Date', 'End Date', 'Priority', 'Owner', 'Action'],
      currentPageCompetitor: 1,
      pageSizeCompetitor: 5,
      totalItemCompetitor: 0,
      totalPageCompetitor: 0,

      currentPageHistory: 1,
      pageSizeHistory: 5,
      totalItemHistory: 0,
      totalPageHistory: 0,

      currentPageTask: 1,
      pageSizeTask: 5,
      totalItemTask: 0,
      totalPageTask: 0,
    }
  },
  components: {
    AddCompetitorModal,
    TableInDetail, BasicInfo, Note, VTimeLine, MenuLeft, UserInfo, VLoading, VButton
  }
}
</script>

<style scoped>

</style>