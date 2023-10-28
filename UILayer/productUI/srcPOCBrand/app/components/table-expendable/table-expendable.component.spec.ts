import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableExpendableComponent } from './table-expendable.component';

describe('TableExpendableComponent', () => {
  let component: TableExpendableComponent;
  let fixture: ComponentFixture<TableExpendableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TableExpendableComponent]
    });
    fixture = TestBed.createComponent(TableExpendableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
