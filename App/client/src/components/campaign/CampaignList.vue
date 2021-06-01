<template>
  <div class="background-main">

    <div class="col-sm-10 mx-auto mt-5">
      <VLoading :loading="loading"/>
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
                   :isSortable="true"
      >
        <template slot="button">
          <router-link to="/campaigns/create"><VButton :data="btnCreate"/></router-link>
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
              {{ item.name }}
            </th>
            <th>{{ item.type }}</th>
            <th><span :style="'color: ' + getColor(item.status)">{{ item.status }}</span></th>
            <th>{{ item.startDate | formatDate }}</th>
            <th>{{ item.endDate | formatDate }}</th>
            <th>{{ item.owner ? item.owner  : '' }}</th>
            <th>
              <span class="action">
              <span @click="editItem(item.id)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
              <span @click="deleteItem(item.id)"><img src="images/delete-bin-2-line.png" alt=""></span>
            </span>
            </th>
          </tr>
          </tbody>
        </template>
      </TableInList>
    </div>

  </div>
</template>

<script>

import TableInList from "@/components/common/table/TableInList";
import VButton from "@/components/common/VButton";
import {campaignService} from "@/service/campaign.service";
import VLoading from "@/components/common/VLoading";

export default {
  name: "LeadList",
  components: {VLoading,  VButton, TableInList, },
  methods: {
    getColor(data) {
      if (data.toUpperCase() === 'PLANNING') {
        return '#109CF1';
      }
      if (data.toUpperCase() === 'CANCELLED') {
        return 'red';
      }
      return '#29CC97';
    },
    editItem(id) {
      this.$router.push({path: '/campaigns/detail', query : { id: id}});
    },
    deleteItem(id) {
      if (!confirm('Are you sure to delete?')) {
        return ;
      }
      this.loading = true;
      campaignService.remove(id).then(res => {
        if(res) {
          alert('Deleted successfully!');
          this.loadCampaigns(this.keyword);
        }
      }).finally(() => {
        this.loading = false;
      });
    },
    goToPage(page) {
      this.currentPage = page;
      this.loadCampaigns(this.keyword, this.sortQueries);
    },
    search(keyword, pageSize, sortQueries) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.sortQueries = sortQueries;
      this.loadCampaigns(keyword, sortQueries);
    },
    loadCampaigns(keyword,  sortQueries) {
      this.loading = true;
      this.keyword = keyword;
      this.groups = [];
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      if(sortQueries !== undefined && sortQueries.length > 0){
        query['sort'] = sortQueries
      }
      if (keyword) {
        query['query'] = this.keyword;
      }
      campaignService.getAll(query).then(res => {
        if (res && res.data) {
          this.campaigns = res.data.campaigns;
          this.campaigns = [...this.campaigns]
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
        {text: 'Campaigns Name', style: 'width: 20%;', sortable: true, ascSort: Boolean, objectName: "name", isSorting: Boolean},
        {text: 'Type', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "type", isSorting: Boolean},
        {text: 'Status', style: 'width: 10%', sortable: true, ascSort: Boolean, objectName: "status", isSorting: Boolean},
        {text: 'Start Date', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "startDate", isSorting: Boolean},
        {text: 'End Date', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "endDate", isSorting: Boolean},
        {text: 'Campaigns Owner', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "owner", isSorting: Boolean},
        {text: 'Action', style: 'width: 20%;'},
      ],
      loading: false,
      sortQueries: Array,
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