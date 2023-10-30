import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadItemsSelectionComponent } from './upload-items-selection.component';

describe('UploadItemsSelectionComponent', () => {
  let component: UploadItemsSelectionComponent;
  let fixture: ComponentFixture<UploadItemsSelectionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UploadItemsSelectionComponent]
    });
    fixture = TestBed.createComponent(UploadItemsSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
