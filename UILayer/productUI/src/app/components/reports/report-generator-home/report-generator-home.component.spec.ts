import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportGeneratorHomeComponent } from './report-generator-home.component';

describe('ReportGeneratorHomeComponent', () => {
  let component: ReportGeneratorHomeComponent;
  let fixture: ComponentFixture<ReportGeneratorHomeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportGeneratorHomeComponent]
    });
    fixture = TestBed.createComponent(ReportGeneratorHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
