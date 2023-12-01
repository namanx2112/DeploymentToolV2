import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportFoldersComponent } from './report-folders.component';

describe('ReportFoldersComponent', () => {
  let component: ReportFoldersComponent;
  let fixture: ComponentFixture<ReportFoldersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportFoldersComponent]
    });
    fixture = TestBed.createComponent(ReportFoldersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
