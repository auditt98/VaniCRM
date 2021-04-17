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
              <span class="col-sm-2 m-auto cursor-pointer" @click="item.isAdd = !item.isAdd">
                <i class="fa fa-plus-circle"></i>
              </span>
            </div>
            <p class="card-header-under-line w-100"></p>
            <div class="card add-campaign" v-if="item.isAdd">
              <div class="card-body p-2">
                <input type="text" class="form-control mb-2" placeholder="Name">
                <input type="text" class="form-control mb-2" placeholder="Email">
                <input type="text" class="form-control mb-2" placeholder="Phone">
                <div class="d-flex justify-content-between">
                  <VButton :data="btnEdit"/>
                  <VButton :data="btnCancel"/>
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
          <div class="col-2" :data-id="1">
            <div class="mb-3">
              <div class="card-header-text row justify-content-between">
                <p class="col-sm-10" :style="'color:#D93915'">Leads</p>
                <span class="col-sm-2 m-auto">
                <i class="fa fa-plus-circle"></i>
              </span>
              </div>
              <p class="card-header-under-line w-100"></p>
            </div>
            <draggable :list="leads" v-bind="dragOptions"
                       @start="leadDrag = true"
            >
              <transition-group type="transition" :name="!leadDrag ? 'flip-list' : null">
                <div v-for="(element) in leads"
                     :key="element.title">
                  <DashboardCard :data="element"/>
                </div>
              </transition-group>
            </draggable>
          </div>
          <div class="col-2" :data-id="2">
            <div class="mb-3">
              <div class="card-header-text row justify-content-between">
                <p class="col-sm-10" :style="'color:#D93915'">Accounts</p>
                <span class="col-sm-2 m-auto">
                <i class="fa fa-plus-circle"></i>
              </span>
              </div>
              <p class="card-header-under-line w-100"></p>
            </div>
            <draggable :list="accounts" :move="checkMoveMar" v-bind="dragOptions"
                       @start="accountDrag = true"
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
      leadDrag: false,
      accountDrag: false,
      btnCancel: {btnClass: 'btn-white px-3', icon: '', text: 'CANCEL'},
      btnEdit: {btnClass: 'btn-red px-4', icon: '', text: 'CREATE'},
    };
  },
  methods: {
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
    }
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
    if (window.location.pathname.indexOf('dashboard-marketing') > -1) {
      this.type = 1;
      this.loadLeads();
      this.loadAccounts();
    } else {
      this.type = 2;
      this.loadListDashboardSale();
    }
  },
  components: {VButton, VLoading, DashboardCard, draggable}
}
</script>

<style scoped>
.col-1 div:nth-child(2) span {
  min-height: 200px;
}

.testimonial-group {
  min-height: 800px;
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
</style>