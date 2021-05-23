<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link :to="{name : 'CampaignList'}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3">
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Campaigns Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr>
                    <td><label for="cOwner">Campaigns Owner</label></td>
                    <td style="width: 80%">
                      <vc-select id="cOwner" label="username" :filterable="false" :options="owners" @search="onSearch"
                                v-model="campaign.owner">

                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td>Campaigns Name</td>
                    <td><input class="form-control " type="text" v-model="campaign.campaignName"></td>
                  </tr>
                  <tr>
                    <td>Start Date</td>
                    <td><input class="form-control " type="date" v-model="campaign.startDate"></td>
                  </tr>
                  <tr>
                    <td>End Date</td>
                    <td><input class="form-control " type="date" v-model="campaign.endDate"></td>
                  </tr>
                  <tr>
                    <td>Actual Cost</td>
                    <td><input class="form-control " type="text" v-model="campaign.actualCost"></td>
                  </tr>
                  <tr>
                    <td>Budgeted Cost</td>
                    <td><input class="form-control " type="text" v-model="campaign.budgetedCost"></td>
                  </tr>

                  </tbody>
                </table>
              </div>
              <div class="col-sm-1"></div>
              <div class="col-sm-5">
                <table>
                  <tbody>
                  <tr>
                    <td>Expected Response</td>
                    <td><input class="form-control" type="text" v-model="campaign.expectedResponse"></td>
                  </tr>
                  <tr>
                    <td>Expected Revenue</td>
                    <td><input class="form-control " type="text" v-model="campaign.expectedRevenue"></td>
                  </tr>
                  <tr>
                    <td>Number Sent</td>
                    <td><input class="form-control " type="text" v-model="campaign.numberSent"></td>
                  </tr>
                  <tr>
                    <td><label for="type">Type</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="type" v-if="types" v-model.trim="campaign.type">
                        <option v-for="(i, index) in types" :key="index" :value="i.id">{{i.name}}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Status">Status</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Status" v-if="statuses" v-model.trim="campaign.status">
                        <option v-for="(i, index) in statuses" :key="index" :value="i.id">{{i.name}}</option>
                      </select>
                    </td>
                  </tr>
                  
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Email Content</strong></h4>
            <div class="row email-row-border">
              <div class="col-sm-1 text-right email-row" style="border: 0px;">From:</div>
                <input class="col-sm-11 email-row-disabled"  value="vanicrm.noreply@gmail.com" disabled>
            </div>
            <div class="row email-row-border">
              <div class="col-sm-1 text-right email-row" style="border: 0px;">Subject:</div>
              <input class="col-sm-11 email-row" v-model.trim="campaign.emailTitle" >
            </div>
            <div class="row mt-4" style="">
              <vue-editor 
                :editorOptions="editorSettings" 
                :editor-toolbar="customToolbar" 
                :placeholder="'Email body'"
                v-model.trim="campaign.description" name="" class="" 
                style="width: 100%; height: 100%; border-top: 0px;" id="" cols="25" rows="5"
                
              ></vue-editor>
              <!-- <textarea v-model.trim="campaign.description" name="" class="form-control" id="" cols="25" rows="5"></textarea> -->
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { VueEditor, Quill} from "vue2-editor";
import VButton from "@/components/common/VButton";
import {userService} from "@/service/user.service";
import {campaignService} from "@/service/campaign.service";
import {getValueInArr} from "@/config/config";

var AlignStyle = Quill.import('attributors/style/align');

var FontStyle = Quill.import("attributors/style/font");
var fontList = ["Arial", "Roboto", "times-new-roman", "Verdana", "Helvetica", "Tahoma", "trebuchet-ms", "sans-serif", "monospace", "serif"];
FontStyle.whitelist = fontList;
Quill.register(FontStyle, true);
Quill.register(AlignStyle, true);

var toolbarOptions = [ 
          [{'font': fontList }],
          [{ 'header': 1 }, { 'header': 2 }],               // custom button values
          [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
          ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
          [{ 'list': 'ordered'}, { 'list': 'bullet' }],
          [{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript       // outdent/indent                    // text direction
          [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
          [{ 'align': [] }],
          ['clean']     
      ]

export default {
  name: "CampaignCreate",
  components: {VButton, 'vue-editor': VueEditor,},
  methods: {
    save() {
      this.campaign.owner = this.campaign.owner ? this.campaign.owner.id : null;
      if (!this.campaign.id) {
        campaignService.create(this.campaign)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/campaigns')
              }
            })
      } else {
        campaignService.update(this.campaign, this.campaign.id)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/campaigns')
              }
            })
      }
    },
    loadCampaign() {
      campaignService.getById(this.campaign.id)
          .then(res => {
            if (res && res.data) {
              this.campaign = res.data;
              this.campaign.status = getValueInArr(res.data.statuses, 'selected', 'id');
              this.campaign.type = getValueInArr(res.data.types, 'selected', 'id');
            }
          })
    },
    loadSelectList() {
      this.loading = true;
      campaignService.loadAllObject()
          .then(res => {
            if (res && res.data &&res.status === 'success') {
              this.statuses = res.data.statuses;
              this.types = res.data.types;
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    onSearch(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search) {
        query['query'] = search;
      }
      userService.getAll(query).then(res => {
        if (res) {
          this.owners = res.data.users;
        }
      })
    }
  },
  created() {

    if (this.$route.query.id) {
      this.campaign.id = this.$route.query.id;
      this.loadCampaign();
    }
    this.loadSelectList();
    this.onSearch(null);
  },
  data: function () {
    return {
      campaign: {},
      loading: false,
      statuses: [],
      types: [],
      owners: [],
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},
      customToolbar: toolbarOptions,
      editorSettings: {
        formats: {
          Font: true,
          Align: true,
        }
      },
    }
  }
}
</script>

<style scoped>
/deep/.vs__search {
  width: unset;
  border: none;
}
/deep/ .vs--open .vs--single .vs__selected {
  position: unset;
}

.email-row-disabled{
  margin-top: 10px; 
  margin-bottom: 10px; 
  background-color: white !important;
  color: rgba(0,0,0, 0.5);
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
}

textarea:focus, input:focus{
    outline: none;
}
</style>