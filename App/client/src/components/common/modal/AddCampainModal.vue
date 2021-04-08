<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal w-75" ref="modal">
        <header class="modal-header">
          <slot name="header">
            <h4 class="text-left w-100">Add Campains</h4>
          </slot>
        </header>
        <section class="modal-body text-center">
          <slot name="body">
            <div class="row table-in-list border">
              <table class="table mb-0">
                <thead>
                <tr >
                  <th ></th>
                  <th >TYPE</th>
                  <th >CAMPAINS NAME</th>
                  <th >STATUS</th>
                  <th >START DATE</th>
                  <th >END DATE</th>
                  <th>OWNER</th>
                </tr>
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
                <div class="col-sm-4 text-right table-pagination pr-4  pt-2">
                  <span class="mr-3">1-10 of 176</span>
                  <i class="fa fa-angle-left mr-3" aria-hidden="true"></i>
                  <i class="fa fa-angle-right" aria-hidden="true"></i>
                </div>
              </div>
            </div>
          </slot>
        </section>

        <footer class="modal-footer text-center">
          <slot name="footer">
            <span @click="close"><VButton :data="btnSend"/></span>
            <span @click="close"><VButton :data="btnCancel"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
export default {
  name: "AddCampainModal",
  components: {VButton},
  props: {
    headerColumns: {
      type: Array,
      required: false
    },
    totalRecord: Number,
    startRecord: Number,
  },
  data () {
    return {
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-times', text: 'Cancel'},
      btnSend: {btnClass: 'btn-red px-4', icon: 'fa-pencil', text: 'Send'},
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
  }
}
</script>

<style scoped>
input:focus{
  outline: none;
}
</style>