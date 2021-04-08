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
          <router-link to="/account-create"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!accounts || accounts.length === 0">
          <tr>
            <td colspan="4" class="text-center">Không có dữ liệu</td>
          </tr>
          </tbody>
          <tbody v-if="accounts && accounts.length > 0">
          <tr v-for="(item,index) in accounts" :key="index">
            <th>{{ item.name }}</th>
            <th>{{ item.phone }}</th>
            <th>{{ item.website }}</th>
            <th>{{ item.owner }}</th>
          </tr>
          </tbody>
        </template>
      </TableInList>
    </div>

  </div>
</template>

<script>
import TableInList from "@/components/common/table/TableInList";
import Header from "@/components/common/Header";
import {accountService} from "@/service/account.service";
import VLoading from "@/components/common/VLoading";
import VButton from "@/components/common/VButton";

export default {
  name: "AccountList",
  components: {VButton, VLoading, TableInList, Header},
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.loadAccounts(this.keyword);
    },
    search(keyword, pageSize) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.loadAccounts(keyword);
    },
    loadAccounts(keyword) {
      this.accounts = [];
      this.loading = true;
      this.keyword = keyword;
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      if (keyword) {
        query['query'] = this.keyword;
      }
      accountService.getAll(query).then(res => {
        if (res && res.data) {
          this.accounts = res.data.accounts;
          this.totalPage = Number(res.data.pageInfo.TotalPages);
        }
      }).finally(() => {
        this.loading = false;
      })
    }
  },

  created() {
    this.loadAccounts(null);
  },
  data: function () {
    return {
      accounts: Array,
      currentPage: 1,
      pageSize: 10,
      totalPage: 0,
      keyword: '',
      loading: false,
      columns: [
        {text: 'Account Name', style: 'width: 20%;'},
        {text: 'Phone', style: 'width: 15%;'},
        {text: 'Website', style: 'width: 15%'},
        {text: 'Account Owner', style: 'width: 15%;'},
      ],
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Account'},
    };
  }
}
</script>

<style scoped>
  button{
    border-radius: 5px;
  }
  .user-info-avatar-image img{
    border-radius: 50%;
  }
  td , th{
    text-align: center;
  }
</style>