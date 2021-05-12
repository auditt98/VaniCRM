<template>
    <div>
        <div class="chart-card">
            <vcl-code v-if="!loaded" style="padding: 20px;"></vcl-code>
            <div v-if="loaded">
              <h6 class="p-4">{{reportName}}</h6>
              <apexcharts type="line" :options="options" :series="series" :chart="chart" :height="400"></apexcharts>
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
  name: 'KeyAccountsChart',
  components: {
      apexcharts: VueApexCharts,
      VclCode,
  },
  data: function () {
    return {
      reportName: "",
      loaded: false,
      labels: Array,
      chart: {
          id: 'keyAccounts-report',
          zoom: {
            // enabled: true
          }
      },
      options: {
        legend:{
          show: true,
        },
        plotOptions: {
            bar: {
                distributed: false,
                borderRadius: 8,
            }
        },
        labels: this.labels,
        colors: ["#1192e8", "#6929c4"],
        xaxis: {
            type: 'category',
            categories: this.labels,
            // tickPlacement: 'on'
        },
        yaxis:[
          {
            title: {
                text: 'Amount',
                style: {
                  color: '#1192e8',
                  fontSize: '13px',
                  fontFamily: 'Segoe, sans-serif',
                  fontWeight: '800',
                  cssClass: 'apexcharts-yaxis-title',
                },
            },
            axisBorder: {
              show: true,
              color: '#1192e8'
            },
            
          },
          {
            opposite: true,
            title:{
              text: 'Number of deals',
              style: {
                  color: '#6929c4',
                  fontSize: '13px',
                  fontFamily: 'Segoe, sans-serif',
                  fontWeight: '800',
                  cssClass: 'apexcharts-yaxis-title',
                },
            },
            axisBorder: {
              show: true,
              color: '#6929c4'
            },
          }
        ],
        stroke: {
          width: [0, 4],
        },
        dataLabels: {
          enabled: true,
          enabledOnSeries: [1],
          style: {
              fontSize: '12px',
              fontFamily: 'Segoe UI, sans-serif',
              fontWeight: 'bold',
              // colors: ["#4c419e"]
          },
          background:{
            enabled: true,
            foreColor: "#fff",
            padding: 4,
            borderWidth: 0,
          }
        },
        tooltip:{
            // enabled: true,
        }
      },
      series: Array,
    };
  },
  mounted() {
    reportService.getKeyAccountsReport().then(res => {
        if (res && res.data) {
          var amountSeries = [];
          var amountData = [...res.data.data1]
          amountData.forEach(element => amountSeries.push(element.y))

          var countSeries = [];
          var countData = [...res.data.data];
          countData.forEach(element => countSeries.push(element.y))
          //force update labels
          this.labels = res.data.labels;
          this.updateLabels();
          this.series = [{'name': 'Amount', type : 'column', 'data': amountSeries}, {'name': 'Number of deals', 'type': 'line', 'data': countSeries}];
          this.reportName = res.data.reportName;
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