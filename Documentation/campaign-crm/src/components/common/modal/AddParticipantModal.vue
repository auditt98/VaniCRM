<template>
  <transition name="modal-fade">
    <div class="modal-backdrop" role="dialog">
      <div class="modal" ref="modal">
        <VLoading :loading="loading"/>
        <header class="modal-header mt-4">
          <slot name="header">
            <div class="row">
              <div class="col-sm-2">
                <h3 style="color: #192A3E"><strong>Participants</strong></h3>
              </div>
              <div class="row col-sm-10 text-center">
                <div class="col-sm-1"></div>
                <div class="form-group col-sm-6 input-group mb-3 pl-0">
                  <div class="btn-search">
                    <input type="text" class="form-control" placeholder="Search" v-model.trim="keyword">
                    <i class="fa fa-search"></i>
                  </div>
                </div>
                <div class="col-sm-5">
                  <button class="btn btn-light btn-purple mr-3" @click="search">
                    <i class="fa fa-search mr-2"></i>
                    Search
                  </button>
                  <button class="btn btn-light btn-purple" :class="{'btn-disable' : !keyword}" @click="clear">
                    <img width="22" src="../../../assets/clear_all.png" alt="" class="mr-1">
                    Clear
                  </button>
                </div>
                <div class="col-sm-3 text-right">
                  <slot name="button"></slot>
                </div>
              </div>
            </div>
            <div class="row">
                <div class="col-sm-2"></div>
                <div class="col-sm-6 ml-2 d-inline-flex justify-content-sm-around">
                  <div class="form-check">
                    <input class="form-check-input" v-model="type" :value="1" type="radio" name="flexRadioDefault" id="flexRadioDefault1">
                    <label class="form-check-label" for="flexRadioDefault1">
                      Users
                    </label>
                  </div>
                  <div class="form-check">
                    <input class="form-check-input" v-model="type"  :value="2" type="radio" name="flexRadioDefault" id="flexRadioDefault2">
                    <label class="form-check-label" for="flexRadioDefault2">
                      Leads
                    </label>
                  </div>
                  <div class="form-check">
                    <input class="form-check-input" type="radio" v-model="type" :value="3" name="flexRadioDefault" id="flexRadioDefault3" checked>
                    <label class="form-check-label" for="flexRadioDefault3">
                      Contacts
                    </label>
                  </div>
                </div>
            </div>
          </slot>
        </header>
        <section class="modal-body text-center">
          <slot name="body">
            <TableInDetail :header-columns="columns" :page-config="{page: currentPage, totalItems: totalItems, totalPage: totalPage}"
            @go-to-page="goToPage">
              <template slot="body">
                <tbody v-if="items && items.length > 0">
                <tr v-for="(t, i) in items" :key="i">
                  <td>{{t.name}}</td>
                  <td>{{t.email}}</td>
                  <td>
                    <span v-if="t.isAdd"><i class="fa fa-check text-success"></i></span>
                    <span v-else @click="addItem(t)"><i class="fa fa-plus text-danger"></i></span>
                  </td>
                </tr>
                </tbody>
                <tbody v-else>
                <tr><td colspan="3" class="text-center">Không có dữ liệu</td></tr>
                </tbody>
              </template>
            </TableInDetail>
          </slot>
        </section>

        <footer class="modal-footer text-center">
          <slot name="footer">
            <span @click="close"><VButton :data="btnCancel"/></span>
          </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script>
