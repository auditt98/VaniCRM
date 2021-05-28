<template>
    <div class="background-main">
        <div class="container-fluid" style="margin-top: 30px;">
            <div class="px-3">
                <ExportablesModal v-show="isModalVisible" @close="closeExportablesModal"/>
                <div class="row">
                    <div class="col-sm-8"></div>
                    <div class="col-sm-4 d-flex justify-content-end">
                        <span @click="openExportables"><VButton :data="btnExport"/></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-7">
                        <AmountByStageChart />
                    </div>
                    <div class="col-sm-5">
                        <DealsByStageChart />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <TopSalesChart />
                    </div>
                    <div class="col-sm-6">
                        <TopMarketingsChart />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <KeyAccountsChart />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <AccountsByIndustryChart />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <RevenueComparisonChart />
                    </div>
                </div>
            </div>
            
        </div>

    </div>
</template>

<script>
// import ApexCharts from 'apexcharts'
import VButton from "@/components/common/VButton";
import AmountByStageChart from "./AmountByStageChart.vue"
import TopSalesChart from "./TopSalesChart.vue"
import TopMarketingsChart from "./TopMarketingsChart.vue"
import KeyAccountsChart from "./KeyAccountsChart.vue"
import DealsByStageChart from "./DealsByStageChart.vue"
import AccountsByIndustryChart from "./AccountsByIndustryChart.vue"
import RevenueComparisonChart from "./RevenueComparisonChart.vue"
import ExportablesModal from "@/components/common/modal/ExportablesModal"
// import {reportService} from "../../service/report.service.js"
export default {
  name: 'Report',
  data: function () {
    return {
      isModalVisible: false,
      btnExport: {btnClass: 'btn-white px-3 mr-4', icon: 'fa-file-pdf-o', text: 'Exportables'},
    };
  },
  components:{ 
        AmountByStageChart, 
        TopSalesChart, 
        TopMarketingsChart, 
        KeyAccountsChart, 
        DealsByStageChart, 
        AccountsByIndustryChart, 
        RevenueComparisonChart,
        VButton,
        ExportablesModal,
    },
  mounted() {

  },
  methods:{
      openExportables(){
          this.isModalVisible = true;
      },
      closeExportablesModal(){
          this.isModalVisible = false;
      }
  }
}
</script>

<style scoped>


</style>