<template>
  <div class="background-main">

    <VLoading :loading="loading"/>
    <div class="col-sm-9 mx-auto mt-5">
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
                   :isSortable="true"
                   v-if="!loading"
      >
        <template slot="button">
          <router-link :to="{name: 'UserCreate'}"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!users || users.length === 0">
          <tr><td colspan="7" class="text-center">No data available :)</td></tr>
          </tbody>
          <tbody v-if="users && users.length > 0">
          <tr v-for="(user,index) in users" :key="index">
            <th>{{ user.username }}</th>
            <th>{{ user.firstName }}</th>
            <th>{{ user.lastName }}</th>
            <th>{{ user.phone }}</th>
            <th>{{ user.email }}</th>
            <th>{{ user.skype }}</th>
            <th>
            <span class="action">
              <span @click="editUser(user.id)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
              <span @click="deleteUser(user.id)"><img src="images/delete-bin-2-line.png" alt=""></span>
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

import {userService} from "@/service/user.service";
import VButton from "@/components/common/VButton";
import VLoading from "@/components/common/VLoading";
// import {VclTable} from 'vue-content-loading';
export default {
  name: "UserList",
  components: {
  VLoading, 
  VButton, TableInList},
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.loadUsers(this.keyword, this.sortQueries);
    },
    search(keyword, pageSize, sortQueries) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.sortQueries = sortQueries;
      this.loadUsers(keyword, this.sortQueries);
    },
    editUser(id) {
      this.$router.push({path: '/users/page', query : { id: id}});
    },
    deleteUser(id) {
      if (!confirm('Are you sure to delete?')) {
        return ;
      }
      this.loading = true;
      userService.remove(id).then(res => {
        if(res) {
          alert('Deleted successfully!');
          this.loadUsers(this.keyword, this.sortQueries);
        }
      }).finally(() => {
        this.loading = false;
      });
    }
    ,
    loadUsers(keyword, sortQueries) {
      this.loading = true;
      this.keyword = keyword;
      this.users = [];
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
      userService.getAll(query).then(res => {
        if (res) {
          this.users = res.data.users;
          this.totalPage = Number(res.data.pageInfo.TotalPages);
        }
      }).finally(() => {
        this.loading = false;
      })
    }
  },

  created() {
    this.currentPage = 1;
    this.pageSize = 5;
    this.loadUsers(null);
  },
  data: function () {
    return {
      users: Array,
      sortQueries: Array,
      currentPage: Number,
      pageSize: Number,
      totalPage: 1,
      keyword: null,
      loading: false,
      columns: [
        {text: 'Username', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "username", isSorting: Boolean},
        {text: 'First Name', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "firstName", isSorting: Boolean},
        {text: 'Last Name', style: 'width: 15%', sortable: true, ascSort: Boolean, objectName: "lastName", isSorting: Boolean},
        {text: 'Phone', style: 'width: 10%;', sortable: true, ascSort: Boolean, objectName: "phone", isSorting: Boolean},
        {text: 'Email', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "email", isSorting: Boolean},
        {text: 'Skype', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "skype", isSorting: Boolean},
        {text: 'Action', style: 'width: 15%;'},
      ],
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create User'},
    };
  }
}
</script>

<style scoped>
th, td  {
  text-align: center;
}
</style>