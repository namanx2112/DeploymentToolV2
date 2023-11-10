import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentsTabComponent } from './documents-tab.component';

describe('DocumentsTabComponent', () => {
  let component: DocumentsTabComponent;
  let fixture: ComponentFixture<DocumentsTabComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DocumentsTabComponent]
    });
    fixture = TestBed.createComponent(DocumentsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
