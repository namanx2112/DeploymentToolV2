import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolderEditorComponent } from './folder-editor.component';

describe('FolderEditorComponent', () => {
  let component: FolderEditorComponent;
  let fixture: ComponentFixture<FolderEditorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FolderEditorComponent]
    });
    fixture = TestBed.createComponent(FolderEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
