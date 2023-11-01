import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseOrderTemplateListComponent } from './purchase-order-template-list.component';

describe('PurchaseOrderTemplateListComponent', () => {
  let component: PurchaseOrderTemplateListComponent;
  let fixture: ComponentFixture<PurchaseOrderTemplateListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PurchaseOrderTemplateListComponent]
    });
    fixture = TestBed.createComponent(PurchaseOrderTemplateListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
