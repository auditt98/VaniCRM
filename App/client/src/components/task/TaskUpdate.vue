<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main">
      <VLoading :loading="loading"/>
      <div class="row ">
        <div class="col-sm-8">
        </div>
        <div class="col-sm-4 d-flex justify-content-end">
          <router-link :to="{name : 'TaskList'}">
            <VButton :data="btnCancel"/>
          </router-link>

          <span class="ml-5" @click="save()"><VButton :data="btnSave"/></span>
        </div>
      </div>
      <div class="mt-3">
        <div class="row mt-3">
          <div class="basic-edit">
            <h4 class="ml-2"><strong>Task Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>

                  <tr :class="{ 'form-group--error': $v.task.title.$error }">
                    <td class="required">Title</td>
                    <td><input class="form-control " type="text" v-model="task.title"></td>
                  </tr>
                  <tr>
                    <td>Start Date</td>
                    <td><input class="form-control " type="datetime-local" v-model="task.startDate"></td>
                  </tr>
                  <tr>
                    <td>Due Date</td>
                    <td><input class="form-control " type="datetime-local" v-model="task.dueDate"></td>
                  </tr>
                  <tr>
                    <td><label for="Source1">Contact</label></td>
                    <td style="width: 80%">
                      <vc-select id="Source1" label="contactName" :class="{'select-disabled': task.lead}"
                                :filterable="false" :options="contacts"
                                @search="onSearchContact"
                                v-model="task.contact"
                                >
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Lead">Lead</label></td>
                    <td style="width: 80%">
                      <vc-select id="Lead" label="name" :class="{'select-disabled': task.contact}" :filterable="false"
                                :options="leads" @search="onSearchLead"
                                v-model="task.lead">
                      </vc-select>
                    </td>
                  </tr>

                  <tr>
                    <td><label for="Priority">Priority</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Priority" v-if="priorities"
                              v-model.trim="task.priority">
                        <option v-for="(i, index) in priorities" :key="index" :value="i.id">{{ i.name }}</option>
                      </select>
                    </td>
                  </tr>
                  <tr>
                    <td>Owner</td>
                    <td style="width: 80%">
                      <vc-select id="cOwner" label="username" :filterable="false" :options="owners" @search="onSearch"
                                v-model="task.owner">
                        <!--<template slot="no-options">
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
                        </template>-->
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Status">Status</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Status" v-if="statuses"
                              v-model.trim="task.status">
                        <option v-for="(i, index) in statuses" :key="index" :value="i.id">{{ i.name }}</option>
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
                    <td><label for="Deal">Related Deal</label></td>
                    <td style="width: 80%">
                      <vc-select id="Deal" label="name" :filterable="false" :options="deals" @search="onSearchDeal"
                                v-model="task.relatedDeal">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Account">Related Account</label></td>
                    <td style="width: 80%">
                      <vc-select id="Account" label="name" :filterable="false" :options="accounts"
                                @search="onSearchAccount"
                                v-model="task.relatedAccount">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Campaign">Related Campaign</label></td>
                    <td style="width: 80%">
                      <vc-select id="Campaign" label="name" :filterable="false" :options="campaigns"
                                @search="onSearchCampaign"
                                v-model="task.relatedCampaign">
                      </vc-select>
                    </td>
                  </tr>
                  <tr>
                    <td>Remider</td>
                    <td>
                      <VSwitchButton v-model="task.isReminder"/>
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
              <textarea name="" class="form-control" v-model.trim="task.description" id="" cols="25"
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
import {taskService} from "@/service/task.service";
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
  name: "TaskUpdate",
  components: {VLoading, VSwitchButton, VButton, },
  validations: {
    task: {
      title: {
        required
      }
    }
  },
  methods: {
    loadTask() {
      this.loading = true;
      taskService.getById(this.task.id)
          .then(res => {
            if (res && res.data) {
              console.log(res.data)
              this.task = res.data;
              if (res.data.duration) {
                const times = new Date(res.data.duration * 1000).toISOString().substr(11, 8).split(':');
                this.duration.h = Number(times[0]);
                this.duration.m = Number(times[1]);
                this.duration.s = Number(times[2]);
              }
              this.task.isReminder = res.data.isRepeat;
              this.task.contact = res.data.contact ? {id: res.data.contact.id, contactName: res.data.contact.name} : null;
              this.task.priority = getValueInArr(res.data.priorities, 'selected', 'id');
              this.task.status = getValueInArr(res.data.statuses, 'selected', 'id');
              this.mapRRule(this.task.rrule);
            } else {
              alert('Không có dữ liệu');
              this.$router.push('/tasks');
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
      if (this.task.contact && this.task.lead) {
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
      this.task.rrule = r.toString();
      this.task.duration = hourToSecond(this.duration);
      if (!this.task.id) {
        taskService.create(this.mapTaskModel())
            .then(res => {
              alert(res.message);
              if (this.contactId) {
                this.$router.push('/contacts/detail?id=' + this.contactId);
              }
              else if (this.dealId) {
                this.$router.push('/deals/detail?id=' + this.dealId);
              } else {
                this.$router.push('/tasks');
              }
            }).finally(() => {
          this.loading = false;
        })
      } else {
        taskService.update(this.mapTaskModel(), this.task.id)
            .then(res => {
              alert(res.message);
              this.$router.push('/tasks/detail?id=' + this.task.id);
            }).finally(() => {
          this.loading = false;
        })
      }
    },
    loadSelectList() {
      this.loading = true;
      taskService.loadAllObject()
          .then(res => {
            if (res && res.data && res.status === 'success') {
              this.statuses = res.data.statuses;
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
    mapTaskModel() {
      return {
        title: this.task.title,
        dueDate: this.task.dueDate,
        startDate: this.task.startDate,
        contact: this.task.contact ? this.task.contact.id : null,
        lead: this.task.lead ? this.task.lead.id : null,
        priority: this.task.priority,
        owner: this.task.owner ? this.task.owner.id : null,
        status: this.task.status,
        relatedDeal: this.task.relatedDeal ? this.task.relatedDeal.id : null,
        relatedAccount: this.task.relatedAccount ? this.task.relatedAccount.id : null,
        relatedCampaign: this.task.relatedCampaign ? this.task.relatedCampaign.id : null,
        description: this.task.description,
        rrule: this.task.rrule,
        isReminder: this.task.isReminder,
      };
    },
    async getContactById() {
      await contactService.getById(this.contactId).then(res => {
        if (res && res.data) {
          this.task.contact = {
            id: res.data.id,
            contactName: res.data.name
          };
        }
      })
    },
    async getDealById() {
      await dealService.getById(this.dealId).then(res => {
        if (res && res.data) {
          this.task.relatedDeal = {
            id: res.data.id,
            name: res.data.name
          };
        }
      })
    }
  },
  created() {
    if (this.$route.path.indexOf('tasks/update') > -1) {
      if (!this.$route.query.id) {
        this.$router.push('/');
        return;
      }
      this.task.id = this.$route.query.id;
      this.loadTask();
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
      task: {
        title: null,
        dueDate: null,
        startDate: null,
        contact: null,
        lead: 0,
        priority: 0,
        owner: 0,
        status: 0,
        relatedDeal: null,
        relatedAccount: 0,
        relatedCampaign: 0,
        description: null,
        rrule: null,
        isReminder: true
      },
      duration: {
        h: 1,
        m: 2,
        s: 3
      },
      contactId: null,
      dealId: null,
      owners: [],
      statuses: [],
      contacts: [],
      leads: [],
      deals: [],
      accounts: [],
      campaigns: [],
      priorities: [],
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