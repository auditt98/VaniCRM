<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'LeadList'}">
            <VButton :data="btnBack"/>
          </router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <span @click="sendEmail"><VButton :data="btnSend"/></span>
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link class="ml-4" :to="{name: 'CampaignUpdate', query: {id: campaign.id}}">
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
            <UserInfo ref="userInfo" @create-tag="createTag" :tags="campaign.tags" :image="campaign.avatar"
                      :title="'Campaign Name'" :title-detail="campaign.campaignName"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="campaign.description" :emailTitle="campaign.emailTitle" :isEmail="true" :title="'Email'"/>
          </div>

          <!-- <div class="row mt-3" id="email">
            <BasicInfo :description="campaign.description"  :title="'Email'"/>
          </div> -->

          <div class="row mt-3" id="notes" v-if="campaign.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="createNote" :notes="campaign.notes"/>
          </div>
          <div class="row mt-3" id="allContacts">
            <TableInDetail :header-columns="contractColumns" :title="'All Contacts'"
                           :page-config="{page: currentPageContact, pageSize: pageSizeContact, totalItems: totalItemContact, totalPage: totalPageContact}"
                           @page-size-change="onPageSizeChange($event, 'CONTACT')"
                           @go-to-page="goToPage($event, 'CONTACT')">
              <template slot="button">
                <span @click="openContactModal"><VButton :data="btnCreateContact"/></span>
              </template>
              <template slot="body">
                <tbody v-if="contactLst">
                <tr v-for="(t, i) in contactLst" :key="i">
                  <td>{{ t.contactName }}</td>
                  <td><a :href="'mailto:'+t.email">{{ t.email }}</a></td>
                  <td>{{ t.phone }}</td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr>
                  <td colspan="7">Không có dữ liệu</td>
                </tr>
                </tbody>
              </template>
            </TableInDetail>
            <AddContactModal
              ref="contactModal"
              v-if="campaign.id && isOpenContactModal"
              :campaign-id="Number(campaign.id)"
              @close="closeContactModal"/>
          </div>
          <div class="row mt-3" id="allLeads">
            <TableInDetail :header-columns="leadColumns" :title="'Leads'"
                           :page-config="{page: currentPageLead, pageSize: pageSizeLead, totalItems: totalItemLead, totalPage: totalPageLead}"
                           @page-size-change="onPageSizeChange($event, 'LEAD')"
                           @go-to-page="goToPage($event, 'LEAD')">
              <template slot="button">
                <span @click="openLeadModal"><VButton :data="btnCreateLead"/></span>
              </template>
              <template slot="body">
                <tbody v-if="leadLst">
                <tr v-for="(t, i) in leadLst" :key="i">
                  <td>{{ t.name }}</td>
                  <td><a :href="'mailto:'+t.email">{{ t.email }}</a></td>
                  <td>{{ t.phone }}</td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr>
                  <td colspan="7">Không có dữ liệu</td>
                </tr>
                </tbody>
              </template>
            </TableInDetail>
            <AddLeadModal
              ref="leadModal"
              v-if="campaign.id && isOpenLeadModal"
              :campaign-id="Number(campaign.id)"
              @close="closeLeadModal"/>
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
import {campaignService} from "@/service/campaign.service";
import VLoading from "@/components/common/VLoading";
import TableInDetail from "@/components/common/table/TableInDetail";
import AddContactModal from "@/components/common/modal/AddContactModal";
import AddLeadModal from "@/components/common/modal/AddLeadModal";

