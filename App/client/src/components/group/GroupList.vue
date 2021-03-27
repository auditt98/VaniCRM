<template>
  <div class="background-main">
    <Header/>
    <div class="col-sm-9 mx-auto mt-5">
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
      >
        <template slot="button">
          <router-link to="/group-update"><VButton :data="btnCreate"/></router-link>
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
import Header from "@/components/common/Header";
import {groupService} from "@/service/group.service";
import VButton from "@/components/common/VButton";

export default {
  name: "GroupList",
  components: {VButton, TableInList, Header},
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
    create() {
      alert(123)
    },
    edit(id) {
      this.$router.push({path: '/group-update', query : { id: id}});
    },
    remove(id) {
      if (!confirm("Xác nhận xóa!")) {
        return ;
      }
      groupService.remove(id);
      this.loadGroups(this.keyword);
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
      columns: ['Group Name', 'Number Of Users', 'Number Of Permission', 'Action'
      ],
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Group'},
    };
  }
}
</script>

<style scoped>

</style>