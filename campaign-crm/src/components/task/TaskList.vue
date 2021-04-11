<template>
  <div class="background-main">

    <div class="col-sm-9 mx-auto mt-5">
      <VLoading :loading="loading"/>
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
      >
        <template slot="button">
          <div class="d-flex justify-content-between">
            <router-link to="/task-create"><VButton :data="btnCreateTask"/></router-link>
            <router-link to="/meeting-create"><VButton :data="btnCreateMeeting"/></router-link>
            <router-link to="/call-create"><VButton :data="btnCreateCall"/></router-link>
          </div>
        </template>
        <template slot="body">
          <tbody v-if="!tasks || tasks.length === 0">
          <tr>
            <td colspan="8" class="text-center">Không có dữ liệu</td>
          </tr>
          </tbody>
          <tbody v-if="tasks && tasks.length > 0">
          <tr v-for="(item,index) in tasks" :key="index">
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

import VLoading from "@/components/common/VLoading";
import VButton from "@/components/common/VButton";
import {taskService} from "@/service/task.service";

export default {
  name: "TaskList",
  components: {VButton, VLoading, TableInList, },
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.loadTasks(this.keyword);
    },
    search(keyword, pageSize) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.loadTasks(keyword);
    },
    loadTasks(keyword) {
      this.tasks = [];
      this.loading = true;
      this.keyword = keyword;
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      if (keyword) {
        query['query'] = this.keyword;
      }
      taskService.getAll(query).then(res => {
        if (res && res.data) {
          this.tasks = res.data.tasks;
          this.totalPage = Number(res.data.pageInfo.TotalPages);
        }
      }).finally(() => {
        this.loading = false;
      })
    }
  },

  created() {
    this.loadTasks(null);
  },
  data: function () {
    return {
      tasks: Array,
      currentPage: 1,
      pageSize: 10,
      totalPage: 0,
      keyword: '',
      loading: false,
      columns: [
        {text: 'Title', style: 'width: 20%;'},
        {text: 'Type', style: 'width: 10%;'},
        {text: 'From', style: 'width: 10%'},
        {text: 'Ovner Date', style: 'width: 10%;'},
        {text: 'Prority', style: 'width: 10%'},
        {text: 'Related To', style: 'width: 10%'},
        {text: 'Contact Name', style: 'width: 10%'},
        {text: 'Campains Owner', style: 'width: 10%'},
      ],
      btnCreateTask: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Task'},
      btnCreateCall: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Call'},
      btnCreateMeeting: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Meeting'},
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