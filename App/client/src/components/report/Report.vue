<template>
    <div class="background-main">
        <div class="container-fluid" style="margin-top: 30px;">
            <div class="px-3">
                <div class="row">
                    <div class="col-sm-12">
                        <AmountByStageChart />
                    </div>
                    <div class="col-sm-5"></div> 
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <TopSalesChart />
                    </div>
                    <div class="col-sm-5"></div> 
                </div>
            </div>
            
        </div>

    </div>
</template>

<script>
// import ApexCharts from 'apexcharts'
import AmountByStageChart from "./AmountByStageChart.vue"
import TopSalesChart from "./TopSalesChart.vue"
// import {reportService} from "../../service/report.service.js"
export default {
  name: 'Report',
  data: function () {
    return {
      
    };
  },
  components:{ AmountByStageChart, TopSalesChart},
  mounted() {

  },
}
</script>

<style scoped>


</style>