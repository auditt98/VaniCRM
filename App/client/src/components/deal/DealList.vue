<template>
  <div class="background-main">

    <div class="col-sm-11 mx-auto mt-5">
      <VLoading :loading="loading"/>
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
                   :isSortable="true"
      >
        <template slot="button">
          <router-link to="/deals/create"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!deals || deals.length === 0">
          <tr>
            <td colspan="7" class="text-center">No data available :)</td>
          </tr>
          </tbody>
          <tbody v-if="deals && deals.length > 0">
          <tr v-for="(item,index) in deals" :key="index">
            <th>{{item.name}}</th>
            <th>{{item.expectedDate | formatDate}}</th>
            <th>{{new Intl.NumberFormat().format(item.amount)}}</th>
            <th>{{ item.stage }}</th>
            <th>
              <VTag v-if="item.priority" :data="{text: item.priority, bgColor: getBgColor(item.priority)}"/>
            </th>
            <th>{{ item.accountName }}</th>
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
import VButton from "@/components/common/VButton";
import VTag from "@/components/common/VTag";
import VLoading from "@/components/common/VLoading";
import {dealService} from "@/service/deal.service";

export default {
  name: "DealList",
  components: {VLoading, VTag, VButton, TableInList, },
  methods: {
    getBgColor(data) {
      if (data.toUpperCase() === 'HIGH') {
        return '#F12B2C';
      }
      if (data.toUpperCase() === 'MEDIUM') {
        return '#FEC400';
      }
      return '#29CC97';
    },
    editItem(id) {
      this.$router.push({path: '/deals/detail', query : { id: id}});
    },
    deleteItem(id) {
      if (!confirm('Are you sure to delete?')) {
        return ;
      }
      this.loading = true;
      dealService.remove(id).then(res => {
        if(res) {
          alert('Deleted successfully!');
          this.loadDeals(this.keyword, this.sortQueries);
        }
      }).finally(() => {
        this.loading = false;
      });
    },
    goToPage(page) {
      this.currentPage = page;
      this.loadDeals(this.keyword, this.sortQueries);
    },
    search(keyword, pageSize, sortQueries) {
      this.currentPage = 1;
      this.pageSize = Number(pageSize);
      this.sortQueries = sortQueries;
      this.loadDeals(keyword, sortQueries);
    },
    loadDeals(keyword, sortQueries) {
      this.loading = true;
      this.keyword = keyword;
      this.deals = [];
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
      dealService.getAll(query).then(res => {
        if (res && res.data) {
          this.deals = res.data.deals;
          this.deals = [...this.deals]
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
    this.loadDeals(null);
  },
  data: function () {
    return {
      columns: [
        {text: 'Deal Name', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "name", isSorting: Boolean},
        {text: 'Expected Closing Date', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "expectedDate", isSorting: Boolean},
        {text: 'Amount', style: 'width: 10%', sortable: true, ascSort: Boolean, objectName: "amount", isSorting: Boolean},
        {text: 'Stage', style: 'width: 10%;'},
        {text: 'Priority', style: 'width: 10%;', sortable: true, ascSort: Boolean, objectName: "priority", isSorting: Boolean},
        {text: 'Account Name', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "accountName", isSorting: Boolean},
        {text: 'Deal Owner', style: 'width: 15%;', sortable: true, ascSort: Boolean, objectName: "owner", isSorting: Boolean},
        {text: 'Action', style: 'width: 15%;'},
      ],
      sortQueries: Array,
      deals: Array,
      keyword: '',
      pageSize: 5,
      totalPage: 5,
      loading: false,
      btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Deal'},
    };
  }
}
</script>

<style scoped>
th, td {
  text-align: center;
}
</style>