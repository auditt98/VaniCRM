<template>
  <div>
    <div class="row">
      <div class="form-group col-md-4 input-group mb-3 pl-0">
        <div class="btn-search">
          <input type="text" class="form-control" placeholder="Search" v-model="keyword" @keyup="inputChangeHandler()">
          <i class="fa fa-search"></i>
        </div>
      </div>
      <div :class="{'col-sm-6' : isSortable, 'col-sm-4': !isSortable}" >
        <button class="btn btn-light btn-purple mr-3" style="cursor: pointer;" @click="search()">
          <i class="fa fa-search mr-2"></i>
          Search
        </button>
        <slot name="extraButton"></slot>
        <button class="btn btn-light btn-purple mr-3" :class="{'btn-disable' : !keyword && !tblSorting}" @click="clear()">
          <img width="22" src="../../../assets/clear_all.png" alt="" class="mr-1">
          Clear
        </button>
        <button class="btn btn-light btn-purple" v-show="isSortable" :class="{'btn-disable' : !tblSorting}" @click="clearSort()">
          <img width="22" src="../../../assets/clear_sort.png" alt="" class="mr-1">
          Clear sort
        </button>
      </div>
      <div class="text-right" :class="{'col-sm-2' : isSortable, 'col-sm-4': !isSortable}">
        <slot name="button"></slot>
      </div>
    </div>
    <div class="row table-in-list border">
      <table class="table mb-0">
        <thead>
        <tr v-if="headerColumns">
          <th v-for="(col, index) in headerColumns" :key="index" :style="col.style" scope="col">
            <div @click="sort(col, index)" class="noselect">
              {{col.text}}
              <span v-if="col.sortable">
                <span class="arrow-up" :class="{'sort-asc' :  col.ascSort != undefined && col.ascSort == true && col.isSorting == true}"></span>
                <span class="arrow-down" :class="{'sort-desc' : col.ascSort != undefined && col.ascSort == false && col.isSorting == true}"></span>
              </span>
            </div>
          </th>
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
      sortQueries: [],
      currentPage: 1,
      size: 5,
      keyword: '',
      tblSorting: Boolean,
    }
  },
  props: {
    totalPage: Number,
    isSortable: Boolean,
    pageSize: Number,
    headerColumns: {
      type: Array,
      required: false,
      sortable: false,
      ascSort: Boolean,
      objectName: "",
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
      var flatten = this.sortQueries.flatMap(value => [value.objectName])
      this.$emit('search', this.keyword, this.size, flatten);
    },
    clear() {
      this.keyword = null;
      this.size = this.pageSize;
      this.clearSort();
      // this.search();
    },
    inputChangeHandler(){
      this.$emit('inputchange', this.keyword, this.size)
    },
    clearSort(){
      this.tblSorting = false;
      this.headerColumns.forEach(element => {
        element.isSorting = false;
        element.ascSort = Boolean;
        this.sortQueries = [];
        this.search()
        // this.$emit('search', this.keyword, this.size);
      });
    },
    sort(col, index){
      col.isSorting = true;
      this.tblSorting = true;
      if(col.ascSort != undefined){
        col.ascSort = !col.ascSort
        //look for object that has index property
        let obj = this.sortQueries.find(x => x.index == index)
        //if there is none, push it into the array, else change objectName then emit the request
        if(col.ascSort){
          if(obj === undefined){
            obj = {index: index, objectName: "asc." + col.objectName}
            this.sortQueries.push(obj)
          } else{
            obj.objectName = "asc." + col.objectName
          }
        } else{
          if(obj === undefined){
            obj = {index: index, objectName: "desc." + col.objectName}
            this.sortQueries.push(obj)
          } else{
            obj.objectName = "desc." + col.objectName
          }
        }
        var flatten = this.sortQueries.flatMap(value => [value.objectName])
        this.$emit('search', this.keyword, this.size, flatten);
      }
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

 .arrow-up {
  width: 0; 
  height: 0; 
  position: absolute;
  margin-left: 5px;
  top: 79px;
    border-left: 6px solid transparent;
    border-right: 6px solid transparent;
    border-bottom: 6px solid #ccc;
  /* border-left: 5px solid transparent;
  border-right: 5px solid transparent;
  
  border-bottom: 5px solid black; */
}

.arrow-down {
  width: 0; 
  height: 0; 
  position: absolute;
  margin-left: 5px;
  /* left: px; */
  top: 90px;
    border-left: 6px solid transparent;
    border-right: 6px solid transparent;
  
  border-top: 6px solid #ccc;
}

.sort-asc{
  border-bottom: 6px solid #D93915 !important;
}

.sort-desc{
  border-top: 6px solid #D93915 !important;
}

.noselect{
  -webkit-touch-callout: none;
    -webkit-user-select: none;
    -khtml-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}

</style>