export default {
  name: "LeadDetail",
  methods: {
    openContactModal() {
      this.isOpenContactModal = true;
    },
    closeContactModal() {
      this.loadCampaign();
      this.isOpenContactModal = false;
    },
    openLeadModal() {
      this.isOpenLeadModal = true;
    },
    closeLeadModal() {
      this.loadCampaign();
      this.isOpenLeadModal = false;
    },
    scrollTo(element) {
      this.$scrollTo(element);
    },

    loadCampaign() {
      this.loading = true;
      campaignService.getById(this.campaign.id).then(res => {
        if (res && res.data) {
          this.campaign = res.data;
          this.loadContacts();
          this.loadLeads();
          mapValue(this.dataLeftBaseInfo, [
            this.campaign.owner ? this.campaign.owner.username : '',
            getValueInArr(this.campaign.types, 'selected', 'name'),
            getValueInArr(this.campaign.statuses, 'selected', 'name'),
              this.campaign.actualCost
          ]);
          mapValue(this.dataLeftDetail, [
            this.campaign.campaignName,
            this.campaign.owner ? this.campaign.owner.username : '',
            formatDate(this.campaign.startDate, 'dd/MM/yyyy HH:mm'),
            formatDate(this.campaign.endDate, 'dd/MM/yyyy HH:mm'),
            this.campaign.actualCost,
            this.campaign.budgetedCost,
            this.campaign.expectedResponse

          ]);
          mapValue(this.dataRightDetail, [
            this.campaign.numberSent,
            getValueInArr(this.campaign.types, 'selected', 'name'),
            getValueInArr(this.campaign.statuses, 'selected', 'name'),
            this.campaign.createdBy ? this.campaign.createdBy.username : '',
            formatDate(this.campaign.createdAt, 'dd/MM/yyyy HH:mm'),
            this.campaign.modifiedBy ? this.campaign.modifiedBy.username : '',
            formatDate(this.campaign.modifiedAt, 'dd/MM/yyyy HH:mm'),
          ]);

        } else {
          alert('No data found');
          this.$router.push('/campaigns');
        }
      }).catch(err => alert(err))
          .finally(() => this.loading = false);
    },
    removeNote(noteId) {
      if (!confirm('Are you sure to delete?!')) {
        return;
      }
      campaignService.removeNote(this.campaign.id, noteId)
          .then(res => {
            if (res) {
              this.loadCampaign();
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
      campaignService.createNote(formData, this.campaign.id).then(res => {
        if (res && res.status === 'success') {
          alert('Successfully!');
          this.$refs.notes.clear();
          this.loadCampaign();
        }
      }).catch(err => alert(err))
    },
    sendEmail(){
      campaignService.sendEmail(this.campaign.id).then(res =>{
        if(res){
          alert(res.message);
        }
      })
    },

    remove() {
      if (!confirm('Are you sure to delete?')) {
        return;
      }
      campaignService.remove(this.campaign.id)
          .then(res => {
            if (res) {
              alert('Successfully');
              this.$router.push('/campaigns')
            }
          })
    },
    createTag(event) {
      if (!confirm('Are you sure?!')) {
        return;
      }
      campaignService.createTag({name: event}, this.campaign.id)
          .then(res => {
            if (res) {
              alert('Successfully!');
              this.$refs.userInfo.closeModal();
              this.loadCampaign();
            }
          })
    },
    loadContacts() {
      this.loading = true;
      this.contactLst = [];
      let query = {
        currentPage: this.currentPageContact,
        pageSize: this.pageSizeContact
      };
      campaignService.loadContacts(query, this.campaign.id).then(res => {
        if (res && res.data) {
          this.contactLst = res.data.contacts;
          this.totalPageContact= Number(res.data.pageInfo.TotalPages);
          this.totalItemContact = Number(res.data.pageInfo.TotalItems);
        }
      }).finally(() => {
        this.loading = false;
      })
    },
    loadLeads() {
      this.loading = true;
      this.leadLst = [];
      let query = {
        currentPage: this.currentPageLead,
        pageSize: this.pageSizeLead
      };
      campaignService.loadLeads(query, this.campaign.id).then(res => {
        if (res && res.data) {
          this.leadLst = res.data.leads;
          this.totalPageLead = Number(res.data.pageInfo.TotalPages);
          this.totalItemLead = Number(res.data.pageInfo.TotalItems);
        }
      }).finally(() => {
        this.loading = false;
      })
    },
    onPageSizeChange(event, type) {
      if (type === 'LEAD') {
        this.pageSizeLead = Number(event);
        this.loadLeads();
      }
      if (type === 'CONTACT') {
        this.pageSizeContact = Number(event);
        this.loadContacts();
      }
    },
    goToPage(event, type) {
      if (type === 'LEAD') {
        this.currentPageLead = Number(event);
        this.loadLeads();
      }
      if (type === 'CONTACT') {
        this.currentPageContact = Number(event);
        this.loadContacts();
      }
    }
  },
  created() {
    if (this.$route.query.id) {
      this.campaign.id = this.$route.query.id;
      this.loadCampaign();
    } else {
      this.$router.push('/');
    }
  },
  data: function () {
    return {
      isOpenContactModal: false,
      isOpenLeadModal: false,
      campaign: {},
      loading: false,
      contactLst: [],
      leadLst: [],
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#detail', text: 'Detail'},
        {id: '#description', text: 'Email'},
        {id: '#notes', text: 'Notes'},
        {id: '#Contact', text: 'Contact'},
      ],
      btnSend: {btnClass: 'btn-white px-3', icon: 'fa-paper-plane', text: 'Send Email'},
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnDelete: {btnClass: 'btn-white px-3 ml-4', icon: 'fa-times', text: 'Delete'},
      btnEdit: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Edit'},
      dataLeftBaseInfo: [
        {key: 'Campaigns Owner', value: 'Lead'},
        {key: 'Type', value: 's'},
        {key: 'Status', value: 's'},
        {key: 'Actual Cost', value: ''},
      ],
      dataLeftDetail: [
        {key: 'Campaigns Name', value: ''},
        {key: 'Campaigns Owner', value: ''},
        {key: 'Start Date', value: ''},
        {key: 'End Date', value: ''},
        {key: 'Actual Cost', value: ''},
        {key: 'Budgeted Cost', value: ''},
        {key: 'Expected Response', value: ''}
      ],
      dataRightDetail: [
        {key: 'Number Sent', value: ''},
        {key: 'Type', value: ''},
        {key: 'Status', value: ''},
        {key: 'Create At', value: ''},
        {key: 'Create By', value: ''},
        {key: 'Modified At', value: ''},
        {key: 'Modified By', value: ''}
      ],
      description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum',
      contractColumns: ['Contact Name', 'Email', 'Phone'],
      leadColumns: ['Name', 'Email', 'Phone'],
      currentPageContact: 1,
      pageSizeContact: 5,
      totalItemContact: 0,
      totalPageContact:0,
      currentPageLead: 1,
      pageSizeLead: 1,
      totalItemLead: 0,
      totalPageLead:0,
      btnCreateContact: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Add Contact'},
      btnCreateLead: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Add Lead'},
    }
  },
  components: {TableInDetail, VLoading, Note, BasicInfo, UserInfo, MenuLeft, VButton, AddContactModal, AddLeadModal}
}
</script>

<style scoped>

</style>