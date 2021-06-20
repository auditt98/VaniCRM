<template>
  <div class="w-100">
    <div class="basic-info p-3">
      <h4 class="ml-2"><strong>{{title}}</strong></h4>
      <div class="row" v-if="image">
        <div class="user-info-avatar">
          <div class="user-info-avatar-image">
            <img class="w-100" src="../../../assets/default_avatar.png" alt="">
          </div>
          <div class="image-select-background">
          </div>
          <div>
            <img src="../../../assets/image.png" class="image-select" alt="">
            <img src="../../../assets/done.png" class="image-done" alt="">
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-sm-5" >
          <table class="ml-4" v-if="arrLeft">
            <tbody>
            <tr v-for="(item,index) in arrLeft" :key="index">
              <td>{{item.key}}</td>
              <td>
                <input v-if="item.type==='INPUT_TEXT'" type="text" v-model="item.model">
                <select class="w-100" v-if="item.type==='INPUT_SELECT'" name="" :id="'x_'+index">
                  <option value="">select</option>
                </select>
              </td>
            </tr>
            </tbody>
          </table>
        </div>
        <div class="col-sm-1"></div>
        <div class="col-sm-5">
          <table v-if="arrRight">
            <tbody>
            <tr v-for="(item,index) in arrRight" :key="index">
              <td>{{item.key}}</td>
              <td>
                <input v-if="item.type==='INPUT_TEXT'" type="text">
                <select class="w-100" v-if="item.type==='INPUT_SELECT'" name="" :id="'y_'+index">
                  <option value="">select</option>
                </select>
              </td>
            </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="row" v-if="description">
        <textarea class="col-sm-11 mx-auto" name="" id="" cols="30" rows="3"></textarea>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseEdit",
  props: {
    description: String,
    image: String,
    title: String,
    arrLeft: Array,
    arrRight: Array
  }
}
</script>

<style scoped>

</style>