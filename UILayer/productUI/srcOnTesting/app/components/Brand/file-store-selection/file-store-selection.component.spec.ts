import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileStoreSelectionComponent } from './file-store-selection.component';

describe('FileStoreSelectionComponent', () => {
  let component: FileStoreSelectionComponent;
  let fixture: ComponentFixture<FileStoreSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileStoreSelectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileStoreSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
