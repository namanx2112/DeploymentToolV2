import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateChangeRevisedPOComponent } from './date-change-revised-po.component';

describe('DateChangeRevisedPOComponent', () => {
  let component: DateChangeRevisedPOComponent;
  let fixture: ComponentFixture<DateChangeRevisedPOComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DateChangeRevisedPOComponent]
    });
    fixture = TestBed.createComponent(DateChangeRevisedPOComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
