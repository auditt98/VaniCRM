<template>
  <div class="background-main">
    <Header/>
    <div class="col-sm-9 mx-auto mt-5">
      <VLoading :loading="loading"/>
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
      >
        <template slot="button">
          <router-link to="/campaign-create"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!campaigns || campaigns.length === 0">
          <tr>
            <td colspan="6" class="text-center">Không có dữ liệu</td>
          </tr>
          </tbody>
          <tbody v-if="campaigns && campaigns.length > 0">
          <tr v-for="(item,index) in campaigns" :key="index">
            <th>
              <router-link :to="{name: 'CampaignDetail', query: {id: item.id}}">{{ item.name }}</router-link>
            </th>
            <th>{{ item.name }}</th>
            <th>{{ item.type }}</th>
            <th>{{ item.status }}</th>
            <th>{{ item.startDate | formatDate }}</th>
            <th>{{ item.endDate | formatDate }}</th>
          </tr>
          </tbody>
        </template>
      </TableInList>
    </div>

  </div>
</template>

<script>
import Header from "@/components/common/Header";
import TableInList from "@/components/common/table/TableInList";
import VButton from "@/components/common/VButton";
import {campaignService} from "@/service/campaign.service";
import VLoading from "@/components/common/VLoading";

export default {
  name: "LeadList",
  components: {VLoading,  VButton, TableInList, Header},
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.loadCampaigns(this.keyword);
    },
    search(keyword, pageSize) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.loadCampaigns(keyword);
    },
    loadCampaigns(keyword) {
      this.loading = true;
      this.keyword = keyword;
      this.groups = [];
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      if (keyword) {
        query['query'] = this.keyword;
      }
      campaignService.getAll(query).then(res => {
        if (res && res.data) {
          this.campaigns = res.data.campaigns;
          this.totalPage = res.data.pageInfo.TotalPages;
        }
      }).catch(err => {
        alert(err);
      }).finally(() => {
        this.loading = false;
      })
    }
  },
  created() {
    this.currentPage = 1;
    this.pageSize = 5;
    this.loadCampaigns(null);
  },
  data: function () {
    return {
      columns: [
        {text: 'Campaigns Name', style: 'width: 20%;'},
        {text: 'Type', style: 'width: 15%;'},
        {text: 'Status', style: 'width: 15%'},
        {text: 'Start Date', style: 'width: 15%;'},
        {text: 'End Date', style: 'width: 15%;'},
        {text: 'Campaigns Owner', style: 'width: 20%;'},
      ],
      loading: false,
      campaigns: Array,
      keyword: '',
      pageSize: 5,
      totalPage: 5,
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Campaign'},
    };
  }
}
</script>

<style scoped>
th, td {
  text-align: center;
}
</style>