<template>
  <div class="background-main">

    <VLoading :loading="loading"/>
    <div class="col-sm-9 mx-auto mt-5">
      <TableInList :header-columns="columns"
                   :page-size="pageSize"
                   :total-page="totalPage"
                   @go-to-page="goToPage"
                   @search="search">
        <template slot="button">
          <router-link :to="{name: 'ContactCreate'}"><VButton :data="btnCreate"/></router-link>
        </template>
        <template slot="body">
          <tbody v-if="!contacts || contacts.length === 0">
          <tr><td colspan="6" class="text-center">Không có dữ liệu</td></tr>
          </tbody>
          <tbody v-if="contacts && contacts.length > 0">
          <tr v-for="(item,index) in contacts" :key="index">
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
        this.loadContacts(this.keyword);
      },
      search(keyword, pageSize) {
        this.currentPage = 1;
        this.pageSize = Number(pageSize);
        this.loadContacts(keyword);
      },
      deleteContact(id) {
        if (!confirm("Xác nhận xóa!")) {
          return ;
        }
        this.loading = true;
        contactService.remove(id).then(res => {
          if(res) {
            alert('Xóa thành công!');
            this.loadContacts(this.keyword);
          }
        }).finally(() => {
          this.loading = false;
        });
      }
      ,
      loadContacts(keyword) {
        this.loading = true;
        this.keyword = keyword;
        this.contacts = [];
        let query = {
          currentPage: this.currentPage,
          pageSize: this.pageSize
        };
        if (keyword) {
          query['query'] = this.keyword;
        }
        contactService.getAll(query).then(res => {
          if (res) {
            this.contacts = res.data.contacts;
            this.totalPage = Number(res.data.pageInfo.TotalPages);
          }
        }).finally(() => {
          this.loading = false;
        })
      },
      editContact(id) {
        this.$router.push({path: '/contact-detail', query : { id: id}});
      },
    },

    created() {
      this.currentPage = 1;
      this.pageSize = 5;
      this.loadContacts(null);
    },
    data: function () {
      return {
        contacts: Array,
        currentPage: Number,
        pageSize: Number,
        totalPage: 1,
        keyword: null,
        loading: false,
        columns: [
          {text: 'Contact Name', style: 'width: 20%'},
          {text: 'Account Name', style: 'width: 20%'},
          {text: 'Phone', style: 'width: 10%'},
          {text: 'Email', style: 'width: 20%'},
          {text: 'Account Owner', style: 'width: 15%'},
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