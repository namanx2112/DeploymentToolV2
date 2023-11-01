import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminDashboardShortcutsComponent } from './admin-dashboard-shortcuts.component';

describe('AdminDashboardShortcutsComponent', () => {
  let component: AdminDashboardShortcutsComponent;
  let fixture: ComponentFixture<AdminDashboardShortcutsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminDashboardShortcutsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminDashboardShortcutsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
