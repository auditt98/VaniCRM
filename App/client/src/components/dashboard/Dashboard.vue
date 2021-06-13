<template>
  <div>
    <div class="p-4 my-0">
      <VLoading :loading="loading"/>
      <div class="dashboard-search form-group col-md-8 input-group mx-auto mb-5">
        <button class="btn btn-default" type="button">
          <i class="fa fa-search"></i>
        </button>
        <input type="text" class="form-control" v-model="searchQuery" placeholder="Search">
      </div>
      <div class="testimonial-group">
      <div class="row flex-row flex-nowrap custom" v-if="type === 2">
        <div class="col-1" v-for="item in filteredDealsBySearchText" :key="item.title" :data-id="item.id">
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
                <input type="text" class="form-control mb-2" placeholder="Enter deal name" v-model.trim="item.deal.name">

                <div>
                  <vc-select label="name"
                    placeholder="Choose account"
                    :filterable="false" :options="accountSearchs"
                    @search="onSearchAccount"
                    v-model="item.deal.account"
                  />
                </div>

                <select class="form-control py-0 w-100 my-2" v-model="item.deal.priority">
                  <option value="null" hidden disabled selected>Choose priority</option>
                  <option v-for="(f, i) in priorities" :key="i" :value="f.id">{{ f.name }}</option>
                </select>
                <input type="text" class="form-control mb-2" placeholder="Enter amount" v-model.trim="item.deal.expectedRevenue">
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
              <div v-for="(element, idx) in item.deals"
                   :key="idx">
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
                  <input type="text" class="form-control mb-2" v-model.trim="lead.companyName" placeholder="Company Name">
                  <div class="d-flex justify-content-between">
                    <span @click="save(lead, 'LEAD')"><VButton :data="btnEdit"/></span>
                    <span @click="lead.isAdd = false"><VButton :data="btnCancel"/></span>
                  </div>
                </div>
              </div>
            </div>
            <draggable class="drag-scroll" :list="filteredLeadsBySearchText" :move="checkMoveLead" v-bind="dragOptions"
              @start="leadDrag = true"
              :scrollSensitivity="200"
              :force-fallback="true"
              @change="dragDashboardMarketing($event)"
            >
              <transition-group type="transition" :name="!leadDrag ? 'flip-list' : null">
                <div v-for="(element, idx) in filteredLeadsBySearchText"
                     :key="idx">
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
            <draggable class="drag-scroll" :list="filteredAccountsBySearchText" :move="checkMoveMar" v-bind="dragOptions"
                       @start="accountDrag = true"
                       :scrollSensitivity="200"
                       :force-fallback="true"
            >
              <transition-group type="transition" :name="!accountDrag ? 'flip-list' : null">
                <div v-for="(element, idx) in filteredAccountsBySearchText"
                     :key="idx">
                  <DashboardCard :data="element"/>
                </div>
              </transition-group>
            </draggable>
          </div>
          <div class="col-3" :data-id="3">
            <div class="mb-3">
              <div class="card-header-text row justify-content-between">
                <p class="col-sm-10" :style="'color:#D93915'">Deals</p>
              </div>
              <p class="card-header-under-line w-100"></p>
            </div>
            <draggable class="drag-scroll" :list="filteredDeals" :move="checkMoveDeal" v-bind="dragOptions"
              @start="dealDrag = true"
              :scrollSensitivity="200"
              :force-fallback="true"
              @change="dragDeal($event)"
            >
              <transition-group type="transition" :name="!dealDrag ? 'flip-list' : null">
                <div v-for="(element, idx) in filteredDeals"
                     :key="idx">
                  <DashboardCard :data="element"/>
                </div>
              </transition-group>
            </draggable>
          </div>
        </div>
      </div>
    </div>
    <template v-if="showModal">
      <transition name="modal">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-container">
              <div class="modal-header">
                Convert Account to Lead
              </div>

              <div class="modal-body">
                <div class="row">
                  <div class="col-4">
                    <div class="mb-2">
                      <p>Account Name:</p>
                    </div>
                    <div class="mb-2">
                      <p>Deal Name:</p>
                    </div>
                    <div class="mb-2">
                      <p>Owner:</p>
                    </div>
                  </div>
                  <div class="col-8">
                    <div class="mb-2">
                      <p>{{convertDealData.title}}</p>
                    </div>
                    <div class="mb-2">
                      <input type="text" class="form-control" v-model="convertDealData.dealName" placeholder="Deal Name">
                    </div>
                    <div class="mb-2">
                      <vc-select id="Owner" label="username"
                        :filterable="false" :options="owners"
                        @search="onSearchUser"
                        v-model="convertDealData.owner">
                      </vc-select>
                    </div>
                  </div>
                </div>
              </div>

              <div class="d-flex" style="justify-content: flex-end;">
                <span @click="convertDeal" class="mr-4"><VButton :data="btnEdit"/></span>
                <span @click="cancelConvertDeal"><VButton :data="btnCancel"/></span>
              </div>
            </div>
          </div>
        </div>
      </transition>
    </template>
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
import _cloneDeep from 'lodash/cloneDeep'
import _toLower from 'lodash/toLower'
import {userService} from "@/service/user.service";

