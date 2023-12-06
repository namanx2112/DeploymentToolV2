import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldSelectionComponent } from './field-selection.component';

describe('FieldSelectionComponent', () => {
  let component: FieldSelectionComponent;
  let fixture: ComponentFixture<FieldSelectionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FieldSelectionComponent]
    });
    fixture = TestBed.createComponent(FieldSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
