<template>
    <div>
        <div class="chart-card">
          <vcl-code v-if="!loaded" style="padding: 20px;"></vcl-code>
          <!-- <vcl-table></vcl-table> -->
          <div v-if="loaded">
            <h6 class="p-4">{{reportName}}</h6>
            <apexcharts type="bar" :options="options" :series="series" :height="450" :chart="chart"></apexcharts>
          </div>
            
        </div>
    </div>

</template>

<style>
    .chart-card {
        margin-top: 20px;
        margin-bottom: 20px;
        background: white;
        box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
        border: 1px solid #e7e7e7;
        border-radius: 15px;
    }

</style>

<script>
import { VclCode} from 'vue-content-loading';
// import { VclTable} from 'vue-content-loading';
import VueApexCharts from "vue-apexcharts";
import {reportService} from "../../service/report.service.js"
export default {
  name: 'AmountByStageChart',
  components: {
      apexcharts: VueApexCharts,
      VclCode,
      // VclTable
  },
  data: function () {
    return {
      reportName: "",
      loaded: false,
      chart: {
          id: 'topsales-report',
      },
      options: {
        chart:{
          id: "TopSales-" + new Date(Date.now()).toLocaleDateString()
        },
        plotOptions: {
            bar: {
                distributed: true,
                borderRadius: 8,
                horizontal: true,
            }
        },
        xaxis: {
            type: 'category'
        },
        dataLabels: {
            enabled: false
        },
        legend:{
            show: false
        },
        tooltip:{
            enabled: true,
        }
      },
      series: Array,
    };
  },
  mounted() {
    reportService.getTopSalesReport().then(res => {
        if (res && res.data) {
          this.series = [{'name': 'Amount' ,'data': [...res.data.data]}];
          this.reportName = res.data.reportName;
          this.loaded = true;
        }
      })
  },
  methods:{
  }
}
</script>