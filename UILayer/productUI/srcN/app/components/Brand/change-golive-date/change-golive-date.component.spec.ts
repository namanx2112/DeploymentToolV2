import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeGoliveDateComponent } from './change-golive-date.component';

describe('ChangeGoliveDateComponent', () => {
  let component: ChangeGoliveDateComponent;
  let fixture: ComponentFixture<ChangeGoliveDateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChangeGoliveDateComponent]
    });
    fixture = TestBed.createComponent(ChangeGoliveDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