export default {
  name: "Dashboard",
  data: function () {
    return {
      searchQuery: '',
      loading: false,
      type: null,
      colors: ['#109CF1', '#A29BFE', '#E84393', '#F2994A', '#F7685B', '#8949F2', '#1A9A05', '#FF002E'],
      items: [],
      leads: [],
      accounts: [],
      deals: [],
      priorities: [],
      accountSearchs: [],
      lead: {
        isAdd: false,
        name: null,
        email: null,
        phone: null,
        companyName: null,
      },
      account: {
        isAdd: false,
        name: null,
        email: null,
        phone: null
      },
      leadDrag: false,
      accountDrag: false,
      dealDrag: false,
      showModal: false,
      convertDealData: {},
      owners: [],
      btnCancel: {btnClass: 'btn-white px-3', icon: '', text: 'CANCEL'},
      btnEdit: {btnClass: 'btn-red px-4', icon: '', text: 'CREATE'},
    };
  },
  methods: {
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
    dragDashboardMarketing(event){
      if (event.removed) {
        leadService.convertToAccount(event.removed.element.id)
      }
    },
    dragDeal(event) {
      if (event.added) {
        this.showModal = true
        this.onSearchUser()
        this.convertDealData = event.added.element
      }
    },
    openAdd(item, type) {
      if (type === 'DEAL') {
        item.deal.name = null;
        item.deal.account = null;
        item.deal.priority = null;
        item.deal.expectedRevenue
      } else if (type === 'LEAD') {
        item.companyName = null
        item.name = null;
        item.email = null;
        item.phone = null;
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
        let expectedRevenuePercentage = 0
        switch (item.id) {
          case 1:
            expectedRevenuePercentage = 0.1
            break;
          case 2:
            expectedRevenuePercentage = 0.2
            break;
          case 3:
            expectedRevenuePercentage = 0.4
            break;
          case 4:
            expectedRevenuePercentage = 0.5
            break;
          case 5:
            expectedRevenuePercentage = 0.6
            break;
          case 6:
            expectedRevenuePercentage = 0.8
            break;
          case 7:
            expectedRevenuePercentage = 1
            break;
          default:
            break;
        }
        const deal = {
          name: item.deal.name,
          account: item.deal.account ? item.deal.account.id : null,
          priority: item.deal.priority,
          stage: item.id,
          amount: item.deal.expectedRevenue,
          expectedRevenue: item.deal.expectedRevenue * expectedRevenuePercentage
        };
        dealService.create(deal)
            .then(res => {
              if (res && res.status === 'success') {
                alert(res.message);
                item.isAdd = false;
                this.loadListDashboardSale();
              }
            }).finally(() => {
          // this.loading = false;
        })
      } else if (type === 'LEAD') {
        leadService.create(item).then(res => {
          if (res) {
            alert(res.message);
            item.isAdd = false;
            this.loadLeads();
          }
        })
      } else if (type === 'ACCOUNT') {
        accountService.create(item).then(res => {
          if (res) {
            alert(res.message);
            item.isAdd = false;
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
    checkMoveLead: function (evt) {
      if (evt.relatedContext && evt.relatedContext.element && (evt.relatedContext.element.type === 2)) {
        return true;
      }
      return false;
    },
    checkMoveMar: function (evt) {
        if (evt.relatedContext && evt.relatedContext.element && (evt.relatedContext.element.type === 3)) {
          return true;
        }
      return false;
    },
    checkMoveDeal: function () {
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
                      title2: d.ownerUsername, type: s.stageID, tags: d.tags, expectedRevenue: d.expectedRevenue,
                      routeName: 'DealDetail'
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
              id: d.id, title: d.name, type: 1, tags: [],title1: d.phone, title2: d.email,
              routeName: 'LeadDetail', isConverted: d.isConverted, companyName: d.companyName
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
                  id: d.id, title: d.name, type: 2, tags: [],title1: d.phone, title2: d.website,
                  routeName: 'AccountDetail'
                }
              });
            }
          }).finally(() => {
        this.loading = false;
      })
    },
    async loadDeals() {
      this.deals = [];
      this.loading = true;
      const q = {
        currentPage: 1,
        pageSize: 10
      };
      await dealService.getAll(q)
          .then(res => {
            if (res && res.data) {
              this.deals = res.data.deals.map((d) => {
                return {
                  id: d.id, title: d.name, title1: d.accountName, title2: d.owner, type: 3, routeName: 'DealDetail'
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
    filterDealsBySearchText(items, searchText) {
      if (this.type === 2 && items) {
        if (!searchText) {
          return items
        }
        let list = _cloneDeep(items)
        const filteredList = []
        list.forEach(item => {
          const filteredDeal = item.deals.filter(deal => {
            return _toLower(deal.title).includes(_toLower(searchText))
              || _toLower(deal.title1).includes(_toLower(searchText))
              || _toLower(deal.title2).includes(_toLower(searchText))
          })
          item.deals = filteredDeal
          filteredList.push(item)
        });
        return filteredList
      }
      return []
    },
    filterAccountsBySearchText(items, searchText) {
      if (this.type === 1 && items) {
        if (!searchText) {
          return items
        }
        const accounts = _cloneDeep(items)
        return accounts.filter(account => {
          return _toLower(account.title).includes(_toLower(searchText))
          || _toLower(account.title1).includes(_toLower(searchText))
          || _toLower(account.title2).includes(_toLower(searchText))
        })
      }
      return []
    },
    filterLeadsBySearchText(items, searchText) {
      if (this.type === 1 && items) {
        const leads = _cloneDeep(items)
        if (!searchText) {
          return leads.filter(lead => !lead.isConverted)
        }
        return leads.filter(lead => {
          return (_toLower(lead.title).includes(_toLower(searchText))
          || _toLower(lead.title1).includes(_toLower(searchText))
          || _toLower(lead.title2).includes(_toLower(searchText)))
          && !lead.isConverted
        })
      }
      return []
    },
    filterDeals(items, searchText) {
      if (this.type === 1 && items) {
        const deals = _cloneDeep(items)
        if (!searchText) {
          return deals
        }
        return deals.filter(deal => {
          return _toLower(deal.title).includes(_toLower(searchText))
          || _toLower(deal.title1).includes(_toLower(searchText))
          || _toLower(deal.title2).includes(_toLower(searchText))
        })
      }
      return []
    },
    convertDeal() {
      const payload = {
        stage: 1,
        account: this.convertDealData.id,
        name: this.convertDealData.dealName,
        owner: this.convertDealData.owner.id
      }
      dealService.create(payload)
        .then(() => {
          this.showModal = false
          this.loadAccounts();
          this.loadDeals();
        })
    },
    cancelConvertDeal() {
      this.showModal = false
      this.loadAccounts();
      this.loadDeals();
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
    },
    filteredDealsBySearchText() {
      return this.filterDealsBySearchText(this.items, this.searchQuery)
    },
    filteredAccountsBySearchText() {
      return this.filterAccountsBySearchText(this.accounts, this.searchQuery)
    },
    filteredLeadsBySearchText() {
      return this.filterLeadsBySearchText(this.leads, this.searchQuery)
    },
    filteredDeals() {
      return this.filterDeals(this.deals, this.searchQuery)
    }
  },
  created() {
    this.$root.$refs.Dashboard = this;
    if (this.$route.name === 'DashboardMarketing') {
      this.type = 1;
      this.loadLeads();
      this.loadAccounts();
      this.loadDeals();
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
  padding: 4px 5px;
  line-height: 1;
  font-size: 0.5rem;
}
/deep/ .vs__search {
  width: unset;
  border: none;
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
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 500px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
  font-family: Helvetica, Arial, sans-serif;
}

.modal-header h3 {
  margin-top: 0;
  color: #42b983;
}

.modal-body {
  margin: 20px 0;
}

.modal-default-button {
  float: right;
}

.modal-enter {
  opacity: 0;
}

.modal-leave-active {
  opacity: 0;
}

.modal-enter .modal-container,
.modal-leave-active .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}

</style>