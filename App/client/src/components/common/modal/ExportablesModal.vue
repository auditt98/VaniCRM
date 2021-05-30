<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <VLoading :loading="loading"/>
        <header class="modal-header mt-4">
          <slot name="header">
            <div class="row">
              <div class="col-sm-12 text-center">
                <h3 style="color: #192A3E"><strong>Export To PDF</strong></h3>
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
                  <td>{{t.name}}</td>
                  <td><input type="date" v-model="t.fromDate" v-if="t.isSupportDate"/></td>
                  <td><input type="date" v-model="t.toDate" v-if="t.isSupportDate"/></td>
                  <td>
                    <a :href="getHref(t.url, t.fromDate, t.toDate)" ><i class="fa fa-download text-primary"></i></a>
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
import {reportService} from "@/service/report.service";

export default {
  name: "ExportablesModal",
  components: {VLoading, TableInDetail, VButton},
  props: {
    meetingId: Number
  },
  data () {
    return {
      items: [],
      type: 1,
      loading: false,
      currentPage: 1,
      pageSize: 5,
      totalItems: 0,
      totalPage: 0,
      keyword: '',
      columns: [
          'Report Name', 
          'From Date',
          'To Date',
          'Action'
      ],
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-times', text: 'Close'},
    }
  },
  watch: {
    type: function () {
      this.currentPage = 1;
      this.loadItems();
    }
  },
  methods: {
    getHref(url, fromDate, toDate){
      if(fromDate != undefined && toDate != undefined){
        return url + '?fromDate=' + fromDate + '&toDate=' + toDate
      } else{
        return url
      }
    },
    close() {
      this.$emit('close');
    },
    goToPage(page) {
      this.currentPage = page;
      this.loadItems();
    },
    loadItems() {
      this.loading = true;
      this.items = [];
      let query = {
        currentPage: this.currentPage,
        pageSize: this.pageSize
      };
      reportService.getExportables(query).then(res => {
          if (res) {
            this.items = res.data.exportables;
            this.totalPage = Number(res.data.pageInfo.TotalPages);
            this.totalItems = Number(res.data.pageInfo.TotalItems);
          }
        }).finally(() => {
          this.loading = false;
        }) 
    },
    clear() {
      this.keyword = '';
      this.loadItems();
    },
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