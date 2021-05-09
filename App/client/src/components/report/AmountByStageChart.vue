<template>
    <div>
        <div class="chart-card">
            <h6 class="p-4">AMOUNT BY STAGE</h6>
            <apexcharts type="bar" :options="options" :series="series" :height="400" :chart="chart"></apexcharts>
        </div>
    </div>

</template>

<style>
    .chart-card {
        background: white;
        box-shadow: 0 10px 20px -10px rgb(92 92 92 / 10%);
        border: 1px solid #e7e7e7;
        border-radius: 5px;
    }

</style>

<script>
import VueApexCharts from "vue-apexcharts";
import {reportService} from "../../service/report.service.js"
export default {
  name: 'AmountByStageChart',
  components: {
      apexcharts: VueApexCharts
  },
  data: function () {
    return {
      options: {
        plotOptions: {
            bar: {
                distributed: true
            }
        },
        theme: {
            mode: 'light', 
            palette: 'palette1', 
        },
        chart: {
          id: 'amountByStage-report',
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
    reportService.getAmountByStageReport().then(res => {
        if (res && res.data) {
          this.series = [{'data': [...res.data.data]}];
        }
      })
  },
  methods:{
  }
}
</script>