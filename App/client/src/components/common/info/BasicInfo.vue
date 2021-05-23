<template>
  <div class="w-100">
    <div class="basic-info p-3">
      <h4 class="ml-2"><strong>{{title}}</strong></h4>
      
      <div v-if="isEmail == true">
        <div class="row email-row-border">
          <div class="col-sm-1 text-right email-row" style="border: 0px;">From:</div>
            <input class="col-sm-11 email-row-disabled"  value="vanicrm.noreply@gmail.com" disabled>
        </div>
        <div class="row email-row-border">
          <div class="col-sm-1 text-right email-row" style="border: 0px;">Subject:</div>
          <input class="col-sm-11 email-row" :value="emailTitle" disabled>
        </div>
        <div v-html="description" class="col-sm-12 px-5 mt-4" style=""></div>
      </div>
      <div v-else>
        <div class="row" v-if="description">
          <div class="col-sm-12 px-5 mt-2">
            {{description}}
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col-sm-6" >
          <table class="ml-4" v-if="arrLeft">
            <tbody>
            <tr v-for="(item,index) in arrLeft" :key="index">
              <td v-html="item.key"></td>
              <td><span class="px-5"></span></td>
              <td v-if="item.value" v-html="item.value"></td>
            </tr>
            </tbody>
          </table>
        </div>
        <div class="col-sm-6">
          <table v-if="arrRight" class="w-100">
            <tbody>
            <tr v-for="(item,index) in arrRight" :key="index">
              <td>{{item.key}}</td>
              <td v-if="item.value" v-html="item.value"></td>
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
    arrRight: Array,
    isEmail: Boolean,
    emailTitle: String,
  }
}

</script>

<style>

.email-row-disabled{
  margin-top: 10px; 
  margin-bottom: 10px; 
  background-color: white !important;
  color: rgba(0,0,0, 0.3);
  /* padding-left: 2rem; */
  border: 0px;

}

.email-row-border{
    border-top: 0px;
    border-left: 0px; 
    border-right: 0px; 
    border-bottom: 1px solid rgba(0,0,0,0.1); 
}

.email-row{
  margin-top: 10px; 
  margin-bottom: 10px; 
  border: 0px;
  background-color: white !important;
  /* color: rgba(0,0,0, 0.3); */
  padding-left: 2rem;
}

td {
  min-width: 145px;
}
</style>