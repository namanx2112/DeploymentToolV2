import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardPaneComponent } from './dashboard-pane.component';

describe('DashboardPaneComponent', () => {
  let component: DashboardPaneComponent;
  let fixture: ComponentFixture<DashboardPaneComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DashboardPaneComponent]
    });
    fixture = TestBed.createComponent(DashboardPaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
