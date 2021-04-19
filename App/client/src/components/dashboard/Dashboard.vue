<template>
  <div>

    <div class="p-4 my-0">
      <VLoading :loading="loading"/>
      <div class="dashboard-search form-group col-md-8 input-group mx-auto mb-5">
        <button class="btn btn-default" type="button">
          <i class="fa fa-search"></i>
        </button>
        <input type="text" class="form-control" placeholder="Search">
      </div>
      <div class="testimonial-group">
      <div class="row flex-row flex-nowrap custom" v-if="type === 2">
        <div class="col-1" v-for="item in items" :key="item.title" :data-id="item.id">
          <div class="mb-3">
            <div class="card-header-text row justify-content-between">
              <p class="col-sm-10" :style="'color:' + item.color">{{item.title}}</p>
              <span class="col-sm-2 m-auto cursor-pointer" @click="openAdd(item, 'DEAL')">
                <i class="fa fa-plus-circle"></i>
              </span>
            </div>
            <p class="card-header-under-line w-100"></p>
            <div class="card add-campaign" v-if="item.isAdd">
              <div class="card-body p-2">
                <input type="text" class="form-control mb-2" v-model.trim="item.deal.name">

                <div>
                  <vc-select label="name"
                             :filterable="false" :options="accountSearchs"
                             @search="onSearchAccount"
                             v-model="item.deal.account">
                  </vc-select>
                </div>

                <select class="form-control py-0 w-100 my-2" v-model="item.deal.priority">
                  <option v-for="(f, i) in priorities" :key="i" :value="f.id">{{ f.name }}</option>
                </select>
                <div class="d-flex justify-content-between">
                  <span @click="save(item, 'DEAL')"><VButton :data="btnEdit"/></span>
                  <span @click="item.isAdd = false"><VButton :data="btnCancel"/></span>
                </div>
              </div>
            </div>
          </div>
          <draggable :list="item.deals" :move="checkMove" v-bind="dragOptions"
                     @start="item.drag = true"
                     :empty-insert-threshold="60"
                     @change="log($event, item)"
          >
            <!--                     @end="item.drag = false"-->
            <transition-group type="transition" :name="!item.drag ? 'flip-list' : null">
              <div v-for="(element) in item.deals"
                   :key="element.title">
                <DashboardCard :data="element"/>
              </div>
            </transition-group>
          </draggable>
        </div>
      </div>
<!--        marketing-->
        <div class="row flex-row flex-nowrap custom" v-if="type === 1">
          <div class="col-3" :data-id="1">
            <div class="mb-3">
              <div class="card-header-text row justify-content-between">
                <p class="col-sm-10" :style="'color:#D93915'">Leads</p>
                <span class="col-sm-2 m-auto cursor-pointer"  @click="openAdd(lead, 'LEAD')">
                <i class="fa fa-plus-circle"></i>
              </span>
              </div>
              <p class="card-header-under-line w-100"></p>
              <div class="card add-campaign" v-if="lead.isAdd">
                <div class="card-body p-2">
                  <input type="text" class="form-control mb-2" v-model.trim="lead.name" placeholder="Name">
                  <input type="email" class="form-control mb-2" v-model.trim="lead.email" placeholder="Email">
                  <input type="text" class="form-control mb-2" v-model.trim="lead.phone" placeholder="Phone">

                  <div class="d-flex justify-content-between">
                    <span @click="save(lead, 'LEAD')"><VButton :data="btnEdit"/></span>
                    <span @click="lead.isAdd = false"><VButton :data="btnCancel"/></span>
                  </div>
                </div>
              </div>
            </div>
            <draggable class="drag-scroll" :list="leads" v-bind="dragOptions"
                       @start="leadDrag = true"
                       :scrollSensitivity="200"
                       :force-fallback="true"
            >
              <transition-group type="transition" :name="!leadDrag ? 'flip-list' : null">
                <div v-for="(element) in leads"
                     :key="element.title">
                  <DashboardCard :data="element"/>
                </div>
              </transition-group>
            </draggable>
          </div>
          <div class="col-3" :data-id="2">
            <div class="mb-3">
              <div class="card-header-text row justify-content-between">
                <p class="col-sm-10" :style="'color:#D93915'">Accounts</p>
                <span class="col-sm-2 m-auto cursor-pointer"  @click="openAdd(account, 'ACCOUNT')">
                <i class="fa fa-plus-circle"></i>
              </span>
              </div>
              <p class="card-header-under-line w-100"></p>
              <div class="card add-campaign" v-if="account.isAdd">
                <div class="card-body p-2">
                  <input type="text" class="form-control mb-2" v-model.trim="account.name" placeholder="Name">
                  <input type="email" class="form-control mb-2" v-model.trim="account.email" placeholder="Email">
                  <input type="text" class="form-control mb-2" v-model.trim="account.phone" placeholder="Phone">

                  <div class="d-flex justify-content-between">
                    <span @click="save(account, 'ACCOUNT')"><VButton :data="btnEdit"/></span>
                    <span @click="account.isAdd = false"><VButton :data="btnCancel"/></span>
                  </div>
                </div>
              </div>
            </div>
            <draggable class="drag-scroll" :list="accounts" :move="checkMoveMar" v-bind="dragOptions"
                       @start="accountDrag = true"
                       :scrollSensitivity="200"
                       :force-fallback="true"
            >
              <transition-group type="transition" :name="!accountDrag ? 'flip-list' : null">
                <div v-for="(element) in accounts"
                     :key="element.title">
                  <DashboardCard :data="element"/>
                </div>
              </transition-group>
            </draggable>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

