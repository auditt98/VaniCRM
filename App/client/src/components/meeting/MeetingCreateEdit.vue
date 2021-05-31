<template>
  <div class="">

    <div class="px-5 pt-3 m-0 background-main" >
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
            <h4 class="ml-2"><strong>Meeting Information</strong></h4>
            <div class="row">
              <div class="col-sm-5">
                <table class="ml-4">
                  <tbody>
                  <tr :class="{ 'form-group--error': $v.meeting.host.$error }">
                    <td class="required">Host</td>
                    <td style="width: 80%">
                      <vc-select id="cOwner" label="username" :filterable="false" :options="users" @search="onSearch"
                                v-model="meeting.host">
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
                  <tr :class="{ 'form-group--error': $v.meeting.title.$error }">
                    <td class="required">Title</td>
                    <td><input class="form-control " type="text" v-model="meeting.title"></td>
                  </tr>
                  <tr>
                    <td>Location</td>
                    <td><input class="form-control " type="text" v-model="meeting.location"></td>
                  </tr>
                  <tr>
                    <td>From</td>
                    <td>
                      <input class="form-control " type="datetime-local" v-model="meeting.from">
                    </td>
                  </tr>
                  <tr>
                    <td>To</td>
                    <td><input class="form-control " type="datetime-local" v-model="meeting.to">
                    </td>
                  </tr>
                  <tr>
                    <td>All day</td>
                    <td>
                      <VSwitchButton v-model="meeting.isAllDay"/>
                    </td>
                  </tr>
                  <tr v-if="meeting.id">
                    <td>Participants</td>
                    <td>
                      <span class="cursor-pointer" @click="openModal">
                        <i class="fa fa-plus"></i> Add
<!--                        <strong class="text-danger" v-if="meeting.participants.users">{{meeting.participants.users.length}} users</strong>
                        <strong class="text-danger" v-if="meeting.participants.contacts">{{meeting.participants.contacts.length}} contacts</strong>
                        <strong class="text-danger" v-if="meeting.participants.leads">{{meeting.participants.leads.length}} leads</strong>-->

                      </span>
                    </td>
                  </tr>
                  <tr>
                    <td><label for="Priority">Priority</label></td>
                    <td>
                      <select class="form-control py-0 w-100" id="Priority" v-if="priorities"
                              v-model.trim="meeting.priority">
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
                    <td>Remider</td>
                    <td>
                      <VSwitchButton v-model="meeting.isRepeat"/>
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
              <textarea name="" class="form-control" v-model.trim="meeting.description" id="" cols="25"
                        rows="5"></textarea>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-if="meeting.id">
      <AddParticipantModal
          ref="modal"
          :meeting-id="Number(meeting.id)"
          v-show="isOpenModal"
          @close="closeModal"/>
    </div>
  </div>
</template>

<script>

import VButton from "@/components/common/VButton";
import VSwitchButton from "../common/VSwitchButton";
import {meetingService} from "@/service/meeting.service";
import {RRule} from 'rrule'
import {formatDate, getValueInArr, parseDate} from "@/config/config";
import AddParticipantModal from "@/components/common/modal/AddParticipantModal";
import {required} from "vuelidate/lib/validators";
import VLoading from "@/components/common/VLoading";
import {userService} from "@/service/user.service";

export default {
  name: "MeetingCreateEdit",
  components: {VLoading, AddParticipantModal, VSwitchButton, VButton, },
  validations: {
    meeting: {
      host: {
        required
      },
      title: {
        required
      }
    }
  },
  methods: {
    save() {
      this.$v.$touch()
      if (this.$v.$invalid) {
        alert('Failed')
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
      this.meeting.rrule = r.toString();
      this.meeting.host = this.meeting.host ? this.meeting.host.id : null;
      if (this.meeting.id) {
        meetingService.update(this.meeting, this.meeting.id)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/tasks')
              }
            })
            .catch(err => alert(err))
            .finally(() => {
              this.loading = false;
            })
      } else {
        meetingService.create(this.meeting)
            .then(res => {
              if (res) {
                alert(res.message);
                this.$router.push('/tasks')
              }
            })
            .catch(err => alert(err))
            .finally(() => {
              this.loading = false;
            })
      }
    },
    loadMeeting() {
      this.loading = true;
      meetingService.getById(this.meeting.id)
          .then(res => {
            if (res && res.data) {
              this.meeting = res.data;
              this.meeting.isRepeat = res.data.isReminder;
              this.meeting.from = formatDate(res.data.fromDate, 'yyyy-MM-dd') + 'T' + formatDate(res.data.fromDate, 'HH:mm');
              this.meeting.to = formatDate(res.data.toDate, 'yyyy-MM-dd') + 'T' + formatDate(res.data.toDate, 'HH:mm');
              this.meeting.priority = getValueInArr(res.data.priorities, 'selected', 'id');
              this.mapRRule(this.meeting.rrule);
            } else {
              alert('No data found');
              this.$router.push('/');
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    loadSelectList() {
      this.loading = true;
      meetingService.loadAllObject()
          .then(res => {
            if (res && res.data && res.status === 'success') {
              this.priorities = res.data.priorities;
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    closeModal() {
      this.isOpenModal = false;
    },
    async openModal() {
      await this.$refs['modal'].loadMeeting();
      this.$refs['modal'].loadItems();
      this.isOpenModal = true;
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
          this.users = res.data.users;
        }
      })
    }
  },
  created() {
    if (this.$route.path.indexOf('meetings/update') > -1) {
      if (!this.$route.query.id) {
        this.$router.push('/');
        return;
      }
      this.meeting.id = this.$route.query.id;
      this.loadMeeting();
    }

    this.loadSelectList();
    this.onSearch(null);
  },
  data: function () {
    return {
      meeting: {
        host: null,
        title: null,
        location: null,
        from: new Date().toISOString().substr(0, 16),
        to: new Date().toISOString().substr(0, 16),
        isAllDay: null,
        isRepeat: null,
        rrule: null,
        priority: 1,
        description: null
      },
      rruleProp: {
        repeatType: 2,
        rruleType: 1,
        times: 1,
        util: '2021-04-06',
      },
      menu: false,
      loading: false,
      priorities: [],
      users: [],
      frequencies: RRule.FREQUENCIES,
      isOpenModal: false,
      btnCancel: {btnClass: 'btn-white px-3', icon: 'fa-times', text: 'Cancel'},
      btnSave: {btnClass: 'btn-red px-4', icon: 'fa-floppy-o', text: 'Save'},
    }
  }
}
</script>

<style scoped>
/*@import '~vuetify/dist/vuetify.min.css';
@import '~material-design-icons-iconfont/dist/material-design-icons.css';*/
/deep/.vs__search {
  width: unset;
  border: none;
  margin-top: 0;
}
/deep/ .vs--open .vs--single .vs__selected {
  position: unset;
  margin-top: 0;
}
/deep/ .vs__dropdown-toggle {
  height: 36px;
}
.background-main {
  position: relative;
  min-height: 900px;
}
</style>