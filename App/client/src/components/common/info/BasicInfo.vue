<template>
  <div class="w-100">
    <div class="basic-info p-3">
      <h4 class="ml-2"><strong>{{title}}</strong></h4>
      <div class="row" v-if="description">
        <div class="col-sm-12 px-5 mt-2">
          {{description}}
        </div>
      </div>
      <div class="row">
        <div class="col-sm-6" >
          <table class="ml-4" v-if="arrLeft">
            <tbody>
            <tr v-for="(item,index) in arrLeft" :key="index">
              <td>{{item.key}}</td>
              <td><span class="px-5"></span></td>
              <td v-html="item.value"></td>
            </tr>
            </tbody>
          </table>
        </div>
        <div class="col-sm-6">
          <table v-if="arrRight">
            <tbody>
            <tr v-for="(item,index) in arrRight" :key="index">
              <td>{{item.key}}</td>
              <td>{{item.value}}</td>
            </tr>
            </tbody>
          </table>
        </div>
      </div>

    </div>
  </div>
</template>

<script>
export default {
  name: "BasicInfo",
  props: {
    description: String,
    title: String,
    arrLeft: Array,
    arrRight: Array
  }
}
</script>

<style scoped>

</style>