import draggable from 'vuedraggable'
import DashboardCard from "@/components/dashboard/DashboardCard";
import {dashboardService} from "@/service/dashboard.service";
import VLoading from "@/components/common/VLoading";
import {dealService} from "@/service/deal.service";
import {leadService} from "@/service/lead.service";
import {accountService} from "@/service/account.service";
import VButton from "@/components/common/VButton";

export default {
  name: "Dashboard",
  data: function () {
    return {
      loading: false,
      type: null,
      colors: ['#109CF1', '#A29BFE', '#E84393', '#F2994A', '#F7685B', '#8949F2', '#1A9A05', '#FF002E'],
      items: [],
      leads: [],
      accounts: [],
      priorities: [],
      accountSearchs: [],
      lead: {
        isAdd: false,
        name: null,
        email: null,
        phone: null
      },
      account: {
        isAdd: false,
        name: null,
        email: null,
        phone: null
      },
      leadDrag: false,
      accountDrag: false,
      btnCancel: {btnClass: 'btn-white px-3', icon: '', text: 'CANCEL'},
      btnEdit: {btnClass: 'btn-red px-4', icon: '', text: 'CREATE'},
    };
  },
  methods: {
    openAdd(item, type) {
      if (type === 'DEAL') {
        item.deal.name = null;
        item.deal.account = null;
        item.deal.priority = null;
      } else {
        item.name = null;
        item.email = null;
        item.phone = null;
      }
      if (!item.isAdd) {
        item.isAdd = true;
      }
    },
    save(item, type) {
      if (type === 'DEAL') {
        const deal = {
          name: item.deal.name,
          account: item.deal.account ? item.deal.account.id : null,
          priority: item.deal.priority,
          stage: item.deal.id
        };
        dealService.create(deal)
            .then(res => {
              if (res && res.data) {
                alert(res.message);
                this.loadListDashboardSale();
              }
            }).finally(() => {
          this.loading = false;
        })
      } else if (type === 'LEAD') {
        leadService.create(item).then(res => {
          if (res && res.data) {
            alert(res.message);
            this.loadLeads();
          }
        })
      } else if (type === 'ACCOUNT') {
        accountService.create(item).then(res => {
          if (res && res.data) {
            alert(res.message);
            this.loadAccounts();
          }
        })
      }
    },
    log(e, item) {
      item.drag = false;
      if (e && e.added) {
        this.loading = true;
        dealService.changeStage(Number(e.added.element.id), item.id)
        .then(res => {
          if (res && res.status === 'success') {
            alert('Thành công');
            e.added.element.type = item.id;
          }
        }).finally(() => {
          this.loading = false;
        })
      }
    },
    checkMove: function (evt) {
      if (evt.draggedContext.element.type === 7 || evt.draggedContext.element.type === 8) {
        if (evt.relatedContext && evt.relatedContext.element &&
            (evt.relatedContext.element.type === 7 || evt.relatedContext && evt.relatedContext.element.type === 8)) {
          return true;
        }
        return false;
      }
      return true;
    },
    checkMoveMar: function (evt) {
        if (evt.relatedContext && evt.relatedContext.element && (evt.relatedContext.element.type === 2)) {
          return true;
        }
      return false;
    },
    loadListDashboardSale() {
      this.loading = true;
      dashboardService.getAllForSale().then(res => {
        if (res && res.data) {
          this.items = res.data.stages.map((s, index) => {
            return {
              id: s.stageID, title: s.stageName + ' - ' + s.probability + '%', color: this.colors[index], drag: false, isAdd: false,
              deal: {
                account: null,
                name: null,
                priority: null,
              },
              deals:
                  s.deals ? s.deals.map(d => {
                    return {
                      id: d.dealID, title: d.dealName, title1: d.accountName,
                      title2: d.ownerUsername, type: s.stageID, tags: d.tags
                    }
                  }) : []
            }
          });
        }
      }).catch(err => {
        alert(err);
      }).finally(() => {
        this.loading = false;
      })
    },
    async loadLeads() {
      this.leads = [];
      this.loading = true;
      const q = {
        currentPage: 1,
        pageSize: 10
      };
      await leadService.getAll(q)
      .then(res => {
        if (res && res.data) {
          this.leads = res.data.leads.map((d) => {
            return {
              id: d.id, title: d.name, type: 1, tags: [],title1: d.phone, title2: d.email
            }
          });
        }
      }).finally(() => {
        this.loading = false;
      })
    },
    async loadAccounts() {
      this.accounts = [];
      this.loading = true;
      const q = {
        currentPage: 1,
        pageSize: 10
      };
      await accountService.getAll(q)
          .then(res => {
            if (res && res.data) {
              this.accounts = res.data.accounts.map((d) => {
                return {
                  id: d.id, title: d.name, type: 2, tags: [],title1: d.phone, title2: d.website
                }
              });
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    loadSelectList() {
      dealService.loadAllObject()
          .then(res => {
            if (res && res.data && res.status === 'success') {
              this.priorities = res.data.priorities;
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
          this.accountSearchs = res.data.accounts;
        }
      })
    },
  },
  computed: {
    dragOptions() {
      return {
        animation: 200,
        group: "description",
        disabled: false,
        ghostClass: "ghost"
      };
    }
  },
  created() {
    if (this.$route.name === 'DashboardMarketing') {
      this.type = 1;
      this.loadLeads();
      this.loadAccounts();
    } else {
      this.type = 2;
      this.loadListDashboardSale();
      this.loadSelectList();
      this.onSearchAccount(null);
    }
  },
  components: {VButton, VLoading, DashboardCard, draggable}
}
</script>

<style scoped>
.custom {
  min-height: 600px;
}
.col-1 div:nth-child(2) span {
  min-height: 200px;
}

.testimonial-group {
  min-height: 700px;
}
.testimonial-group > .row {
  overflow-x: auto;
  white-space: nowrap;
}

.card-header-under-line {
  background: #109CF1;
  border-radius: 4px;
  height: 2px;
}
.card-header-text p {
  font-weight: 500;
  font-size: 16px;
  line-height: 28px;
  color: #109CF1;
}
.card-header-text span {
  color: #109CF1;
  height: 28px;
  margin-bottom: 0.8rem !important;
  text-align: right;
}
.dashboard-search {
  position: relative;
}

.dashboard-search button {
  position: absolute;
  top: 0;
  left: 14px;
  z-index: 6;
  background: none;
}

.dashboard-search input {
  padding-left: 40px;
}

i {
  color: black;
}

.flip-list-move {
  transition: transform 0.5s;
}

.no-move {
  transition: transform 0s;
}

.ghost {
  opacity: 0.5;
  background: #c8ebfb;
}

.custom .col-1 {
  flex: 0 0 12.5%;
  max-width: 12.5%;
}
.add-campaign input[type='text'].form-control {
  padding: 4px 5px;
  line-height: 0.4;
}
.add-campaign /deep/ button {
  padding: 5px 10px;
  line-height: 1;
  font-size: 0.8rem;
}
/deep/ .vs__search {
  width: unset;
  border: none;
  z-index: -1 !important;
}

/deep/ .vs--open .vs--single .vs__selected {
  position: unset;
}

/deep/ span.vs__selected {
  padding-left: 5px;
  margin: 0;
  max-height: 31px;
  overflow: hidden;
}

/deep/ .vs__dropdown-toggle {
  height: 31px;
  padding: 0 !important;
}
select.form-control {
  height: 31px !important;
  padding-left: 5px !important;
}
/deep/  .vs__actions {
  display: none;
}
.drag-scroll {
  max-height: 600px;
  overflow-y: auto;
}
</style>