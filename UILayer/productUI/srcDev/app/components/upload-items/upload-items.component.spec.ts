import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadItemsComponent } from './UploadItemsComponent';

describe('UploadItemsComponent', () => {
  let component: UploadItemsComponent;
  let fixture: ComponentFixture<UploadItemsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UploadItemsComponent]
    });
    fixture = TestBed.createComponent(UploadItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
