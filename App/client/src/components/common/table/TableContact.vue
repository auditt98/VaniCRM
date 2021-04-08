<template>
  <div>
    <div class="row">
      <div class="form-group col-md-4 input-group mb-3 pl-0">
        <div class="btn-search">
          <input type="text" class="form-control" placeholder="Search">
          <i class="fa fa-search"></i>
        </div>
      </div>
      <div class="col-sm-5">
        <button class="btn btn-light btn-purple mr-3">
          <i class="fa fa-search mr-2"></i>
          Search
        </button>
        <button class="btn btn-light btn-purple">
          <img width="22" src="src/assets/clear_all.png" alt="" class="mr-1">
          Clear
        </button>
      </div>
      <div class="col-sm-3 text-right">
        <button class="btn btn-danger">
          <i class="fa fa-plus"></i>
          Create Contact
        </button>
      </div>
    </div>
    <div class="row table-in-list border">
      <table class="table mb-0">
        <thead>
        <tr v-if="headerColumns">
          <th v-for="(col, index) in headerColumns" :key="index" scope="col">{{col.text}}</th>
        </tr>
        </thead>
        <slot></slot>
      </table>
      <div class="row justify-content-between w-100 mr-0 ml-0 mb-4 border-top pt-4 pl-4">
        <div class="col-sm-4 p-0">

          <div class="form-inline">
            <label for="exampleFormControlSelect1">Rows per page:</label>
            <select class="form-control py-0 ml-3" id="exampleFormControlSelect1">
              <option>10</option>
              <option>2</option>
              <option>3</option>
              <option>4</option>
              <option>5</option>
            </select>
          </div>
        </div>
        <div class="col-sm-4 text-right table-pagination pr-4">
          <i class="fa fa-angle-left mr-3" aria-hidden="true"></i>
          <span class="mr-3" v-for="(i, index) in 5" :key="index" :class="{'active': i===1}">{{ i }}</span>
          <i class="fa fa-angle-right" aria-hidden="true"></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TableInList",
  components: {},
  props: {
    btnCreate: String,
    btnCreateContact: String,
    headerColumns: {
      type: Array,
      required: false
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
}
table tr {
  line-height: 40px;
}
table {
  border-radius: 8px;
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
</style>