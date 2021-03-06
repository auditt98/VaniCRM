<template>
  <div class="background-main">

    <VLoading :loading="loading"/>
    <div class="col-sm-11 mx-auto mt-5">
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search"
                   :isSortable="true"
      >
        <template slot="button">
          <router-link :to="{name: 'ContactCreate'}"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!contacts || contacts.length === 0">
          <tr><td colspan="6" class="text-center">No data available :)</td></tr>
          </tbody>
          <tbody v-if="contacts && contacts.length > 0">
          <tr v-for="(item,index) in contacts" :key="index">
            <th>
              <img :src="item.avatar" style="width: 60px; height: 60px; border-radius: 50%"/>
            </th>
            <th>{{ item.contactName }}</th>
            <th>{{ item.accountName }}</th>
            <th>{{ item.phone }}</th>
            <th>{{ item.email }}</th>
            <th>{{ item.owner }}</th>
            <th>
              <span class="action">
                  <span @click="editContact(item.id)" class="mr-1"><img src="images/newspaper-line.png" alt=""></span>
                  <span @click="deleteContact(item.id)"><img src="images/delete-bin-2-line.png" alt=""></span>
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
  import VLoading from "@/components/common/VLoading";
  import {contactService} from "../../service/contact.service";

  export default {
    name: "UserList",
    components: {VLoading, VButton, TableInList, },
    methods: {
      goToPage(page) {
        this.currentPage = page;
        this.loadContacts(this.keyword, this.sortQueries);
      },
      search(keyword, pageSize, sortQueries) {
        this.currentPage = 1;
        this.pageSize = Number(pageSize);
        this.sortQueries = sortQueries;
        this.loadContacts(keyword, sortQueries);
      },
      deleteContact(id) {
        if (!confirm('Are you sure to delete?')) {
          return ;
        }
        this.loading = true;
        contactService.remove(id).then(res => {
          if(res) {
            alert('Deleted successfully!');
            this.loadContacts(this.keyword);
          }
        }).finally(() => {
          this.loading = false;
        });
      }
      ,
      loadContacts(keyword, sortQueries) {
        this.loading = true;
        this.keyword = keyword;
        this.contacts = [];
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
        contactService.getAll(query).then(res => {
          if (res) {
            this.contacts = res.data.contacts;
            this.contacts = [...this.contacts]
            this.totalPage = Number(res.data.pageInfo.TotalPages);
          }
        }).finally(() => {
          this.loading = false;
        })
      },
      editContact(id) {
        this.$router.push({path: '/contacts/detail', query : { id: id}});
      },
    },

    created() {
      this.currentPage = 1;
      this.pageSize = 5;
      this.loadContacts(null);
    },
    data: function () {
      return {
        sortQueries: Array,
        contacts: Array,
        currentPage: Number,
        pageSize: Number,
        totalPage: 1,
        keyword: null,
        loading: false,
        columns: [
         {text: 'Avatar', style: 'width: 5%;'},
          {text: 'Contact Name', style: 'width: 20%', sortable: true, ascSort: Boolean, objectName: "contactName", isSorting: Boolean},
          {text: 'Account Name', style: 'width: 20%', sortable: true, ascSort: Boolean, objectName: "accountName", isSorting: Boolean},
          {text: 'Phone', style: 'width: 10%', sortable: true, ascSort: Boolean, objectName: "phone", isSorting: Boolean},
          {text: 'Email', style: 'width: 15%', sortable: true, ascSort: Boolean, objectName: "email", isSorting: Boolean},
          {text: 'Account Owner', style: 'width: 15%', sortable: true, ascSort: Boolean, objectName: "owner", isSorting: Boolean},
          {text: 'Actions', style: 'width: 15%'},
        ],
        btnCreate: {btnClass: 'btn-red px-4', icon: 'fa-plus', text: 'Create Contact'},
      };
    }
  }
</script>

<style scoped>
th, td {
  text-align: center;
}
</style>