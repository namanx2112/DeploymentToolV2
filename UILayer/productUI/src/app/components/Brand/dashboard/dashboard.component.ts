import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import Chart, { ChartType, ChartTypeRegistry } from 'chart.js/auto';
import { Colors } from 'chart.js';
import { DashboardTileType, ProjectHighlights } from 'src/app/interfaces/commons';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})


export class DashboardComponent implements OnInit {
  chart: any;
  @Input()
  set record(val: ProjectHighlights) {
    this._record = val;
    if (this._record.type != DashboardTileType.Chart){
      this.generageTextHighlight();
    }
  }
  _record: ProjectHighlights;
  @Output() ChartClicked = new EventEmitter<string>();
  compareText: string;
  compareIcon: string;
  compareClass: string;
  isChartType: boolean;
  constructor(private elementRef: ElementRef) {
    Chart.register(Colors);
  }
  ngOnInit(): void {

  }

  generageTextHighlight() {
    if (typeof this._record.compareWith != 'undefined') {
      this.compareIcon = (this._record.count < this._record.compareWith) ? "arrow_downward" : "arrow_upward";
      this.compareClass = (this._record.count < this._record.compareWith) ? "compare down" : "compare up";
      let tPercentage = Math.round((this._record.count < this._record.compareWith) ? (this._record.count / this._record.compareWith) * 100 : (this._record.compareWith / this._record.count) * 100);
      this.compareText = tPercentage.toString() + "%";
    }
  }

  reportClicked(){
    this.ChartClicked.emit("clicked");
  }

  onclick(points: any, evt: any) {
    console.log(points[0]._view.label);
  }
}
