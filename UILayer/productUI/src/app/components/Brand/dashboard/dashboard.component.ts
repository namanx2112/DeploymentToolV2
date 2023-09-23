import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import Chart, { ChartType, ChartTypeRegistry } from 'chart.js/auto';
import { Colors } from 'chart.js';
import { DashboardTileType, DahboardTile } from 'src/app/interfaces/commons';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})


export class DashboardComponent implements OnInit {
  chart: any;
  @Input()
  set record(val: DahboardTile) {
    this._record = val;
    if (this._record.type != DashboardTileType.Chart){
      this.generageTextHighlight();
    }
  }
  _record: DahboardTile;
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
    if (this._record.type == DashboardTileType.TextWithCompare) {
      this.compareIcon = (this._record.count < this._record.compareWith) ? "arrow_downward" : "arrow_upward";
      this.compareClass = (this._record.count < this._record.compareWith) ? "compare down" : "compare up";
      let tPercentage = Math.round((this._record.count < this._record.compareWith) ? (this._record.count / this._record.compareWith) * 100 : (this._record.compareWith / this._record.count) * 100);
      this.compareText = tPercentage.toString() + "%";
    }
  }

  reportClicked(){
    this.ChartClicked.emit(this._record.reportId.toString());
  }

  onclick(points: any, evt: any) {
    console.log(points[0]._view.label);
  }
}
