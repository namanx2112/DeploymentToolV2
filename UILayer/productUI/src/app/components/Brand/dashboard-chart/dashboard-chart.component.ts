import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import Chart, { ChartType, ChartTypeRegistry } from 'chart.js/auto';
import { Colors } from 'chart.js';
import { DashboardTileType, ProjectHighlights } from 'src/app/interfaces/commons';

@Component({
  selector: 'app-dashboard-chart',
  templateUrl: './dashboard-chart.component.html',
  styleUrls: ['./dashboard-chart.component.css']
})
export class DashboardChartComponent {
  chart: any;
  @Input()
  set record(val: ProjectHighlights) {
    this._record = val;
    if (this._record.type == DashboardTileType.Chart){
      this.createChart();
    }
  }
  _record: ProjectHighlights;
  @Output() ChartClicked = new EventEmitter<string>();
  constructor(private elementRef: ElementRef) {
    Chart.register(Colors);
  }
  ngOnInit(): void {

  }

  createChart() {
    if (typeof this._record.chartType != 'undefined' && typeof this._record.chartLabels != 'undefined') {
      let htmlRef = this.elementRef.nativeElement.querySelector("#MyChartId");
      let cThis = this;

      this.chart = new Chart(htmlRef, {
        type: this._record.chartType, //this denotes tha type of chart        
        data: {// values on X-Axis
          labels: this._record.chartLabels,
          datasets: [{
            label: this._record.title,
            data: this._record.chartValues,
            // backgroundColor: bgColors,
            // hoverOffset: 4
          }],
        },
        options: {
          responsive: true,
          onClick: function (event: any, array: any) {
            // if (array[0]) {
            //   let item = event;
            // }
            cThis.ChartClicked.emit("clicked");
          }
        }

      });
    }
  }

  reportClicked(){
    this.ChartClicked.emit("clicked");
  }

  onclick(points: any, evt: any) {
    console.log(points[0]._view.label);
  }
}