import VButton from "@/components/common/VButton";
import TableInDetail from "@/components/common/table/TableInDetail";
import {userService} from "@/service/user.service";
import VLoading from "@/components/common/VLoading";
import {leadService} from "@/service/lead.service";
import {contactService} from "@/service/contact.service";
import {meetingService} from "@/service/meeting.service";
export default {
  name: "AddParticipantModal",
  components: {VLoading, TableInDetail, VButton},
  props: {
    meetingId: Number
  },
  data () {
    return {
      meeting: null,
      items: [],
      type: 1,
      loading: false,
      currentPage: 1,
      pageSize: 5,
      totalItems: 0,
      totalPage: 0,
      keyword: '',
      columns: [
          'Name', 'Email', 'Action'
      ],
      btnCancel: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-times', text: 'Close'},
    }
  },
  watch: {
    type: function () {
      this.currentPage = 1;
      this.loadItems();
    }
  },
  methods: {
    close() {
      this.$emit('close');
    },
    goToPage(page) {
      this.currentPage = page;
      this.loadItems();
    },
    loadMeeting() {
      this.loading = true;
      meetingService.getById(this.meetingId)
          .then(res => {
            if (res && res.data) {
              this.meeting = res.data;
            } else {
              alert('Không có dữ liệu');
              this.$router.push('/');
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    addItem(item) {
      if (!item.id) {
        return ;
      }
      if (!confirm('Xác nhân')) {
        return ;
      }
      const body = {
        id: this.meetingId,
        leadId: this.type === 2 ? item.id : 0,
        userId: this.type === 1 ? item.id : 0,
        contactId: this.type === 3 ? item.id : 0,
      };
      meetingService.addParticipant(body)
      .then(res => {
        if (res) {
          item.isAdd = true;
          alert(res.message);
          this.loadMeeting();
        }
      })
    },
    loadItems() {
      this.loading = true;
      this.items = [];
      if (this.type === 1) {//user
        let query = {
          currentPage: this.currentPage,
          pageSize: this.pageSize
        };
        if (this.keyword) {
          query['query'] = this.keyword;
        }
        userService.getAll(query).then(res => {
          if (res) {
            this.items = res.data.users.map(u => {
              if (this.meeting.participants && this.meeting.participants.users) {
                if (this.meeting.participants.users.filter(c => {
                  return c.id === u.id;
                }).length > 0) {
                  u.isAdd = true;
                } else {
                  u.isAdd = false;
                }
              }
              u.name = `${u.firstName} ${u.lastName}`;
              return u;
            })
            this.totalPage = Number(res.data.pageInfo.TotalPages);
            this.totalItems = Number(res.data.pageInfo.TotalItems);
          }
        }).finally(() => {
          this.loading = false;
        })
      } else if (this.type === 2) { //lead
        let query = {
          currentPage: this.currentPage,
          pageSize: this.pageSize
        };
        if (this.keyword) {
          query['query'] = this.keyword;
        }
        leadService.getAll(query).then(res => {
          if (res) {
            this.items = res.data.leads.map(u => {
              if (this.meeting.participants && this.meeting.participants.leads) {
                if (this.meeting.participants.leads.filter(lead => {
                  return lead.id === u.id;
                }).length > 0) {
                  u.isAdd = true;
                } else {
                  u.isAdd = false;
                }
              }
              return u;
            })
            this.totalPage = Number(res.data.pageInfo.TotalPages);
            this.totalItems = Number(res.data.pageInfo.TotalItems);
          }
        }).finally(() => {
          this.loading = false;
        })
      } else if (this.type === 3) { //contact
        let query = {
          currentPage: this.currentPage,
          pageSize: this.pageSize
        };
        if (this.keyword) {
          query['query'] = this.keyword;
        }
        contactService.getAll(query).then(res => {
          if (res) {
            this.items = res.data.contacts.map(u => {
              if (this.meeting.participants && this.meeting.participants.contacts) {
                if (this.meeting.participants.contacts.filter(c => {
                  return c.id === u.id;
                }).length > 0) {
                  u.isAdd = true;
                } else {
                  u.isAdd = false;
                }
              }
              u.name = u.contactName;
              return u;
            })
            this.totalPage = Number(res.data.pageInfo.TotalPages);
            this.totalItems = Number(res.data.pageInfo.TotalItems);
          }
        }).finally(() => {
          this.loading = false;
        })
      }
    },
    search() {
      this.loadItems();
    },
    clear() {
      this.keyword = '';
      this.loadItems();
    }
  },
  created() {
    // this.loadItems();
  }
}
</script>

<style scoped>
.modal-header {
  display: unset;
}
.modal {
  min-width: 60% !important;
}
.btn-search {
  width: 100%;
  position: relative;
}
.btn-search input {
  padding-left: 40px;
}
.btn-search i {
  position: absolute;
  top: 25%;
  left: 3%;
  color: #ccc;
}
.btn-purple {
  background: #6E6893;
  border-radius: 5px;
  color: white;
}
.btn-purple i, .btn-danger i {
  color: white !important;
}
.btn-danger {
  background: #D93915;
}
.btn {
  padding: 5px 20px;
}
.btn-disable {
  background: rgba(110, 104, 147, 0.7);
  cursor: unset !important;
  pointer-events: none;
}
/deep/ table {
  min-height: 330px !important;
}
</style>