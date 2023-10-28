import { Component, Input } from '@angular/core';
import { BrandModel } from 'src/app/interfaces/models';

@Component({
  selector: 'app-brand-insights',
  templateUrl: './brand-insights.component.html',
  styleUrls: ['./brand-insights.component.css']
})
export class BrandInsightsComponent {

  @Input()
  curBrand: BrandModel;
}
