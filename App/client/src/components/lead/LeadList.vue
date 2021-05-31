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
          <router-link to="/leads/create"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!leads || leads.length === 0">
          <tr>
            <td colspan="7" class="text-center">Không có dữ liệu</td>
          </tr>
          </tbody>
          <tbody v-if="leads && leads.length > 0">
          <tr v-for="(item,index) in leads" :key="index">
            <th>
              <img :src="item.avatar" style="width: 60px; height: 60px; border-radius: 50%"/>
            </th>
            <th>
              {{ item.name }}
            </th>
            <th>{{ item.companyName }}</th>
            <th>{{ item.email }}</th>
            <th>{{ item.phone }}</th>
            <th>{{ item.leadSource }}</th>
            <th>{{ item.leadOwner }}</th>
            <th>
              <VTag :data="{text: item.priority, bgColor: getBgColor(item.priority)}"/>
            </th>
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
import {leadService} from "@/service/lead.service";
import VButton from "@/components/common/VButton";
import VTag from "@/components/common/VTag";
import VLoading from "@/components/common/VLoading";

export default {
  name: "LeadList",
  components: {VLoading, VTag, VButton, TableInList, },
  methods: {
    getBgColor(data) {
      if (!data) {
        return '';
      }
      if (data.toUpperCase() === 'HIGH') {
        return '#F12B2C';
      }
      if (data.toUpperCase() === 'LOW') {
        return '#FEC400';
      }
      if (data.toUpperCase() === 'NORMAL') {
        return '#29CC97';
      }
      return 'red';
    },
    editItem(id) {
      this.$router.push({path: '/leads/detail', query : { id: id}});
    },
    deleteItem(id) {
      if (!confirm("Xác nhận xóa!")) {
        return ;
      }
      this.loading = true;
      leadService.remove(id).then(res => {
        if(res) {
          alert('Xóa thành công!');
          this.loadLeads(this.keyword);
        }
      }).finally(() => {
        this.loading = false;
      });
    },
    goToPage(page) {
      this.currentPage = page;
      this.loadLeads(this.keyword, this.sortQueries);
    },
    search(keyword, pageSize, sortQueries) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.sortQueries = sortQueries;
      this.loadLeads(keyword, sortQueries);
    },
    loadLeads(keyword, sortQueries) {
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
      leadService.getAll(query).then(res => {
        if (res && res.data) {
          this.leads = res.data.leads;
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
    this.loadLeads(null);
  },
  data: function () {
    return {
      columns: [
        {text: 'Avatar', style: 'width: 5%;'},
        {text: 'Lead Name', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "name", isSorting: Boolean},
        {text: 'Company Name', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "companyName", isSorting: Boolean},
        {text: 'Email', style: 'width: 10%', sortable: true, ascSort: Boolean, objectName: "email", isSorting: Boolean},
        {text: 'Phone', style: 'width: 10%;', sortable: true, ascSort: Boolean, objectName: "phone", isSorting: Boolean},
        {text: 'Lead Source', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "leadSource", isSorting: Boolean},
        {text: 'Lead Owner', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "leadOwner", isSorting: Boolean},
        {text: 'Priority', style: 'width: 5%;', sortable: true, ascSort: Boolean, objectName: "priority", isSorting: Boolean},
        {text: 'Action', style: 'width: 10%;'},
      ],
      sortQueries: Array,
      leads: Array,
      keyword: '',
      pageSize: 5,
      totalPage: 5,
      loading: false,
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Lead'},
    };
  }
}
</script>

<style scoped>
th, td {
  text-align: center;
}
</style>