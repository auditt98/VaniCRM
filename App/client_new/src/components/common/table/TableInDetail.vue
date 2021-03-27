<template>
  <div class="w-100">
    <div class="table-in-detail p-3">
      <div class="row mx-2">
        <h4><strong>{{ title }}</strong></h4>
        <div class="row mx-2" v-if="tags">
          <VTag v-for="(tag, index) in tags" :key="index" :data="tag"/>
        </div>
      </div>
      <div class="row ml-2 mt-4 mr-0">
        <table class="w-100">
          <thead>
          <tr v-if="headerColumns">
            <th v-for="(col, index) in headerColumns" :key="index" scope="col">{{ col }}</th>
          </tr>
          </thead>
          <slot name="body"></slot>
        </table>
        <div class="table-in-detail-bottom w-100 row justify-content-end m-0" v-if="totalPage">
          <div class="form-inline col-sm-2">
            <label for="exampleFormControlSelect1">Rows per page:</label>
            <select class="form-control py-0 ml-3" id="exampleFormControlSelect1">
              <option>10</option>
              <option>2</option>
              <option>3</option>
              <option>4</option>
              <option>5</option>
            </select>
          </div>
          <div class="col-sm-2 pt-2 text-right">
            <span class="col-sm-8">1-10 of 276</span>
            <span class="col-sm-4 justify-content-between d-inline-flex">
            <i class="fa fa-angle-left" aria-hidden="true"></i>
            <i class="fa fa-angle-right" aria-hidden="true"></i>
          </span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VTag from "@/components/common/VTag";

export default {
  name: "TableInDetail",
  components: {VTag},
  props: {
    title: String,
    totalPage: Number,
    tags: Array,
    headerColumns: {
      type: Array,
      required: false
    }
  }
}
</script>

<style scoped>
.table-in-detail {
  background: #FFFFFF;
  box-shadow: 0px -8px 16px rgba(255, 255, 255, 0.4), 0px 16px 24px rgba(55, 71, 79, 0.16);
  border-radius: 30px;
  min-height: 300px;
}

.table-in-detail th {
  background: #FFE9E4;
}

.table-in-detail td, .table-in-detail th {
  padding: 15px 0px 15px 15px;
  border-bottom: 1px solid #D9D5EC;
}

.table-in-detail-bottom {
  background: #FFE9E4;
}

.table-in-detail-bottom select, .table-in-detail-bottom option {
  background: unset;
  border: none;
}

i {
  color: black;
}
</style>