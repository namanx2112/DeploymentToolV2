import { ComponentFixture, TestBed } from '@angular/core/testing';

import { POConfigQuantityFieldsComponent } from './poconfig-quantity-fields.component';

describe('POConfigQuantityFieldsComponent', () => {
  let component: POConfigQuantityFieldsComponent;
  let fixture: ComponentFixture<POConfigQuantityFieldsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [POConfigQuantityFieldsComponent]
    });
    fixture = TestBed.createComponent(POConfigQuantityFieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
