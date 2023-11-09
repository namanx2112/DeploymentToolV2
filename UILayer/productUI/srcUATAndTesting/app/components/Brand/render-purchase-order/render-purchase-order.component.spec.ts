import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenderPurchaseOrderComponent } from './render-purchase-order.component';

describe('RenderPurchaseOrderComponent', () => {
  let component: RenderPurchaseOrderComponent;
  let fixture: ComponentFixture<RenderPurchaseOrderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RenderPurchaseOrderComponent]
    });
    fixture = TestBed.createComponent(RenderPurchaseOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
