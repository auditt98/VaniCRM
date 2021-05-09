<template>
    <div class="background-main">
        <div class="container-fluid" style="margin-top: 30px;">
            <div class="px-3">
                <div class="row">
                    <div class="col-sm-7">
                        <AmountByStageChart />
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
// import {reportService} from "../../service/report.service.js"
export default {
  name: 'Report',
  data: function () {
    return {
      
    };
  },
  components:{ AmountByStageChart},
  mounted() {
    // reportService.getAmountByStageReport().then(res => {
    //     if (res && res.data) {
    //         console.log(res.data)
    //       this.series = [{'data': [...res.data.data]}];
    //     }
    //   })
    
  },
}
</script>

<style scoped>


</style>