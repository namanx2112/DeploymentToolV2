import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import Chart, { ChartType, ChartTypeRegistry } from 'chart.js/auto';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { Colors } from 'chart.js';
import { DashboardTileType, DahboardTile } from 'src/app/interfaces/commons';

@Component({
  selector: 'app-dashboard-chart',
  templateUrl: './dashboard-chart.component.html',
  styleUrls: ['./dashboard-chart.component.css']
})
export class DashboardChartComponent {
  chart: any;
  @Input()
  set record(val: DahboardTile) {
    this._record = val;
    if (this._record.type == DashboardTileType.Chart) {
      this.createChart();
    }
  }
  _record: DahboardTile;
  @Output() ChartClicked = new EventEmitter<string>();
  tClass: string;
  constructor(private elementRef: ElementRef) {
    Chart.register(Colors);
    // Chart.overrides.doughnut.cutout = '75%';
    Chart.register(ChartDataLabels);
  }
  ngOnInit(): void {

  }

  setSizeClass() {
    switch (this._record.size) {
      case 1:
        this.tClass = "canvas X1";
        break;
      case 2:
        this.tClass = "canvas X2";
        break;
      case 3:
        this.tClass = "canvas X3";
        break;
      case 4:
        this.tClass = "canvas X4";
        break;
    }
  }

  createChart() {
    if (typeof this._record.chartType != 'undefined' && typeof this._record.chartLabels != 'undefined') {
      this.setSizeClass();
      let htmlRef = this.elementRef.nativeElement.querySelector("#MyChartId");
      let cThis = this;
      this.chart = new Chart(htmlRef, {
        type: this._record.chartType, //this denotes tha type of chart        
        data: {// values on X-Axis
          labels: this._record.chartLabels,
          datasets: [{
            label: "",
            data: this._record.chartValues,
            // backgroundColor: bgColors,
            // hoverOffset: 4
          }],
        },
        options: {
          responsive: true,
          aspectRatio: 4/1,
          plugins: {
            legend: {
              display: false
            },
            datalabels: {
              color: 'white',
              display: function (context) {
                var dataset = context.dataset;
                var count = dataset.data.length;
                var value = dataset.data[context.dataIndex];
                return "1000";
              },
              font: {
                weight: 'bold'
              },
              padding: 6,
              formatter: Math.round
            },
          },
          onClick: function (event: any, array: any) {
            // if (array[0]) {
            //   let item = event;
            // }
            cThis.ChartClicked.emit(cThis._record.reportId.toString());
          }
        }

      });
    }
  }

  onclick(points: any, evt: any) {
    console.log(points[0]._view.label);
  }
}
