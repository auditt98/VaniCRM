<template>
  <div>
    <Header/>
    <div class="p-4 my-0">
      <VLoading :loading="loading"/>
      <div class="dashboard-search form-group col-md-8 input-group mx-auto mb-5">
        <button class="btn btn-default" type="button">
          <i class="fa fa-search"></i>
        </button>
        <input type="text" class="form-control" placeholder="Search">
      </div>
      <div class="testimonial-group">
      <div class="row flex-row flex-nowrap custom" >
        <div class="col-1" v-for="item in items" :key="item.title" :data-id="item.id">
          <div class="mb-3">
            <div class="card-header-text row justify-content-between">
              <p class="col-sm-10" :style="'color:' + item.color">{{item.title}}</p>
              <span class="col-sm-2 m-auto">
                <i class="fa fa-plus-circle"></i>
              </span>
            </div>
            <p class="card-header-under-line w-100"></p>
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
      </div>
    </div>
  </div>
</template>

<script>
import Header from "@/components/common/Header";
import draggable from 'vuedraggable'
import DashboardCard from "@/components/dashboard/DashboardCard";
import {dashboardService} from "@/service/dashboard.service";
import VLoading from "@/components/common/VLoading";
import {dealService} from "@/service/deal.service";

export default {
  name: "Dashboard",
  data: function () {
    return {
      loading: false,
      colors: ['#109CF1', '#A29BFE', '#E84393', '#F2994A', '#F7685B', '#8949F2', '#1A9A05', '#FF002E'],
      items: []
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
    loadListDashboardSale() {
      this.loading = true;
      dashboardService.getAllForSale().then(res => {
        if (res && res.data) {
          this.items = res.data.stages.map((s, index) => {
            return {
              id: s.stageID, title: s.stageName + ' - ' + s.probability + '%', color: this.colors[index], drag: false, deals:
                  s.deals ? s.deals.map(d => {
                    return {
                      id: d.dealID, title: d.dealName, accountName: d.accountName,
                      ownerUsername: d.ownerUsername, type: s.stageID, tags: d.tags
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
    this.loadListDashboardSale();
  },
  components: {VLoading, DashboardCard, Header, draggable}
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
</style>