<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link :to="{name : 'DealList'}">
            <VButton :data="btnCancel"/>
          </router-link>
          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3">
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Deal Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr :class="{ 'form-group--error': $v.deal.name.$error }">
                    <td class="required">Deal Name</td>
                    <td><input class="form-control " type="text" v-model="deal.name"></td>
                  </tr>
                  <tr>
                    <td><label for="Account">Account</label></td>
                    <td style="width: 80%">
                      <vc-select id="Account" label="name"
                                :filterable="false" :options="accounts"
                                @search="onSearchAccount"
                                v-model="deal.account">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Source1">Contact</label></td>
                    <td style="width: 80%">
                      <vc-select id="Source1" label="contactName" :class="{'select-disabled': deal.lead}"
                                :filterable="false" :options="contacts"
                                @search="onSearchContact"
                                v-model="deal.contact">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Campaign">Campaign</label></td>
                    <td style="width: 80%">
                      <vc-select id="Campaign" label="name"
                                :filterable="false" :options="campaigns"
                                @search="onSearchCampaign"
                                v-model="deal.campaign">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Owner">Owner</label></td>
                    <td style="width: 80%">
                      <vc-select id="Owner" label="username"
                                :filterable="false" :options="owners"
                                @search="onSearchUser"
                                v-model="deal.owner">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td>Expected Closing Date</td>
                    <td><input class="form-control " type="date" v-model="deal.expectedClosingDate"></td>
                  </tr>
                  </tbody>
                </table>
              </div>
              <div class="col-sm-1"></div>
              <div class="col-sm-5">
                <table>
                  <tbody>
                  <tr v-if="stages">
                    <td><label for="Stage">Stage</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Stage" v-model="deal.stage">
                        <option v-for="(f, i) in stages" :key="i" :value="f.id">{{ f.name }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td>Amount</td>
                    <td>
                      <input type="number" v-model="deal.amount">
                    </td>
                  </tr>
                  <tr>
                    <td>Expected Revenue</td>
                    <td>
                      <input type="text" v-model="deal.expectedRevenue">
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Reason">Lost Reason</label></td>
                    <td>
                      <vc-select id="Reason" label="reason" ref="reasonSelect"
                                :filterable="false" :options="reasons" taggable
                                @search="onSearchReasons"
                                v-model="deal.lostReason">
                      </vc-select>
                    </td>
                    <!-- <td>
                      <select class="form-control py-0 w-100" id="Reason" v-model="deal.lostReason">
                        <option v-for="(f, i) in reasons" :key="i" :value="f.id">{{ f.name }}</option>
                      </select>
                    </td> -->
                  </tr>
                  <tr v-if="priorities">
                    <td><label for="Priority">Priority</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Priority" v-model="deal.priority">
                        <option v-for="(f, i) in priorities" :key="i" :value="f.id">{{ f.name }}</option>
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
              <textarea name="" class="form-control" v-model.trim="deal.description" id="" cols="25"
                        rows="5"></textarea>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

import VButton from "@/components/common/VButton";
import {dealService} from "@/service/deal.service";
import {accountService} from "@/service/account.service";
import {campaignService} from "@/service/campaign.service";
import {contactService} from "@/service/contact.service";
import {userService} from "@/service/user.service";
import {formatDate, getValueInArr} from "@/config/config";
import VLoading from "@/components/common/VLoading";
import {required} from "vuelidate/lib/validators";

export default {
  name: "DealCreateEdit",
  components: {VLoading, VButton, },
  validations: {
    deal: {
      name: {
        required
      }
    }
  },
  methods: {
    loadDeal() {
      this.loading = true;
      dealService.getById(this.deal.id)
          .then(res => {
            if (res && res.data) {
              this.deal = res.data;
              // console.log(res.data)
              this.deal.expectedClosingDate = formatDate(this.deal.expectedClosingDate, 'yyyy-MM-dd') ;
              this.deal.contact = res.data.contact ? {id: res.data.contact.id, contactName: res.data.contact.name} : null;
              this.deal.priority = getValueInArr(res.data.priorities, 'selected', 'id');
              this.deal.stage = getValueInArr(res.data.stages, 'selected', 'id');
              this.deal.lostReason = res.data.lostReason ? {id: res.data.lostReason.id, reason: res.data.lostReason.reason} : null;
              // this.deal.lostReason = getValueInArr(res.data.lostReason, 'selected', 'id');
              // this.mapRRule(this.deal.rrule);
            } else {
              alert('No data found');
              this.$router.push('/');
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    save() {
      this.$v.$touch()
      if (this.$v.$invalid) {
        alert('Failed')
        return;
      }
      this.loading = true;
      if (!this.deal.id) {
        dealService.create(this.mapTaskModel())
            .then(res => {
              alert(res.message);
              this.$router.push('/deals');
            }).finally(() => {
          this.loading = false;
        })
      } else {
        dealService.update(this.mapTaskModel(), this.deal.id)
            .then(res => {
              
              alert(res.message);
              this.$router.push('/deals/detail?id=' + this.deal.id);
            }).finally(() => {
          this.loading = false;
        })
      }
    },
    mapTaskModel() {
      // console.log("Lost reason: ",this.deal.lostReason ? (typeof this.deal.lostReason.reason !== 'undefined' ? this.deal.lostReason.reason : this.deal.lostReason ? this.deal.lostReason : null) : null)
      return {
        name: this.deal.name,
        expectedClosingDate: this.deal.expectedClosingDate,
        closingDate: this.deal.closingDate,
        account: this.deal.account ? this.deal.account.id : null,
        contact: this.deal.contact ? this.deal.contact.id : null,
        campaign: this.deal.campaign ? this.deal.campaign.id : null,
        owner: this.deal.owner ? this.deal.owner.id : null,
        priority: this.deal.priority,
        stage: this.deal.stage,
        amount: this.deal.amount,
        expectedRevenue: this.deal.expectedRevenue,
        lostReason: this.deal.lostReason ? (typeof this.deal.lostReason.reason !== 'undefined' ? this.deal.lostReason.reason : this.deal.lostReason ? this.deal.lostReason : null) : null,
        description: this.deal.description
      };
    },
    loadSelectList() {
      this.loading = true;
      dealService.loadAllObject()
          .then(res => {
            if (res && res.data && res.status === 'success') {
              // this.reasons = res.data.lostReasons;
              this.stages = res.data.stages;
              this.priorities = res.data.priorities;
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    onSearchContact(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search && search.trim()) {
        query['query'] = search;
      }
      contactService.getAll(query).then(res => {
        if (res && res.data) {
          this.contacts = res.data.contacts;
        }
      })
    },
    onSearchUser(search) {
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
    },
    onSearchAccount(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search && search.trim()) {
        query['query'] = search;
      }
      accountService.getAll(query).then(res => {
        if (res && res.data) {
          this.accounts = res.data.accounts;
        }
      })
    },
    onSearchCampaign(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search && search.trim()) {
        query['query'] = search;
      }
      campaignService.getAll(query).then(res => {
        if (res && res.data) {
          this.campaigns = res.data.campaigns;
        }
      })
    },

    onSearchReasons(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search && search.trim()) {
        query['query'] = search;
      }
      dealService.getAllReasons(query).then(res => {
        if (res && res.data) {
          this.reasons = res.data.reasons;
        }
      })
    }
  },
  created() {
    if (this.$route.path.indexOf('deals/update') > -1) {
      if (!this.$route.query.id) {
        this.$router.push('/');
        return;
      }
      this.deal.id = this.$route.query.id;
      this.loadDeal();
    }
    this.loadSelectList();
    this.onSearchCampaign(null);
    this.onSearchAccount(null);
    this.onSearchContact(null);
    this.onSearchReasons(null);
  },
  data: function () {
    return {
      deal: {
        name: null,
        closingDate: null,
        expectedClosingDate: null,
        account: null,
        contact: null,
        campaign: null,
        priority: 1,
        stage: 1,
        amount: 1,
        expectedRevenue: 0,
        lostReason: null,
        description: null,
        owner: null,
      },
      owners: [],
      contacts: [],
      accounts: [],
      campaigns: [],
      stages: [],
      reasons: [],
      priorities: [],
      loading: false,
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},

    }
  }
}
</script>

<style scoped>
/deep/ .vs__search {
  width: unset;
  border: none;
}

/deep/ .vs--open .vs--single .vs__selected {
  position: unset;
}

/deep/ span.vs__selected {
  padding-left: 0.7rem;
  margin: 0;
}

/deep/ .vs__dropdown-toggle {
  height: 36px;
}

input {
  padding: .375rem .75rem !important;
}

.select-disabled {
  pointer-events: none;
  color: #bfcbd9;
  cursor: not-allowed;
  background-image: none;
  background-color: #eef1f6;
  border-color: #d1dbe5;
}
</style>