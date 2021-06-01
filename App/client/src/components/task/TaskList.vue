<template>
  <div class="background-main">

    <div class="col-sm-10 mx-auto mt-5">
      <VLoading :loading="loading"/>
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
                   @inputchange="inputchange"
      >
        <template slot="extraButton">
            <button class="btn btn-light btn-purple mr-3" @click="filterOpen()">
              <i class="fa fa-filter" aria-hidden="true"></i> Open tasks
            </button>

        </template>
        <template slot="button">
          <div class="d-flex justify-content-between">
            <router-link to="/tasks/create"><VButton :data="btnCreateTask"/></router-link>
            <router-link to="/meetings/create"><VButton :data="btnCreateMeeting"/></router-link>
            <router-link to="/calls/create"><VButton :data="btnCreateCall"/></router-link>
          </div>
        </template>
        <template slot="body">
          <tbody v-if="!tasks || tasks.length === 0">
          <tr>
            <td colspan="9" class="text-center">Không có dữ liệu</td>
          </tr>
          </tbody>
          <tbody v-if="tasks && tasks.length > 0">
          <tr v-for="(item,index) in tasks" :key="index">
            <th>{{item.title}}</th>
            <th>{{item.type}}</th>
            <th>{{ item.startDate | formatDate }}</th>
            <th>{{ item.endDate | formatDate }}</th>
            
            <th><VTag :data="{text: item.priority, bgColor: getBgColor(item.priority)}"/></th>
            <th><VTag :data="{text: item.status, bgColor: getBgColor(item.status)}"/></th>
            <th>{{item.lead ? item.lead.name : ''}}</th>
            <th>{{item.contact ? item.contact.name : ''}}</th>
            <!-- <th>{{item.relatedAccount ? item.relatedAccount.name : ''}}</th> -->
            <!-- <th>{{item.relatedCampaign ? item.relatedCampaign.name : ''}}</th> -->
            <!-- <th>{{item.relatedDeal ? item.relatedDeal.name : ''}}</th> -->
            <!-- <th>{{ item.name }}</th>
            <th>{{ item.phone }}</th>
            <th>{{ item.website }}</th>
            <th>{{ item.owner }}</th> -->
            <th>
              <span class="action">
              <span @click="editItem(item.id, item.type)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
              <span @click="deleteItem(item.id, item.type)"><img src="images/delete-bin-2-line.png" alt=""></span>
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
import VTag from "@/components/common/VTag";
import VLoading from "@/components/common/VLoading";
import VButton from "@/components/common/VButton";
import {taskService} from "@/service/task.service";
import {authenticationService} from "@/service/authentication.service";
import {userService} from "@/service/user.service";
export default {
  name: "TaskList",
  components: {VButton, VLoading, TableInList, VTag, },
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
      if (data.toUpperCase() === 'MEDIUM') {
        return '#29CC97';
      }
      if (data.toUpperCase() === 'OPEN') {
        return '#29CC97';
      }
      return 'red';
    },
    editItem(id, type) {
      this.$router.push({path: '/' + type + '/detail', query : { id: id}});
    },
    deleteItem(id, type) {
      if (!confirm('Are you sure to delete?')) {
        return ;
      }
      this.loading = true;
      taskService.remove(id, type).then(res => {
        if(res) {
          alert('Deleted successfully!');
          this.loadTasks(this.keyword);
        }
      }).finally(() => {
        this.loading = false;
      });
    },
    goToPage(page) {
      this.currentPage = page;
      this.loadTasks(this.keyword);
    },
    inputchange(keyword, pageSize){
      console.log(keyword)
      this.keyword = keyword;
      this.pageSize = Number(pageSize)
    },
    search(keyword, pageSize) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.loadTasks(keyword);
    },
    filterOpen(){
      this.loadTasks(this.keyword, "Open")
      // console.log(this.keyword, "Open");      //this.loadTasks()
    },
    loadTasks(keyword, status) {
      console.log(status)
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
      if(status){
        query['status'] = status;
      }
      userService.getAllTasks(authenticationService.currentUserValue.id, query).then(res => {
        if (res && res.data) {
          this.tasks = res.data.tasks;
          this.tasks = [...this.tasks]
          this.totalPage= Number(res.data.pageInfo.TotalPages);
          // this.totalItemTask = Number(res.data.pageInfo.TotalItems);
        }
      }).finally(() => {
        this.loading = false;
      })
      // taskService.getAll(query).then(res => {
      //   if (res && res.data) {
      //     this.tasks = res.data.tasks;
      //     this.totalPage = Number(res.data.pageInfo.TotalPages);
      //   }
      // })
    }
  },

  created() {
    this.currentPage = 1;
    this.pageSize = 5;
    this.loadTasks(null);
  },
  data: function () {
    return {
      // user: {},
      tasks: Array,
      currentPage: 1,
      pageSize: 10,
      totalPage: 0,
      keyword: '',
      loading: false,
      columns: [
        {text: 'Title', style: 'width: 15%;'},
        {text: 'Type', style: 'width: 10%;'},
        {text: 'Start Date', style: 'width: 10%'},
        {text: 'End Date', style: 'width: 10%;'},
        {text: 'Priority', style: 'width: 10%'},
        {text: 'Status', style: 'width: 10%'},
        {text: 'Lead', style: 'width: 10%'},
        {text: 'Contact', style: 'width: 10%'},
        // {text: 'Related Account', style: 'width: 10%'},
        // {text: 'Related Campaign', style: 'width: 10%'},
        // {text: 'Related Deal', style: 'width: 10%'},
        {text: 'Action', style: 'width: 5%'}
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