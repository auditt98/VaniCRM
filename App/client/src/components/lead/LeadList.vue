<template>
  <div class="background-main">
    <!-- <Header/> -->
    <div class="col-sm-9 mx-auto mt-5">
      <VLoading :loading="loading"/>
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
      >
        <template slot="button">
          <router-link to="/lead-create"><VButton :data="btnCreate"/></router-link>
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
              <router-link :to="{name: 'LeadDetail', query: {id: item.id}}">{{ item.name }}</router-link>
            </th>
            <th>{{ item.companyName }}</th>
            <th>{{ item.email }}</th>
            <th>{{ item.phone }}</th>
            <th>{{ item.leadSource }}</th>
            <th>{{ item.leadOwner }}</th>
            <th>
              <VTag :data="{text: item.priority, bgColor: 'rgb(222, 0, 80)'}"/>
            </th>

          </tr>
          </tbody>
        </template>
      </TableInList>
    </div>

  </div>
</template>

<script>
// import Header from "@/components/common/Header";
import TableInList from "@/components/common/table/TableInList";
import {leadService} from "@/service/lead.service";
import VButton from "@/components/common/VButton";
import VTag from "@/components/common/VTag";
import VLoading from "@/components/common/VLoading";

export default {
  name: "LeadList",
  components: {VLoading, VTag, VButton, TableInList,
  //  Header
   },
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.loadLeads(this.keyword);

    },
    search(keyword, pageSize) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.loadLeads(keyword);
    },
    loadLeads(keyword) {
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
        {text: 'Lead Name', style: 'width: 15%;'},
        {text: 'Company Name', style: 'width: 15%;'},
        {text: 'Email', style: 'width: 15%'},
        {text: 'Phone', style: 'width: 10%;'},
        {text: 'Lead Source', style: 'width: 15%;'},
        {text: 'Lead Owner', style: 'width: 15%;'},
        {text: 'Priority', style: 'width: 10%;'},
      ],
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