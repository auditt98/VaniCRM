<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link :to="{name : '/'}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3">
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Call Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr>
                    <td>Call Owner</td>
                    <td style="width: 80%">
                      <vc-select id="cOwner" label="username" :filterable="false" :options="owners" @search="onSearch"
                                v-model="call.owner">
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
                  <tr :class="{ 'form-group--error': $v.call.title.$error }">
                    <td class="required">Title</td>
                    <td><input class="form-control " type="text" v-model="call.title"></td>
                  </tr>
                  <tr>
                    <td><label for="Source1">Contact</label></td>
                    <td style="width: 80%">
                      <vc-select id="Source1" label="contactName" :class="{'select-disabled': call.lead}"
                                :filterable="false" :options="contacts"
                                @search="onSearchContact"
                                v-model="call.contact">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Lead">Lead</label></td>
                    <td style="width: 80%">
                      <vc-select id="Lead" label="name" :class="{'select-disabled': call.contact}" :filterable="false"
                                :options="leads" @search="onSearchLead"
                                v-model="call.lead"
                      >
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Purpose">Call Purpose</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Purpose" v-if="purposes" v-model="call.purpose">
                        <option v-for="(p, i) in purposes" :key="i" :value="p.id">{{ p.name }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Result">Call Result</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Result" v-if="results" v-model="call.result">
                        <option v-for="(p, i) in results" :key="i" :value="p.id">{{ p.name }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td class="w-25">Call Duration</td>
                    <td class="w-75">
                      <p class="row">
                        <span class="col"><input v-model="duration.h" class="w-50" type="number"><span
                            class="ml-2 w-25">h</span></span>
                        <span class="col"><input v-model="duration.m" class="w-50" type="number"><span
                            class="ml-2 w-25">m</span></span>
                        <span class="col"><input v-model="duration.s" class="w-50" type="number"><span
                            class="ml-2 w-25">s</span></span>
                      </p>
                    </td>
                  </tr>
                  <tr>
                    <td>Start Time</td>
                    <td>
                      <input type="datetime-local" v-model="call.startTime">
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Priority">Priority</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Priority" v-if="priorities"
                              v-model.trim="call.priority">
                        <option v-for="(i, index) in priorities" :key="index" :value="i.id">{{ i.name }}</option>
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
                    <td><label for="Status1">Status</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Status1" v-if="statuses" v-model="call.status">
                        <option v-for="(p, i) in statuses" :key="i" :value="p.id">{{ p.name }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Type1">Call Type</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Type1" v-if="types" v-model="call.type">
                        <option v-for="(p, i) in types" :key="i" :value="p.id">{{ p.name }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Deal">Related Deal</label></td>
                    <td style="width: 80%">
                      <vc-select id="Deal" label="name" :filterable="false" :options="deals" @search="onSearchDeal"
                                v-model="call.relatedDeal">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Account">Related Account</label></td>
                    <td style="width: 80%">
                      <vc-select id="Account" label="name" :filterable="false" :options="accounts"
                                @search="onSearchAccount"
                                v-model="call.relatedAccount">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Campaign">Related Campaign</label></td>
                    <td style="width: 80%">
                      <vc-select id="Campaign" label="name" :filterable="false" :options="campaigns"
                                @search="onSearchCampaign"
                                v-model="call.relatedCampaign">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td>Remider</td>
                    <td>
                      <VSwitchButton v-model="call.isReminder"/>
                    </td>
                  </tr>
                  <tr v-if="frequencies">
                    <td><label for="Source">Repeat Type</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Source" v-model="rruleProp.repeatType">
                        <option v-for="(f, i) in frequencies" :key="i" :value="i">{{ f }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td>End</td>
                    <td>
                      <p class="row">
                                                <span class="col-2">
                                                    <label class="checkbox-custom">Never
                                                      <input type="radio" :value="1" name="rrule"
                                                             v-model="rruleProp.rruleType">
                                                      <span class="checkmark"></span>
                                                    </label>
                                                </span>
                      </p>
                      <p class="row">
                                                <span class="col-2">
                                                    <label class="checkbox-custom">After
                                                      <input type="radio" :value="2" name="rrule"
                                                             v-model="rruleProp.rruleType">
                                                      <span class="checkmark"></span>
                                                    </label>
                                                </span>
                        <span class="col-3">
                                                    <input class="col" type="number" v-model="rruleProp.times">
                                                </span>
                        <span class="col-3">Times</span>
                      </p>
                      <p class="row">
                                                <span class="col-2">
                                                    <label class="checkbox-custom">On
                                                      <input type="radio" :value="3" name="rrule"
                                                             v-model="rruleProp.rruleType" checked>
                                                      <span class="checkmark"></span>
                                                    </label>
                                                </span>
                        <span class="col-8">
                                                    <input type="date" v-model="rruleProp.util">
                                                </span>
                      </p>
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
              <textarea name="" class="form-control" v-model.trim="call.description" id="" cols="25"
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
import VSwitchButton from "../common/VSwitchButton";
import {callService} from "@/service/call.service";
import {leadService} from "@/service/lead.service";
import {dealService} from "@/service/deal.service";
import {accountService} from "@/service/account.service";
import {campaignService} from "@/service/campaign.service";
import {contactService} from "@/service/contact.service";
import {formatDate, getValueInArr, hourToSecond, parseDate} from "@/config/config";
import VLoading from "@/components/common/VLoading";
import {userService} from "@/service/user.service";
import {RRule} from "rrule";
import {required} from "vuelidate/lib/validators";

export default {
  name: "CallCreateEdit",
  components: {VLoading, VSwitchButton, VButton, },
  validations: {
    call: {
      title: {
        required
      }
    }
  },
  methods: {
    loadCall() {
      this.loading = true;
      callService.getById(this.call.id)
          .then(res => {
            if (res && res.data) {
              this.call = res.data;
              if (res.data.duration) {
                const times = new Date(res.data.duration * 1000).toISOString().substr(11, 8).split(':');
                this.duration.h = Number(times[0]);
                this.duration.m = Number(times[1]);
                this.duration.s = Number(times[2]);
              }
              this.call.isReminder = res.data.isRepeat;
              this.call.contact = res.data.contact ? {id: res.data.contact.id, contactName: res.data.contact.name} : null;
              this.call.priority = getValueInArr(res.data.priorities, 'selected', 'id');
              this.call.result = getValueInArr(res.data.results, 'selected', 'id');
              this.call.status = getValueInArr(res.data.statuses, 'selected', 'id');
              this.call.type = getValueInArr(res.data.types, 'selected', 'id');
              this.call.purpose = getValueInArr(res.data.purposes, 'selected', 'id');
              this.mapRRule(this.call.rrule);
            } else {
              alert('Không có dữ liệu');
              this.$router.push('/');
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    save() {
      this.$v.$touch()
      if (this.$v.$invalid) {
        alert('loi')
        return;
      }
      if (this.call.contact && this.call.lead) {
        alert('Loi');
        return;
      }
      this.loading = true;
      let rrule = {
        freq: Number(this.rruleProp.repeatType),
        interval: 1,
      };
      if (this.rruleProp.rruleType === 2) {
        rrule['count'] = this.rruleProp.times;
      }
      if (this.rruleProp.rruleType === 3) {
        rrule['until'] = parseDate(this.rruleProp.util);
      }
      const r = new RRule(rrule);
      this.call.rrule = r.toString();
      this.call.duration = hourToSecond(this.duration);
      if (!this.call.id) {
        callService.create(this.mapModel())
            .then(res => {
              alert(res.message);
              if (this.contactId) {
                this.$router.push('/contact-detail?id=' + this.contactId);
              }
              if (this.dealId) {
                this.$router.push('/deal-detail?id=' + this.dealId);
              }
            }).finally(() => {
          this.loading = false;
        })
      } else {
        callService.update(this.mapModel(), this.call.id)
            .then(res => {
              alert(res.message);
              // this.$router.push('/call-detail?i')
            }).finally(() => {
          this.loading = false;
        })
      }
    },
    mapModel() {
      return {
        owner: this.call.owner ? this.call.owner.id : null,
        contact: this.call.contact ? this.call.contact.id : null,
        lead: this.call.lead ? this.call.lead.id : null,
        purpose: this.call.purpose,
        status: this.call.status,
        result: this.call.result,
        type: this.call.type,
        priority: this.call.priority,
        relatedDeal: this.call.relatedDeal ? this.call.relatedDeal.id : null,
        relatedAccount: this.call.relatedAccount ? this.call.relatedAccount.id : null,
        relatedCampaign: this.call.relatedCampaign ? this.call.relatedCampaign.id : null,
        title: this.call.title,
        duration: this.call.duration,
        startTime: this.call.startTime,
        isReminder: this.call.isReminder,
        rrule: this.call.rrule,
        description: this.call.description
      };
    },
    loadSelectList() {
      this.loading = true;
      callService.loadAllObject()
          .then(res => {
            if (res && res.data && res.status === 'success') {
              this.purposes = res.data.purposes;
              this.types = res.data.types;
              this.statuses = res.data.statuses;
              this.results = res.data.results;
              this.priorities = res.data.priorities;
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
    onSearchLead(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search && search.trim()) {
        query['query'] = search;
      }
      leadService.getAll(query).then(res => {
        if (res && res.data) {
          this.leads = res.data.leads;
        }
      })
    },
    onSearchDeal(search) {
      let query = {
        currentPage: 1,
        pageSize: 10
      };
      if (search && search.trim()) {
        query['query'] = search;
      }
      dealService.getAll(query).then(res => {
        if (res && res.data) {
          this.deals = res.data.deals;
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
    mapRRule(rrule) {
      rrule = RRule.fromString(rrule);
      this.rruleProp.repeatType = rrule.origOptions.freq;
      if (rrule.origOptions.until) {
        this.rruleProp.rruleType = 3;
        this.rruleProp.util = formatDate(new Date(rrule.origOptions.until).toISOString(), 'yyyy-MM-dd');
      } else if (rrule.origOptions.count) {
        this.rruleProp.rruleType = 2;
        this.rruleProp.times = rrule.origOptions.count;
      } else {
        this.rruleProp.rruleType = 1;
      }
    },
    async getContactById() {
      await contactService.getById(this.contactId).then(res => {
        if (res && res.data) {
          this.call.contact = {
            id: res.data.id,
            contactName: res.data.name
          };
        }
      })
    },
    async getDealById() {
      await dealService.getById(this.dealId).then(res => {
        if (res && res.data) {
          this.call.relatedDeal = {
            id: res.data.id,
            name: res.data.name
          };
        }
      })
    }
  },
  created() {
    if (this.$route.path.indexOf('call-update') > -1) {
      if (!this.$route.query.id) {
        this.$router.push('/');
        return;
      }
      this.call.id = this.$route.query.id;
      this.loadCall();
    } else {
      if (this.$route.query.contactId) {
        this.contactId = Number(this.$route.query.contactId);
        this.getContactById();
      }
      if (this.$route.query.dealId) {
        this.dealId = Number(this.$route.query.dealId);
        this.getDealById();
      }
    }
    this.loadSelectList();
    this.onSearchLead(null);
    this.onSearchCampaign(null);
    this.onSearchDeal(null);
    this.onSearchAccount(null);
    this.onSearchContact(null);
    this.onSearch(null);
  },
  data: function () {
    return {
      call: {
        owner: null,
        contact: null,
        lead: null,
        purpose: 0,
        status: 0,
        result: 0,
        priority: 0,
        type: 0,
        relatedDeal: null,
        relatedAccount: null,
        relatedCampaign: null,
        title: "string",
        duration: 0,
        startTime: null,
        isReminder: true,
        rrule: null,
        description: "string"
      },
      duration: {
        h: 1,
        m: 2,
        s: 3
      },
      owners: [],
      purposes: [],
      types: [],
      statuses: [],
      results: [],
      contacts: [],
      leads: [],
      deals: [],
      accounts: [],
      campaigns: [],
      priorities: [],
      contactId: null,
      dealId: null,
      rruleProp: {
        repeatType: 2,
        rruleType: 1,
        times: 1,
        util: '2021-04-06',
      },
      frequencies: RRule.FREQUENCIES,
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