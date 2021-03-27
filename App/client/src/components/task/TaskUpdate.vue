<template>
  <div class="">
    <Header/>
    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <VButton :data="btnCancel"/>
          <span @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class=" mt-3">
        <div class="row">
          <BaseEdit :image="'true'" :title="'Lead Avatar'"/>
        </div>
        <div class="row mt-3">

          <div class="basic-edit">
            <h4 class="ml-2"><strong>Task Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr>
                    <td class="required">Title</td>
                    <td><input class="form-control " type="text" v-model="user.FirstName">

                    </td>
                  </tr>
                  <tr>
                    <td class="required">Due Date</td>
                    <td><input class="form-control " type="text" v-model="user.LastName" >

                    </td>
                  </tr>
                  <tr>
                    <td class="required">Contract</td>
                    <td><input class="form-control " type="text" v-model="user.Username" >

                    </td>
                  </tr>
                  <tr>
                    <td class="required">Account</td>
                    <td><input class="form-control " type="text" v-model="user.Password" >

                    </td>
                  </tr>
                  <tr>
                    <td><label for="Priority" class="col-sm-1">Priority</label></td>
                    <td>
                      <select class="form-control py-0 col-sm-3" id="Priority" v-model="user.GROUP_ID">
                        <option :value="1">Marketing</option>
                        <option :value="3">Sale</option>
                      </select>
                    </td>
                  </tr>
                  </tbody>
                </table>
              </div>
              <div class="col-sm-1"></div>
              <div class="col-sm-5">
                <table>
                  <tbody>
                  <tr>
                    <td><label for="Status" class="col-sm-1">Status</label></td>
                    <td>
                      <select class="form-control py-0 col-sm-3" id="Status" v-model="user.GROUP_ID">
                        <option :value="1">Marketing</option>
                        <option :value="3">Sale</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td class="required">Repeat Type</td>
                    <td><input type="text" v-model="user.Skype"></td>
                  </tr>
                  <tr>
                    <td>Skype</td>
                    <td><input type="text" v-model="user.Skype"></td>
                  </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Information</strong></h4>
            <div class="row ml-4">
                <textarea class="col-sm-11 mx-auto" name="" id="" cols="30" rows="3"></textarea>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TaskUpdate",
  data: function () {
    return {
      user: {
        id: null,
        Username: null,
        FirstName: null,
        LastName: null,
        Password: null,
        Email: null,
        Skype: null,
        Phone: null,
        GROUP_ID: 1

      },
      isValidate: false,
      requireError: 'Không được bỏ trống.',
      passwordError: '',
      emailError: '',
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'}

    }
  },
  methods: {
    save() {

    }
  }
}
</script>

<style scoped>

</style>