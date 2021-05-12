<template>
  <div class="background-main">

    <div class="col-sm-10 mx-auto mt-5">
      <VLoading :loading="loading"/>
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
      >
        <template slot="button">
          <router-link to="/accounts/create"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!accounts || accounts.length === 0">
          <tr>
            <td colspan="5" class="text-center">Không có dữ liệu</td>
          </tr>
          </tbody>
          <tbody v-if="accounts && accounts.length > 0">
          <tr v-for="(item,index) in accounts" :key="index">
            <th>
              <img :src="item.avatar" style="width: 60px; height: 60px; border-radius: 50%"/>
            </th>
            <th>
                {{ item.name }}
            </th>
            <th>{{ item.phone }}</th>
            <th>{{ item.website }}</th>
            <th>{{ item.owner }}</th>
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

import {accountService} from "@/service/account.service";
import VLoading from "@/components/common/VLoading";
import VButton from "@/components/common/VButton";

export default {
  name: "AccountList",
  components: {VButton, VLoading, TableInList, },
  methods: {
    editItem(id) {
      this.$router.push({path: '/accounts/detail', query : { id: id}});
    },
    deleteItem(id) {
      if (!confirm("Xác nhận xóa!")) {
        return ;
      }
      this.loading = true;
      accountService.remove(id).then(res => {
        if(res) {
          alert('Xóa thành công!');
          this.loadAccounts(this.keyword);
        }
      }).finally(() => {
        this.loading = false;
      });
    },
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
        {text: 'Avatar', style: 'width: 5%;'},
        {text: 'Account Name', style: 'width: 20%;'},
        {text: 'Phone', style: 'width: 15%;'},
        {text: 'Website', style: 'width: 15%'},
        {text: 'Account Owner', style: 'width: 15%;'},
        {text: 'Action', style: 'width: 10%;'},
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