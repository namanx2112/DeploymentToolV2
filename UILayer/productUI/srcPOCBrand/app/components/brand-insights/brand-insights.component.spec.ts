import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandInsightsComponent } from './brand-insights.component';

describe('BrandInsightsComponent', () => {
  let component: BrandInsightsComponent;
  let fixture: ComponentFixture<BrandInsightsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BrandInsightsComponent]
    });
    fixture = TestBed.createComponent(BrandInsightsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
