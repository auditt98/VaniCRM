<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <VLoading :loading="loading"/>
        <header class="modal-header mt-4">
          <slot name="header">
            <div class="row">
              <div class="col-sm-2">
                <h3 style="color: #192A3E"><strong>Contacts</strong></h3>
              </div>
              <div class="row col-sm-10 text-center">
                <div class="col-sm-1"></div>
                <div class="form-group col-sm-6 input-group mb-3 pl-0">
                  <div class="btn-search">
                    <input type="text" class="form-control" placeholder="Search" v-model.trim="keyword">
                    <i class="fa fa-search"></i>
                  </div>
                </div>
                <div class="col-sm-5">
                  <button class="btn btn-light btn-purple mr-3" @click="search">
                    <i class="fa fa-search mr-2"></i>
                    Search
                  </button>
                  <button class="btn btn-light btn-purple" :class="{'btn-disable' : !keyword}" @click="clear">
                    <img width="22" src="../../../assets/clear_all.png" alt="" class="mr-1">
                    Clear
                  </button>
                </div>
                <div class="col-sm-3 text-right">
                  <slot name="button"></slot>
                </div>
              </div>
            </div>
          </slot>
        </header>
        <section class="modal-body text-center">
          <slot name="body">
            <TableInDetail :header-columns="columns" :page-config="{page: currentPage, totalItems: totalItems, totalPage: totalPage}"
            @go-to-page="goToPage">
              <template slot="body">
                <tbody v-if="items && items.length > 0">
                <tr v-for="(t, i) in items" :key="i">
                  <td>{{t.contactName}}</td>
                  <td>{{t.email}}</td>
                  <td>{{t.phone}}</td>
                  <td>
                    <span v-if="t.isAdd"><i class="fa fa-check text-success"></i></span>
                    <span v-else @click="addItem(t)"><i class="fa fa-plus text-danger"></i></span>
                  </td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr><td colspan="3" class="text-center">Không có dữ liệu</td></tr>
                </tbody>
              </template>
            </TableInDetail>
          </slot>
        </section>

        <footer class="modal-footer text-center">
          <slot name="footer">
            <span @click="close"><VButton :data="btnCancel"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
import TableInDetail from "@/components/common/table/TableInDetail";
import VLoading from "@/components/common/VLoading";
import {contactService} from "@/service/contact.service";
import {campaignService} from "@/service/campaign.service";
import _get from 'lodash/get'
export default {
  name: "AddContactModal",
  components: {VLoading, TableInDetail, VButton},
  props: {
    campaignId: Number
  },
  data () {
    return {
      meeting: null,
      items: [],
      loading: false,
      currentPage: 1,
      pageSize: 5,
      totalItems: 0,
      totalPage: 0,
      keyword: '',
      columns: [
        'Contact Name', 'Email', 'Phone', 'Action'
      ],
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-times', text: 'Close'},
    }
  },
  methods: {
    loadItems() {
      this.loading = true;
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      if (this.keyword) {
        query['query'] = this.keyword;
      }
      contactService.getAll().then(res => {
        this.items = _get(res, 'data.contacts') || []
        this.totalItems = Number(_get(res, 'data.pageInfo.TotalItems'))
        this.totalPage = Number(_get(res, 'data.pageInfo.TotalPages'))
        }).finally(() => {
          this.loading = false;
        })
    },
    addItem(contact) {
      if (!confirm('Are you sure?')) {
        return ;
      }
      campaignService.addContact(this.campaignId, {contactId: contact.id}).then((res) => alert(res.message))
    },
    close() {
      this.$emit('close');
    },
    goToPage(page) {
      this.currentPage = page;
      this.loadItems();
    },
    
    search() {
      this.loadItems();
    },
    clear() {
      this.keyword = '';
      this.loadItems();
    }
  },
  created() {
    this.loadItems();
  }
}
</script>

<style scoped>
.modal-header {
  display: unset;
}
.modal {
  min-width: 60% !important;
}
.btn-search {
  width: 100%;
  position: relative;
}
.btn-search input {
  padding-left: 40px;
}
.btn-search i {
  position: absolute;
  top: 25%;
  left: 3%;
  color: #ccc;
}
.btn-purple {
  background: #6E6893;
  border-radius: 5px;
  color: white;
}
.btn-purple i, .btn-danger i {
  color: white !important;
}
.btn-danger {
  background: #D93915;
}
.btn {
  padding: 5px 20px;
}
.btn-disable {
  background: rgba(110, 104, 147, 0.7);
  cursor: unset !important;
  pointer-events: none;
}
/deep/ table {
  min-height: 330px !important;
}
</style>