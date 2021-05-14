<template>
    <div class="background-main">
        <div class="container-fluid" style="margin-top: 30px;">
            <div class="px-3">
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
            </div>
            
        </div>

    </div>
</template>

<script>
// import ApexCharts from 'apexcharts'
import AmountByStageChart from "./AmountByStageChart.vue"
import TopSalesChart from "./TopSalesChart.vue"
import TopMarketingsChart from "./TopMarketingsChart.vue"
import KeyAccountsChart from "./KeyAccountsChart.vue"
import DealsByStageChart from "./DealsByStageChart.vue"
import AccountsByIndustryChart from "./AccountsByIndustryChart.vue"
// import {reportService} from "../../service/report.service.js"
export default {
  name: 'Report',
  data: function () {
    return {
      
    };
  },
  components:{ AmountByStageChart, TopSalesChart, TopMarketingsChart, KeyAccountsChart, DealsByStageChart, AccountsByIndustryChart},
  mounted() {

  },
}
</script>

<style scoped>


</style>