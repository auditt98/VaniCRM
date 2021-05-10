<template>
    <div>
        <div class="chart-card">
            <h6 class="p-4">TOP SALES HAVE HIGH TURNOVER</h6>
            <apexcharts type="bar" :options="options" :series="series" :height="450" :chart="chart"></apexcharts>
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
                distributed: true,
                borderRadius: 8,
                horizontal: true,
            }
        },
        chart: {
          id: 'topsales-report',
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
            console.log("Res", res)
          this.series = [{'name': 'Amount' ,'data': [...res.data.data]}];
        }
      })
  },
  methods:{
  }
}
</script>