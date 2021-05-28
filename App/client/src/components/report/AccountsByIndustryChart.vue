<template>
    <div>
        <div class="chart-card">
            <vcl-code v-if="!loaded" style="padding: 20px;"></vcl-code>
            <div v-if="loaded">
              <h6 class="p-4">{{reportName}}</h6>
              <apexcharts type="treemap" :options="options" :series="series" :height="300" :chart="chart"></apexcharts>
            </div>

        </div>
    </div>

</template>

<style>
    .chart-card {
        background: white;
        box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
        border: 1px solid #e7e7e7;
        border-radius: 15px;
    }

</style>

<script>
import {VclCode} from 'vue-content-loading';
import VueApexCharts from "vue-apexcharts";
import {reportService} from "../../service/report.service.js"
export default {
  name: 'AccountsByIndustryChart',
  components: {
      apexcharts: VueApexCharts,
      VclCode,
  },
  data: function () {
    return {
      reportName: "",
      loaded: false,
      chart: {
          id: 'accountsByIndustry-report',
      },
      options: {
        chart:{
          id: "AccountsByIndustry-" + new Date(Date.now()).toLocaleDateString()
        },
        colors: [
              '#3B93A5',
              '#F7B844',
              '#ADD8C7',
              '#EC3C65',
              '#CDD7B6',
              '#C1F666',
              '#D43F97',
              '#1E5D8C',
              '#421243',
              '#7F94B0',
              '#EF6537',
              '#C0ADDB'
        ],
        plotOptions: {
            treemap:{
              // distributed: true,
              enableShades: true,
            }
        },
        theme: {
            mode: 'light', 
            palette: 'palette1', 
        },
        xaxis: {
            type: 'category',
            // tickPlacement: 'on'
        },
        dataLabels: {
            enabled: true,
            formatter: function(text, op) {
              return [text, op.value]
            },
        },
        legend:{
            show: false,
        },
        tooltip:{
            // enabled: true,
        }
      },
      series: Array,
    };
  },
  mounted() {
    reportService.getAccountsByIndustryReport().then(res => {
        if (res && res.data) {
          this.reportName = res.data.reportName;
          this.series = [{name:"Number of accounts", data: [...res.data.data]}]
          this.loaded = true;
        }
      })
  },
  methods:{
    updateLabels(){
      this.options = {
        ...this.options,
        ...{
          labels: this.labels,
        }
      }
    }
  }
}
</script>