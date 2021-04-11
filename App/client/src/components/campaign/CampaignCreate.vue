<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-between">
          <router-link :to="{name : 'CampaignList'}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span @click="save()"><VButton :data="btnSave"/></span>
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
                        <template slot="no-options">
                          Type for searching...
                        </template>
                        <template slot="option" slot-scope="option">
                          <div class="d-center">
                            {{ `${option.username} - ${option.firstName}` }}
                          </div>
                        </template>
                        <template slot="selected-option" slot-scope="option">
                          <div class="selected d-center">
                            {{ `${option.username} - ${option.firstName}` }}
                          </div>
                        </template>
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
            <h4 class="ml-2"><strong>Description</strong></h4>
            <div class="row ml-2">
              <textarea v-model.trim="campaign.description" name="" class="form-control" id="" cols="25" rows="5"></textarea>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

import VButton from "@/components/common/VButton";
import {userService} from "@/service/user.service";
import {campaignService} from "@/service/campaign.service";
import {getValueInArr} from "@/config/config";

export default {
  name: "CampaignCreate",
  components: {VButton, },
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
</style>