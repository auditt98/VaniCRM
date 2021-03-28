<template>
    
  <div class="background-main">
    <Header/>
    <div class="col-sm-9 mx-auto mt-5">
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search">
        <template slot="button">
          <router-link to="/user-update"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!users || users.length === 0">
          <tr><td colspan="7">Không có dữ liệu</td></tr>
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
import Header from "@/components/common/Header";
import {userService} from "@/service/user.service";
import VButton from "@/components/common/VButton";

export default {
  name: "UserList",
  components: {VButton, TableInList, Header},
  // components: {VButton, TableInList},
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.loadUsers(this.keyword);
    },
    search(keyword, pageSize) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.loadUsers(keyword);
    },
    createUser() {
      alert(123)
    },
    editUser(id) {
      this.$router.push({path: '/user-update', query : { id: id}});
    },
    deleteUser(id) {
      if (!confirm("Xác nhận xóa!")) {
        return ;
      }
      userService.remove(id);
      this.loadUsers(this.keyword);
    }
    ,
    loadUsers(keyword) {
      this.keyword = keyword;
      this.users = [];
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      if (keyword) {
        query['query'] = this.keyword;
      }
      userService.getAll(query).then(res => {
        if (res) {
          this.users = res.data.users;
          this.totalPage = Number(res.data.pageInfo.totalPage);
        }
      })
    }
  },

  created() {
    this.currentPage = 1;
    this.pageSize = 10;
    this.loadUsers(null);
  },
  data: function () {
    return {
      users: Array,
      currentPage: Number,
      pageSize: Number,
      totalPage: 1,
      keyword: null,
      columns: ['Username','First Name','Last Name','Phone','Email','Skype', 'Action'],
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create User'},
    };
  }
}
</script>

<style scoped>

</style>