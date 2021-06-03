<template>
  <el-dialog :visible.sync="visible">
    <el-date-picker
        v-model="value2"
        type="daterange"
        range-separator="至"
        start-placeholder="开始日期"
        end-placeholder="结束日期">
    </el-date-picker>
    <el-button @click="queryMoney">查询</el-button>
    <el-alert
        v-if="showTextVisible"
        :title="showText"
        type="success">
    </el-alert>
    <div ref="main" id="main" class="chart"/>
  </el-dialog>
</template>

<script>
import * as echarts from 'echarts';
import {moneyHistory} from "@/api/user";

export default {
  name: "ProfileLineChart",
  watch: {
    visible(newValue) {
      if (newValue !== true) {
        // echarts.dispose(this.myChart);
        // this.myChart = null
      }
    }
  },
  methods: {
    queryMoney() {
      if (this.value2.length !== 2) {
        return;
      }
      moneyHistory(this.value2[0], this.value2[1]).then(res => {
        echarts.dispose(this.myChart);
        this.myChart = null
        let data = res.data.data;
        for (let i = 0; i < data.length; i++) {
          data[i].createTime = new Date(data[i].createTime)
        }
        let option = {
          dataset: {
            source: data,
            dimensions: ['after', 'amount', 'createTime'],
          },
          xAxis: {
            type: 'time',
            axisLabel: {
              formatter: '{yyyy}-{MM}-{dd}'
            }
          },
          yAxis: {
            type: 'value',
            scale: true
          },
          tooltip: {
            trigger: 'axis'
          },
          series: [{
            name: '明细',
            type: 'line',
            encode: {
              x: 'createTime',
              y: 'after'
            }
          }]
        }
        this.myChart = echarts.init(this.$refs.main);
        this.myChart.setOption(option);
        this.myChart.on("click", params => {
          this.showText = `时间： ${params.data.createTime}\n说明： ${params.data.note}\n金额： ${params.data.amount}元\n余额： ${params.data.after}元`;
          this.showTextVisible = true;
        })
      })
    }
  },
  data() {
    return {
      visible: false,
      myChart: {},
      value2: '',
      showText: '',
      showTextVisible: false
    }
  },
}
</script>

<style scoped>
.chart {
  height: 400px;
}
</style>