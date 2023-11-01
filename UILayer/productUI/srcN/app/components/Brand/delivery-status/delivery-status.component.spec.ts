import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryStatusComponent } from './delivery-status.component';

describe('DeliveryStatusComponent', () => {
  let component: DeliveryStatusComponent;
  let fixture: ComponentFixture<DeliveryStatusComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeliveryStatusComponent]
    });
    fixture = TestBed.createComponent(DeliveryStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
