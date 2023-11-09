import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandDashboardComponent } from './brand-dashboard.component';

describe('BrandDashboardComponent', () => {
  let component: BrandDashboardComponent;
  let fixture: ComponentFixture<BrandDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BrandDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BrandDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
