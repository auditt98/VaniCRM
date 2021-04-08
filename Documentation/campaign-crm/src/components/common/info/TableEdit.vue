<template>
  <div class="w-100">
    <div class="basic-info p-3">
      <div class="row mb-2">
        <div class="col-sm-8">
          <h4 v-if="title">{{title}}</h4>
        </div>
        <div class="col-sm-4  text-right">
          <div class="mr-2" style="display: inline" v-for="(col, index) in buttonAction" :key="index" scope="col">
            <button class="btn btn-danger"  @click="$emit('modal')">
              <i  class="fa fa-plus"></i>
              {{col.title}}
            </button>
          </div>
        </div>
      </div>
      <div class="col-sm-12 table-in-list">
        <table class="table">
          <thead>
          <tr v-if="headerColumns">
            <th class="background-pink " v-for="(col, index) in headerColumns" :key="index" scope="col">{{col.text}}</th>
          </tr>
          </thead>
          <slot></slot>
        </table>
        <div class="row justify-content-between background-pink w-100 mr-0 ml-0 border-top pt-2 pl-4">
          <div class="col-sm-4 p-2">

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
          <div class="col-sm-4 text-right table-pagination pr-4  pt-2">
            <span class="mr-3">{{ startRecord }}-{{ startRecord*10}} of {{totalRecord}}</span>
            <i class="fa fa-angle-left mr-3" aria-hidden="true"></i>
            <i class="fa fa-angle-right" aria-hidden="true"></i>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseEdit",
  props: {
    button: Array,
    title: String,
    totalRecord: Number,
    startRecord: Number,
    isModalVisible:Boolean,
    headerColumns: Array,
    buttonAction: Array,
  },
  methods:{
    showModal() {
      this.isModalVisible = true;
    },
  }
}
</script>

<style scoped>


  .background-pink{
    background: #FFE9E4;
  }
  th.background-pink {
    color: #6E6893;
    font-weight: 500;
    text-transform: uppercase;
   }

</style>