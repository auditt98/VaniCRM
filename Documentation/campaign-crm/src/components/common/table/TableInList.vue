<template>
  <div>
    <div class="row">
      <div class="form-group col-md-4 input-group mb-3 pl-0">
        <div class="btn-search">
          <input type="text" class="form-control" placeholder="Search" v-model="keyword">
          <i class="fa fa-search"></i>
        </div>
      </div>
      <div class="col-sm-4">
        <button class="btn btn-light btn-purple mr-3" @click="search()">
          <i class="fa fa-search mr-2"></i>
          Search
        </button>
        <button class="btn btn-light btn-purple" :class="{'btn-disable' : !keyword}" @click="clear()">
          <img width="22" src="../../../assets/clear_all.png" alt="" class="mr-1">
          Clear
        </button>
      </div>
      <div class="col-sm-4 text-right">
        <slot name="button"></slot>
      </div>
    </div>
    <div class="row table-in-list border">
      <table class="table mb-0">
        <thead>
        <tr v-if="headerColumns">
          <th v-for="(col, index) in headerColumns" :key="index" :style="col.style" scope="col">{{col.text}}</th>
        </tr>
        </thead>
        <slot name="body"></slot>
      </table>
      <div class="row justify-content-between w-100 mr-0 ml-0 mb-4 border-top pt-4 pl-4" v-if="totalPage">
        <div class="col-sm-4 p-0">

          <div class="form-inline">
            <label for="exampleFormControlSelect1">Rows per page:</label>
            <select class="form-control py-0 ml-3" id="exampleFormControlSelect1" v-model="size" @change="onChange($event)">
              <option :value="5">5</option>
              <option :value="10">10</option>
            </select>
          </div>
        </div>
        <div class="col-sm-4 text-right table-pagination pr-4" v-if="totalPage > 0">
<!--
          <i class="fa fa-angle-left mr-3" aria-hidden="true"></i>
          <span class="mr-3" v-for="(i, index) in totalPage" :key="index" :class="{'active': i===currentPage}"
                @click="goToPage(i)" >{{ i }}</span>
          <i class="fa fa-angle-right" aria-hidden="true"></i>
-->

          <VPagination
              v-model="currentPage"
              :page-count="totalPage"
              :page-range="pageSize"
              :margin-pages="5"
              :click-handler="goToPage"
              :prev-text="'<'"
              :next-text="'>'"
              :container-class="'pagination'"
              :page-class="'page-item'">
          </VPagination>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VPagination from "@/components/common/VPagination";
export default {
  name: "TableInList",
  components: {VPagination},
  data: () => {
    return {
      currentPage: 1,
      size: 5,
      keyword: ''
    }
  },
  props: {
    totalPage: Number,
    pageSize: Number,
    headerColumns: {
      type: Array,
      required: false
    }
  },
  created() {
      this.size = this.pageSize;
  },
  methods: {
    goToPage(page) {
      this.currentPage = page;
      this.$emit('go-to-page', page);
    },
    search() {
      this.currentPage = 1;
      this.$emit('search', this.keyword, this.size);
    },
    clear() {
      this.keyword = null;
      this.size = this.pageSize;
      this.search();
    },
    onChange(event) {
      this.size = event.target.value;
      this.search();
    }
  }
}

</script>

<style scoped>
.btn-search {
  width: 100%;
  position: relative;
}
.btn-search input {
  padding-left: 30px;
}
.btn-search i {
  position: absolute;
  top: 25%;
  left: 2%;
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
.table-in-list {
  border-radius: 8px;
  box-sizing: border-box;
  background: white;
}
.btn-purple {
  background: #6E6893;
  border-radius: 5px;
  color: white;
}
 table thead th {
   color: #9FA2B4;
   border: none;
   text-align: center;
 }
 table tr {
   line-height: 40px;
 }
 table {
   border-radius: 8px;
   min-height: 515px;
 }
 select {
   height: 25px !important;
 }
 .table-pagination {
   color: #BDBDBD;
 }
.table-pagination span, i {
  cursor: pointer;
}
 .table-pagination span.active, i {
   color: black;
   cursor: unset;
 }
 table tbody span {
   font-weight: 600;
   font-size: 14px;
 }
 .btn-disable {
   background: rgba(110, 104, 147, 0.7);
   cursor: unset !important;
   pointer-events: none;
 }
</style>