import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuoteRequestWorkflowTemplateComponent } from './quote-request-workflow-template.component';

describe('QuoteRequestWorkflowTemplateComponent', () => {
  let component: QuoteRequestWorkflowTemplateComponent;
  let fixture: ComponentFixture<QuoteRequestWorkflowTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuoteRequestWorkflowTemplateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuoteRequestWorkflowTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
