<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <VButton :data="btnCancel"/>
          <VButton :data="btnSave"/>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-2">
          <MenuLeft/>
        </div>
        <div class="col-sm-10">
          <div class="row">
            <BaseEdit :image="'true'" :title="'Lead Avatar'"/>
          </div>
          <div class="row mt-3">
            <BaseEdit :arr-left="dataLeftInfo" :arr-right="dataRightInfo" :title="'Lead Information'"/>
          </div>
          <div class="row mt-3">
            <BaseEdit :description="description" :title="'Description'"/>
          </div>
          <div class="row mt-3">
            <BaseEdit :arr-left="dataLeftAddress" :title="'Address'"/>
          </div>
          <div class="row mt-3">
            <Note/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

import VButton from "@/components/common/VButton";
import MenuLeft from "@/components/common/MenuLeft";
import BaseEdit from "@/components/common/info/BaseEdit";
export default {
  name: "LeadUpdate",
  components: {BaseEdit, MenuLeft, VButton, },
  data: function () {
    return {
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},
      dataLeftInfo: [
        {key: 'Name', value: 'Lead', type: 'INPUT_TEXT'},
        {key: 'Email', value: 'x', type: 'INPUT_TEXT'},
        {key: 'Phone', value: 'y', type: 'INPUT_TEXT'},
        {key: 'Fax', value: '', type: 'INPUT_TEXT'},
        {key: 'Industry', value: '', type: 'INPUT_TEXT'},
        {key: 'Website', value: '', type: 'INPUT_TEXT'},
        {key: 'Annual Revenue', value: '', type: 'INPUT_TEXT'},
        {key: 'Priority', value: '', type: 'INPUT_SELECT'}
      ],
      dataRightInfo: [
        {key: 'No Email', value: '', type: 'INPUT_TEXT'},
        {key: 'No Call', value: '', type: 'INPUT_TEXT'},
        {key: 'Lead Source', value: '', type: 'INPUT_TEXT'},
        {key: 'Company Name', value: '', type: 'INPUT_TEXT'},
        {key: 'Number Of Employee', value: '', type: 'INPUT_TEXT'},
        {key: 'Lead Status', value: '', type: 'INPUT_SELECT'},
        {key: 'Skype', value: '', type: 'INPUT_TEXT'}
      ],
      description: 'Lorem ipsum dolor sit amet',
      dataLeftAddress: [
        {key: 'Country', value: '', type: 'INPUT_TEXT'},
        {key: 'City', value: '', type: 'INPUT_TEXT'},
        {key: 'Address Detail', value: '', type: 'INPUT_TEXT'}
      ]
    }
  }
}
</script>

<style scoped>

</style>