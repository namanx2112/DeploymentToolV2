import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseOrderWorkflowTemplateComponent } from './purchase-order-workflow-template.component';

describe('PurchaseOrderWorkflowTemplateComponent', () => {
  let component: PurchaseOrderWorkflowTemplateComponent;
  let fixture: ComponentFixture<PurchaseOrderWorkflowTemplateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PurchaseOrderWorkflowTemplateComponent]
    });
    fixture = TestBed.createComponent(PurchaseOrderWorkflowTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
