import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SonicDashboardComponent } from './sonic-dashboard.component';

describe('SonicDashboardComponent', () => {
  let component: SonicDashboardComponent;
  let fixture: ComponentFixture<SonicDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SonicDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SonicDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
