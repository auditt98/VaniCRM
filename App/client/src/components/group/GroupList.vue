<template>
  <div class="background-main">

    <div class="col-sm-9 mx-auto mt-5">
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
      >
        <template slot="button">
          <router-link to="/groups/update"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!groups || groups.length === 0">
          <tr>
            <td colspan="4" class="text-center">Không có dữ liệu</td>
          </tr>
          </tbody>
          <tbody v-if="groups && groups.length > 0">
          <tr v-for="(item,index) in groups" :key="index">
            <th>{{ item.groupName }}</th>
            <th>{{ item.numberOfUsers }}</th>
            <th>{{ item.numberOfPermissions }}</th>
            <th>
            <span class="action">
              <span @click="edit(item.id)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
              <span @click="remove(item.id)"><img src="images/delete-bin-2-line.png" alt=""></span>
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

import {groupService} from "@/service/group.service";
import VButton from "@/components/common/VButton";

export default {
  name: "GroupList",
  components: {VButton, TableInList, },
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.loadGroups(this.keyword);
    },
    search(keyword, pageSize) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.loadGroups(keyword);
    },
    edit(id) {
      this.$router.push({path: '/groups/update', query : { id: id}});
    },
    remove(id) {
      if (!confirm("Xác nhận xóa!")) {
        return ;
      }
      groupService.remove(id).then(res => {
        if (res) {
          this.loadGroups(this.keyword);
        }
      });
    }
    ,
    loadGroups(keyword) {
      this.keyword = keyword;
      this.groups = [];
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      if (keyword) {
        query['query'] = this.keyword;
      }
      groupService.getAll(query).then(res => {
        if (res && res.data) {
          this.groups = res.data.groups;
          this.totalPage = res.data.pageInfo.TotalPages;
        }
      }).catch(err => {
        alert(err);
      })
    }
  },

  created() {
    this.currentPage = 1;
    this.pageSize = 5;
    this.loadGroups(null);
  },
  data: function () {
    return {
      groups: Array,
      keyword: '',
      pageSize: 5,
      totalPage: 5,
      columns: [
        {text: 'Group Name', style: 'width: 25%;'},
        {text: 'Number Of Users', style: 'width: 25%;'},
        {text: 'Number Of Permission', style: 'width: 25%'},
        {text: 'Action', style: 'width: 25%;'},
      ],
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Group'},
    };
  }
}
</script>

<style scoped>
th, td {
  text-align: center;
}
</style>