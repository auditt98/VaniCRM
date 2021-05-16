<template>
    <div>
        <div class="chart-card">
            <vcl-code v-if="!loaded" style="padding: 20px;"></vcl-code>
            <div v-if="loaded">
              <h6 class="p-4">{{reportName}}</h6>
              <apexcharts type="bar" :options="options" :series="series" :height="300" :chart="chart"></apexcharts>
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
  name: 'DealsByStageChart',
  components: {
      apexcharts: VueApexCharts,
      VclCode,
  },
  data: function () {
    return {
      reportName: "",
      loaded: false,
      chart: {
          id: 'dealByStage-report',
      },
      options: {
        plotOptions: {
            bar: {
                distributed: true
            }
        },
        theme: {
            mode: 'light', 
            palette: 'palette3', 
        },
        xaxis: {
            type: 'category',
            // tickPlacement: 'on'
        },
        dataLabels: {
            enabled: false
        },
        legend:{
            show: false
        },
        tooltip:{
            // enabled: true,
        }
      },
      series: Array,
    };
  },
  mounted() {
    reportService.getAmountByStageReport().then(res => {
        if (res && res.data) {
          this.labels = res.data.labels;
          this.updateLabels();
          this.series = [{'name': 'Deals by stage', 'data': [...res.data.data1]}];
          this.reportName = "DEALS BY STAGE";
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