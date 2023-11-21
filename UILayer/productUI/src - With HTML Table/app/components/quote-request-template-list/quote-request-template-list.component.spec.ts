import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuoteRequestTemplateListComponent } from './quote-request-template-list.component';

describe('QuoteRequestTemplateListComponent', () => {
  let component: QuoteRequestTemplateListComponent;
  let fixture: ComponentFixture<QuoteRequestTemplateListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuoteRequestTemplateListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuoteRequestTemplateListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
