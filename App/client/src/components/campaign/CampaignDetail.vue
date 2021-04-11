<template>
  <div class="">
    <Header/>
    <div class="px-5 pt-3 m-0 background-main" style="position: relative">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
          <router-link :to="{name: 'LeadList'}">
            <VButton :data="btnBack"/>
          </router-link>
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <span @click="remove"><VButton :data="btnDelete"/></span>
          <router-link :to="{name: 'LeadUpdate', query: {id: campaign.id}}">
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
                      :title="'User'" :title-detail="'Username'"/>
          </div>
          <div class="row mt-3" id="basicInfo">
            <BasicInfo :arr-left="dataLeftBaseInfo" :arr-right="[]" :title="'Basic Info'"/>
          </div>
          <div class="row mt-3" id="detail">
            <BasicInfo :arr-left="dataLeftDetail" :arr-right="dataRightDetail" :title="'Detail'"/>
          </div>
          <div class="row mt-3" id="description">
            <BasicInfo :description="campaign.description" :title="'Description'"/>
          </div>
          <div class="row mt-3" id="notes" v-if="campaign.notes">
            <Note ref="notes" @remove-note="removeNote" @submit="createNote" :notes="campaign.notes"/>
          </div>
          <div class="row mt-3" id="allContacts">
            <TableInDetail :header-columns="contractColumns" :data="contractLst" :title="'All Contracts'">
              <template slot="body">
                <tbody v-if="contractLst">
                <tr v-for="(t, i) in contractLst" :key="i">
                  <td>{{ t.name }}</td>
                  <td><a :href="'mailto:'+t.email">{{ t.email }}</a></td>
                  <td>{{ t.phone }}</td>
                  <td>{{ t.mobile }}</td>
                  <td><a href="#">{{ t.skype }}</a></td>
                  <td><input type="checkbox" name="isOwn" :checked="t.isOwner"></td>
                  <td><input type="checkbox" name="isCollaborator" :checked="t.isCollaborator"></td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr>
                  <td colspan="7">Không có dữ liệu</td>
                </tr>
                </tbody>
              </template>
            </TableInDetail>
          </div>
          <div class="row mt-3" id="allLeads">
            <TableInDetail :header-columns="leadColumns" :title="'Leads'">
              <template slot="body">
                <tbody v-if="leadLst">
                <tr v-for="(t, i) in leadLst" :key="i">
                  <td>{{ t.name }}</td>
                  <td><a :href="'mailto:'+t.email">{{ t.email }}</a></td>
                  <td>{{ t.phone }}</td>
                  <td>{{ t.mobile }}</td>
                  <td><a href="#">{{ t.skype }}</a></td>
                  <td><input type="checkbox" name="isOwn" :checked="t.isOwner"></td>
                  <td><input type="checkbox" name="isCollaborator" :checked="t.isCollaborator"></td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr>
                  <td colspan="7">Không có dữ liệu</td>
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
import Header from "@/components/common/Header";
import MenuLeft from "@/components/common/MenuLeft";
import UserInfo from "@/components/common/info/UserInfo";
import BasicInfo from "@/components/common/info/BasicInfo";
import Note from "@/components/common/info/Note";
import {formatDate, getValueInArr, mapValue} from "@/config/config";
import {campaignService} from "@/service/campaign.service";
import VLoading from "@/components/common/VLoading";

export default {
  name: "LeadDetail",
  methods: {
    scrollTo(element) {
      this.$scrollTo(element);
    },

    loadCampaign() {
      this.loading = true;
      campaignService.getById(this.campaign.id).then(res => {
        if (res && res.data) {
          this.lead = res.data;
          mapValue(this.dataLeftBaseInfo, [
            this.campaign.name,
            `<i class="fa fa-envelope-o"></i> <a href="mailto:${this.campaign.email}">${this.campaign.email}</a>`,
            `<i class="fa fa-phone"></i> ${this.campaign.phone}`,
            getValueInArr(this.campaign.status, 'selected', 'name'),
            getValueInArr(this.campaign.priority, 'selected', 'name')
          ]);
          mapValue(this.dataLeftDetail, [
            this.campaign.name,
            `<i class="fa fa-envelope-o"></i> <a href="mailto:${this.campaign.email}">${this.campaign.email}</a>`,
            `<i class="fa fa-phone"></i> ${this.campaign.phone}`,
            this.campaign.fax,
            getValueInArr(this.campaign.industry, 'selected', 'name'),
            this.campaign.website,
            this.campaign.annualRevenue,
            getValueInArr(this.campaign.status, 'selected', 'name'),
            this.campaign.skype,
            getValueInArr(this.campaign.priority, 'selected', 'name')

          ]);
          mapValue(this.dataRightDetail, [
            this.campaign.noEmail,
            this.campaign.noCall,
            getValueInArr(this.campaign.leadSource, 'selected', 'name'),
            this.campaign.companyName,
            formatDate(this.campaign.CreatedAt, 'dd/MM/yyyy HH:mm'),
            this.campaign.CreatedBy ? this.campaign.CreatedBy.username : '',
            formatDate(this.campaign.ModifiedAt, 'dd/MM/yyyy HH:mm'),
            this.campaign.ModifiedBy ? this.campaign.ModifiedBy.username : '',
          ]);

        } else {
          //this.$router.push('/');
        }
      }).catch(err => alert(err))
          .finally(() => this.loading = false);
    },
    removeNote(noteId) {
      console.log(noteId)
      if (!confirm('Xác nhận xóa!')) {
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
      console.log(event)
      let formData = new FormData();
      formData.append("body", event.text);
      if (event.files && event.files.length > 0) {
        for (let i = 0; i < event.files.length; i++) {
          formData.append("files" + i, event.files[i]);
        }
      }
      campaignService.createNote(formData, this.campaign.id).then(res => {
        if (res && res.status === 'success') {
          alert('Thành công!');
          this.$refs.notes.clear();
          this.loadCampaign();
        }
      }).catch(err => alert(err))
    },
    remove() {
      if (!confirm('Xác nhận xóa')) {
        return;
      }
      campaignService.remove(this.campaign.id)
          .then(res => {
            if (res) {
              alert('Thành công');
              this.$router.push('/campaigns')
            }
          })
    },
    createTag(event) {
      if (!confirm('Xác nhận!')) {
        return;
      }
      campaignService.createTag({name: event}, this.campaign.id)
          .then(res => {
            if (res) {
              alert('Thành công!');
              this.$refs.userInfo.closeModal();
              this.loadCampaign();
            }
          })
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
      campaign: {},
      loading: false,
      contactLst: [],
      leadLst: [],
      dataMenuLeft: [
        {id: '#basicInfo', text: 'Basic Info'},
        {id: '#detail', text: 'Detail'},
        {id: '#description', text: 'Description'},
        {id: '#notes', text: 'Notes'},
        {id: '#Contact', text: 'Contact'},
      ],
      btnBack: {btnClass: 'btn-purple px-3', icon: 'fa-arrow-left', text: 'Back'},
      btnDelete: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Delete'},
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
      contractColumns: ['Contract Name', 'Email', 'Phone', 'Mobile', 'Skype'],
      leadColumns: ['Lead Name', 'Email', 'Phone', 'Mobile', 'Skype'],
    }
  },
  components: {VLoading, Note, BasicInfo, UserInfo, MenuLeft, Header, VButton}
}
</script>

<style scoped>

</style>