<template>
  <div class="w-100">
    <div class="table-in-detail p-3" v-if="pageConfig && pageConfig.totalPage && pageConfig.totalPage > 0">
      <div class="row mx-2">
        <h4 :class="{'col-8': !tags, 'col-2': tags}"><strong>{{ title }}</strong></h4>
        <div class="row col-6" v-if="tags">
          <VTag v-for="(tag, index) in tags" :class-prop="'ml-3'" :key="index" :data="tag"/>
        </div>
        <div class="col-4 text-right">
          <slot name="button"></slot>
        </div>
      </div>
      <div class="row ml-2 mt-4 mr-0">
        <table class="w-100"  style="min-height: 200px">
          <thead>
          <tr v-if="headerColumns">
            <th v-for="(col, index) in headerColumns" :key="index" scope="col">{{ col }}</th>
          </tr>
          </thead>
          <slot name="body"></slot>
        </table>
        <div class="table-in-detail-bottom w-100 row justify-content-end m-0" v-if="pageConfig && pageConfig.totalPage && pageConfig.totalPage > 0">
          <div class="form-inline col-sm-2" v-if="pageConfig.pageSize">
            <label for="exampleFormControlSelect1">Rows per page:</label>
            <select class="form-control py-0 ml-3" id="exampleFormControlSelect1" v-model="pageConfig.pageSize" @change="onChangePageSize">
              <option :value="5">5</option>
              <option :value="10">10</option>
              <option :value="20">20</option>
            </select>
          </div>
          <div class="col-sm-2 pt-2 text-right" v-if="pageConfig && pageConfig.totalPage && pageConfig.totalPage > 0">
            <span  v-if="pageConfig.pageSize" class="col-sm-8">1 - {{pageConfig.totalItems > pageConfig.pageSize ? pageConfig.pageSize : pageConfig.totalItems}} of {{pageConfig.totalItems}}</span>
            <span class="col-sm-4 justify-content-between d-inline-flex">
            <i class="fa fa-angle-left" :class="{'disabled' : pageConfig.page === 1}" aria-hidden="true" @click="goToPage(pageConfig.page - 1)"></i>
            <i class="fa fa-angle-right" :class="{'disabled' : pageConfig.page === pageConfig.totalPage}" aria-hidden="true" @click="goToPage(pageConfig.page + 1)"></i>
          </span>
          </div>
        </div>
        <slot name="pagination"></slot>
      </div>
    </div>
    <div class="table-in-detail p-3" v-else style="min-height: 150px">
      <div class="row mx-2">
        <h4 :class="'col-12'"><strong>{{ title }}</strong></h4>
        <div class="row col-12 mt-3">
          <span class="col-2 pt-2">No record found</span>
          <div class="col-4 text-left">
            <slot name="button"></slot>
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
  methods: {
    onChangePageSize() {
      this.$emit('page-size-change', this.pageConfig.pageSize);
    },
    goToPage(page) {
      this.$emit('go-to-page', page)
    }
  },
  props: {
    title: String,
    pageConfig: Object,
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
  color: #6E6893;
}

.table-in-detail td, .table-in-detail th {
  padding: 15px 0px 15px 15px;
  border-bottom: 1px solid #D9D5EC;
}

.table-in-detail-bottom {
  background: #FFE9E4;
  border-bottom-left-radius: 10px;
  border-bottom-right-radius: 10px;
}

.table-in-detail-bottom select, .table-in-detail-bottom option {
  background: unset;
  border: none;
}

i {
  color: black;
  cursor: pointer;
}
i.disabled {
  pointer-events: none;
  color: #ccc;
}
</style>